using Microsoft.AspNetCore.Mvc;

namespace MaliciousApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
