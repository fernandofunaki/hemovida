using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FluentNHibernate.Mapping;
using Domain.HealthChecker;
using System.Linq;
using HeartBeat.Core.Repositories;
using HeartBeat.Core;


namespace Data.Repositories
{

    public class HeartBeatInfoRepository : IHeartBeatInfoRepository
    {
        private readonly ISession _session;

        public HeartBeatInfoRepository(ISession session)
        {
            _session = session;
        }
        public void Salvar(HeartBeatInfo item)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Save(item);
                tran.Commit();
            }
        }
        public void Excluir(HeartBeatInfo item)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Delete(item);
                tran.Commit();
            }
        }

        public void Alterar(HeartBeatInfo item)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Update(item);
                tran.Commit();
            }
        }
        public HeartBeatInfo ObterPor(int id)
        {
            return _session.Get<HeartBeatInfo>(id);
        }
    }
}
