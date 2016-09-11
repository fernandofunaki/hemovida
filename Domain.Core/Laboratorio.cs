using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [DataContract]
    public class laboratorio
    {
        private int _id;

        [DataMember]
        public virtual int id { get { return _id; }
            set { _id = value; }
        }
        [DataMember]
        public virtual string razao_social { get; set; }
        [DataMember]
        public virtual string nome_fantasia { get; set; }
        [DataMember]
        public virtual string telefone { get; set; }
    }
}
