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

    [Route("Shop")]
    public class ShopController : Controller
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
    }
}
