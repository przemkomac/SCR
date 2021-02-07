using System.ComponentModel;

namespace WebApp.Models.Parameters
{
    public class PriorityParameterViewModel : BaseParameterViewModel
    {
        [DisplayName("Priorytet")]
        public int Priority { get; set; }
    }
}