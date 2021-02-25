using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_CONFIRMACAO
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual string GUID_USUARIO1 { get; set; }
        public virtual string GUID_USUARIO2 { get; set; }
        public virtual string INDICE_REV { get; set; }
        public virtual DateTime DATA { get; set; }
        public virtual string GUID_DOCUMENTO { get; set; }
        public virtual int ORDENADOR { get; set; }
    }
}
