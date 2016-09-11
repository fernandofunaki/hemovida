using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [ServiceContract]
    public interface IAgendaService
    {

        [OperationContract]
        string GetStatus();

        [OperationContract]
        IList<laboratorio> GetLaboratorios(string name);

        [OperationContract]
        laboratorio GetLaboratorio(int id);
    }
}
