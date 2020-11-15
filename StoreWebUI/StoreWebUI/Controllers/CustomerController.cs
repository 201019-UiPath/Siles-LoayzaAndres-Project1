using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebUI.Models;
using System.Web;
using System.Net.Http;

namespace StoreWebUI.Controllers
{
    //[Route("UserAccount")]
    public class CustomerController : Controller
    {
        public const string SessionKeyUsername = "_Username";
        
        public ActionResult Index()
        {
            var username = HttpContext.Session.Get(SessionKeyUsername);
            ViewData["Username"] = username;
            return View(ViewData);
        }

        [Route("login")]
        public ViewResult Login()
        {
            Console.WriteLine("Going to login page.");
            return View();
        }

        public IActionResult Login(Customer c)
        {
            string url = "https://localhost:44362/Customer/signin";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync($"?username={c.UserName}&password={c.Password}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Customer>();
                    readTask.Wait();

                    Customer customer = readTask.Result;
                    HttpContext.Session.SetString(SessionKeyUsername, customer.UserName);
                    return RedirectToAction("UserAccount/Index");
                }
            }
            Console.WriteLine("Failed to log in");
            return View();
        }

    }
}
