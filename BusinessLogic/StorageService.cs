using BusinessLogic.Enums;
using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public static class StorageService
    {
        public static List<BaseThread> GetThreads()
        {
            return ThreadStorage.GetThreads();
        }

        public static List<KeyValuePair<DateTime, string>> GetLogs()
        {
            return ThreadStorage.GetLogs()
                .OrderBy(l => l.Key)
                .ToList();
        }
    }
}