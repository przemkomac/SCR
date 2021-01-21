using DataAccess.Enums;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Schedulers
{
    public class PriorityScheduler : BaseSchedule<PriorityThread>
    {
        public override PriorityThread GetNextThread(List<PriorityThread> threads)
        {
            return threads
                .Where(t => t.ThreadStatus == EThreadStatus.Added)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.Inserted)
                .FirstOrDefault();
        }
    }
}