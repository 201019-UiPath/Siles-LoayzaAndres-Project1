using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebUI.Models;

namespace StoreWebUI.Controllers
{

    [Route("Shop")]
    public class ShopController : Controller
    {
        private readonly string apiDomainName = "https://localhost:44362/";

        public IActionResult Index()
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
        public IActionResult GetInventory(int locationId, string locationName)
        {
            string url = $"{apiDomainName}Location/GetInventory";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync($"?locationId={locationId}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<InvItem>>();
                    readTask.Wait();

                    List<InvItem> inventory = readTask.Result;
                    ViewData["LocationName"] = locationName;
                    return View(inventory);
                }
            }
            return StatusCode(502); //bad gateway
        }

        [Route("item")]
        public IActionResult GetItem(int locationId, int productId)
        {
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
                    return View(item);
                }
            }
            return StatusCode(502); //bad gateway
        }
    }
}
