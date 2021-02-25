using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class DisciplinaMap : ClassMap<Disciplina>
    {
        public DisciplinaMap()
        {
            Table("LV_DISCIPLINA");
            Id(x => x.ID_DISCIPLINA);
            Map(x => x.NOME);
            Map(x => x.SIGLA);

            
        }
    }
}
