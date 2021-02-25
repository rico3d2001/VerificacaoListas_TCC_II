using LVModel;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using RepositorioNoSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioNoSQL
{
    public class ConfiguracoesLista
    {
        ////public static List<LvConfiguracao> Buscar()
        ////{
        ////    List<LvConfiguracao> lista = new List<LvConfiguracao>();

        ////    using (IDocumentSession session = ConexaoRDB.Instancia().DocumentStore.OpenSession())
        ////    {
        ////        lista = session.Query<LvConfiguracao>().ToList();
        ////    }

        ////    return lista;

        ////}

        public static List<LvConfiguracao> Buscar()
        {

            List<LvConfiguracao> lista = new List<LvConfiguracao>();

            try
            {

                using (IDocumentStore store = new DocumentStore
                {
                    Urls = new[] { "http://localhost:8082" },
                    Database = "lv_leitura",
                    Conventions = { }
                })
                {
                    store.Initialize();

                    using (IDocumentSession session = store.OpenSession())
                    {

                        lista = session.Query<LvConfiguracao>().ToList();


                    }
                }


                
            }
            catch (Exception ex)
            {
                string msg = ex.Source;
               
            }

            return lista;

        }

        public static List<LvConfiguracao> Salvar()
        {

            List<LvConfiguracao> lista = new List<LvConfiguracao>();

            try
            {

                using (IDocumentStore store = new DocumentStore
                {
                    Urls = new[] { "http://localhost:8082" },
                    Database = "lv_leitura",
                    Conventions = { }
                })
                {
                    store.Initialize();

                    using (IDocumentSession session = store.OpenSession())
                    {
                        ConfigDTO configDTO = new ConfigDTO()
                        {
                            GUID = Guid.NewGuid().ToString(),
                            NOME = "Eletrica"
                        };

                        session.Store(configDTO);

                        session.SaveChanges();

                        //lista = session.Query<LvConfiguracao>().ToList();


                    }
                }



            }
            catch (Exception ex)
            {
                string msg = ex.Source;

            }

            return lista;

        }



    }
}
