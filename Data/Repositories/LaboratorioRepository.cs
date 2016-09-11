
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FluentNHibernate.Mapping;
using System.Linq;
using Domain.Core;
using Domain.Core.Repositories;

namespace Data
{
    public class LaboratorioRepository : ILaboratorioRepository
    {
        private readonly ISession _session;

        public LaboratorioRepository(ISession session)
        {
            _session = session;
        }
        public void Add(laboratorio contato)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(contato);
                tran.Commit();
            }
        }
        public void Remove(laboratorio contato)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Delete(contato);
                tran.Commit();
            }
        }

        public void Update(laboratorio contato)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Update(contato);
                tran.Commit();
            }
        }

        public IList<laboratorio> FindByName(string name)
        {
            IList<laboratorio> laboratorios = _session
                .CreateSQLQuery(string.Format(@"select top 10 * from laboratorio  where nome_fantasia like '%{0}%'", name))
                .AddEntity("a", typeof(laboratorio)).List<laboratorio>();

            return laboratorios;
        }


        public laboratorio Get(int id)
        {
            return _session.Get<laboratorio>(id);
        }
    }
}
