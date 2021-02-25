using System;
using System.Collections.Generic;
using System.IO;
using LV14FluentNHB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerificacaoListas.LeExcel;

namespace UnitTestInsereLV
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Inserir()
        {
            

            

            FileInfo fileInfo = new FileInfo(@"C:\Trabalho - Ricardo\Inserir\LV - Infraestrutura");

            List<FileInfo> f = new List<FileInfo>
            {
                fileInfo
            };

            LV_DISCIPLINA lv = new LV_DISCIPLINA()
            {
                ID_DISCIPLINA = 12
            };

            

            LeitorArquivos.Ler(f, lv);

        }
    }
}
