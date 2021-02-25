using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLeituraExcel
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string teste = "";

            Assert.IsTrue(string.IsNullOrEmpty(teste));
        }
    }
}
