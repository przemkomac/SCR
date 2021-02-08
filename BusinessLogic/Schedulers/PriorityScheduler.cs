using BusinessLogic.Schedulers.Models;
using BusinessLogic.Threads;
using System;
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

        public override IEnumerable<ThreadExecution> SortThreads(IEnumerable<PriorityThread> threads)
        {
            throw new NotImplementedException();
        }
    }
}