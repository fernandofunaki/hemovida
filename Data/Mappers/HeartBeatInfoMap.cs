using Domain.Core;
using FluentNHibernate.Mapping;
using HeartBeat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappers
{
    public class HeartBeatInfoMap : ClassMap<HeartBeatInfo>
    {
        public HeartBeatInfoMap()
        {
            Id(x => x.Id)
                .GeneratedBy.Identity()
                .UnsavedValue(0)
                .Access.CamelCaseField(Prefix.Underscore);

            Map(x => x.Beat);
            Map(x => x.CreatedAt);
            Map(x => x.WorkerId);
        }
    }
}
