using Domain.Core;
using Domain.HealthChecker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace Hemovida.HealthChecker
{
    public class HealthCheckerComponent : IHealthChecker
    {
        private PerformanceCounter _cpuCounter;
        private PerformanceCounter _ramCounter;
        private System.Timers.Timer _bitTime;
        public List<HealthCheckerInfoCapture> _reportList;
        private HealthCheckerInfoCapture hc = null;
        private int currentId = 0;
        private int _monitoredItemId = 0;
        private Stopwatch _stopWatch;
        private IHealthCheckerInfoCaptureRepository _healthCheckerInfoCaptureRepository;

        /// <summary>
        /// HealthChecker Contructor
        /// </summary>
        public HealthCheckerComponent(IHealthCheckerInfoCaptureRepository healthCheckerInfoCaptureRepository)
        {
            _healthCheckerInfoCaptureRepository = healthCheckerInfoCaptureRepository;
        }


        /// <summary>
        /// Initiliaze Component
        /// </summary>
        public void Initialize(int monitoredItemId)
        {
            _monitoredItemId = monitoredItemId;
            _reportList = new List<HealthCheckerInfoCapture>();
            _cpuCounter = new PerformanceCounter();
            _cpuCounter.CategoryName = "Processor";
            _cpuCounter.CounterName = "% Processor Time";
            _cpuCounter.InstanceName = "_Total";
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            _bitTime = new System.Timers.Timer(60000);
            _stopWatch = new Stopwatch();

            _bitTime.Elapsed += new ElapsedEventHandler(SendReport);
            _bitTime.Start();

            hc = new HealthCheckerInfoCapture(HealthCheckerType.Stopped);
            _stopWatch.Start();
        }


        /// <summary>
        /// Send Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SendReport(object sender, ElapsedEventArgs e)
        {
            List<HealthCheckerInfoCapture> clonedList;

            //stop to clone list
            lock (_reportList)
                clonedList = CloneList(_reportList);

            try
            {
                if (clonedList == null)
                    return;

                //group items by Type and sum items
                List<HealthCheckerInfoCapture> groupedItems = GroupByTypeAndSumItems(clonedList);

                //compute average
                ComputeAverage(groupedItems, _bitTime.Interval, clonedList.Count(a => a.Type == HealthCheckerType.Working));

                //save items in db
                Save(groupedItems);
            }
            catch (Exception ex)
            {
                //TODO : Handler exceptions

                //rollback list
                _reportList.InsertRange(0, clonedList);
            }
            finally
            {
                if (clonedList != null)
                    clonedList.Clear();
            }
        }


        /// <summary>
        /// Compute Average
        /// </summary>
        /// <param name="items">List of HealthCheckerInfoCapture</param>
        /// <param name="IntervalTime">Interval time to compute average time</param>
        /// <param name="totalItems">Total Captured items to compute average usage of memory and CPU</param>
        private void ComputeAverage(List<HealthCheckerInfoCapture> items, double IntervalTime, int totalItems)
        {
            foreach (var item in items)
            {
                item.PercentageTime = item.Time * 100 / Convert.ToDecimal(IntervalTime);
                item.CPUUsage = item.CPUUsage / totalItems;
                item.MemoryUsage = item.MemoryUsage / totalItems;
            }
        }

        /// <summary>
        /// Group By Type And Sum Captured Items
        /// </summary>
        /// <param name="items">List of HealthCheckerInfoCapture</param>
        /// <returns>Grouped Items</returns>
        private List<HealthCheckerInfoCapture> GroupByTypeAndSumItems(List<HealthCheckerInfoCapture> items)
        {
            //Sum items
            items = (items.GroupBy(a => a.Type)
                .Select(x => new HealthCheckerInfoCapture(x.Key)
            {
                CPUUsage = x.Sum(z => z.CPUUsage),
                MemoryUsage = x.Sum(z => z.MemoryUsage),
                Time = x.Sum(a => a.Time),
                Type = x.Key
            })).ToList();

            return items;
        }

        /// <summary>
        /// Clone Report List
        /// </summary>
        /// <param name="reportList">List of HealthCheckerInfoCapture</param>
        /// <returns>List of HealthCheckerInfoCapture</returns>
        private List<HealthCheckerInfoCapture> CloneList(List<HealthCheckerInfoCapture> reportList)
        {
            List<HealthCheckerInfoCapture> clonedList = new List<HealthCheckerInfoCapture>(reportList.Count);
            reportList.ForEach((item) => { clonedList.Add(item); });
            reportList.Clear();
            return clonedList;
        }

        /// <summary>
        /// Save report in data base
        /// </summary>
        /// <param name="items">List of HealthCheckerInfoCapture</param>
        private void Save(List<HealthCheckerInfoCapture> items)
        {
            foreach (var item in items)
            {
                item.WorkerId = _monitoredItemId;
                _healthCheckerInfoCaptureRepository.Salvar(item);
            }
        }

        /// <summary>
        /// Finalize Health Checker
        /// </summary>
        public void Finalizer()
        {
            _bitTime.Stop();
            _stopWatch.Stop();
        }


        /// <summary>
        /// Start Capturing
        /// </summary>
        public void StartCapture(HealthCheckerType type)
        {
            SaveCapture(type);
        }

        /// <summary>
        /// Stop Capturing
        /// </summary>
        public void StopCapture(HealthCheckerType type)
        {
            SaveCapture(type);
        }

        /// <summary>
        /// Save report in data base
        /// </summary>
        /// <param name="type">HealthCheckerType</param>
        private void SaveCapture(HealthCheckerType type)
        {
            //Para contagem de ociosidade
            if (_stopWatch.IsRunning)
            {
                _stopWatch.Stop();
                hc.Time = _stopWatch.ElapsedMilliseconds;
                hc.CPUUsage = _cpuCounter.NextValue();
                hc.MemoryUsage = _ramCounter.NextValue();
                _stopWatch.Reset();
                _reportList.Add(hc);
            }

            //inicia contagem processamento
            _stopWatch.Start();
            hc = new HealthCheckerInfoCapture(type);
        }
    }
}
