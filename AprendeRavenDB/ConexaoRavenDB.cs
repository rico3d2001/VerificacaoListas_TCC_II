using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprendeRavenDB
{
    public class ConexaoRavenDB:IDisposable
    {
        private DocumentStore _documentStore;
        private static ConexaoRavenDB _instancia;

        public DocumentStore DocumentStore { get => _documentStore;  }

        private ConexaoRavenDB()
        {
            _documentStore = new DocumentStore()
            {
                Urls = new string[] { "http://localhost:8082" },
                Database = "lv_leitura",
                Conventions = { }
            };
            _documentStore.Initialize();
        }

        public static ConexaoRavenDB Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new ConexaoRavenDB();
            }

            return _instancia;
        }


        public void Dispose()
        {
            _documentStore.Dispose();
        }
    }
}
