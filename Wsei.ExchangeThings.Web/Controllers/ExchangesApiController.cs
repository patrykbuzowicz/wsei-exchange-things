using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Wsei.ExchangeThings.Web.Models;

namespace Wsei.ExchangeThings.Web.Controllers
{
    [ApiController]
    [Route("api/exchanges")]
    public class ExchangesApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var list = new List<ItemModel>()
            {
                new ItemModel {Name = "one item"},
                new ItemModel {Name = "another item"},
            };

            return Ok(list);
        }

        [HttpPost]
        public AddNewItemResponse Post(ItemModel item)
        {
            // TODO add to database

            return new AddNewItemResponse
            {
                Success = true
            };
        }
    }
}
