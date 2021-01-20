using System;

namespace DataAccess.Models
{
    public abstract class BaseThread
    {
        public Guid Id { get; set; }
        public int Cost { get; set; }
    }
}