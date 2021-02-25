using AppListaVerificacao.Interface;
using EntidadesRepositoriosLeitura;
using LV_DI;
using LVModel;
using System;
using System.Linq;
using Unity;

namespace ConsumidorLV_Oracle.Comandos
{
    public class CmdsListaVerficacao
    {
        public static ListaVerificacao CriaLV(ValoresComandoCriaLV valoresComandoCriaLV)
        {
            ListaVerificacao listaVerificacao = new ListaVerificacao();

           


                NumeroDocSNCLavalin numeroDocSNCLavalin = new NumeroDocSNCLavalin(valoresComandoCriaLV.NumeroSNC);


                //Insere GUID
                numeroDocSNCLavalin.GUID = valoresComandoCriaLV.NovoGuidLV;

                Projeto projeto = null;

                using (var contextoProjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
                {
                    contextoProjeto.Start();

                    var listaProjetos = contextoProjeto.GetByProperty("NUMERO", numeroDocSNCLavalin.PROJETO).ToList();

                    if (listaProjetos.Count > 0 && listaProjetos.Count < 2)
                    {
                        projeto = listaProjetos.FirstOrDefault();

                        if (projeto.ListaOSs.FirstOrDefault(x => x.NUMERO == numeroDocSNCLavalin.OS) == null)
                        {
                            projeto.AddOS(new OS()
                            {
                                GUID = Guid.NewGuid().ToString(),
                                NUMERO = numeroDocSNCLavalin.OS,
                                Projeto = projeto
                            });

                        }

                        if (projeto.ListaAreas.FirstOrDefault(x => x.NUMERO == numeroDocSNCLavalin.AREA) == null)
                        {
                            projeto.AddArea(new Area()
                            {
                                GUID = Guid.NewGuid().ToString(),
                                NUMERO = numeroDocSNCLavalin.AREA,
                                Projeto = projeto
                            });


                        }

                        contextoProjeto.Update(projeto);
                        contextoProjeto.Commit();


                    }
                    else
                    {
                        projeto = new Projeto()
                        {
                            GUID = Guid.NewGuid().ToString(),
                            NUMERO = numeroDocSNCLavalin.PROJETO
                        };

                        projeto.AddOS(new OS()
                        {
                            GUID = Guid.NewGuid().ToString(),
                            NUMERO = numeroDocSNCLavalin.OS,
                            Projeto = projeto
                        });

                        projeto.AddArea(new Area()
                        {
                            GUID = Guid.NewGuid().ToString(),
                            NUMERO = numeroDocSNCLavalin.AREA,
                            Projeto = projeto
                        });


                        contextoProjeto.Insert(projeto);
                        contextoProjeto.Commit();

                    }


                }

                //Prepara planilha
                Planilha planilha = null;

                using (var contextoPlanilha = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>())
                {
                    contextoPlanilha.Start();
                    planilha = contextoPlanilha.ReturnByGUID(valoresComandoCriaLV.GuidPlanilha);

                }


                //Insere Lista
                using (var contextoLV = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                {
                    contextoLV.Start();

                    listaVerificacao = new ListaVerificacao()
                    {
                        GUID = numeroDocSNCLavalin.GUID,
                        NUMERO = "1",
                        DOC_VERIFICADO = numeroDocSNCLavalin.ToString(),
                        Planilha = planilha,
                        Projeto = projeto,
                        OS = projeto.ListaOSs.Last(),
                        Area = projeto.ListaAreas.Last()
                    };

                    contextoLV.Insert(listaVerificacao);
                    contextoLV.Commit();

                }


                //Insere NumeroSNCLavalin
                using (var contextoNumeroDocSNCLavalin = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
                {


                    contextoNumeroDocSNCLavalin.Start();
                    contextoNumeroDocSNCLavalin.Insert(numeroDocSNCLavalin);
                    contextoNumeroDocSNCLavalin.Commit();
                }

                //return true;

            
           

            return listaVerificacao;
        }

    }
}