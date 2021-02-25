using EntidadesRepositoriosLeitura;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;

namespace WebApiLV.NosSQL
{
    public class RCmdAcrescimoRevisaoRV
    {

        public static bool Acrescenta(ValoresColunasRev valoresCriaColuna)
        {
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


                        session.Store(valoresCriaColuna);

                        session.SaveChanges();

                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Source;
                return false;
            }

        }
    }
}