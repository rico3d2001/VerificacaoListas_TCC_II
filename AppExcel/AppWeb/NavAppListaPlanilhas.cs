using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using Unity;

namespace AppExcel.AppWeb
{
    public class NavAppListaPlanilhas
    {
        public List<Template> GetListaPlanilhas(string guid)//Template> GetListaPlanilhas()
        {





            //var tipoLV = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<TipoLV>>().ReturnByGUID(guid);
            ////new Repository<LV_TIPO>().ReturnByGUID(_guid);

            var lista = new List<Template>();



            //var listaPoco = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().GetByProperty("GUID_TIPO", tipoLV.GUID);

            //var listaPocoOrdenada = listaPoco.OrderBy(x => x.NOME).ToList();
            //    //new Repository<LV_PLANILHA>().GetByProperty("GUID_TIPO", lv.GUID) as List<LV_PLANILHA>;

            //foreach (var item in listaPocoOrdenada)
            //{
            //    lista.Add(new Template(item.GUID));
            //}

            return lista;

        }
    }
}
