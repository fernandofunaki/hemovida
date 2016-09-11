using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartBeat.Core.Repositories
{
    public interface IHeartBeatInfoRepository
    {
        void Salvar(HeartBeatInfo item);
        void Excluir(HeartBeatInfo item);
        void Alterar(HeartBeatInfo item);
        HeartBeatInfo ObterPor(int id);
    }
}
