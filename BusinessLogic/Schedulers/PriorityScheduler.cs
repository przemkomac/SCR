using BusinessLogic.Threads;
using System.Collections.Generic;

namespace BusinessLogic.Schedulers
{
    public class PriorityScheduler : BaseSchedule<PriorityThread>
    {
        private readonly int _excutionTime;

        public PriorityScheduler(int excutionTime)
        {
            _excutionTime = excutionTime;
        }

        public override IEnumerable<PriorityThread> SortThreads(IEnumerable<PriorityThread> threads)
        {
            return threads;
        }
    }
}