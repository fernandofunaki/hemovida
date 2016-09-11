using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerProject
{
    public interface IRouteMessage
    {
        void SendMessage(MessageRequest message);
    }
}
