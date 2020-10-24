using Microsoft.AspNetCore.Mvc;
using Wsei.ExchangeThings.Web.Filters;

namespace Wsei.ExchangeThings.Web.Controllers
{
    public class ExchangesController : Controller
    {
        [ServiceFilter(typeof(MyCustomActionFilter))]
        public IActionResult Show(string id)
        {
            return View();
        }
    }
}
