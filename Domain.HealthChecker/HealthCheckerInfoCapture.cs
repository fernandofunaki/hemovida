using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HealthChecker
{
    public class HealthCheckerInfoCapture
    {
        private int _id;
        public virtual int Id { get { return _id; } }
        public virtual float CPUUsage { get; set; }
        public virtual float MemoryUsage { get; set; }
        public virtual long Time { get; set; }
        public virtual decimal PercentageTime { get; set; }
        public virtual HealthCheckerType Type { get; set; }
        public virtual int WorkerId { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public HealthCheckerInfoCapture()
        {
            CreatedAt = DateTime.Now;
        }

        public HealthCheckerInfoCapture(HealthCheckerType type)
        {
            Type = type;
            CreatedAt = DateTime.Now;
        }
    }
}
