using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AppExcel.AppWeb
{
    public class NumDocSNCApp
    {
        public NumeroDocSNCLavalin GetNumero(string guidprojeto, string guidos, string guidarea, string iddisciplina, string guidtipo, string sequencial)
        {
            var projeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>().ReturnByGUID(guidprojeto);
            var os = projeto.ListaOSs.First(x => x.GUID == guidos);
            var area = projeto.ListaAreas.First(x => x.GUID == guidarea);
            var disciplina = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>().ReturnById(int.Parse(iddisciplina));

            //fazer busca atrvez objetos
           

            //gambiarra
            var listaTiposDocumentos = getTiposDocumentos(projeto);
            string codigoGambiarra = listaTiposDocumentos.First(x => x.GUID == guidtipo).CODIGO;


            NumeroDocSNCLavalin numeroDocSNCLavalin = new NumeroDocSNCLavalin(projeto, os, area, disciplina, codigoGambiarra, sequencial);

            return numeroDocSNCLavalin;

        }

        //rotina provisória
        private List<TipoDocumento> getTiposDocumentos(Projeto projeto)
        {

            List<NumeroDocSNCLavalin> listaNumeroDocSNCLavalin =
               DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().GetByProperty("PROJETO", projeto.NUMERO).ToList();


            List<string> listaStr = new List<string>();

            //var listaCodigos = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<CodigoDocumento>>().GetByProperty("ID_DISCIPLINA", idDisciplina);

            var codAgrup = listaNumeroDocSNCLavalin.Distinct();

            

            foreach (var item in codAgrup)
            {
                var numero = item.NUMERO;
                var strarray = numero.ToString().Split('-');
                var str = strarray[3];
                str = str.Substring(2, 2);
                listaStr.Add(item.TIPO);
            }

            var agrupado = listaStr.Distinct().OrderBy(x => x).ToList();



            List<TipoDocumento> listaTipoDocumentos = new List<TipoDocumento>();

            for (int i = 0; i < agrupado.Count; i++)
            {
                listaTipoDocumentos.Add(new TipoDocumento(agrupado[i], i.ToString()));
            }

            return listaTipoDocumentos;
        }
    }
}
