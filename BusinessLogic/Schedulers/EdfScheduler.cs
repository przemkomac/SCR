using BusinessLogic.Threads;
using System.Collections.Generic;

namespace BusinessLogic.Schedulers
{
    public class EdfScheduler : BaseSchedule<DeadlineThread>
    {
        private readonly int _excutionTime;

        public EdfScheduler(int excutionTime)
        {
            _excutionTime = excutionTime;
        }

        public override IEnumerable<DeadlineThread> SortThreads(IEnumerable<DeadlineThread> threads)
        {
            return threads;
        }
    }
}