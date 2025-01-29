using Microsoft.AspNetCore.Mvc;

namespace SkinetEndPoint.Controller
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
