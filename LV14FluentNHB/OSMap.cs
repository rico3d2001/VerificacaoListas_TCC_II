using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LV14FluentNHB
{
    public class OSMap : ClassMap<OS>
    {
        public OSMap()
        {
            Table("LV_OS");
            Id(x => x.GUID);
            Map(x => x.NUMERO);
            //Map(x => x.GUID_PROJETO);
            References(x => x.Projeto, "GUID_PROJETO")
                .Fetch.Join()
                .Cascade.None();
        }
    }
}
