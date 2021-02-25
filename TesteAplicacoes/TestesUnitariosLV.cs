using System;
using System.Linq;
using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace TesteAplicacoes
{
    [TestClass]
    public class TestesUnitariosLV
    {
        [TestMethod]
        public void TestPegaDOC()
        {

           

            

            //PRÉ-REQUSITO
            //Foi executado pegadoc com 9988-999-9999-46XX-00001 
            //Foi executado pegadoc com 9988-888-8888-46XX-00001 
            //CRIANDO O DOCUMENTO, ÁREA E OS  NÃO EXISTENTES

            var listadocumentos = DIContainer.Instance.AppContainer.Resolve<DocumentoAppServiceBase>()
                             .GetByProperty("DOC_VERIFICADO", "9988-888-8888-46XX-00001");

            var listaProjetos = DIContainer.Instance.AppContainer.Resolve<AppServiceBaseGUID<LV_PROJETO>>()
                    .GetByProperty("NUMERO", "9988");

            var projeto = listaProjetos.First();

            var listaOSsProjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBaseGUID<LV_OS>>()
                    .GetByProperty("GUID_PROJETO", projeto.GUID);

            var listaAreasProjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBaseGUID<LV_AREA>>()
                    .GetByProperty("GUID_PROJETO", projeto.GUID);

            //Documento é unico
            Assert.IsTrue(listadocumentos.Count > 0 && listadocumentos.Count < 2);

            //Foram inseridos OS e Área
            Assert.IsTrue(listaOSsProjeto.Count == 2);
            Assert.IsTrue(listaAreasProjeto.Count == 2);
            Assert.IsTrue(listaOSsProjeto.First().NUMERO == "888");
            Assert.IsTrue(listaAreasProjeto.First().NUMERO == "8888");
            Assert.IsTrue(listaOSsProjeto.Last().NUMERO == "999");
            Assert.IsTrue(listaAreasProjeto.Last().NUMERO == "9999");



        }

        [TestMethod]
        public void TesteAddRevisao()
        {



        

            //PRÉ-REQUSITO
            //Foi executado addrevisao com 9988-888-8888-46XX-00001 

            var listadocumentos = DIContainer.Instance.AppContainer.Resolve<DocumentoAppServiceBase>()
                             .GetByProperty("DOC_VERIFICADO", "9988-888-8888-46XX-00001");

            LV_DOC documento = listadocumentos.First();

            var listaRevisoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBaseGUID<LV_REVISAO>>()
                    .GetByProperty("GUID_DOC_VERIFICACAO", documento.GUID);


            //Documento é unico
            Assert.IsTrue(listaRevisoes.Count > 0 && listaRevisoes.Count <= 9);

            //Amostra revisao
            Random randNum = new Random();
            Assert.IsTrue(listaRevisoes[randNum.Next(0, 8)].INDICE == "0");
            Assert.IsTrue(listaRevisoes[randNum.Next(0,8)].CONFIRMADO == 0);
            Assert.IsTrue(listaRevisoes[randNum.Next(0,8)].ID_ESTADO == 5);
            Assert.IsTrue(listaRevisoes[randNum.Next(0, 8)].GUID_CONFIRMADO == null);

        }

        [TestMethod]
        public void TesteMudaStatusRevisao()
        {



           

            //PRÉ-REQUSITO
            //Foi executado addrevisao com 9988-888-8888-46XX-00001 

            var listadocumentos = DIContainer.Instance.AppContainer.Resolve<DocumentoAppServiceBase>()
                             .GetByProperty("DOC_VERIFICADO", "9988-888-8888-46XX-00001");

            var documentoCompleto = DIContainer.Instance.AppContainer.Resolve<DocumentoAppServiceBase>()
                .ReturnByGUID(listadocumentos.First().GUID);

            var listaRevisoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBaseGUID<LV_REVISAO>>()
                    .GetByProperty("GUID_DOC_VERIFICACAO", documentoCompleto.GUID).OrderBy(x => x.ID_ESTADO).ToList();

            var estados = DIContainer.Instance.AppContainer.Resolve<AppServiceBaseGUID<LV_REVISAO_ESTADO>>().Query();
            var estado_X = estados.First(x => x.NOME == "X");



            //Documento é unico
            Assert.IsTrue(listaRevisoes.Count > 0 && listaRevisoes.Count <= 9);

            //Amostra revisao
            Random randNum = new Random();
            Assert.IsTrue(listaRevisoes[0].ID_ESTADO == estado_X.ID_ESTADO);
            Assert.IsTrue(listaRevisoes[1].ID_ESTADO == estado_X.ID_ESTADO);
            Assert.IsTrue(listaRevisoes[2].ID_ESTADO == 5);
            Assert.IsTrue(listaRevisoes[3].ID_ESTADO == 5);
            Assert.IsTrue(listaRevisoes[4].ID_ESTADO == 5);
            Assert.IsTrue(listaRevisoes[5].ID_ESTADO == 5);
            Assert.IsTrue(listaRevisoes[6].ID_ESTADO == 5);
            Assert.IsTrue(listaRevisoes[7].ID_ESTADO == 5);
            Assert.IsTrue(listaRevisoes[8].ID_ESTADO == 5);




        }


    }
}
