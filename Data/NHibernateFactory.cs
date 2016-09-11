using Data.Mappers;
using Domain.HealthChecker;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HeartBeat.Core;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class NHibernateFactory
    {
        private static ISessionFactory _sessionFactory;
        public static ISession ObterSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                MsSqlConfiguration.MsSql2012.ConnectionString(
                    @"Data Source=.\sqlexpress;Integrated Security=True;Initial Catalog=TCC;timeout=5;MultipleActiveResultSets=True")

                //.ShowSql())//shows SQL scripts on prompt
                ).Mappings(c => c.FluentMappings.AddFromAssemblyOf<ContatoMap>())
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<HealthCheckerInfoCaptureMap>())
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<HeartBeatInfoMap>())
                     .Mappings(c => c.FluentMappings.AddFromAssemblyOf<LaboratorioMap>())
                //.ExposeConfiguration(c => new SchemaExport(c).Create(false, true))
                .BuildSessionFactory().OpenSession();
        }

        public static ISession ObterAzureSession()
        {
            return GetSessionFactory().OpenSession();
        }

        public static ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null || _sessionFactory.IsClosed)
            {
                _sessionFactory = Fluently.Configure()
                .Database(
                MsSqlConfiguration.MsSql2012.ConnectionString(
                    @"Data Source=xxx.database.windows.net;Initial Catalog=xxx;Integrated Security=False;timeout=5;MultipleActiveResultSets=True;User ID=xxx;Password=xxx")
                .ShowSql())
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<ContatoMap>())
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<HealthCheckerInfoCaptureMap>())
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<HeartBeatInfoMap>())
                     .Mappings(c => c.FluentMappings.AddFromAssemblyOf<LaboratorioMap>()).BuildSessionFactory();
            }
            return _sessionFactory;
        }
    }
}
