using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntidadesRepositoriosLeitura
{
    public class ProjetoVM
    {
        public string GUID { get; set; }
        public string NUMERO { get; set; }

        public List<AreaVM> Areas { get; set; }

        public List<OSVM> OSs { get; set; }

        public List<DisciplinaVM> Disciplinas { get; set; }

        public List<TipoLVVM> Tipos { get; set; }


    }
}