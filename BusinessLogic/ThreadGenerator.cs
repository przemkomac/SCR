using BusinessLogic.Enums;
using DataAccess;
using DataAccess.Models;
using System;
using System.Threading;

namespace Logic
{
    public class ThreadGenerator
    {
        private readonly EScheduleType _scheduleType;

        private readonly int _generationIntervalFrom;
        private readonly int _generationIntervalTo;
        private readonly int _costFrom;
        private readonly int _costTo;
        private readonly int _priorityFrom;
        private readonly int _priorityTo;

        private readonly Random _random;

        public ThreadGenerator(
            EScheduleType scheduleType,
            int generationIntervalFrom,
            int generationIntervalTo,
            int costFrom,
            int costTo)
        {
            _scheduleType = scheduleType;

            _generationIntervalFrom = generationIntervalFrom;
            _generationIntervalTo = generationIntervalTo;
            _costFrom = costFrom;
            _costTo = costTo;
            _priorityFrom = 1;
            _priorityTo = 10;

            _random = new Random(DateTime.Now.Millisecond);
        }

        public void Start(int generateCount)
        {
            ThreadStorage.ClearThreads();
            ThreadStorage.ClearLogs();

            ThreadStorage.AddLog($"Generator rozpoczął pracę.");

            while (generateCount > 0)
            {
                GenerateNext();
                generateCount--;
                Thread.Sleep(_random.Next(_generationIntervalFrom, _generationIntervalTo) * 1000);
            }

            ThreadStorage.AddLog($"Generator zakończył pracę.");
        }

        private void GenerateNext()
        {
            if (_scheduleType == EScheduleType.Priority)
            {
                var thread = new PriorityThread
                {
                    Id = Guid.NewGuid(),
                    Cost = _random.Next(_costFrom, _costTo),
                    Priority = _random.Next(_priorityFrom, _priorityTo)
                };

                ThreadStorage.AddThread(thread);
                ThreadStorage.AddLog($"Dodano zadanie o ID {thread.Id}, złożoności {thread.Cost} s. i priorytecie {thread.Priority}.");
            }
            else
            {
                var thread = new DeadlineThread
                {
                    Id = Guid.NewGuid(),
                    Cost = _random.Next(_costFrom, _costTo),
                    Deadline = DateTime.Now.AddSeconds(_random.Next(_costFrom, _costTo) * 2)
                };

                ThreadStorage.AddThread(thread);
                ThreadStorage.AddLog($"Dodano zadanie o ID {thread.Id}, złożoności {thread.Cost} s. i deadline {thread.Deadline}.");
            }
        }
    }
}