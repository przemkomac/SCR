using BusinessLogic;
using BusinessLogic.Enums;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCR.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApp.Models;

namespace SCR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Priority()
        {
            var emptyModel = new PriorityViewModel();
            return View(emptyModel);
        }

        [HttpPost]
        public IActionResult PriorityResult(PriorityViewModel model)
        {
            var generator = new ThreadGenerator(
                EScheduleType.Priority,
                model.GenerationIntervalFrom,
                model.GenerationIntervalTo,
                model.CostFrom,
                model.CostTo);

            var consumer = new ThreadConsumer(EScheduleType.Priority);

            //run in backgroud
            _ = Task.Run(() => generator.Start(model.GenerateCount));
            _ = Task.Run(() => consumer.Start(model.GenerateCount));

            return View(model);
        }

        public IActionResult Dms()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DmsResult()
        {
            return View();
        }

        public IActionResult Edf()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EdfResult()
        {
            return View();
        }

        public IActionResult Threads()
        {
            return Json(StorageService.GetThreads());
        }

        public IActionResult Logs()
        {
            return Json(StorageService.GetLogs());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}