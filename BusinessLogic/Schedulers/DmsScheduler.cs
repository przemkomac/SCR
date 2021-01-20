using BusinessLogic.Schedulers;
using DataAccess.Models;

namespace Logic.Schedulers
{
    public class DmsScheduler : BaseSchedule<DeadlineThread>
    {
        public override DeadlineThread GetNextThread()
        {
            throw new System.NotImplementedException();
        }
    }
}