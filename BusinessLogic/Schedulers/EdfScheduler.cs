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
            LogStorage.AddLog("Uruchomiono algorytm zarządcy typu EDF");

            var processesWithPeriodRange = AssingProcessesIntoPeriods(threads);
            LogStorage.AddLog($"Wyznaczono {processesWithPeriodRange.Count() - 1} wykonywań procesów");

            var orderedProcessesWithPeriodRange = processesWithPeriodRange.OrderBy(t => t.DeadlineTo);
            LogStorage.AddLog("Posortowano procesy rosnąco");

            var threadsAndIdlesExecutionSequence = DesignateExecutionOrder(orderedProcessesWithPeriodRange);
            LogStorage.AddLog("Przydzielono czas procesora do procesów");

            return threadsAndIdlesExecutionSequence;
        }

        private IEnumerable<ThreadExecution> DesignateExecutionOrder(IEnumerable<ThreadProcessWithPeriodRange> orderedProcessesWithPeriodRange)
        {
            if (orderedProcessesWithPeriodRange == null || !orderedProcessesWithPeriodRange.Any())
                throw new ArgumentException();

            var queue = new Queue<ThreadProcessWithPeriodRange>(orderedProcessesWithPeriodRange);

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

        public IEnumerable<ThreadProcessWithPeriodRange> AssingProcessesIntoPeriods(IEnumerable<DeadlineThread> threads)
        {
            var container = new List<List<ThreadProcessWithPeriodRange>>();

            foreach (var thread in threads)
            {
                var periods = new List<ThreadProcessWithPeriodRange>();

                var wholePeriod = 0;
                while (wholePeriod <= _excutionTime)
                {
                    var from = periods.LastOrDefault() == null
                        ? 0
                        : periods.Last().PeriodTo + 1;
                    var to = from + thread.Deadline;
                    var periodTo = periods.LastOrDefault() == null
                        ? thread.Period
                        : periods.Last().PeriodTo + thread.Period + 1;

                    periods.Add(new ThreadProcessWithPeriodRange
                    {
                        ThreadNo = thread.ThreadNo,
                        Capacity = thread.Capacity,
                        DeadlineFrom = from,
                        DeadlineTo = to,
                        PeriodTo = periodTo
                    });
                    wholePeriod = periodTo;
                }

                var threadPeriods = new List<ThreadProcessWithPeriodRange>(periods);
                container.Add(threadPeriods);
            }

            return container.SelectMany(t => t).Select(t => t);
        }
    }
}