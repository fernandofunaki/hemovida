using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerProject
{
    public class MailBox
    {
        public Queue<MessageRequest> MessagesToRead { get; set; }

        public List<MessageRequest> MessagesSent { get; set; }

        private IRouteMessage _routeMessage;

        public MailBox()
        {
            this.MessagesToRead = new Queue<MessageRequest>();
            this.MessagesSent = new List<MessageRequest>();
        }
        public MessageRequest ReadMessage()
        {
            return this.MessagesToRead.Dequeue();
        }

        public void SendMessage(MessageRequest message)
        {
            _routeMessage.SendMessage(message);
        }
    }
}
