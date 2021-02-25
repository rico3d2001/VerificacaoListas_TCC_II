using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_ITEM_REVISAO
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual string DESCRICAO { get; set; }
        public virtual string GUID_GRUPO { get; set; }
        public virtual int ORDENADOR { get; set; }

    }
}
