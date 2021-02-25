using AppListaVerificacao.Interface;
using EntidadesRepositoriosLeitura;
using LV_DI;
using LVModel;
using System;
using System.Linq;
using Unity;

namespace ConsumidorLV_Oracle.Comandos
{
    public class CmdRetomarRevisao
    {
        public static bool Atualiza(ValoresConfirma valores)
        {
            try
            {


                //using (var contextoLV = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                //{
                //    contextoLV.Start();
                   
                //    var listaVerificacao = contextoLV.ReturnByGUID(valores.GUID_LV);
                //    var listaRevisoesIndiceAtual = listaVerificacao.ListaRevisoes.Distinct().Where(x => x.INDICE == valores.INDICE).ToList();

                //    foreach (var rev in listaRevisoesIndiceAtual)
                //    {
                //        rev.GUID_CONFIRMADO = "";
                //        rev.CONFIRMADO = 0;
                       
                //    }

              

                //    if(listaVerificacao.ListaConfirmacoes.Count > 0)
                //    {
                //        var confirmacaoApagar = listaVerificacao.ListaConfirmacoes.Distinct().FirstOrDefault(x => x.INDICE_REV == valores.INDICE);
                            
                           
                //        if(confirmacaoApagar != null)
                //        {
                //            listaVerificacao.ListaConfirmacoes.Remove(confirmacaoApagar);
                //        }
                        
                //    }

                 
                //    contextoLV.Update(listaVerificacao);
                //    contextoLV.Commit();



                //}



                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}