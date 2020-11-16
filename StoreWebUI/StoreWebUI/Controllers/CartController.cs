using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreWebUI.Models;
using System.Text.Json;
using System.Text;

namespace StoreWebUI.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        public const string SessionKeyCustomer = "CurrentCustomer";
        public const string SessionKeyLocation = "CurrentLocation";
        public const string SessionKeyCart = "CurrentCart";

        private readonly string apiDomainName = "https://localhost:44362/";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(InvItem item)
        {
            if (HttpContext.Session.Get(SessionKeyCustomer) == null)
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (HttpContext.Session.Get(SessionKeyCart) == null)
            { AddCartToSession(); } //if no cart in session, get it from api

            string url = $"{apiDomainName}Cart/AddItem";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                Cart cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.Get("CurrentCart"));
                CartItem cartItem = new CartItem() //map invitem to cartitem
                {
                    CartId = cart.Id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                var response = client.PostAsJsonAsync("", cartItem); //post to client
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                    return RedirectToAction("GetInventory", "Shop", location); //back to inventory
                }
            }
            return StatusCode(502); //bad gateway
        }

        [HttpGet("Items")]
        public IActionResult GetCartItems()
        {
            if (HttpContext.Session.Get(SessionKeyCustomer) == null)
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (HttpContext.Session.Get(SessionKeyCart) == null)
            { AddCartToSession(); } //if no cart in session, get it from api

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

        [Route("RemoveItem")] //lol xd html.actionlink doesn't support httpDelete
        public IActionResult RemoveItem(CartItem item)
        {
            if (HttpContext.Session.Get(SessionKeyCustomer) == null)
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (HttpContext.Session.Get(SessionKeyCart) == null)
            { AddCartToSession(); } //if no cart in session, get it from api

            string url = $"{apiDomainName}Cart/RemoveItem";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                HttpRequestMessage request = new HttpRequestMessage //create delete request with json
                {
                    Content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(url)
                };
                var response = client.SendAsync(request); //send delete request
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetCartItems"); //refresh cart items page
                }
            }
            return StatusCode(502); //bad gateway
        }

        [Route("EmptyCart")]
        public IActionResult EmptyCart()
        {/*
            if (HttpContext.Session.Get(SessionKeyCustomer) == null)
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (HttpContext.Session.Get(SessionKeyCart) == null)
            { AddCartToSession(); } //if no cart in session, get it from api

            string url = $"{apiDomainName}Cart/Empty";
            Cart cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.Get(SessionKeyCart));
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.DeleteAsync($"?cartId={cart.Id}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetCartItems"); //refresh cart items page
                }
            }*/
            return StatusCode(502); //bad gateway
        }

        [Route("PlaceOrder")]
        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder(InputOrder order)
        {
            if (HttpContext.Session.Get(SessionKeyCustomer) == null)
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (HttpContext.Session.Get(SessionKeyCart) == null)
            { AddCartToSession(); } //if no cart in session, get it from api

            string url = $"{apiDomainName}Cart/PlaceOrder";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                Customer customer = JsonSerializer.Deserialize<Customer>(HttpContext.Session.Get(SessionKeyCustomer));
                Location location= JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                Order newOrder = new Order() //map invitem to cartitem
                {
                    CustomerId = customer.Id,
                    LocationId = location.Id,
                    DestinationAddress = order.DestinationAddress,
                    ReturnAddress = new Address()
                    {
                        Street = "123 Main Street",
                        City = "Washington",
                        State = "DC",
                        Zip = 12345,
                        Country = "USA"
                    }
                };
                var response = client.PostAsJsonAsync("", newOrder); //post to client
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewOrders", "Customer"); //back to inventory
                }
            }
            return StatusCode(502); //bad gateway
        }

        private IActionResult AddCartToSession()
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
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Cart>();
                    readTask.Wait();
                    Cart cart = readTask.Result;
                    HttpContext.Session.SetString(SessionKeyCart, JsonSerializer.Serialize(cart));
                }
            }
            return StatusCode(502); //bad gateway
        }
    }
}
