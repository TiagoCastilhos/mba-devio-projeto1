using Microsoft.AspNetCore.Mvc;

namespace SuperStore.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
