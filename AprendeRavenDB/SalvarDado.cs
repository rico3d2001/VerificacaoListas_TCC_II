using Raven.Client.Documents.Session;
using System;

namespace AprendeRavenDB
{
    public class SalvarDado
    {
        public static void Salvar()
        {
            using (IDocumentSession session = ConexaoRavenDB.Instancia().DocumentStore.OpenSession())
            {
                CFG configuracao = new CFG()
                {
                    GUID = Guid.NewGuid().ToString(),
                    NOME = "TesteCFG",
                    SIGLA_DISCIPLINA = "TST"
                };

                session.Store(configuracao);

                session.SaveChanges();
            }
        }

        public static void Buscar()
        {
            using (IDocumentSession session = ConexaoRavenDB.Instancia().DocumentStore.OpenSession())
            {
                var configuracoes = session.Query<CFG>();
            }
        }

        public static void Sair()
        {
            ConexaoRavenDB.Instancia().Dispose();
        }

    }
}
