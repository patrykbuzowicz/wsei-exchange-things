using Microsoft.AspNetCore.Mvc;
using Wsei.ExchangeThings.Web.Filters;
using Wsei.ExchangeThings.Web.Models;

namespace Wsei.ExchangeThings.Web.Controllers
{
    public class ExchangesController : Controller
    {
        [ServiceFilter(typeof(MyCustomActionFilter))]
        public IActionResult Show(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ItemModel item)
        {
            // TODO add to database

            var viewModel = new AddNewItemConfirmationViewModel
            {
                Id = 1,
                Name = item.Name,
            };

            return View("AddConfirmation", viewModel);
        }
    }
}
