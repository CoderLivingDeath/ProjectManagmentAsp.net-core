using Microsoft.AspNetCore.Mvc;

namespace ProjectManagment.Controllers.ProjectController
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
