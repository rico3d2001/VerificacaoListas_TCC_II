using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_USUARIO
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual string NOME { get; set; }
        public virtual int ISCONFIGURADOR { get; set; }
        public virtual int ISVERIFICADOR { get; set; }
        public virtual int ISGESTOR { get; set; }
        public virtual string SIGLA { get; set; }
        public virtual string SENHA { get; set; }
    }
}
