using BusinessLogic.Schedulers.Models;
using BusinessLogic.Threads;
using System;
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

        public override IEnumerable<ThreadExecution> SortThreads(IEnumerable<DeadlineThread> threads)
        {
            throw new NotImplementedException();
        }
    }
}