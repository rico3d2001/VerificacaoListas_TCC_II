using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    [DataContract]
    public class LV_GrupoVM
    {
        [DataMember]
        public string GUID { get; set; }

        [DataMember]
        public string NOME { get; set; }

        [DataMember]
        public int ORDENADOR { get; set; }

        [DataMember]
        public List<LinhaRevisaoVM> Linhas { get; set; }

    }
}
