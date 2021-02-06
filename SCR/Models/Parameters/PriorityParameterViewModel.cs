using System.ComponentModel;

namespace WebApp.Models.Parameters
{
    public class PriorityParameterViewModel
    {
        [DisplayName("Czas przetwarzania")]
        public int Capacity { get; set; }

        [DisplayName("Okres")]
        public int Period { get; set; }

        [DisplayName("Priorytet")]
        public int Priority { get; set; }
    }
}