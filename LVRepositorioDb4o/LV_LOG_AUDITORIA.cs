using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_LOG_AUDITORIA
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }


        public virtual DateTime DATA { get; set; }
        public virtual string DESCRICAO { get; set; }
        public virtual string GUID_USUARIO { get; set; }
    }
}
