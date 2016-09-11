using Domain.Core;
using Domain.HealthChecker;
using FluentNHibernate.Mapping;
using HeartBeat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappers
{
    public class HealthCheckerInfoCaptureMap : ClassMap<HealthCheckerInfoCapture>
    {
        public HealthCheckerInfoCaptureMap()
        {
            Id(x => x.Id)
                .GeneratedBy.Identity()
                .UnsavedValue(0)
                .Access.CamelCaseField(Prefix.Underscore);
            
            Map(x => x.PercentageTime);
            Map(x => x.CPUUsage);
            Map(x => x.MemoryUsage);
            Map(x => x.Time);
            Map(x => x.Type);
            Map(x => x.WorkerId);
            Map(x => x.CreatedAt);
        }
    }
}
