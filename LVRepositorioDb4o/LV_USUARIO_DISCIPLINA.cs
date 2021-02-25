using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_USUARIO_DISCIPLINA
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual int ID_DISCIPLINA { get; set; }
        public virtual string GUID_USUARIO { get; set; }
    }
}
