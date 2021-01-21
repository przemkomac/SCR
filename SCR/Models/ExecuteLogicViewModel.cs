using System.ComponentModel;

namespace WebApp.Models
{
    public class ExecuteLogicViewModel
    {
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