using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreWebUI.Models;
using System.Text.Json;

namespace StoreWebUI.Controllers
{
    public class CartController : Controller
    {
        public const string SessionKeyCustomer = "CurrentCustomer";
        public const string SessionKeyLocation = "CurrentLocation";
        public const string SessionKeyCart = "CurrentCart";

        private readonly string apiDomainName = "https://localhost:44362/";

        private IActionResult CheckCartRedirect(string continueAction)
        {
            if (HttpContext.Session.Get(SessionKeyCustomer)==null) 
            {
                return RedirectToAction("Login", "Customer"); //if not logged in, go to login
            }
            else if (HttpContext.Session.Get(SessionKeyLocation)==null) 
            {
                return RedirectToAction("SelectLocation", "Shop"); //if no location, go to shop locations
            }
            else if (HttpContext.Session.Get(SessionKeyCart)==null)
            {
                //Get cart from api based on current customer and location; and enter it into session
                var customer = JsonSerializer.Deserialize<Customer>(HttpContext.Session.Get(SessionKeyCustomer));
                var location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                string url = $"{apiDomainName}Cart/Get";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var response = client.GetAsync($"?customerId={customer.Id}&locationId={location.Id}");
                    response.Wait();
                    var result = response.Result;
                    if (!result.IsSuccessStatusCode) { return StatusCode(502); }
                    var readTask = result.Content.ReadAsAsync<Cart>();
                    readTask.Wait();
                    Cart cart = readTask.Result;

                    HttpContext.Session.SetString(SessionKeyCart, JsonSerializer.Serialize(cart));
                }
            }
            return RedirectToAction(continueAction); //continue last action
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(InvItem item)
        {
            CheckCartRedirect("AddToCart");

            string url = $"{apiDomainName}Cart/AddItem";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                Cart cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.Get("CurrentCart"));
                CartItem cartItem = new CartItem()
                {
                    CartId = cart.Id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                var response = client.PostAsJsonAsync("", cartItem);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                    return RedirectToAction("GetInventory", "Shop", location);
                }
            }
            return StatusCode(502); //bad gateway
        }

        [Route("Items")]
        public IActionResult GetCartItems()
        {
            CheckCartRedirect("GetCartItems");
            Cart cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.Get(SessionKeyCart));

            string url = $"{apiDomainName}Cart/GetItems";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync($"?cartId={cart.Id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode) 
                {
                    var readTask = result.Content.ReadAsAsync<List<CartItem>>();
                    readTask.Wait();
                    return View(readTask.Result);
                }
            }
            return StatusCode(502); //bad gateway
        }
    }
}
