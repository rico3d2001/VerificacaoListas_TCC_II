using AppUtils;
using EntidadesRepositoriosLeitura;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumidorRavenDB
{
    public class CmdAcrescimoRevisaoRV
    {
        public static bool Acrescenta(string message)
        {
            try
            {
                var valor = MeuJson.ConverteJSonParaObject<ValoresColunasRev>(message);

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


                        session.Store(valor);

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
