using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartBeat.Core
{
    public interface IHeartBeat
    {
        void Start(int monitoredItemId);
        void Stop();
    }
}
