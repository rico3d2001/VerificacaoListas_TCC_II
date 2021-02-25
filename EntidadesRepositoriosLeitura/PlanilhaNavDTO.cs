using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class PlanilhaNavDTO
    {
        //
        private string _guid;


        public PlanilhaNavDTO()
        {

        }

        public PlanilhaNavDTO(string guid)
        {
            _guid = guid;
        }



        public virtual string NOME { get; set; }
        public virtual string GUID_TIPO { get; set; }
        public virtual string REV { get; set; }
        public virtual string GUID { get => _guid; set => _guid = value; }
    }
}
