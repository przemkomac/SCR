using BusinessLogic.Enums;
using BusinessLogic.Schedulers;
using DataAccess;
using DataAccess.Models;
using Logic.Schedulers;
using System;
using System.Threading;

namespace Logic
{
    public class ThreadConsumer
    {
        private const int START_DELAY_SECONDS = 10;

        private readonly EScheduleType _scheduleType;

        private readonly PriorityScheduler _priorityScheduler;
        private readonly DmsScheduler _dmsScheduler;
        private readonly EdfScheduler _edfScheduler;

        public ThreadConsumer(EScheduleType scheduleType)
        {
            _scheduleType = scheduleType;

            _priorityScheduler = new PriorityScheduler();
            _dmsScheduler = new DmsScheduler();
            _edfScheduler = new EdfScheduler();
        }

        public void Start(int consumeCount)
        {
            Thread.Sleep(START_DELAY_SECONDS * 1000);

            while (consumeCount > 0)
            {
                ConsumeNext();
                consumeCount--;
            }
        }

        private void ConsumeNext()
        {
            var threads = ThreadStorage.GetThreads();

            BaseThread nextThread;

            switch (_scheduleType)
            {
                case EScheduleType.Priority:
                    {
                        nextThread = _priorityScheduler.GetNextThread(threads);
                        break;
                    }

                case EScheduleType.Dms:
                    {
                        nextThread = _dmsScheduler.GetNextThread(threads);
                        break;
                    }

                case EScheduleType.Edf:
                    {
                        nextThread = _edfScheduler.GetNextThread(threads);
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            ThreadStorage.RunThread(nextThread.Id);
            Thread.Sleep(nextThread.Cost * 1000);
            ThreadStorage.FinishThread(nextThread.Id);
        }
    }
}