using BusinessLogic.Schedulers.Models;
using BusinessLogic.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace BusinessLogic.Schedulers
{
    public class DmsScheduler : BaseSchedule<DeadlineThread>
    {
        private readonly int _excutionTime;

        public DmsScheduler(int excutionTime)
        {
            _excutionTime = excutionTime;
        }

        public override IEnumerable<ThreadExecution> SortThreads(IEnumerable<DeadlineThread> threads)
        {
            LogStorage.ClearLogs();

            LogStorage.AddLog("Uruchomiono algorytm zarządcy typu DMS");

            var processesWithPeriodRange = AssingProcessesIntoPeriods(threads);
            LogStorage.AddLog($"Wyznaczono {processesWithPeriodRange.Count() - 1} wykonywań procesów");

            var orderedProcessesWithPeriodRange = OrderThreadsExecution(processesWithPeriodRange);
            LogStorage.AddLog("Posortowano procesy rosnącym terminem");

            var threadsAndIdlesExecutionSequence = DesignateExecutionOrder(orderedProcessesWithPeriodRange);
            LogStorage.AddLog("Przydzielono czas procesora do procesów");

            return threadsAndIdlesExecutionSequence;
        }

        private IEnumerable<ThreadExecution> DesignateExecutionOrder(IEnumerable<ThreadProcessWithPriorityDeadlineRange> orderedProcessesWithPeriodRange)
        {
            if (orderedProcessesWithPeriodRange == null || !orderedProcessesWithPeriodRange.Any())
                throw new ArgumentException();

            var queue = new Queue<ThreadProcessWithPriorityDeadlineRange>(orderedProcessesWithPeriodRange);

            var threadExecutionsSequence = new List<ThreadExecution>();

            for (var i = 0; i <= _excutionTime;)
            {
                if (!queue.Any())
                    break;

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

        private IEnumerable<ThreadProcessWithPriorityDeadlineRange> OrderThreadsExecution(IEnumerable<ThreadProcessWithPriorityDeadlineRange> processesWithPeriodRange)
        {
            if (processesWithPeriodRange == null || !processesWithPeriodRange.Any())
                throw new ArgumentException();

            var processesWithPeriodRangeTempList = processesWithPeriodRange.OrderBy(t => t.DeadlineFrom).ToList();
            var threadExecutionsSequence = new List<ThreadProcessWithPriorityDeadlineRange>();

            for (var i = 0; i <= _excutionTime;)
            {
                var consideringToGet = processesWithPeriodRangeTempList.Where(t => Between(i, t.DeadlineFrom, t.DeadlineTo - t.Capacity));

                if (!processesWithPeriodRangeTempList.Any())
                    return threadExecutionsSequence;

                if (!consideringToGet.Any())
                {
                    i++;
                    continue;
                }

                var result = consideringToGet.OrderBy(t => t.BaseDeadline).First();

                processesWithPeriodRangeTempList.Remove(result);

                threadExecutionsSequence.Add(result);
                i += result.Capacity;
            }

            return threadExecutionsSequence;

            bool Between(int x, int a, int b) => (a <= x) && (x <= b);
        }

        public IEnumerable<ThreadProcessWithPriorityDeadlineRange> AssingProcessesIntoPeriods(IEnumerable<DeadlineThread> threads)
        {
            var container = new List<List<ThreadProcessWithPriorityDeadlineRange>>();

            foreach (var thread in threads)
            {
                var periods = new List<ThreadProcessWithPriorityDeadlineRange>();

                var wholePeriod = 0;
                while (wholePeriod <= _excutionTime)
                {
                    var deadlineFrom = periods.LastOrDefault() == null
                        ? 0
                        : periods.Last().PeriodTo;
                    var deadlineTo = deadlineFrom + thread.Deadline + 1;
                    var periodTo = periods.LastOrDefault() == null
                        ? thread.Period
                        : periods.Last().PeriodTo + thread.Period;

                    if (periodTo <= _excutionTime)
                    {
                        periods.Add(new ThreadProcessWithPriorityDeadlineRange
                        {
                            ThreadNo = thread.ThreadNo,
                            Capacity = thread.Capacity,
                            DeadlineFrom = deadlineFrom,
                            DeadlineTo = deadlineTo,
                            PeriodTo = periodTo,
                            BaseDeadline = thread.Deadline
                        });
                    }
                        
                    wholePeriod = periodTo;
                }

                var threadPeriods = new List<ThreadProcessWithPriorityDeadlineRange>(periods);
                container.Add(threadPeriods);
            }

            return container.SelectMany(t => t).Select(t => t);
        }
    }
}