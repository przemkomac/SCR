using BusinessLogic.Schedulers;
using DataAccess.Enums;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Schedulers
{
    public class EdfScheduler : BaseSchedule<DeadlineThread>
    {
        public override DeadlineThread GetNextThread(List<DeadlineThread> threads)
        {
            return threads
                .Where(t => t.ThreadStatus == EThreadStatus.Added)
                .OrderBy(t => t.Deadline)
                .ThenBy(t => t.Inserted)
                .FirstOrDefault();
        }
    }
}