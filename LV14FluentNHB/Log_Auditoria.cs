using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LV14FluentNHB
{
    public class Log_Auditoria
    {
        public virtual string GUID { get; set; }
        public virtual DateTime DATA { get; set; }
        public virtual string DESCRICAO { get; set; }
        public virtual string GUID_USUARIO { get; set; }
    }
}
