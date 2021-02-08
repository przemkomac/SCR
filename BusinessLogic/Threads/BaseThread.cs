namespace BusinessLogic.Threads
{
    public abstract class BaseThread
    {
        public int ThreadNo { get; set; }
        public int Capacity { get; set; }
        public int Period { get; set; }
    }
}