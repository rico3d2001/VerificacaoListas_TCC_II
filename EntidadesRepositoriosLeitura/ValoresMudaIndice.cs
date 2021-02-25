using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntidadesRepositoriosLeitura
{
    public class ValoresMudaIndice
    {
        public ValoresMudaIndice(bool aindaNaoInseriuDesteIndice, string gUID_LV, string indiceNovo)
        {
            Classe = "ValoresMudaIndice";
            AindaNaoInseriuDesteIndice = aindaNaoInseriuDesteIndice;
            GUID_LV = gUID_LV;
            IndiceNovo = indiceNovo;
        }

        public string Classe { get; private set; }
        public bool AindaNaoInseriuDesteIndice { get; set; }

        public string GUID_LV { get; set; }

        public string IndiceNovo { get; set; }

    }
}