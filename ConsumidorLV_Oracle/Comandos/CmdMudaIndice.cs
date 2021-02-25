using AppListaVerificacao.Interface;
using EntidadesRepositoriosLeitura;
using LV_DI;
using LVModel;
using System;
using System.Linq;
using Unity;

namespace ConsumidorLV_Oracle.Comandos
{
    public class CmdMudaIndice
    {
        public static bool Atualiza(ValoresMudaIndice valores)
        {
            try
            {


                using (var contextoListaVerificacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                {
                    contextoListaVerificacao.Start();

                    //listaRevisoes = contextoRevisao.GetByProperty("GUID_DOC_VERIFICACAO", mudado.GuidDocumento).ToList();

                    ListaVerificacao listaVerificacao = contextoListaVerificacao.ReturnByGUID(valores.GUID_LV);


                    var listaRevisoes = listaVerificacao.ListaRevisoes.Distinct().ToList();
                    var listaRevisoesNoConfirm = listaRevisoes.Where(x => x.CONFIRMADO == 0).ToList();




                    if (valores.AindaNaoInseriuDesteIndice) //(mudado, listaRevisoes))
                    {

                        //using (var contextoConfirmacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>())
                        //{
                            //contextoConfirmacao.Start();

                            var listaConfirmacoes = listaVerificacao.ListaConfirmacoes.Distinct().Where(x => x.GUID_DOCUMENTO == valores.GUID_LV).OrderBy(x => x.ORDENADOR).ToList();


                            if (listaConfirmacoes.Count > 0)
                            {
                                var ultimaRevisaoCadastrada = listaRevisoesNoConfirm.Last();

                                if (listaConfirmacoes.Exists(x => x.INDICE_REV == ultimaRevisaoCadastrada.INDICE))
                                {
                                    var conf_a_alterar = listaConfirmacoes.First(x => x.INDICE_REV == ultimaRevisaoCadastrada.INDICE);
                                    conf_a_alterar.INDICE_REV = valores.IndiceNovo;
                                    //contextoConfirmacao.Update(conf_a_alterar);
                                    //contextoConfirmacao.Commit();
                                }



                            }

                        //}

                        //listaVerificacao.MudaIndiceUltimaRevisao(valores.IndiceNovo, listaRevisoesNoConfirm);

                        foreach (var rev in listaRevisoesNoConfirm)
                        {
                            rev.INDICE = valores.IndiceNovo;

                        }

                        contextoListaVerificacao.Update(listaVerificacao);
                        contextoListaVerificacao.Commit();

                        //_pemitido = true;

                    }


                }



                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}