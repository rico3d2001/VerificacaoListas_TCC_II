using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioNoSQL
{
    public class ConexaoRDB
    {
        private DocumentStore _documentStore;
        private static ConexaoRDB _instancia;

        public DocumentStore DocumentStore { get => _documentStore; }

        private ConexaoRDB()
        {
            _documentStore = new DocumentStore()
            {
                Urls = new string[] { "http://localhost:8082" },
                Database = "lv_leitura",
                Conventions = { }
            };
            _documentStore.Initialize();
        }

        public static ConexaoRDB Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new ConexaoRDB();
            }

            return _instancia;
        }


        public void Dispose()
        {
            _documentStore.Dispose();
        }
    }
}
