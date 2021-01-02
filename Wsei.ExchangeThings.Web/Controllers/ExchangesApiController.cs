using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Wsei.ExchangeThings.Web.Models;
using Wsei.ExchangeThings.Web.Services;

namespace Wsei.ExchangeThings.Web.Controllers
{
    [ApiController]
    [Route("api/exchanges")]
    public class ExchangesApiController : ControllerBase
    {
        private readonly ExchangeItemsRepository _repository;

        public ExchangesApiController(ExchangeItemsRepository repository)
        {
            _repository = repository;
        }

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
            _repository.Add(item);

            return new AddNewItemResponse
            {
                Success = true
            };
        }
    }
}
