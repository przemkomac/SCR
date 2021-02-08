using System.Diagnostics;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Enums;
using BusinessLogic.Threads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCR.Models;
using WebApp.Models;
using WebApp.ViewModelBuilders;

namespace WebApp.Controllers
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
            var model = PrioritySchedulingParametersViewModelBuilder.Build();
            return View(model);
        }

        [HttpPost]
        public IActionResult PriorityResult(PrioritySchedulingParametersViewModel model)
        {
            var sorter = new ThreadSorter(EScheduleType.Priority, model.ExecutionTime);

            var priorityParameters = model.PriorityParameters
                .Select(PrioritySchedulingParametersViewModelBuilder.ToPriorityThread)
                .ToList();
            model.ThreadExecutionSequence = sorter.Sort(priorityParameters);

            return View("PriorityResult", model);
        }

        public IActionResult Dms()
        {
            var model = DeadlineSchedulingParametersViewModelBuilder.Build();
            return View(model);
        }

        [HttpPost]
        public IActionResult DmsResult(DeadlineSchedulingParametersViewModel model)
        {
            var sorter = new ThreadSorter(EScheduleType.Dms, model.ExecutionTime);

            var deadlineParameters = model.DeadlineParameters
                .Select(DeadlineSchedulingParametersViewModelBuilder.ToDeadlineThread)
                .ToList();
            model.ThreadExecutionSequence = sorter.Sort(deadlineParameters);

            return View("DmsResult", model);
        }

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
            model.ThreadExecutionSequence = sorter.Sort(deadlineParameters);

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