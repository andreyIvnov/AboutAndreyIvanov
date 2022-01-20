using Microsoft.AspNetCore.Mvc;

namespace AboutAndreyIvanov.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
