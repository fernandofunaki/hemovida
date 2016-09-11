using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Core;
using Hemovida.HealthChecker;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Domain.HealthChecker;


namespace HealthCheckerTests
{
    [TestClass]
    public class HealthCheckerTests
    {
        IHealthChecker _hc = null;

        public void Start()
        {
            //_hc = new HealthCheckerComponent();
            //_hc.Initialize();
        }

        [TestMethod]
        public void Test()
        {
            //HealthCheckerComponent hc = new HealthCheckerComponent();
            //hc.Initialize();

            //List<HealthCheckerInfoCapture> _report = new List<HealthCheckerInfoCapture>();
            //_report.Add(new HealthCheckerInfoCapture(HealthCheckerType.Stopped) { MemoryUsage = 2000, CPUUsage = 10000, Time = 3000 });
            //_report.Add(new HealthCheckerInfoCapture(HealthCheckerType.Working) { MemoryUsage = 2500, CPUUsage = 20000, Time = 27000 });
            //_report.Add(new HealthCheckerInfoCapture(HealthCheckerType.Stopped) { MemoryUsage = 2000, CPUUsage = 10000, Time = 3000 });
            //_report.Add(new HealthCheckerInfoCapture(HealthCheckerType.Working) { MemoryUsage = 2500, CPUUsage = 20000, Time = 27000 });

            //hc._reportList = _report;
            //hc.SendReport(null, null);

        }
    }
}



//PerformanceCounter _cpuCounter = new PerformanceCounter();
//_cpuCounter.CategoryName = "Processor";
//_cpuCounter.CounterName = "% Processor Time";
//_cpuCounter.InstanceName = "_Total";
//PerformanceCounter _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
//System.Diagnostics.Process currentProc = System.Diagnostics.Process.GetCurrentProcess();

//for (int i = 0; i < 5; i++)
//{
//    long test1 = GC.GetTotalMemory(true);
//    float beforeCPU = _cpuCounter.NextValue();
//    float beforeRAM = _ramCounter.NextValue();
//    long beforememoryUsed = currentProc.PrivateMemorySize64;
//    ProcessingSimulator();
//    float afterCPU = _cpuCounter.NextValue();
//    float afterRAM = _ramCounter.NextValue();
//    long afterememoryUsed = currentProc.PrivateMemorySize64;
//    long test2 = GC.GetTotalMemory(true);
//}


//List<HealthCheckerInfoCapture> _report = new List<HealthCheckerInfoCapture>();
//_report.Add(new HealthCheckerInfoCapture(1) { Id = 1, CPUUsage = 10, MemoryUsage = 10, Time = 0 });
//_report.Add(new HealthCheckerInfoCapture(2) { Id = 1, CPUUsage = 10, MemoryUsage = 10, Time = 10 });
//_report.Add(new HealthCheckerInfoCapture(3) { Id = 2, CPUUsage = 10, MemoryUsage = 10, Time = 0 });
//_report.Add(new HealthCheckerInfoCapture(4) { Id = 2, CPUUsage = 10, MemoryUsage = 10, Time = 10 });

//totalItems = _report.Count();

//if (totalItems > 0)
//{
//    totalMemory = _report.Sum(a => a.MemoryUsage) / totalItems;
//    totalCPU = _report.Sum(a => a.CPUUsage) / totalItems;
//    time = _report.Sum(a => a.Time) / totalItems;
//}

//var result = _report.GroupBy(x => new { x.Id })
//                            .Select(x => new
//                            {
//                                Id = x.Key.Id,
//                                CPUUsage = x.Sum(z => z.CPUUsage),
//                                MemoryUsage = x.Sum(z => z.MemoryUsage),
//                                Time = x.Sum(a => a.Time),
//                            });

//totalItems = result.Count();

//if (totalItems > 0)
//{
//    totalMemory = result.Sum(a => a.MemoryUsage) / totalItems;
//    totalCPU = result.Sum(a => a.CPUUsage) / totalItems;
//    time = result.Sum(a => a.Time) / totalItems;
//}