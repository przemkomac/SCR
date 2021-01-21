using System.ComponentModel;

namespace WebApp.Models
{
    public class PriorityViewModel
    {
        public PriorityViewModel()
        {
            // default values
            GenerateCount = 5;
            GenerationIntervalFrom = 2;
            GenerationIntervalTo = 4;
            CostFrom = 3;
            CostTo = 6;
        }

        [DisplayName("Ile wygenerować")]
        public int GenerateCount { get; set; }

        [DisplayName("Początek przedziału przerwy między generowaniami (w sekundach)")]
        public int GenerationIntervalFrom { get; set; }

        [DisplayName("Koniec przedziału przerwy między generowaniami (w sekundach)")]
        public int GenerationIntervalTo { get; set; }

        [DisplayName("Początek przedziału kosztowności zadania (w sekundach)")]
        public int CostFrom { get; set; }

        [DisplayName("Koniec przedziału kosztowności zadania (w sekundach)")]
        public int CostTo { get; set; }
    }
}