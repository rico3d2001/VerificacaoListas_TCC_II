using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    [DataContract]
    public class ColunaLVVM
    {
        [DataMember]
        public string INDICE_REV { get; set; }

        [DataMember]
        public int ORDENADOR { get; set; }

        [DataMember]
        public List<LV_GrupoVM> LV_Grupos { get; set; }
    }
}
