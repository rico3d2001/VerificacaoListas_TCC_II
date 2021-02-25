using AppListaVerificacao.Interface;
using EntidadesRepositoriosLeitura;
using LV_DI;
using LVModel;
using System;
using Unity;

namespace ConsumidorLV_Oracle.Comandos
{
    public class CmdRevisaoUnitaria
    {
        public static bool MudaEstado(RevisaoUnitaria valores)
        {
            try
            {
                using (var contextoMudaIndiceRev = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>())
                {
                    contextoMudaIndiceRev.Start();

                    var rev = contextoMudaIndiceRev.ReturnByGUID(valores.GUID);

                    rev.ID_ESTADO = valores.ESTADO;

                    contextoMudaIndiceRev.Update(rev);

                    contextoMudaIndiceRev.Commit();

                    return true;
                }

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}