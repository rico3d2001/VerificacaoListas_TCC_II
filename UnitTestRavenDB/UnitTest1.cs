using System;
using AprendeRavenDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestRavenDB
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSalvarRavenDB()
        {
            SalvarDado.Salvar();
            SalvarDado.Buscar();
            SalvarDado.Sair();
        }
    }
}
