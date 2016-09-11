using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interface;
using Domain.Core;
using System.ServiceModel;

namespace Services.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MailBoxService : Services.Interface.IMailBoxService
    {
        public MessageResponse SendMessage(MessageRequest message)
        {
            MessageResponse response = new RespostaCadastroDeLaboratorioMessage() { Sender = "message test" };
            return response;
        }
    }
}
