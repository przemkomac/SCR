using System.Collections.Generic;

namespace BusinessLogic.Schedulers
{
    public abstract class BaseSchedule<T>
        where T : class
    {
        public abstract IEnumerable<T> SortThreads(IEnumerable<T> threads);
    }
}