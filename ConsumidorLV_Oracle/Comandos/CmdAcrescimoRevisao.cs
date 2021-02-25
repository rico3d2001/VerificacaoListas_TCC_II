using AppListaVerificacao.Interface;
using EntidadesRepositoriosLeitura;
using LV_DI;
using LVModel;
using System;
using Unity;

namespace ConsumidorLV_Oracle.Comandos
{
    public class CmdAcrescimoRevisao
    {
        public static bool Acrescenta(ValoresColunasRev valoresCriaColuna)
        {

            try
            {
                using (var contextoDocumentoRevisoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                {
                    contextoDocumentoRevisoes.Start();

                    var lv = contextoDocumentoRevisoes.ReturnByGUID(valoresCriaColuna.Guid_LV);


                    if (lv.PodeAcrescentarRevisao(valoresCriaColuna.IndiceRevisao))
                    {



                        var incluido = lv.AddRevisao(valoresCriaColuna.IndiceRevisao, valoresCriaColuna.GuidUsuario);



                        if (incluido)
                        {
                            contextoDocumentoRevisoes.Update(lv);

                            contextoDocumentoRevisoes.Commit();


                        }




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
