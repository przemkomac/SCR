using BusinessLogic.Schedulers.Models;
using System.Collections.Generic;

namespace BusinessLogic.Schedulers
{
    public abstract class BaseSchedule<T>
        where T : class
    {
        public abstract IEnumerable<ThreadExecution> SortThreads(IEnumerable<T> threads);
    }
}