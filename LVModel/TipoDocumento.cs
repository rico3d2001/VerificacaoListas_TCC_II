using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVModel
{
    public class TipoDocumento
    {

        string _guid;
        string _codigo;
        string _id_displina;

        public TipoDocumento(string codigo, string guidTipo)
        {
            _guid = guidTipo;
            _codigo = codigo;
            _id_displina = "GAMBIARRA";
            
        }

        public TipoDocumento() { }

        public virtual string CODIGO { get => _codigo; set => _codigo = value; }
        public virtual string GUID { get => _guid; set => _guid = value; } 
        public virtual string ID_DISCIPLINA { get => _id_displina; set => _id_displina = value; }






    }
}
