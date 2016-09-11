using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NHibernate;
using Data;
using Ninject.Syntax;
using Domain.Core;
using HeartBeat.Core.Repositories;
using Data.Repositories;
using HeartBeat.Core;
using HeartBeat.Component;
using Domain.HealthChecker;
using Hemovida.HealthChecker;
using WorkerProject;
using Domain.Agenda;

namespace DIContainer
{
    public class DiModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            try
            {
                this.Bind<ISession>().ToMethod<ISession>(a => NHibernateFactory.ObterSessionFactory()).InSingletonScope();
                this.Bind<IContatoRepositorio>().To<ContatoRepositorio>();
                this.Bind<IHeartBeatInfoRepository>().To<HeartBeatInfoRepository>();
                this.Bind<IHeartBeat>().To<HeartBeatComponent>();
                this.Bind<IHealthChecker>().To<HealthCheckerComponent>();
                this.Bind<Worker>().To<WorkerAgendaComponent>();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }

    public class Kernel : Ninject.StandardKernel
    {
        public Kernel()
            : base(new DiModule())
        {

        }
    }
}

