
using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Comandos;
using LV_PresenterAPI.Consultas;
using LV_PresenterAPI.Models;
using RepositorioMongoDB;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{




    public class TabelasController : BasePresenterController
    {

        enum StatusLocal { V, ND, NA, X, I };
        QryRevisaoUnitaria _qryRevisaoUnitaria;
        int _tentativa;
        private ImagemStatusViewModel _imagemStatusViewModel;

        public TabelasController()
        {
            _tentativa = 0;

            _imagemStatusViewModel = new ImagemStatusViewModel
            {
                ImagePathV = "~/imagens/V.png",
                ImagePathX = "~/imagens/X.png",
                ImagePathNA = "~/imagens/NA.png",
                ImagePathND = "~/imagens/ND.png",
                ImagePathI = "~/imagens/I.png"
            };

            _qryRevisaoUnitaria = QryRevisaoUnitaria.Instancia();
        }

        public ActionResult TabelaTemplate(List<ColunaRevisaoViewModel> listaColunas)
        {

            int divisor = listaColunas.Count == 0 ? 1 : listaColunas.Count;
            ViewBag.LarguraCalculada = 100 / divisor;



            ViewBag.List_ColunaRevisaoViewModel = listaColunas;
            return View();
        }

        public ActionResult TabelasFixas()
        {
            return View();
        }

        public ActionResult TabelaDocumento(ListaVerficacaoVM listaVer)
        {
            TempData["ImagemStatusViewModel"] = _imagemStatusViewModel;

            var divisor = listaVer.Colunas.Count == 0 ? 1 : listaVer.Colunas.Count;
            ViewBag.LarguraCalculada = 100 / divisor;

            ViewBag.LV = listaVer;

            return View();
        }

        public ActionResult TabelaDocumentoNoVerficador(ListaVerficacaoVM listaVer)
        {
            TempData["ImagemStatusViewModel"] = _imagemStatusViewModel;

            var divisor = listaVer.Colunas.Count == 0 ? 1 : listaVer.Colunas.Count;
            ViewBag.LarguraCalculada = 100 / divisor;


            ViewBag.LV = listaVer;

            return View();

        }


        public ActionResult TabelaRegistrosVerificacoes(ListaVerficacaoVM listaVer)
        {
            List<ConfirmacaoViewModel> confirmacaoViewModels = new List<ConfirmacaoViewModel>();

            

            var listaOrdenada = listaVer.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).ToList();


            bool isListaVerificacaoDupla = listaVer.VERFICADOR_UNICO == 0 ? true : false;

            if (isListaVerificacaoDupla)
            {
                foreach (var conf in listaOrdenada)
                {
                    if (conf.CONFIRMACAO_ID_USER1 != null) //&& conf.CONFIRMACAO_ID_USER2 != null)
                    {
                        confirmacaoViewModels.Add(new ConfirmacaoViewModel()
                        {
                            DATA = conf.CONFIRMACAO_DATA.ToShortDateString(),
                            INDICE_REV = conf.CONFIRMACAO_INDICE,
                            NOME_USUARIO = conf.CONFIRMACAO_NOME_USER1,
                            SIGLA_USUARIO = conf.CONFIRMACAO_ID_USER1
                        });

                        confirmacaoViewModels.Add(new ConfirmacaoViewModel()
                        {
                            DATA = conf.CONFIRMACAO_DATA.ToShortDateString(),
                            INDICE_REV = conf.CONFIRMACAO_INDICE,
                            NOME_USUARIO = conf.CONFIRMACAO_NOME_USER2,
                            SIGLA_USUARIO = conf.CONFIRMACAO_ID_USER2
                        });
                    }

                }
            }
            else
            {
                foreach (var conf in listaOrdenada)
                {
                    confirmacaoViewModels.Add(new ConfirmacaoViewModel()
                    {
                        DATA = conf.CONFIRMACAO_DATA.ToShortDateString(),
                        INDICE_REV = conf.CONFIRMACAO_INDICE,
                        NOME_USUARIO = conf.CONFIRMACAO_NOME_USER1,
                        SIGLA_USUARIO = conf.CONFIRMACAO_ID_USER1
                    });
                }
            }



            ///////////////////////

            //foreach (var conf in listaOrdenada)
            //{



            //    confirmacaoViewModels.Add(new ConfirmacaoViewModel()
            //    {
            //        DATA = conf.CONFIRMACAO_DATA.ToShortDateString(),
            //        INDICE_REV = conf.CONFIRMACAO_INDICE,
            //        NOME_USUARIO = conf.CONFIRMACAO_NOME_USER1,
            //        SIGLA_USUARIO = conf.CONFIRMACAO_SIGLA_USER1
            //    });

            //    //bool isListaVerificacaoDupla = listaVer.VERFICADOR_UNICO == 0 ? true : false;

            //    if (conf.CONFIRMACAO_ID_USER2 != null && isListaVerificacaoDupla)
            //    {
            //        //string confGUID_USUARIO2 = conf.CONFIRMACAO_ID_USER2;

            //        confirmacaoViewModels.Add(new ConfirmacaoViewModel()
            //        {
            //            DATA = conf.CONFIRMACAO_DATA.ToShortDateString(),
            //            INDICE_REV = conf.CONFIRMACAO_INDICE,
            //            NOME_USUARIO = conf.CONFIRMACAO_NOME_USER2,
            //            SIGLA_USUARIO = conf.CONFIRMACAO_SIGLA_USER2
            //        });
            //    }
            //}


            ViewBag.ListaColunasOrdenada = confirmacaoViewModels;

            int divisor = confirmacaoViewModels.Count == 0 ? 1 : confirmacaoViewModels.Count;
            ViewBag.LarguraCalculada = 100 / divisor;
            return View();
        }

        public PartialViewResult _MostraImagensStatus(int intTipoRevisao, ImagemStatusViewModel imagensView)
        {
            string strTipoRevisao = "";

            switch (intTipoRevisao)
            {
                case 1:
                    strTipoRevisao = "V";
                    break;
                case 2:
                    strTipoRevisao = "ND";
                    break;
                case 3:
                    strTipoRevisao = "NA";
                    break;
                case 4:
                    strTipoRevisao = "X";
                    break;
                case 5:
                    strTipoRevisao = "I";
                    break;

            }


            ViewBag.StrTipoRev = strTipoRevisao;
            return PartialView(imagensView);
        }


        public PartialViewResult _SetStatuss(string idTipo, string status, string guidLinha, string guidGrupo, string guidRev, string item, ListaVerficacaoVM lv)
        {
            if (int.Parse(status) == 0)
            {
                status = "5";
            }


            List<string> listaStatus = new List<string> { "V", "ND", "NA", "X" };

            ViewBag.LV = lv;
            Session["lv"] = lv;

            ViewBag.GuidRev = guidRev;
            ViewBag.GuidGrupo = guidGrupo;
            ViewBag.GuidLinha = guidLinha;
            ViewBag.IdTipo = idTipo;
            ViewBag.item = item;
            ViewBag.StatusAgora = status;
            return PartialView("_SetStatuss");
        }

        public ActionResult _AjxDefineStatus(string idTipo, string status, string guidGrupo, string guidLinha, string guidRev, string indiceRev, string item)
        {



            // var lista = (ListaVerficacaoVM)Session["lv"];

            

      
           

            //LinhaRevisaoVM linha = null;
            //var lista = new QryListaVerificacao(_baseUrl, (string)Session["GidLV"]).ListaVerificacaoApp;
            foreach (var grupo in QryListaVerificacao.Instancia((string)Session["GidLV"]) 
                .ListaVerificacaoApp.Colunas.Find(x => x.INDICE_REV == indiceRev).LV_Grupos)
            {
                var linha = grupo.Linhas.FirstOrDefault(x => x.GUID_REVISAO == guidRev);
                if (linha != null)
                {
                    if (linha.ID_ESTADO != int.Parse(status))
                    {
                        linha.ID_ESTADO = int.Parse(status);
                        //_tentativa = CmdUpdateRevisao.Instance(_baseUrl)
                        //    .MudaEstado(new RevisaoVM(guidRev, int.Parse(status), (string)Session["GidLV"]), _tentativa);
                        var lv = new LV_NoSQL().
                            MudaEstadoRevisao_ViewModel(new RevisaoVM(guidRev, int.Parse(status), (string)Session["GidLV"]));


                    }
                    break;
                }


            }

            
           









            ViewBag.IndiceRevisao = indiceRev;
            ViewBag.GuidRev = guidRev;
            ViewBag.GuidLinha = guidLinha;
            ViewBag.GuidGrupo = guidGrupo;
            ViewBag.IdTipo = idTipo;
            ViewBag.StatusAgora = status;

            return PartialView("_SetStatuss");

        }





    }
}