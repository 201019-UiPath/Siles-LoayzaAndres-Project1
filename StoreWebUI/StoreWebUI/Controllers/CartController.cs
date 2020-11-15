using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreWebUI.Models;

namespace StoreWebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly string apiDomainName = "https://localhost:44362/";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(InvItem item)
        {
            string url = $"{apiDomainName}Cart/AddItem";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                CartItem cartItem = new CartItem()
                {
                    CartId = 
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                var response = client.PostAsJsonAsync("", cartItem);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Cart/GetCart");
                }
            }
            return StatusCode(502); //bad gateway
        }
    }
}
