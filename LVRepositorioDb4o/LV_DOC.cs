using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_DOC
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual string NUMERO { get; set; }
        public virtual string OBJETO { get; set; }
        public virtual string DOC_VERIFICADO { get; set; }
        public virtual string GUID_PROJETO { get; set; }
    }
}
