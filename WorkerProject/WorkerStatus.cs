using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerProject
{
    public enum WorkerStatus
    {
        Initializing,
        Initialized,
        Stopping,
        Stopped,
        Pausing,
        Paused,
        Processing,
        Processed,
        AwaitingJob,
        Recovering,
        Recovered,
        CancellingRecovery,
        CancelledRecovered,
        Failed
    }
}
