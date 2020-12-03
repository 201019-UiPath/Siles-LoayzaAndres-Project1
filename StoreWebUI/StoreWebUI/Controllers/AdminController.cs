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
        [Route("Index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.HasLocation())
            {
                return RedirectToAction("GetInventory", HttpContext.Session.GetLocation());
            }
            else
            {
                return RedirectToAction("SelectLocation");
            }
        }

        [Route("SelectLocation")]
        public IActionResult SelectLocation()
        {
            string url = $"Location/GetAllLocations";
            try
            {
                List<Location> locations = StoreHttpClient.GetData<List<Location>>(url);
                return View(locations);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502); //bad gateway
            }
        }

        [Route("location")]
        public IActionResult GetInventory(Location location)
        {
            string url = $"Location/GetInventory?locationId={location.Id}";
            try
            {
                List<InvItem> inventory = StoreHttpClient.GetData<List<InvItem>>(url);
                HttpContext.Session.SetLocation(location);
                ViewData["LocationName"] = location.Name;
                return View(inventory);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502); //bad gateway
            }
        }

        [Route("item")]
        public IActionResult GetItem(int locationId, int productId)
        {
            if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            string url = $"Location/GetItem?locationId={locationId}&productId={productId}";
            try
            {
                InvItem item = StoreHttpClient.GetData<InvItem>(url);
                ViewData["location"] = HttpContext.Session.GetLocation();
                return View(item);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502); //bad gateway
            }
        }

        public IActionResult AddToStock(InvItem item)
        {
            if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            string url = $"Admin/AddToItem";
            string param = $"?locationId={item.LocationId}&productId={item.ProductId}&addend={item.Quantity}";
            HttpRequestMessage request = new HttpRequestMessage //create put request
            {
                Method = HttpMethod.Put
            };

            if (StoreHttpClient.SendRequest(url+param, request))
            {
                Location location = HttpContext.Session.GetLocation();
                return RedirectToAction("GetInventory", location); //back to inventory
            }
            return StatusCode(502); //bad gateway
        }
        
        [Route("CreateNewProduct")]
        public IActionResult CreateNewProduct()
        {
            if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            Location location = HttpContext.Session.GetLocation();
            ViewData["location"] = location;
            ViewData["LocationName"] = location.Name;
            return View();
        }

        [Route("AddNewProduct")]
        public IActionResult AddNewProduct(InvItem item)
        {
            if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            Location location = HttpContext.Session.GetLocation();
            string url = $"Admin/AddProduct?locationId={location.Id}";
            if (StoreHttpClient.PostDataAsJson<InvItem>(url, item))
            {
                ViewData["Failed"] = false;
                return RedirectToAction("GetInventory", location);
            }
            ViewData["Failed"] = true;
            return RedirectToAction("CreateNewProduct", ViewData);
        }

        [HttpGet("ViewLocationOrders")]
        public IActionResult GetLocationOrders()
        {
            if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            Location location = HttpContext.Session.GetLocation();
            string url = $"Admin/GetOrders?locationId={location.Id}";
            try
            {
                List<Order> orders = StoreHttpClient.GetData<List<Order>>(url);
                return View(orders);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502); //bad gateway
            }
        }

        [HttpGet("GetLocationOrdersByOrder")]
        public IActionResult GetLocationOrdersByOrder(string orderby, string orderdir)
        {
            if (!HttpContext.Session.HasLocation())
            { return RedirectToAction("SelectLocation"); }//if no location, go to select locations

            Location location = HttpContext.Session.GetLocation();
            string url = $"Admin/GetOrders?locationId={location.Id}&orderby={orderby}&orderdir={orderdir}";
            try
            {
                List<Order> orders = StoreHttpClient.GetData<List<Order>>(url);
                return View("GetLocationOrders", orders);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502); //bad gateway
            }
        }
    }
}
