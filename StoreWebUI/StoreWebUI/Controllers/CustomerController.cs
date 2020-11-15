using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebUI.Models;
using System.Web;
using System.Net.Http;
using System.Text.Json;

namespace StoreWebUI.Controllers
{
    [Route("account")]
    public class CustomerController : Controller
    {
        public const string SessionKeyCustomer = "CurrentCustomer";
        public const string SessionKeyCart = "CurrentCart";

        [Route("index")]
        public ActionResult Index()
        {
            if (HttpContext.Session.IsAvailable && HttpContext.Session.Get(SessionKeyCustomer)!=null)
            {
                var customer = HttpContext.Session.Get(SessionKeyCustomer);
                string username = JsonSerializer.Deserialize<Customer>(customer).UserName;
                ViewData["Username"] = username;
                ViewData["IsLoggedIn"] = true;
            }
            else
            { 
                ViewData["IsLoggedIn"] = false;
                return RedirectToAction("Login");
            }

            return View(ViewData);
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        //[HttpGet]
        public IActionResult Login(Customer c)
        {
            string url = "https://localhost:44362/Customer/signin";
            using (var client = new HttpClient())
            {
                //TODO: Input Validation for Username and Password

                client.BaseAddress = new Uri(url);
                var response = client.GetAsync($"?username={c.UserName}&password={c.Password}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Customer>();
                    readTask.Wait();

                    Customer customer = readTask.Result;
                    HttpContext.Session.SetString(SessionKeyCustomer, JsonSerializer.Serialize(customer));
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKeyCustomer);
            if (HttpContext.Session.Get(SessionKeyCart)!=null)
            {
                HttpContext.Session.Remove(SessionKeyCart);
            }
            return RedirectToAction("Login");
        }

    }
}
