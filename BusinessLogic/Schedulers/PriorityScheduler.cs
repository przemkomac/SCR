using DataAccess.Models;
using System;

namespace BusinessLogic.Schedulers
{
    public class PriorityScheduler : BaseSchedule<PriorityThread>
    {
        public override PriorityThread GetNextThread()
        {
            throw new NotImplementedException();
        }
    }
}