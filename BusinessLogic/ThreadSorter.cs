using BusinessLogic.Enums;
using BusinessLogic.Schedulers;
using BusinessLogic.Schedulers.Models;
using BusinessLogic.Threads;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class ThreadSorter
    {
        private readonly EScheduleType _scheduleType;

        private readonly PriorityScheduler _priorityScheduler;
        private readonly DmsScheduler _dmsScheduler;
        private readonly EdfScheduler _edfScheduler;

        public ThreadSorter(EScheduleType scheduleType, int excutionTime)
        {
            _scheduleType = scheduleType;

            _priorityScheduler = new PriorityScheduler(excutionTime);
            _dmsScheduler = new DmsScheduler(excutionTime);
            _edfScheduler = new EdfScheduler(excutionTime);
        }

        public IEnumerable<ThreadExecution> Sort(IEnumerable<BaseThread> modelDeadlineParameters)
        {
            switch (_scheduleType)
            {
                case EScheduleType.Priority:
                    return _priorityScheduler.SortThreads(modelDeadlineParameters.Cast<PriorityThread>());

                case EScheduleType.Dms:
                    return _dmsScheduler.SortThreads(modelDeadlineParameters.Cast<DeadlineThread>());

                case EScheduleType.Edf:
                    return _edfScheduler.SortThreads(modelDeadlineParameters.Cast<DeadlineThread>());

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}