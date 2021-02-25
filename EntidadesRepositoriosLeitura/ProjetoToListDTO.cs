using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class ProjetoToListDTO
    {
        public ProjetoToListDTO(string gUID, string nUMERO)
        {
            Id = ObjectId.GenerateNewId().ToString();
            GUID = gUID;
            NUMERO = nUMERO;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; private set; }

        public virtual string GUID { get; set; }
        public virtual string NUMERO { get; set; }
    }
}
