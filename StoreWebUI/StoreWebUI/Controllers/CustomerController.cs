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
        private readonly string apiDomainName = "https://localhost:44362/";

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

        [Route("signup")]
        public ViewResult Signup()
        {
            ViewData["SignUpFailed"] = false;
            return View();
        }

        [Route("SignupCustomer")]
        public IActionResult SignupCustomer(Customer customer)
        {
            string url = $"{apiDomainName}Customer/signup";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.PostAsJsonAsync("", customer);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["SignUpFailed"] = false;
                    ViewData["SignUpSuccess"] = true;
                    return RedirectToAction("Login", ViewData);
                }
            }
            ViewData["SignUpFailed"] = true;
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

        [HttpGet("ViewOrders")]
        public IActionResult ViewOrders()
        {
            if (HttpContext.Session.Get(SessionKeyCustomer) == null)
            { return RedirectToAction("Login"); }//if not logged in, go to login
            string url = $"{apiDomainName}Customer/GetOrders";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                Customer c = JsonSerializer.Deserialize<Customer>(HttpContext.Session.Get(SessionKeyCustomer));
                var response = client.GetAsync($"?customerId={c.Id}");
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

        [HttpGet("ViewOrdersByOrder")]
        public IActionResult ViewOrdersByOrder(string orderby, string orderdir)
        {
            if (HttpContext.Session.Get(SessionKeyCustomer) == null)
            { return RedirectToAction("Login"); } 

            string url = $"{apiDomainName}Customer/GetOrders";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                Customer customer = JsonSerializer.Deserialize<Customer>(HttpContext.Session.Get(SessionKeyCustomer));
                var response = client.GetAsync($"?customerId={customer.Id}&orderby={orderby}&orderdir={orderdir}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Order>>();
                    readTask.Wait();

                    List<Order> orders = readTask.Result;
                    return View("ViewOrders", orders);
                }
            }
            return StatusCode(502); //bad gateway
        }

    }
}
