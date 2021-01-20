using DataAccess.Models;
using System;
using System.Collections.Generic;

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