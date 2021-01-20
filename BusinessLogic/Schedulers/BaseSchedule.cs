namespace BusinessLogic.Schedulers
{
    public abstract class BaseSchedule<T>
        where T : class
    {
        public abstract T GetNextThread();
    }
}