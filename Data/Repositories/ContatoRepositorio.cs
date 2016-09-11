
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

namespace Data
{
    public class ContatoRepositorio : IContatoRepositorio, IDisposable
    {
        private readonly ISession _session;

        public ContatoRepositorio(ISession session)
        {
            _session = session;
        }
        public void Salvar(Contato contato)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(contato);
                tran.Commit();
            }
        }
        public void Excluir(Contato contato)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Delete(contato);
                tran.Commit();
            }
        }

        public void Alterar(Contato contato)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Update(contato);
                tran.Commit();
            }
        }
        public Contato ObterPor(int id)
        {
            return _session.Get<Contato>(id);
        }
   

        public IQueryable<T> ObterTodos<T>()
        {
            return _session.Query<T>();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
