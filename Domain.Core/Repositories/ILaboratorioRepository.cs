using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Repositories
{
    public interface  ILaboratorioRepository
    {
        void Add(laboratorio item);
        void Remove(laboratorio item);
        void Update(laboratorio item);
        IList<laboratorio> FindByName(string name);
        laboratorio Get(int id);
    }
}
