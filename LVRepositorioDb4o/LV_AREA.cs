using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    
    public class LV_AREA
    {
        [GUIDAtributo]
        public string GUID { get; set; }

        public string NUMERO { get; set; }
        public string GUID_PROJETO { get; set; }
    }
}
