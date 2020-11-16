using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreDB.Models;
using StoreLib;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;


        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("signin")]
        [Produces("application/json")]
        public IActionResult SignIn(string username, string password)
        {
            try
            {
                return Ok(_customerService.SignInExistingCustomer(username, password));
            }
            catch (Exception e)
            {
                if (e.GetType()==typeof(ArgumentException)) { return NotFound(); }
                else { return BadRequest(); }
            }
        }

        [HttpPost("signup")]
        public IActionResult SignUp(Customer customer)
        {
            try
            {
                _customerService.SignUpNewCustomer(customer);
                return AcceptedAtAction("signup");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetOrders")]
        [Produces("application/json")]
        public IActionResult GetOrders(int customerId, string orderby="date", string orderdir="desc")
        {
            try
            {
                if (orderby=="cost" && orderdir=="asc")
                { return Ok(_customerService.GetOrdersByCostAscend(customerId));}
                else if (orderby=="cost" && orderdir=="desc")
                { return Ok(_customerService.GetOrdersByCostDescend(customerId)); }
                else if (orderby=="date" && orderdir=="asc")
                { return Ok(_customerService.GetOrdersByDateAscend(customerId)); }
                else //default to order by date descending
                { return Ok(_customerService.GetOrdersByDateDescend(customerId)); } 
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
    }
}
