using Microsoft.AspNetCore.Mvc;
using Wsei.ExchangeThings.Web.Models;

namespace Wsei.ExchangeThings.Web.Controllers
{
    [ApiController]
    [Route("api/exchanges")]
    public class ExchangesApiController : ControllerBase
    {
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
