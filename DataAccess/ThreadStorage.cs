using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public static class ThreadStorage
    {
        public static List<BaseThread> PriorityThreads = new List<BaseThread>();

        public static KeyValuePair<DateTime, string> Logs = new KeyValuePair<DateTime, string>();

        public static void ClearThreads()
        {
            PriorityThreads = new List<BaseThread>();
        }
    }
}