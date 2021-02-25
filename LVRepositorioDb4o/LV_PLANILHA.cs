using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_PLANILHA
    {
        [GUIDAtributo]
        public virtual string GUID { get; set; }

        public virtual string NOME { get; set; }
        public virtual string GUID_TIPO { get; set; }
        public virtual string FUNCAO { get; set; }
        public virtual string DESCRICAO { get; set; }
        public virtual int VERIFICADOR_UNICO { get; set; }

        public LV_PLANILHA() { }
    }
}
