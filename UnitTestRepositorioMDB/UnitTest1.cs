using System;
using EntidadesRepositoriosLeitura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositorioMongoDB;

namespace UnitTestRepositorioMDB
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMDB_Buscar()
        {
            ConfiguracoesListaMDB configuracoesListaMDB = new ConfiguracoesListaMDB();

          var lista =  configuracoesListaMDB.Buscar();
        }

        [TestMethod]
        public void TestMDB_Inserir()
        {
            ConfiguracoesListaMDB configuracoesListaMDB = new ConfiguracoesListaMDB();

            ConfiguracaoNavDTO cfg = new ConfiguracaoNavDTO(
                Guid.NewGuid().ToString(),"Engenharia Mecanica","45");

            configuracoesListaMDB.Inserir(cfg);
        }


    }
}
