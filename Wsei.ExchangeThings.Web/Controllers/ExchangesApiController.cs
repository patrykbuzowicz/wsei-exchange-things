using Microsoft.AspNetCore.Mvc;

namespace Wsei.ExchangeThings.Web.Controllers
{
    [Route("[controller]")]
    public class ExchangesApiController : ControllerBase
    {
        [HttpGet("/{id}")]
        public string Get(string id)
        {
            return $"hello {id}";
        }
    }
}
