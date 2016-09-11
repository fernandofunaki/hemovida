using HeartBeat.Core;
using HeartBeat.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HeartBeat.Component
{
    public class HeartBeatComponent : IHeartBeat
    {
        private System.Timers.Timer beatTime;
        private IHeartBeatInfoRepository _heartBeatInfoRepository;
        private int _monitoredItemId;

        public HeartBeatComponent(IHeartBeatInfoRepository heartBeatInfoRepository)
        {
            _heartBeatInfoRepository = heartBeatInfoRepository;
            beatTime = new System.Timers.Timer(3000);
        }

        public void Start(int monitoredItemId)
        {
            _monitoredItemId = monitoredItemId;
            beatTime.Elapsed += new ElapsedEventHandler(Register);
            beatTime.Start();
        }

        private void Register(object sender, ElapsedEventArgs e)
        {
            _heartBeatInfoRepository.Salvar(new HeartBeatInfo() { WorkerId = _monitoredItemId, Beat = true, CreatedAt = DateTime.Now });
        }

        public void Stop()
        {
            beatTime.Stop();
        }
    }
}
