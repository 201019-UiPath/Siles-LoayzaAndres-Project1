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
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return Content("Welcome!");
        }

        [HttpGet("get")]
        [Produces("application/json")]
        public IActionResult GetInventoryItem([FromQuery]int locationId, [FromQuery]int productId)
        {
            return Ok(_itemService.GetInvItem(locationId, productId));
        }
    }
}
