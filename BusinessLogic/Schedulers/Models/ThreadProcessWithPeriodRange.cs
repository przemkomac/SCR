namespace BusinessLogic.Schedulers.Models
{
    public class ThreadProcessWithPeriodRange
    {
        public int ThreadNo { get; set; }
        public int Capacity { get; set; }
        public int DeadlineFrom { get; set; }
        public int DeadlineTo { get; set; }
        public int PeriodTo { get; set; }
    }
}