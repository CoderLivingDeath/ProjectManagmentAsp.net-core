using Microsoft.AspNetCore.Mvc;

namespace ProjectManagment.Controllers.HomeController
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
