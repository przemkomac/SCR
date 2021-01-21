using DataAccess.Models;
using System.Collections.Generic;

namespace BusinessLogic.Schedulers
{
    public abstract class BaseSchedule<T>
        where T : class
    {
        public abstract T GetNextThread(List<T> threads);
    }
}