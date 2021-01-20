using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Schedulers
{
    public class PriorityScheduler : BaseSchedule<PriorityThread>
    {
        public override PriorityThread GetNextThread(List<BaseThread> threads)
        {
            throw new NotImplementedException();
        }
    }
}