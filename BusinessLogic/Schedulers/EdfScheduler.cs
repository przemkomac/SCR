using BusinessLogic.Schedulers.Models;
using BusinessLogic.Threads;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Schedulers
{
    public class EdfScheduler : BaseSchedule<DeadlineThread>
    {
        private readonly int _excutionTime;

        public EdfScheduler(int excutionTime)
        {
            _excutionTime = excutionTime;
        }

        public override IEnumerable<ThreadExecution> SortThreads(IEnumerable<DeadlineThread> threads)
        {
            LogStorage.ClearLogs();

            LogStorage.AddLog("Uruchomiono algorytm zarządcy typu EDF");

            var processesWithPeriodRange = AssingProcessesIntoPeriods(threads);
            LogStorage.AddLog($"Wyznaczono {processesWithPeriodRange.Count() - 1} wykonywań procesów");

            var orderedProcessesWithPeriodRange = processesWithPeriodRange.OrderBy(t => t.DeadlineTo);
            LogStorage.AddLog("Posortowano procesy rosnącym terminem");

            var threadsAndIdlesExecutionSequence = DesignateExecutionOrder(orderedProcessesWithPeriodRange);
            LogStorage.AddLog("Przydzielono czas procesora do procesów");

            return threadsAndIdlesExecutionSequence;
        }

        private IEnumerable<ThreadExecution> DesignateExecutionOrder(IEnumerable<ThreadProcessWithDeadlineRange> orderedProcessesWithPeriodRange)
        {
            if (orderedProcessesWithPeriodRange == null || !orderedProcessesWithPeriodRange.Any())
                throw new ArgumentException();

            var queue = new Queue<ThreadProcessWithDeadlineRange>(orderedProcessesWithPeriodRange);

            var threadExecutionsSequence = new List<ThreadExecution>();

            for (var i = 0; i <= _excutionTime;)
            {
                var thread = queue.Dequeue();

                if (i == 0)
                {
                    var threadExecutionItem = new ThreadExecution
                    {
                        ThreadNo = thread.ThreadNo,
                        Start = 0,
                        Capacity = thread.Capacity
                    };

                    threadExecutionsSequence.Add(threadExecutionItem);

                    i = threadExecutionItem.Capacity;
                }
                else
                {
                    if (thread.DeadlineFrom > i)
                    {
                        var idleExecutionItem = new ThreadExecution
                        {
                            ThreadNo = -1,
                            Start = i,
                            Capacity = thread.DeadlineFrom - i
                        };

                        threadExecutionsSequence.Add(idleExecutionItem);

                        i = thread.DeadlineFrom;

                        if (i > _excutionTime)
                            continue;
                    }

                    var threadExecutionItem = new ThreadExecution
                    {
                        ThreadNo = thread.ThreadNo,
                        Start = i,
                        Capacity = thread.Capacity
                    };

                    threadExecutionsSequence.Add(threadExecutionItem);

                    i += threadExecutionItem.Capacity;
                }
            }

            return threadExecutionsSequence;
        }

        public IEnumerable<ThreadProcessWithDeadlineRange> AssingProcessesIntoPeriods(IEnumerable<DeadlineThread> threads)
        {
            var container = new List<List<ThreadProcessWithDeadlineRange>>();

            foreach (var thread in threads)
            {
                var periods = new List<ThreadProcessWithDeadlineRange>();

                var wholePeriod = 0;
                while (wholePeriod <= _excutionTime)
                {
                    var deadlineFrom = periods.LastOrDefault() == null
                        ? 0
                        : periods.Last().PeriodTo + 1;
                    var deadlineTo = deadlineFrom + thread.Deadline; // last deadlineto + period? ew +1
                    var periodTo = periods.LastOrDefault() == null
                        ? thread.Period
                        : periods.Last().PeriodTo + thread.Period + 1;

                    periods.Add(new ThreadProcessWithDeadlineRange
                    {
                        ThreadNo = thread.ThreadNo,
                        Capacity = thread.Capacity,
                        DeadlineFrom = deadlineFrom,
                        DeadlineTo = deadlineTo,
                        PeriodTo = periodTo
                    });
                    wholePeriod = periodTo;
                }

                var threadPeriods = new List<ThreadProcessWithDeadlineRange>(periods);
                container.Add(threadPeriods);
            }

            return container.SelectMany(t => t).Select(t => t);
        }
    }
}