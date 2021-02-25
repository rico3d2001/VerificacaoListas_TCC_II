using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class GrupoVM
    {

        public string GUID { get; set; }
        public string NOME { get; set; }
        public int ORDENADOR { get; set; }

        public List<ItemVM> Itens { get; set; }

    }
}
