using DataAccess.Enums;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public static class ThreadStorage
    {
        private static object _lockerThreads;
        private static object _lockerLogs;

        private static List<BaseThread> _priorityThreads;
        private static List<KeyValuePair<DateTime, string>> _logs;

        static ThreadStorage()
        {
            _lockerThreads = new object();
            _lockerLogs = new object();

            _priorityThreads = new List<BaseThread>();
            _logs = new List<KeyValuePair<DateTime, string>>();
        }

        public static void ClearThreads()
        {
            lock (_lockerThreads)
            {
                _priorityThreads = new List<BaseThread>();
            }
        }

        public static void ClearLogs()
        {
            lock (_lockerLogs)
            {
                _logs = new List<KeyValuePair<DateTime, string>>();
            }
        }

        public static void AddThread(BaseThread thread)
        {
            lock (_lockerThreads)
            {
                thread.Inserted = DateTime.Now;
                thread.ThreadStatus = EThreadStatus.Added;
                _priorityThreads.Add(thread);
            }
        }

        public static List<BaseThread> GetThreads()
        {
            List<BaseThread> tempList;

            lock (_lockerThreads)
            {
                tempList = new List<BaseThread>(_priorityThreads);
            }

            return tempList;
        }

        public static void FinishThread(Guid threadId)
        {
            lock (_lockerThreads)
            {
                var toRemove = _priorityThreads.First(t => t.Id == threadId);
                toRemove.ThreadStatus = EThreadStatus.Finished;
                toRemove.Finished = DateTime.Now;
            }
        }

        public static void RunThread(Guid threadId)
        {
            lock (_lockerThreads)
            {
                var toStart = _priorityThreads.First(t => t.Id == threadId);
                toStart.Started = DateTime.Now;
                toStart.ThreadStatus = EThreadStatus.Running;
            }
        }

        public static void AddLog(string log)
        {
            lock (_lockerLogs)
            {
                _logs.Add(new KeyValuePair<DateTime, string>(DateTime.Now, log));
            }
        }

        public static List<KeyValuePair<DateTime, string>> GetLogs()
        {
            List<KeyValuePair<DateTime, string>> tempList;

            lock (_lockerLogs)
            {
                tempList = new List<KeyValuePair<DateTime, string>>(_logs);
            }

            return tempList;
        }
    }
}