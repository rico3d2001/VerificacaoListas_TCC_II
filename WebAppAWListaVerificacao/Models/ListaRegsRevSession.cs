using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaRegsRevSession
    {

        List<RegistroRevisao> _listaRegsRevSession;
  

        public ListaRegsRevSession(Planilha pPlanilha)
        {
    
            _listaRegsRevSession = new List<RegistroRevisao>();


            foreach (var grupo in pPlanilha.ListaGrupos.Distinct().OrderBy(x => x.ORDENADOR))
            {

                //var listaItens = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ItemRevisao>>()
                    //.GetByProperty("GUID_GRUPO", grupo.GUID);

                foreach (var item in grupo.ListaItens.Distinct().ToList()) 
                {
                    _listaRegsRevSession.Add(new RegistroRevisao(item.GUID));
                }
            }



        }


        public int Comprimento
        {
            get => _listaRegsRevSession.Count;
        }

        public RegistroRevisao this[int index]    
        {
            get => _listaRegsRevSession[index];
        }


    }
}