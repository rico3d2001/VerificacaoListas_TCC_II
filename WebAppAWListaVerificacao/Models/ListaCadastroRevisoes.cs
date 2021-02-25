using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using LVModel.ObjetosValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaCadastroRevisoes
    {
      

        public List<ListaRegistrosPorColunas> GetListaRevisoes(List<Revisao> listaRevisoes)//Documento documento)
        {

           

            List<ListaRegistrosPorColunas> lista = new List<ListaRegistrosPorColunas>();

             



            if (listaRevisoes.Count < 1)
            {
                return lista;
            }

            var queryIndices =
                from rev in listaRevisoes
                group rev by new { indice = rev.INDICE, ordenador = rev.ORDENADOR };

            var queryIndicesOrdenados = queryIndices.OrderBy(x => x.Key.ordenador);

            foreach (var q in queryIndicesOrdenados)
            {
                lista.Add(new ListaRegistrosPorColunas(q.Key.indice, q.Key.ordenador));
            }


            //using (var contextoEstadoRevisao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<EstadoRevisao>>())
            //{
                //contextoEstadoRevisao.Start();

                foreach (var coluna in lista)
                {
                    var registros = listaRevisoes.Where(x => x.INDICE == coluna.IndiceRevisao);
                    foreach (var reg in registros)
                    {

                    //EstadoRevisao estadoRevisao = contextoEstadoRevisao.ReturnById(reg.ID_ESTADO);
                    //.GetByProperty("ID_ESTADO", reg.ID_ESTADO).First();

                    StatusRevisao estadoRevisao = StatusRevisao.ObtemStatusRevisao(reg.ID_ESTADO);



                        coluna.ListaRegistros.Add(new RegistroVerificacao(reg.GUID_LV_ITEM, reg.INDICE, reg.ORDENADOR, reg.GUID, estadoRevisao, reg.DATA_VERICACAO, reg.GUID_LV_VERIFICADOR, reg.CONFIRMADO, reg.SALVO, reg.EMITIDO));
                    }
                }
            //}

            return lista;
        }
    }
}