using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_REVISAO
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual string GUID_LV_ITEM { get; set; }
        public virtual string GUID_LV_VERIFICADOR { get; set; }
        public virtual DateTime DATA_VERICACAO { get; set; }
        public virtual int ID_ESTADO { get; set; }
        public virtual string GUID_DOC_VERIFICACAO { get; set; }
        public virtual string INDICE { get; set; }
        public virtual int ORDENADOR { get; set; }
        public virtual int CONFIRMADO { get; set; }
        public virtual int SALVO { get; set; }
        public virtual int EMITIDO { get; set; }
        public virtual string GUID_CONFIRMADO { get; set; }
    }
}
