using System.Collections.Generic;
using System.ComponentModel;
using BusinessLogic.Schedulers.Models;
using WebApp.Models.Parameters;

namespace WebApp.Models
{
    public class DeadlineSchedulingParametersViewModel
    {
        public DeadlineSchedulingParametersViewModel()
        {
            DeadlineParameters = new List<DeadlineParameterViewModel>();
        }

        public IEnumerable<DeadlineParameterViewModel> DeadlineParameters { get; set; }

        [DisplayName("Czas wykonywania")]
        public int ExecutionTime { get; set; }

        public IEnumerable<ThreadExecution> ThreadExecutionSequence { get; set; }
    }
}