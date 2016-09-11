using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Remotion.Linq;
using System.Collections;
using FluentNHibernate.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Data;
using NHibernate.Tool.hbm2ddl;
using DryIoc;
using Data.Mappers;
using Domain.Core;
using DIContainer;
using HeartBeat.Core.Repositories;
using Ninject;
using HeartBeat.Core;
using WorkerProject;
using Domain.HealthChecker;

namespace HeartBeatAndHealthCheckerTests
{
    class Program
    {
        /*
         Runs this console application to see HeartBeat and HealthChecker working.
         Will be recorded HeartBeat data in each 3 seconds and HealthChecker in each 1 minute.
         Please, close the prompt to stop this tests.
         */
        static void Main(string[] args)
        {
            //Initializes DI Container
            var _container = DIContainer.DryIoC.DIContainer.Initialize();

            //Get Worker.Agenda
            Worker _workerAgenda = _container.Resolve<Worker>();

            //Start Worker tests
            _workerAgenda.Start();

            //We'll see on database HeartBeat and HealthChecker 
        }
    }
}
