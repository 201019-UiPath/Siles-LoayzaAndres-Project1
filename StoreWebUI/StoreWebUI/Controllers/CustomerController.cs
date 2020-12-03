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

        [Route("index")]
        public ActionResult Index()
        {
            if (HttpContext.Session.HasCustomer())
            {
                ViewData["Username"] = HttpContext.Session.GetCustomer().UserName;
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
            string request = $"Customer/signin?username={c.UserName}&password={c.Password}";
            try
            {
                Customer customer = StoreHttpClient.GetData<Customer>(request);
                HttpContext.Session.SetCustomer(customer);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException)
            {
                return View();
            }
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
            string url = $"Customer/signup";
            if (StoreHttpClient.PostDataAsJson<Customer>(url, customer))
            {
                ViewData["SignUpFailed"] = false;
                ViewData["SignUpSuccess"] = true;
                return RedirectToAction("Login", ViewData);
            }
            ViewData["SignUpFailed"] = true;
            return View();
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.RemoveCustomer();
            if (HttpContext.Session.HasCart())
            {
                HttpContext.Session.RemoveCart();
            }
            return RedirectToAction("Login");
        }

        [HttpGet("ViewOrders")]
        public IActionResult ViewOrders()
        {
            if (!HttpContext.Session.HasCustomer())
            { return RedirectToAction("Login"); }//if not logged in, go to login
            Customer c = HttpContext.Session.GetCustomer();
            string url = $"Customer/GetOrders?customerId={c.Id}";

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

        [HttpGet("ViewOrdersByOrder")]
        public IActionResult ViewOrdersByOrder(string orderby, string orderdir)
        {
            if (!HttpContext.Session.HasCustomer())
            { return RedirectToAction("Login"); }
            Customer c = HttpContext.Session.GetCustomer();
            string url = $"Customer/GetOrders?customerId={c.Id}&orderby={orderby}&orderdir={orderdir}";

            try
            {
                List<Order> orders = StoreHttpClient.GetData<List<Order>>(url);
                return View("ViewOrders", orders);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502); //bad gateway
            }
        }

    }
}
