using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Core
{
    [Serializable]
    public class Message
    {
        public ContentMessage ContentMessage { get; set; }
        private string hash;

        [XmlElement(IsNullable = true)]
        public string Hash
        {
            get { return string.IsNullOrEmpty(this.hash) ? string.Empty : this.hash; }
            set
            {
                if (this.hash != value)
                {
                    this.hash = value;
                }
            }
        }

        private string assinatura;

        [XmlElement(IsNullable = true)]
        public string Assinatura
        {
            get { return string.IsNullOrEmpty(this.assinatura) ? string.Empty : this.assinatura; }
            set
            {
                if (this.assinatura != value)
                {
                    this.assinatura = value;
                }
            }
        }

        public Message()
        {
            ContentMessage = new ContentMessage();
        }
    }

    [Serializable]
    public class ContentMessage
    {
        private string emitenteId;
        private string emitenteNome;
        private string cnpj;
        private string eventoId;
       
        [XmlElement(IsNullable = true)]
        public string EmitenteId
        {
            get { return string.IsNullOrEmpty(this.emitenteId) ? string.Empty : this.emitenteId; }
            set
            {
                if (this.emitenteId != value)
                {
                    this.emitenteId = value;
                }
            }
        }

        [XmlElement(IsNullable = true)]
        public string EmitenteNome
        {
            get { return string.IsNullOrEmpty(this.emitenteNome) ? string.Empty : this.emitenteNome; }
            set
            {
                if (this.emitenteNome != value)
                {
                    this.emitenteNome = value;
                }
            }
        }

        [XmlElement(IsNullable = true)]
        public string CNPJ
        {
            get { return string.IsNullOrEmpty(this.cnpj) ? string.Empty : this.cnpj; }
            set
            {
                if (this.cnpj != value)
                {
                    this.cnpj = value;
                }
            }
        }

        [XmlElement(IsNullable = true)]
        public string EventoId
        {
            get { return string.IsNullOrEmpty(this.eventoId) ? string.Empty : this.eventoId; }
            set
            {
                if (this.eventoId != value)
                {
                    this.eventoId = value;
                }
            }
        }
    }
}
