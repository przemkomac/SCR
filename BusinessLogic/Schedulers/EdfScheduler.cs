using BusinessLogic.Schedulers;
using DataAccess.Models;

namespace Logic.Schedulers
{
    public class EdfScheduler : BaseSchedule<DeadlineThread>
    {
        public override DeadlineThread GetNextThread()
        {
            throw new System.NotImplementedException();
        }
    }
}