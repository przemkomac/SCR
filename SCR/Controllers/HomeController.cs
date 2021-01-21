using BusinessLogic;
using BusinessLogic.Enums;
using DataAccess.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCR.Models;
using System;
using System.Diagnostics;
using System.Linq;
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
            //default values
            var emptyModel = new ExecuteLogicViewModel
            {
                GenerateCount = 5,
                GenerationIntervalFrom = 2,
                GenerationIntervalTo = 4,
                CostFrom = 3,
                CostTo = 6
            };
            return View(emptyModel);
        }

        [HttpPost]
        public IActionResult PriorityResult(ExecuteLogicViewModel model)
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
            var emptyModel = new ExecuteLogicViewModel
            {
                GenerateCount = 5,
                GenerationIntervalFrom = 1,
                GenerationIntervalTo = 3,
                CostFrom = 2,
                CostTo = 10
            };
            return View(emptyModel);
        }

        [HttpPost]
        public IActionResult EdfResult(ExecuteLogicViewModel model)
        {
            var generator = new ThreadGenerator(
                 EScheduleType.Edf,
                 model.GenerationIntervalFrom,
                 model.GenerationIntervalTo,
                 model.CostFrom,
                 model.CostTo);

            var consumer = new ThreadConsumer(EScheduleType.Edf);

            //run in backgroud
            _ = Task.Run(() => generator.Start(model.GenerateCount));
            _ = Task.Run(() => consumer.Start(model.GenerateCount));

            return View(model);
        }

        public IActionResult Threads(int type)
        {
            var threads = StorageService.GetThreads();

            switch ((EScheduleType)type)
            {
                case EScheduleType.Priority:
                    return Json(threads.Cast<PriorityThread>());

                case EScheduleType.Dms:
                case EScheduleType.Edf:
                    return Json(threads.Cast<DeadlineThread>());

                default:
                    throw new ArgumentOutOfRangeException();
            }
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