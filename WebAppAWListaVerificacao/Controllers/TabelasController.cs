using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using LVModel.ObjetosValor;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class TabelasController : DefaultController
    {
        // GET: Tabelas
        public ActionResult TabelaTemplate(List<ColunaRevisaoViewModel> listaColunas)
        {

            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var isVerificador = getUsuario(login).GetBoolIsVerificador();

            ViewBag.IsVerificador = isVerificador;

            int divisor = listaColunas.Count == 0 ? 1 : listaColunas.Count;
            ViewBag.LarguraCalculada = 100 / divisor;



            ViewBag.List_ColunaRevisaoViewModel = listaColunas;
            return View();
        }


        public ActionResult TabelaDocumento(List<ColunaRevisaoViewModel> listaColunas)
        {

            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var isVerificador = getUsuario(login).GetBoolIsVerificador();

            ViewBag.IsVerificador = isVerificador;

            int divisor = listaColunas.Count == 0 ? 1 : listaColunas.Count;
            ViewBag.LarguraCalculada = 100 / divisor;



            ViewBag.List_ColunaRevisaoViewModel = listaColunas;
            return View();
        }

        public ActionResult TabelaDocumentoNoVerficador(List<ColunaRevisaoViewModel> listaColunas)
        {

            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var isVerificador = getUsuario(login).GetBoolIsVerificador();

            ViewBag.IsVerificador = isVerificador;

            int divisor = listaColunas.Count == 0 ? 1 : listaColunas.Count;
            ViewBag.LarguraCalculada = 100 / divisor;



            ViewBag.List_ColunaRevisaoViewModel = listaColunas;
            return View();
        }





        public ActionResult TabelasFixas()
        {
            return View();
        }

        public ActionResult TabelaRegistrosVerificacoes(List<Confirmacao> listaConfirmacoes, string guidDoc, bool isListaVerificacaoDupla)
        {
            List<ConfirmacaoViewModel> confirmacaoViewModels = new List<ConfirmacaoViewModel>();

            var listaOrdenada = listaConfirmacoes.OrderBy(x => x.DATA).ToList();


            using (var contextoUsuarioTblRegVer = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>())
            {
                contextoUsuarioTblRegVer.Start();


                foreach (var conf in listaOrdenada)
                {

                    string confGUID_USUARIO1 = conf.GUID_USUARIO1;
                    var usuario = contextoUsuarioTblRegVer.ReturnByGUID(confGUID_USUARIO1);


                    confirmacaoViewModels.Add(new ConfirmacaoViewModel()
                    {
                        DATA = conf.DATA.ToShortDateString(),
                        INDICE_REV = conf.INDICE_REV,
                        NOME_USUARIO = usuario.NOME,
                        SIGLA_USUARIO = usuario.GUID
                    });

                    if (conf.GUID_USUARIO2 != null && isListaVerificacaoDupla)
                    {
                        string confGUID_USUARIO2 = conf.GUID_USUARIO2;
                        usuario = contextoUsuarioTblRegVer.ReturnByGUID(confGUID_USUARIO2);
                        confirmacaoViewModels.Add(new ConfirmacaoViewModel()
                        {
                            DATA = conf.DATA.ToShortDateString(),
                            INDICE_REV = conf.INDICE_REV,
                            NOME_USUARIO = usuario.NOME,
                            SIGLA_USUARIO = usuario.GUID
                        });
                    }
                }
            }

            ViewBag.ListaColunasOrdenada = confirmacaoViewModels;

            int divisor = confirmacaoViewModels.Count == 0 ? 1 : confirmacaoViewModels.Count;
            ViewBag.LarguraCalculada = 100 / divisor;
            return View();
        }


        public PartialViewResult _SetStatus(string idTipo, string status, string guidLinha, string guidRev, string item)
        {

            ViewBag.GuidRev = guidRev;
            ViewBag.GuidLinha = guidLinha;
            ViewBag.IdTipo = idTipo;
            ViewBag.item = item;
            ViewBag.StatusAgora = status;
            return PartialView("_SetStatus");
        }

        public PartialViewResult _MostraImagensStatus(string strTipoRevisao, ImagemStatusViewModel imagensView)
        {
            ViewBag.StrTipoRev = strTipoRevisao;
            return PartialView(imagensView);
        }



        public PartialViewResult _AjxDefineStatus(string idTipo, string status, string guidLinha, string guidRev, string indiceRev, string item)
        {


            var lista = (List<ListaRegistrosPorColunas>)Session["ListaRevisoes"];

            var registro = lista.Last().ListaRegistros.FirstOrDefault(x => x.GuidVerificacao == guidRev);

            if (!(registro.TipoRevisao.Name == status))
            {
              
                    using (var contextoRevisaoSetStatus = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>())
                    {
                        contextoRevisaoSetStatus.Start();

                        var revisao = contextoRevisaoSetStatus.ReturnByGUID(guidRev);

                        lista.Last().ListaRegistros.FirstOrDefault(x => x.GuidVerificacao == guidRev).TipoRevisao = StatusRevisao.ObtemStatusRevisao(status);

                        revisao.ID_ESTADO = StatusRevisao.ObtemStatusRevisao(status).Id; //dicEstados.First(x => x.Key == status).Value;


                        DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>()
                            .Update(revisao);

                        contextoRevisaoSetStatus.Commit();

                        Session["ListaRevisoes"] = lista;
                    }
                
            }

            ViewBag.IndiceRevisao = indiceRev;
            ViewBag.GuidRev = guidRev;
            ViewBag.GuidLinha = guidLinha;
            ViewBag.IdTipo = idTipo;
            ViewBag.StatusAgora = status;

            return PartialView("_SetStatus");

        }













    }
}