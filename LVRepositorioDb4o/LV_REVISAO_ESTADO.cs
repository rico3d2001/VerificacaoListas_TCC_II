using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_REVISAO_ESTADO
    {
        [IDAtributo]
        public virtual int ID_ESTADO { get; set; }

        public virtual string NOME { get; set; }
        public virtual string DESCRICAO { get; set; }
    }
}
