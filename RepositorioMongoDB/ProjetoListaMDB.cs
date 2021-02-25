using EntidadesRepositoriosLeitura;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositorioMongoDB
{
    public class ProjetoListaMDB
    {
        public List<ProjetoToListDTO> Buscar()
        {
            List<ProjetoToListDTO> lista = new List<ProjetoToListDTO>();

            try
            {
                IMongoClient client = new MongoClient("mongodb://localhost");
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ProjetoToListDTO> colNews = database.GetCollection<ProjetoToListDTO>("ProjetoToListDTO");
                lista = colNews.AsQueryable().ToList();
            }
            catch (Exception)
            {

                throw;
            }


            return lista;
        }
    }
}
