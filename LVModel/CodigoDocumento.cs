using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVModel
{
    public class CodigoDocumento
    {
        public virtual string GUID { get; set; }
        public virtual string CODIGO { get; set; }
        public virtual int ID_DISCIPLINA { get; set; }

        public virtual int ID_GRUPO_COD_DOC { get; set; }

        public CodigoDocumento() { }

    }
}
