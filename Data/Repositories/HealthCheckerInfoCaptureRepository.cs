
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


namespace Data.Repositories
{
    public class HealthCheckerInfoCaptureRepository : IHealthCheckerInfoCaptureRepository
    {
        private readonly ISession _session;

        public HealthCheckerInfoCaptureRepository(ISession session)
        {
            _session = session;
        }
        public void Salvar(HealthCheckerInfoCapture item)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(item);
                tran.Commit();
            }
        }
        public void Excluir(HealthCheckerInfoCapture item)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Delete(item);
                tran.Commit();
            }
        }

        public void Alterar(HealthCheckerInfoCapture item)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Update(item);
                tran.Commit();
            }
        }
        public HealthCheckerInfoCapture ObterPor(int id)
        {
            return _session.Get<HealthCheckerInfoCapture>(id);
        }

    }


}
