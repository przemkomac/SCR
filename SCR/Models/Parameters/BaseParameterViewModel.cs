using System.ComponentModel;

namespace WebApp.Models.Parameters
{
    public abstract class BaseParameterViewModel
    {
        public int ThreadNo { get; set; }

        [DisplayName("Czas przetwarzania")]
        public int Capacity { get; set; }

        [DisplayName("Okres")]
        public int Period { get; set; }
    }
}