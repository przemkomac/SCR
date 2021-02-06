using System.ComponentModel;

namespace WebApp.Models.Parameters
{
    public class DeadlineParameterViewModel
    {
        [DisplayName("Czas przetwarzania")]
        public int Capacity { get; set; }

        [DisplayName("Okres")]
        public int Period { get; set; }

        [DisplayName("Termin")]
        public int Deadline { get; set; }
    }
}