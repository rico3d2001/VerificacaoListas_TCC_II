using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntidadesRepositoriosLeitura
{
    public class ValoresComandoCriaLV
    {
        private string _numeroSNC;
        private string _guidPlanilha;
        private string _novoGuidLV;

        public ValoresComandoCriaLV()
        {
            Classe = "ValoresComandoCriaLV";
        }


        public string Classe { get; private set; }
        public string NumeroSNC { get => _numeroSNC; set => _numeroSNC = value; }
        public string GuidPlanilha { get => _guidPlanilha; set => _guidPlanilha = value; }
        public string NovoGuidLV { get => _novoGuidLV; set => _novoGuidLV = value; }
    }
}