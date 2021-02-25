using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class ConfiguracaoNavDTO
    {
        public ConfiguracaoNavDTO(string gUID, string nOME, string sIGLA_DISCIPLINA)
        {
            Id = ObjectId.GenerateNewId().ToString();
            GUID = gUID;
            NOME = nOME;
            SIGLA_DISCIPLINA = sIGLA_DISCIPLINA;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }



        public string GUID { get; set; }
        public string NOME { get; set; }
        public string SIGLA_DISCIPLINA { get; set; }
    }
}
