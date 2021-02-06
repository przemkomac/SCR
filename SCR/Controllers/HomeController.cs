using BusinessLogic;
using BusinessLogic.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCR.Models;
using System.Diagnostics;
using System.Linq;
using BusinessLogic.Threads;
using WebApp.Models;
using WebApp.Models.Parameters;
using WebApp.ViewModelBuilders;

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

        //public IActionResult Priority()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult PriorityResult(IEnumerable<PrioritySchedulingParametersViewModel> model)
        //{
        //    return View("PriorityResult", model);
        //}

        //public IActionResult Dms()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult DmsResult()
        //{
        //    return View("DmsResult", model);
        //}

        public IActionResult Edf()
        {
            var model = DeadlineSchedulingParametersViewModelBuilder.Build();
            return View(model);
        }

        [HttpPost]
        public IActionResult EdfResult(DeadlineSchedulingParametersViewModel model)
        {
            var sorter = new ThreadSorter(EScheduleType.Edf, model.ExecutionTime);

            var deadlineParameters = model.DeadlineParameters
                .Select(DeadlineSchedulingParametersViewModelBuilder.ToDeadlineThread)
                .ToList();
            var parameters = sorter.Sort(deadlineParameters)
                .Select(param => DeadlineSchedulingParametersViewModelBuilder.ToDeadlineParameterViewModel((DeadlineThread)param))
                .ToList();
            model.DeadlineParameters = parameters;

            return View("EdfResult", model);
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