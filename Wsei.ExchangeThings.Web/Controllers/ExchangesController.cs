using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wsei.ExchangeThings.Web.Auth;
using Wsei.ExchangeThings.Web.Filters;
using Wsei.ExchangeThings.Web.Models;
using Wsei.ExchangeThings.Web.Services;

namespace Wsei.ExchangeThings.Web.Controllers
{
    [Authorize(AuthConsts.AuthenticatedScheme)]
    public class ExchangesController : Controller
    {
        private readonly ExchangeItemsRepository _repository;

        public ExchangesController(ExchangeItemsRepository repository)
        {
            _repository = repository;
        }

        [ServiceFilter(typeof(MyCustomActionFilter))]
        public IActionResult Show(string query)
        {
            var items = string.IsNullOrEmpty(query)
                ? _repository.GetAll()
                : _repository.GetFiltered(query);

            return View(items);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ItemModel item)
        {
            var entity = _repository.Add(item);

            return RedirectToAction("AddConfirmation", new { itemId = entity.Id });
        }

        [HttpGet]
        public IActionResult AddConfirmation(int itemId)
        {
            return View(itemId);
        }
    }
}
