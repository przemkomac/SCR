using System;
using System.Collections.Generic;

namespace DataAccess
{
    public static class LogStorage
    {
        private static readonly object _lockerLogs;

        private static List<KeyValuePair<DateTime, string>> _logs;

        static LogStorage()
        {
            _lockerLogs = new object();

            _logs = new List<KeyValuePair<DateTime, string>>();
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