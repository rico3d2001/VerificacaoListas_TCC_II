using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LV14FluentNHB
{
    public class Log_AuditoriaMap : ClassMap<Log_Auditoria>
    {
        public Log_AuditoriaMap()
        {
            Table("LV_LOG_AUDITORIA");
            Id(x => x.GUID);
            Map(x => x.DATA);
            Map(x => x.DESCRICAO);
            Map(x => x.GUID_USUARIO);
        }
    }
}
