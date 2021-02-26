using EntidadesRepositoriosLeitura;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioMongoDB
{
    public class ConfiguracoesListaMDB
    {

     
        public List<ConfiguracaoNavDTO> Buscar()
        {

            List<ConfiguracaoNavDTO> lista = new List<ConfiguracaoNavDTO>();

            try
            {
                IMongoClient client = new MongoClient("mongodb://localhost");
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ConfiguracaoNavDTO> colNews = database.GetCollection<ConfiguracaoNavDTO>("ConfiguracaoNavDTO");
                lista = colNews.AsQueryable<ConfiguracaoNavDTO>().ToList();
            }
            catch (Exception)
            {

                throw;
            }


            return lista;
        }

        public void Inserir(ConfiguracaoNavDTO configuracaoNavMDB)
        {
            

            try
            {
                IMongoClient client = new MongoClient("mongodb://localhost");
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ConfiguracaoNavDTO> colNews = database.GetCollection<ConfiguracaoNavDTO>("ConfiguracaoNavDTO");
                colNews.InsertOne(configuracaoNavMDB);
            }
            catch (Exception)
            {

                throw;
            }


            
        }





    }
}
