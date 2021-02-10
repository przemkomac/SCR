namespace BusinessLogic.Schedulers.Models
{
    public class ThreadProcessWithPriorityRange
    {
        public int ThreadNo { get; set; }
        public int Capacity { get; set; }
        public int PeriodFrom { get; set; }
        public int PeriodTo { get; set; }
        public int Priority { get; set; }
    }
}