using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PriorityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}