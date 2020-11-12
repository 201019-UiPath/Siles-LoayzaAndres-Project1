using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreLib;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return Content("Welcome!");
        }

        [HttpGet("GetAllLocations")]
        [Produces("application/json")]
        public IActionResult GetLocations()
        {
            try
            {
                return Ok(_locationService.GetLocations());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("GetInventory")]
        [Produces("application/json")]
        public IActionResult GetInventory(int locationId)
        {
            try
            {
                return Ok(_locationService.GetInventory(locationId));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("GetItem")]
        [Produces("application/json")]
        public IActionResult GetInventoryItem(int locationId, int productId)
        {
            return Ok(_locationService.GetInvItem(locationId, productId));
        }
    }
}
