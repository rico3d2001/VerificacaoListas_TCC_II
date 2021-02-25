using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_GRUPO
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual string NOME { get; set; }
        public virtual string GUID_PLANILHA { get; set; }
        public virtual int ORDENADOR { get; set; }
    }
}
