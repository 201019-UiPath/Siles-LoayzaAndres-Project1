using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebUI.Models;

namespace StoreWebUI.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        public const string SessionKeyLocation = "CurrentLocation";
        private readonly string apiDomainName = "https://localhost:44362/";

        [Route("Index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.Get(SessionKeyLocation) != null)
            {
                var location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                return RedirectToAction("GetInventory", location);
            }
            else
            {
                return RedirectToAction("SelectLocation");
            }
        }

        [Route("SelectLocation")]
        public IActionResult SelectLocation()
        {
            string url = $"{apiDomainName}Location/GetAllLocations";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Location>>();
                    readTask.Wait();

                    List<Location> locations = readTask.Result;
                    return View(locations);
                }
            }
            return StatusCode(502); //bad gateway
        }

        [Route("location")]
        public IActionResult GetInventory(Location location)
        {
            string url = $"{apiDomainName}Location/GetInventory";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync($"?locationId={location.Id}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<InvItem>>();
                    readTask.Wait();

                    List<InvItem> inventory = readTask.Result;
                    HttpContext.Session.SetString(SessionKeyLocation, JsonSerializer.Serialize(location));
                    ViewData["LocationName"] = location.Name;
                    return View(inventory);
                }
            }
            return StatusCode(502); //bad gateway
        }

        [Route("item")]
        public IActionResult GetItem(int locationId, int productId)
        {
            if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            string url = $"{apiDomainName}Location/GetItem";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync($"?locationId={locationId}&productId={productId}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<InvItem>();
                    readTask.Wait();

                    InvItem item = readTask.Result;
                    ViewData["location"] = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                    return View(item);
                }
            }
            return StatusCode(502); //bad gateway
        }

        public IActionResult AddToStock(InvItem item)
        {
            if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            string url = $"{apiDomainName}Admin/AddToItem";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                string parameters = $"?locationId={item.LocationId}&productId={item.ProductId}&addend={item.Quantity}";
                HttpRequestMessage request = new HttpRequestMessage //create put request
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(url+parameters)
                };
                var response = client.SendAsync(request); //send put request
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                    return RedirectToAction("GetInventory", location); //back to inventory
                }
            }
            return StatusCode(502); //bad gateway
        }
        
        [Route("CreateNewProduct")]
        public IActionResult CreateNewProduct()
        {
            if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            Location location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
            ViewData["location"] = location;
            ViewData["LocationName"] = location.Name;
            return View();
        }

        [Route("AddNewProduct")]
        public IActionResult AddNewProduct(InvItem item)
        {
            if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            string url = $"{apiDomainName}Admin/AddProduct";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                var response = client.PostAsJsonAsync($"?locationId={location.Id}", item);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Failed"] = false;
                    return RedirectToAction("GetInventory", location);
                }
            }
            ViewData["Failed"] = true;
            return RedirectToAction("CreateNewProduct", ViewData);
        }

        [HttpGet("ViewLocationOrders")]
        public IActionResult GetLocationOrders()
        {
            if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            string url = $"{apiDomainName}Admin/GetOrders";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                Location location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                var response = client.GetAsync($"?locationId={location.Id}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Order>>();
                    readTask.Wait();

                    List<Order> orders = readTask.Result;
                    return View(orders);
                }
            }
            return StatusCode(502); //bad gateway
        }

        [HttpGet("GetLocationOrdersByOrder")]
        public IActionResult GetLocationOrdersByOrder(string orderby, string orderdir)
        {
            if (HttpContext.Session.Get(SessionKeyLocation) == null)
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            string url = $"{apiDomainName}Admin/GetOrders";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                Location location = JsonSerializer.Deserialize<Location>(HttpContext.Session.Get(SessionKeyLocation));
                var response = client.GetAsync($"?locationId={location.Id}&orderby={orderby}&orderdir={orderdir}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Order>>();
                    readTask.Wait();

                    List<Order> orders = readTask.Result;
                    return View("GetLocationOrders", orders);
                }
            }
            return StatusCode(502); //bad gateway
        }
    }
}
