using BusinessLogic.Schedulers.Models;
using BusinessLogic.Threads;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Schedulers
{
    public class PriorityScheduler : BaseSchedule<PriorityThread>
    {
        private readonly int _excutionTime;

        public PriorityScheduler(int excutionTime)
        {
            _excutionTime = excutionTime;
        }

        public override IEnumerable<ThreadExecution> SortThreads(IEnumerable<PriorityThread> threads)
        {
            LogStorage.ClearLogs();

            LogStorage.AddLog("Uruchomiono algorytm zarządcy typu priorytetowego");

            var processesWithPriorityRange = AssingProcessesIntoPeriods(threads).ToList();
            LogStorage.AddLog($"Wyznaczono {processesWithPriorityRange.Count} wykonywań procesów");

            var orderedProcessesWithPriorityRange = OrderThreadsExecution(processesWithPriorityRange);
            LogStorage.AddLog("Posortowano procesy rosnącym początkiem okresu z uwzględnieniem priorytetu");

            var threadsAndIdlesExecutionSequence = DesignateExecutionOrder(orderedProcessesWithPriorityRange);
            LogStorage.AddLog("Przydzielono czas procesora do procesów");

            return threadsAndIdlesExecutionSequence;
        }

        private IEnumerable<ThreadExecution> DesignateExecutionOrder(IEnumerable<ThreadProcessWithPriorityRange> orderedProcessesWithPriorityRange)
        {
            if (orderedProcessesWithPriorityRange == null || !orderedProcessesWithPriorityRange.Any())
                throw new ArgumentException();

            var queue = new Queue<ThreadProcessWithPriorityRange>(orderedProcessesWithPriorityRange);

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
                    if (thread.PeriodFrom > i)
                    {
                        var idleExecutionItem = new ThreadExecution
                        {
                            ThreadNo = -1,
                            Start = i,
                            Capacity = thread.PeriodFrom - i
                        };

                        threadExecutionsSequence.Add(idleExecutionItem);

                        i = thread.PeriodFrom;

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

        private IEnumerable<ThreadProcessWithPriorityRange> OrderThreadsExecution(IEnumerable<ThreadProcessWithPriorityRange> processesWithPriorityRange)
        {
            if (processesWithPriorityRange == null || !processesWithPriorityRange.Any())
                throw new ArgumentException();

            var processesWithPriorityRangeTempList = processesWithPriorityRange.OrderBy(t => t.PeriodFrom).ToList();
            var threadExecutionsSequence = new List<ThreadProcessWithPriorityRange>();

            for (var i = 0; i <= _excutionTime;)
            {
                var consideringToGet = processesWithPriorityRangeTempList.Where(t => Between(i, t.PeriodFrom, t.PeriodTo - t.Capacity));

                if (!processesWithPriorityRangeTempList.Any())
                    return threadExecutionsSequence;

                if (!consideringToGet.Any())
                {
                    i++;
                    continue;
                }

                var result = consideringToGet.OrderByDescending(t => t.Priority).First();

                processesWithPriorityRangeTempList.Remove(result);

                threadExecutionsSequence.Add(result);
                i += result.Capacity;
            }

            return threadExecutionsSequence;

            bool Between(int x, int a, int b) => (a <= x) && (x <= b);
        }

        public IEnumerable<ThreadProcessWithPriorityRange> AssingProcessesIntoPeriods(IEnumerable<PriorityThread> threads)
        {
            var container = new List<List<ThreadProcessWithPriorityRange>>();

            foreach (var thread in threads)
            {
                var periods = new List<ThreadProcessWithPriorityRange>();

                var wholePeriod = 0;
                while (wholePeriod <= _excutionTime)
                {
                    var periodFrom = periods.LastOrDefault() == null
                        ? 0
                        : periods.Last().PeriodTo;
                    var periodTo = periodFrom + thread.Period;

                    if (periodTo <= _excutionTime)
                    {
                        periods.Add(new ThreadProcessWithPriorityRange
                        {
                            ThreadNo = thread.ThreadNo,
                            Capacity = thread.Capacity,
                            PeriodFrom = periodFrom,
                            PeriodTo = periodTo,
                            Priority = thread.Priority
                        });
                    }

                    wholePeriod = periodTo;
                }

                var threadPeriods = new List<ThreadProcessWithPriorityRange>(periods);
                container.Add(threadPeriods);
            }

            return container.SelectMany(t => t);
        }
    }
}