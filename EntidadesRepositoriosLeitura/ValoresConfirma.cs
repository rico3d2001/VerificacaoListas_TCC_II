using System;
using System.Runtime.Serialization;

namespace EntidadesRepositoriosLeitura
{
    [DataContract]
    public class ValoresConfirma
    {
        //public ValoresConfirma(string gUID_LV, string gUID_USUARIO1, string gUID_USUARIO2, 
        //    string gUID_CONFIRMACAO, string indice, DateTime data1,DateTime data2, string nomeUsuario1, string nomeUsuario2) //bool isConfiguarcaoDupla, string gUID_USUARIO, string gUID_CONFIRMACAO, string indice, string nomeUsuario)

        public ValoresConfirma(string gUID_LV, string emissor)
        {
            Classe = "ValoresConfirma";
            GUID_LV = gUID_LV;
            EMISSOR = emissor;

            //IsConfiguarcaoDupla = isConfiguarcaoDupla;
            //GUID_USUARIO1 = gUID_USUARIO1;
            //GUID_USUARIO2 = gUID_USUARIO2;
            //GUID_CONFIRMACAO = gUID_CONFIRMACAO;
            //INDICE = indice;
            //DATA1 = data1;
            //DATA1 = data2;
            //NOME_USUARIO1 = nomeUsuario1;
            //NOME_USUARIO2 = nomeUsuario2;
        }



        [DataMember]
        public string Classe { get; private set; }

        [DataMember]
        public string GUID_LV { get; set; }

        [DataMember]
        public string EMISSOR { get; set; }

        //[DataMember]
        //public bool IsConfiguarcaoDupla { get; set; }

        //[DataMember]
        //public string GUID_USUARIO1 { get; set; }

        //[DataMember]
        //public string GUID_USUARIO2 { get; set; }

        //[DataMember]
        //public string GUID_CONFIRMACAO { get; set; }

        //[DataMember]
        //public string INDICE { get; set; }

        //[DataMember]
        //public DateTime DATA1 { get; set; }

        //[DataMember]
        //public DateTime DATA2 { get; set; }

        //[DataMember]
        //public string NOME_USUARIO1 { get; set; }

        //[DataMember]
        //public string NOME_USUARIO2 { get; set; }

        //[DataMember]
        //public ColunaLVVM Coluna { get; set; }


    }
}