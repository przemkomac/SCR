using DataAccess.Enums;
using System;

namespace DataAccess.Models
{
    public abstract class BaseThread
    {
        public Guid Id { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime? Finished { get; set; }
        public EThreadStatus ThreadStatus { get; set; }
        public int Cost { get; set; }
    }
}