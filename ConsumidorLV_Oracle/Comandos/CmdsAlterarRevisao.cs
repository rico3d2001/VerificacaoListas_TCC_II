using AppListaVerificacao.Interface;
using EntidadesRepositoriosLeitura;
using LV_DI;
using LVModel;
using System;
using Unity;

namespace ConsumidorLV_Oracle.Comandos
{
    public class CmdsAlterarRevisao
    {

       public static bool Atualiza(RevisaoVM valoresRevisao)
        {
            try
            {


                using (var contextoRevisao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>())
                {
                    contextoRevisao.Start();

                    var revisao = contextoRevisao.ReturnByGUID(valoresRevisao.GUID);

                    revisao.ID_ESTADO = valoresRevisao.ID_ESTADO; 

                    contextoRevisao.Update(revisao);

                    contextoRevisao.Commit();

                    
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