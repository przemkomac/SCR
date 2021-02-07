using System.ComponentModel;

namespace WebApp.Models.Parameters
{
    public class DeadlineParameterViewModel : BaseParameterViewModel
    {
        [DisplayName("Termin")]
        public int Deadline { get; set; }
    }
}