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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(InvItem item)
        {
            if (!HttpContext.Session.HasCustomer())
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (!HttpContext.Session.HasCart())
            { AddCartToSession(); } //if no cart in session, get it from api

            Cart cart = HttpContext.Session.GetCart();
            CartItem cartItem = new CartItem() //map invitem to cartitem
            {
                CartId = cart.Id,
                ProductId = item.ProductId,
                Price = item.Price,
                Quantity = item.Quantity
            };
            string url = $"Cart/AddItem";
            if (StoreHttpClient.PostDataAsJson<CartItem>(url, cartItem))
            {
                Location location = HttpContext.Session.GetLocation();
                return RedirectToAction("GetInventory", "Shop", location); //back to inventory
            }
            return StatusCode(502); //bad gateway
        }

        [HttpGet("Items")]
        public IActionResult GetCartItems()
        {
            if (!HttpContext.Session.HasCustomer())
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (!HttpContext.Session.HasCart())
            { AddCartToSession(); } //if no cart in session, get it from api

            Cart cart = HttpContext.Session.GetCart();
            string url = $"Cart/GetItems?cartId={cart.Id}";
            try
            {
                List<CartItem> items = StoreHttpClient.GetData<List<CartItem>>(url);
                return View(items);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502); //bad gateway
            }
        }

        [Route("RemoveItem")] //lol xd html.actionlink doesn't support httpDelete
        public IActionResult RemoveItem(CartItem item)
        {
            if (!HttpContext.Session.HasCustomer())
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (!HttpContext.Session.HasCart())
            { AddCartToSession(); } //if no cart in session, get it from api

            string url = $"Cart/RemoveItem";
            HttpRequestMessage request = new HttpRequestMessage //create delete request with json
            {
                Content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete
            };
            if (StoreHttpClient.SendRequest(url, request))
            {
                return RedirectToAction("GetCartItems"); //refresh cart items page
            }
            return StatusCode(502); //bad gateway
        }

        [Route("EmptyCart")]
        public IActionResult EmptyCart()
        {/*
            if (!HttpContext.Session.HasCustomer())
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (!HttpContext.Session.HasCart())
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
            if (!HttpContext.Session.HasCustomer())
            { return RedirectToAction("Login", "Customer"); }//if not logged in, go to login
            else if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation", "Shop"); }//if no location, go to shop locations
            else if (!HttpContext.Session.HasCart())
            { AddCartToSession(); } //if no cart in session, get it from api

            string url = $"Cart/PlaceOrder";
            Customer customer = HttpContext.Session.GetCustomer();
            Location location = HttpContext.Session.GetLocation();
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
            if (StoreHttpClient.PostDataAsJson<Order>(url, newOrder))
            {
                return RedirectToAction("ViewOrders", "Customer"); //back to inventory
            }
            return StatusCode(502); //bad gateway
        }

        private void AddCartToSession()
        {
            //Get cart from api based on current customer and location; and enter it into session
            Customer customer = HttpContext.Session.GetCustomer();
            Location location = HttpContext.Session.GetLocation();
            string url = $"Cart/Get?customerId={customer.Id}&locationId={location.Id}";
            try
            {
                Cart cart = StoreHttpClient.GetData<Cart>(url);
                HttpContext.Session.SetCart(cart);
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
    }
}
