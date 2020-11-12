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
        public IActionResult signIn(string username, string password)
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
        public IActionResult signUp(Customer customer)
        {
            try
            {
                _customerService.SignUpNewCustomer(customer);
                return CreatedAtAction("signUp", customer);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
