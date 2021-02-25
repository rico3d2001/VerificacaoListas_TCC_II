using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    [DataContract]
    public class LinhaRevisaoVM
    {
        

        [DataMember]
        public string GUID_ITEM { get; set; }

        [DataMember]
        public string GUID_REVISAO { get; set; }

        [DataMember]
        public int ID_ESTADO { get; set; }

        [DataMember]
        public int CONFIRMADO { get; set; }

        [DataMember]
        public int EMITIDO { get; set; }

        [DataMember]
        public int ORDENADOR { get; set; }

        [DataMember]
        public string DESCRICAO { get; set; }

        
    }
}
