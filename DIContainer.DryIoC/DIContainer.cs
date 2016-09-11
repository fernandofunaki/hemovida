using Data;
using Domain.Agenda;
using Domain.Core;
using Domain.HealthChecker;
using DryIoc;
using HeartBeat.Core;
using Hemovida.HealthChecker;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerProject;
using HeartBeat.Component;
using HeartBeat.Core.Repositories;
using Data.Repositories;

namespace DIContainer.DryIoC
{
    public class DIContainer
    {
        public static Container Initialize()
        {
            var container = new Container(rules => rules.With(FactoryMethod.ConstructorWithResolvableArguments));
            
            //Singleton Nhibernate Session
            container.Register<ISession>(made: Made.Of(() => NHibernateFactory.ObterSessionFactory()), reuse: Reuse.Singleton);

            //Will be used the same instance per Resolution Root
            container.Register<IContatoRepositorio, ContatoRepositorio>(reuse: Reuse.InResolutionScope);
            container.Register<IHeartBeatInfoRepository, HeartBeatInfoRepository>(reuse: Reuse.InResolutionScope);
            container.Register<IHealthCheckerInfoCaptureRepository, HealthCheckerInfoCaptureRepository>(reuse: Reuse.InResolutionScope);

            //Components
            container.Register<IHeartBeat, HeartBeat.Component.HeartBeatComponent>(setup: Setup.With(asResolutionCall: true));
            container.Register<IHealthChecker, HealthCheckerComponent>(reuse: Reuse.InResolutionScope);
            container.Register<Worker, WorkerAgendaComponent>(reuse: Reuse.InResolutionScope);

            return container;
        }
    }
}
