using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [DataContract]
    public abstract class MessageRequest
    {
        [DataMember]
        public string Sender { get; set; }
        [DataMember]
        public string Receiver { get; set; }
    }


    [DataContract]
    public abstract class MessageResponse
    {
        [DataMember]
        public string Sender { get; set; }
        [DataMember]
        public string Receiver { get; set; }
    }

    [DataContract]
    public class SolicitacaoCadastroDeLaboratorioMessage : MessageRequest
    {

    }

    [DataContract]
    public class RespostaCadastroDeLaboratorioMessage : MessageResponse
    {

    }
}
