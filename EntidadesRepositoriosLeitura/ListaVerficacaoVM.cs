using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class ListaVerficacaoVM
    {

        public ListaVerficacaoVM()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }



        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string GUID { get; set; }

        public string NUMERODOC { get; set; }

        public int VERFICADOR_UNICO { get; set; }

        public CabecalhoVM CabecalhoApp { get; set; }

        public List<ColunaLVVM> Colunas { get; set; }

        public List<ConfirmacaoVM> Confirmacoes { get; set; }


    }
}
