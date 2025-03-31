using Microsoft.AspNetCore.Mvc;

namespace SuperStore.MVC.Controllers
{
    public class IdentitiesController : Controller
    {
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
