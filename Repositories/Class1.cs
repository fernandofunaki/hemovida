using Domain.Core;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Repositories
{

   public class HealthCheckerInfoCaptureMap : ClassMap<HealthCheckerInfoCapture>
    {
        public HealthCheckerInfoCaptureMap()
        {
            Id(x => x.Id);
            Map(x => x.MemoryUsage);
            Map(x => x.Time);
            Map(x => x.Type);
            Map(x => x.CPUUsage);
            Map(x => x.AverageTime);
        }
    }


    public class HealthCheckerInfoCaptureRepository
    {
        public static void Add(HealthCheckerInfoCapture product)
        {
            ISessionFactory sf = NHibernateHelper.CreateSessionFactory();
            using (ISession session = sf.GetCurrentSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(product);
                transaction.Commit();
            }
        }
    }

    public sealed class NHibernateHelper
    {
        public static ISessionFactory CreateSessionFactory()
        {
            ISessionFactory isessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString("Server=.; Database=TCC; Integrated Security=True;"))
                .Mappings(m => m
                .FluentMappings.AddFromAssemblyOf<Domain.Core.HealthCheckerInfoCapture>())
                .BuildSessionFactory();

            return isessionFactory;
        }

        //private static ISessionFactory SessionFactory;

        //private static void OpenSession()
        //{
        //    Configuration configuration = new Configuration();
        //    configuration.AddAssembly(Assembly.GetCallingAssembly());
        //    SessionFactory = configuration.BuildSessionFactory();
        //}

        //public static ISession GetCurrentSession()
        //{
        //    if (SessionFactory == null)
        //        NHibernateHelper.OpenSession();

        //    return SessionFactory.OpenSession();
        //}

        //public static void CloseSessionFactory()
        //{
        //    if (SessionFactory != null)
        //        SessionFactory.Close();
        //}
    }
}
