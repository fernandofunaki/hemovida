using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Domain.HealthChecker
{
    public interface IHealthChecker
    {
        void Initialize(int monitoredItemId);
        void Finalizer();
        void StartCapture(HealthCheckerType type);
        void StopCapture(HealthCheckerType type);
        void SendReport(object sender, ElapsedEventArgs e);
    }
}
