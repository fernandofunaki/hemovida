using Domain.Core;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappers
{
    public class LaboratorioMap : ClassMap<laboratorio>
    {
        public LaboratorioMap()
        {
            Id(x => x.id)
                .GeneratedBy.Identity()
                .UnsavedValue(0)
                .Access.CamelCaseField(Prefix.Underscore);

            Map(x => x.nome_fantasia);
            Map(x => x.razao_social);
            Map(x => x.telefone);
        }
    }
}
