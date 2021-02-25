using ConsumidorLV_Oracle.Comandos;
using EntidadesRepositoriosLeitura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositorioLeitura.Consultas;

namespace VerificacaoListas.UnitTestAplicacao
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCmdAcrescimoRevisao_Certo()
        {
            //ValoresColunaRevisao valor = 
            //    new ValoresColunaRevisao("d474eaa3-e8ef-4429-9b66-5b3b9246a9cb",
            //    "0","RRP");

            //CmdAcrescimoRevisao.Acrescenta(valor);

            var lista = ConsultaListaVerificacao.ObtemListaCompleta("d474eaa3-e8ef-4429-9b66-5b3b9246a9cb");

            string baseURL = "";

        }

        //da4638a7-7409-407a-bd8d-670ecfd9573b
        [TestMethod]
        public void TestStatusConfirmacoesLV_HouvePrimeira()
        {
            ConsultaListaVerificacao.StatusConfirmacoesLV("da4638a7-7409-407a-bd8d-670ecfd9573b");
        }


        //eb6e5252-f751-4e1e-a59f-278d13c67d2d  //ConsultaProjeto

        [TestMethod]
        public void TestConsultaProjeto()
        {
            ConsultaProjeto.ObtemProjeto("eb6e5252-f751-4e1e-a59f-278d13c67d2d");
        }

        [TestMethod]
        public void TestMudaIndice()
        {
            ValoresMudaIndice value = new ValoresMudaIndice(true, "254c8f60-f9aa-43ff-b0ba-c041264a9b34", "PA");


            var conseguiu = CmdMudaIndice.Atualiza(value);

            Assert.IsTrue(conseguiu);
        }

        //8767664b-629c-4076-be32-40cdbff3d2fe

        [TestMethod]
        public void TestRetomar()
        {
            //ValoresConfirma value = new ValoresConfirma("67252fe0-3f6c-40cc-a8bc-62ab8d010713", false,"","","PA","NomeUser");


            //var conseguiu = CmdRetomarRevisao.Atualiza(value);

            //Assert.IsTrue(conseguiu);
        }


    }
}
