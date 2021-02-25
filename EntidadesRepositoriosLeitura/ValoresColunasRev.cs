using System.Runtime.Serialization;

namespace EntidadesRepositoriosLeitura
{
    [DataContract]
    public class ValoresColunasRev
    {


        public ValoresColunasRev(string guid_lv, string indiceRevisao, string guidUsuario)
        {
            Classe = "ValoresColunasRev";
            Guid_LV = guid_lv;
            IndiceRevisao = indiceRevisao;
            GuidUsuario = guidUsuario;
        }

        [DataMember]
        public string Classe { get; private set; }

        [DataMember]
        public string Guid_LV { get; private set; }
        [DataMember]
        public string IndiceRevisao { get; private set; }
        [DataMember]
        public string GuidUsuario { get; private set; }

    }
}
