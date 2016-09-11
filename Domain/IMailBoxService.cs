using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    [ServiceContract]
    public interface IMailBoxService
    {
        [OperationContract]
        [ServiceKnownType(typeof(MessageRequest))]
        [ServiceKnownType(typeof(SolicitacaoCadastroDeLaboratorioMessage))]
        [ServiceKnownType(typeof(RespostaCadastroDeLaboratorioMessage))]
        MessageResponse SendMessage(MessageRequest message);
    }
}
