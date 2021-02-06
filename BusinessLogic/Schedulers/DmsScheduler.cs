using BusinessLogic.Threads;
using System.Collections.Generic;

namespace BusinessLogic.Schedulers
{
    public class DmsScheduler : BaseSchedule<DeadlineThread>
    {
        private readonly int _excutionTime;

        public DmsScheduler(int excutionTime)
        {
            _excutionTime = excutionTime;
        }

        public override IEnumerable<DeadlineThread> SortThreads(IEnumerable<DeadlineThread> threads)
        {
            return threads;
        }
    }
}