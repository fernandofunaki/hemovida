using Data;
using Domain;
using Domain.Core;
using Domain.Core.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Caching;

namespace WCFServiceWebRole1
{
    public class AgendaService : IAgendaService
    {
        private ISession _session;
        private ILaboratorioRepository _laboratorioRepository;

        public AgendaService()
        {
            _session = NHibernateFactory.ObterAzureSession();
            _laboratorioRepository = new LaboratorioRepository(_session);
        }
        public string GetStatus()
        {
            return string.Format("Status OK: {0}", DateTime.Now);
        }

        public IList<laboratorio> GetLaboratorios(string name)
        {
            return _laboratorioRepository.FindByName(name);
        }

        public laboratorio GetLaboratorio(int id)
        {
            return _laboratorioRepository.Get(id);
        }
    }
}
