using EntidadesRepositoriosLeitura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntidadesRepositoriosLeitura
{
    public class PlanilhaLVVM
    {
        public string GUID { get; set; }



        public CabecalhoVM CabecalhoApp { get; set; }

        public List<GrupoVM> Grupos { get; set; }

    }
}