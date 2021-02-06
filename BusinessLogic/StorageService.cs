using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public static class StorageService
    {
        public static List<KeyValuePair<DateTime, string>> GetLogs()
        {
            return LogStorage.GetLogs()
                .OrderBy(l => l.Key)
                .ToList();
        }
    }
}