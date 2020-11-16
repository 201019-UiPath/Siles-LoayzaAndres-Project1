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
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("AddLocation")]
        public IActionResult AddLocation(Location location)
        {
            try
            {
                _adminService.AddLocation(location);
                return CreatedAtAction("AddLocation", location);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("AddProduct")]
        public IActionResult AddNewProduct (int locationId, InvItem item)
        {
            try
            {
                _adminService.AddNewProductToInventory(locationId, item);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("AddToItem")]
        public IActionResult AddToItem (int locationId, int productId, int addend)
        {
            try
            {
                _adminService.AddToInvItem(locationId, productId, addend);
                return AcceptedAtAction("AddToItem");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetOrders")]
        [Produces("application/json")]
        public IActionResult GetOrders (int locationId, string orderby="date", string orderdir="desc")
        {
            try
            {
                if (orderby == "cost" && orderdir == "asc")
                { return Ok(_adminService.GetOrdersByCostAscend(locationId)); }
                else if (orderby == "cost" && orderdir == "desc")
                { return Ok(_adminService.GetOrdersByCostDescend(locationId)); }
                else if (orderby == "date" && orderdir == "asc")
                { return Ok(_adminService.GetOrdersByDateAscend(locationId)); }
                else //default to order by date descending
                { return Ok(_adminService.GetOrdersByDateDescend(locationId)); }
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
    }
}
