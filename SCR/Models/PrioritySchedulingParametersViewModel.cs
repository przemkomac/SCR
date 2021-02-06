using System.Collections.Generic;
using System.ComponentModel;
using WebApp.Models.Parameters;

namespace WebApp.Models
{
    public class PrioritySchedulingParametersViewModel
    {
        public IEnumerable<PriorityParameterViewModel> PriorityParameters { get; set; }

        [DisplayName("Czas wykonywania")]
        public int ExecutionTime { get; set; }
    }
}