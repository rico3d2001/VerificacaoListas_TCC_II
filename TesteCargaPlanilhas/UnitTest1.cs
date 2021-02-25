using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AppExcel;
using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositorioLeitura;
using Unity;
using System.Data;
using System.Data.SqlClient;
using RepositorioLeitura.Consultas;

namespace TesteCargaPlanilhas
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void CorrigePlanilhaNumeros()
        {
            var listaDocs = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>().Query();
            var listaNumeros = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().Query();
            var listaConfirmacoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>().Query();

            foreach (var doc in listaDocs)
            {
                var numero = listaNumeros.FirstOrDefault(x => x.GUID.Equals(doc.GUID));

                var ultimaConfirmacao = listaConfirmacoes.Where(x => x.GUID_DOCUMENTO == doc.GUID).OrderBy(x => x.ORDENADOR).LastOrDefault();

                if (numero == null)
                {
                    NumeroDocSNCLavalin novoNumero = new NumeroDocSNCLavalin(doc.DOC_VERIFICADO);

                    novoNumero.GUID = doc.GUID;

                    if(ultimaConfirmacao != null)
                    {
                        novoNumero.GUID_ULTIMA_CONFIRMACAO = ultimaConfirmacao.GUID;
                    }
                    
  
                    DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().Insert(novoNumero);

                }
                else
                {
                    if (ultimaConfirmacao != null && numero.GUID_ULTIMA_CONFIRMACAO != ultimaConfirmacao.GUID)
                    {
                        numero.GUID_ULTIMA_CONFIRMACAO = ultimaConfirmacao.GUID;
                        DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().Update(numero);
                    }
                }
            }

            listaNumeros = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().Query();

            Assert.IsTrue(listaNumeros.Count == listaDocs.Count);

            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().Insert(numeroDocSNCLavalin);
        }

        [TestMethod]
        public void CorrigeNullConfirmacao()
        {
            var listaConfirmacoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>().Query();

            foreach (var confirmacao in listaConfirmacoes)
            {
                if(confirmacao.GUID_USUARIO2 ==  null)
                {
                    confirmacao.GUID_USUARIO2 = confirmacao.GUID_USUARIO1;
                    DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>().Update(confirmacao);

                }
            }


        }


        [TestMethod]
        public void InsereDisciplinas()
        {
            List<Disciplina> lista = new List<Disciplina>();


            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 1, NOME = "Comunicação", SIGLA = "4C" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 2, NOME = "Geotecnia", SIGLA = "4G" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 3, NOME = "Sistemas de Utilidades", SIGLA = "4H" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 4, NOME = "Processo Hidráulico", SIGLA = "4X" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 5, NOME = "Coordenação de Projeto", SIGLA = "30" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 6, NOME = "Planejamento", SIGLA = "32" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 7, NOME = "Qualidade", SIGLA = "38" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 8, NOME = "Infraestrutura", SIGLA = "41" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 9, NOME = "Concreto", SIGLA = "42" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 10, NOME = "Estrutura Metálica", SIGLA = "43" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 11, NOME = "Arquitetura", SIGLA = "44" });




            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 12, NOME = "Mecânica", SIGLA = "45" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 13, NOME = "Tubulação", SIGLA = "46" });


            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 14, NOME = "Elétrica", SIGLA = "47" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 15, NOME = "Instrumentação e Automação", SIGLA = "48" });

            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 16, NOME = "Processo", SIGLA = "49" });


            lista.Add(new Disciplina()
            { ID_DISCIPLINA = 17, NOME = "Construção Site", SIGLA = "61" });





            using (var contextoDisciplna = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>())
            {
                contextoDisciplna.Start();

                foreach (var item in lista)
                {
                    contextoDisciplna.Insert(item);
                }

                contextoDisciplna.Commit();
            }

            
        }

        [TestMethod]
        public void CargaPlanilhaEspecifica()
        {

            FileInfo fileInfo = new FileInfo(@"C:\temp\LV - Concreto.xls");

            Disciplina disciplina = null;
            using (var contextoDisciplna = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>())
            {
                contextoDisciplna.Start();

                disciplina = contextoDisciplna.GetByProperty("NOME","Concreto").First();
            }

            LeitorArquivos.LerUnico(fileInfo, disciplina);


        }



        [TestMethod]
        public void CargaGeral()
        {
            LeitorDiretorios.Ler(@"C:\temp\Foi");
        }

        [TestMethod]
        public void InserirPlanilha_Grupo_Item()
        {
            var disciplina = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>()
               .GetByProperty("NOME", "Engenharia Digital")
               .ToList()
               .First();

            var guid_configuracao = Guid.NewGuid().ToString();

            var configuracao = new Configuracao()
            {
                GUID = guid_configuracao,
                NOME = "nome_configuaracao",
                Disciplina = disciplina
            };

            DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Configuracao>>().Insert(configuracao);

            var c = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Configuracao>>().ReturnByGUID(guid_configuracao);

            var guid_arquivo = Guid.NewGuid().ToString();

            var livro = new ArquivoListas()
            {
                GUID = guid_arquivo,
                NOME = "nomea_rquivo",
                SIGLA = "si",
                Configuracao = c
            };

            DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ArquivoListas>>().Insert(livro);

            var l = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ArquivoListas>>().ReturnByGUID(guid_arquivo);

            string guid_planilha = Guid.NewGuid().ToString();

            var planilha = new Planilha()
            {
                GUID = guid_planilha,
                NOME = "nomeplan",
                //GUID_TIPO = "guidtipo",
                Tipo = l,
                FUNCAO = "funcao",
                DESCRICAO = "descricao",
                VERIFICADOR_UNICO = 0
            };

            DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().Insert(planilha);

            var p = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().ReturnByGUID(guid_planilha);

            Assert.IsTrue(p.NOME == "nomeplan");

            string guid_grupo = Guid.NewGuid().ToString();

            var grupo = new Grupo()
            {
                GUID = guid_grupo,
                ORDENADOR = 1,
                //GUID_PLANILHA = "xxxxxx",
                Planilha = p,
                NOME = "nomeGrupo"
            };

            DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Grupo>>().Insert(grupo);

            var g = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Grupo>>().ReturnByGUID(guid_grupo);

            string guid_item = Guid.NewGuid().ToString();

            var itemRev = new ItemRevisao()
            {
                GUID = guid_item,
                ORDENADOR = 1,
                //GUID_GRUPO = "xxxxxx",
                Grupo = g,
                DESCRICAO = "teste"
            };

            DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ItemRevisao>>().Insert(itemRev);

            var plan = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().ReturnByGUID(guid_planilha);

            Assert.IsTrue(plan.ListaGrupos[0].NOME == "nomeGrupo");
            Assert.IsTrue(plan.ListaGrupos[0].ListaItens[0].DESCRICAO == "teste");
        }

        [TestMethod]
        public void Buscar()
        {
            string b = "2089dcb2-8611-44eb-a557-c606bcccf13a";

            var gru = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Grupo>>().ReturnByGUID(b);

            var plan = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().ReturnByGUID(gru.Planilha.GUID);

            var tipo = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ArquivoListas>>().ReturnByGUID(plan.Tipo.GUID);

            Assert.IsTrue(gru.NOME == "xx");
        }

        [TestMethod]
        public void Conexao_Dapper()
        {
           var lista = ConsultaListaProjetos.ObtemListaProjetos();

        }


    }
}
