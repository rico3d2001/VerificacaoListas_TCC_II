using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_PROJETO
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual string NUMERO { get; set; }

        public LV_PROJETO(){}
    }
}
