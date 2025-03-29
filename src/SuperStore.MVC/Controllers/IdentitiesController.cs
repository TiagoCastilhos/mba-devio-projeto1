using Microsoft.AspNetCore.Mvc;

namespace SuperStore.MVC.Controllers
{
    public class IdentitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
