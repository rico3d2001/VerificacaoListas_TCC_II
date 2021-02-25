using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class IndiceController : DefaultController
    {

        private bool _pemitido;

        public IndiceController()
        {
            _pemitido = false;
        }


        // GET: Indice
        public ActionResult IndexIndiceRev(string guidDoc)
        {
            List<Revisao> listaRevisoesNoConfirm = null;
            using (var contextoRevisao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>())
            {
                contextoRevisao.Start();

                var listaRevisoes = contextoRevisao.GetByProperty("GUID_DOC_VERIFICACAO", guidDoc).ToList();

                listaRevisoesNoConfirm = listaRevisoes.Where(x => x.CONFIRMADO == 0).ToList();

            }

            MudaIndiceViewModel mudaIndiceViewModel = new MudaIndiceViewModel();

            mudaIndiceViewModel.Nome = listaRevisoesNoConfirm.Last().INDICE;
            mudaIndiceViewModel.GuidDocumento = guidDoc;



            return View(mudaIndiceViewModel);

        }

        [HttpPost]
        public ContentResult IndexIndiceRev(MudaIndiceViewModel mudado)
        {

            //MudarIndiceValidator
            //[Required(ErrorMessage = "O caracter da nova revisão deve ser informado.")]
            //[RegularExpression(@"[A-Z,0-9]{1,2}$", ErrorMessage = "Formato não permitido.")]

            if (string.IsNullOrEmpty(mudado.Nome) || string.IsNullOrWhiteSpace(mudado.Nome) || mudado.Nome.Count() > 2)
            {
                return Content("");

                
            }

                //mudado.GuidDocumento = guidDoc;

                //if (ModelState.IsValid)
                //{


                //List<Revisao> listaRevisoes = null;
               

                using (var contextoListaVerificacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                {
                     contextoListaVerificacao.Start();

                //listaRevisoes = contextoRevisao.GetByProperty("GUID_DOC_VERIFICACAO", mudado.GuidDocumento).ToList();

                ListaVerificacao listaVerificacao = contextoListaVerificacao.ReturnByGUID(mudado.GuidDocumento);


                var listaRevisoes = listaVerificacao.ListaRevisoes.Distinct().ToList();
                var listaRevisoesNoConfirm = listaRevisoes.Where(x => x.CONFIRMADO == 0).ToList();


                    

                    if (aindaNaoInseriuDesteIndice(mudado, listaRevisoes))
                {

                    using (var contextoConfirmacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>())
                    {
                        contextoConfirmacao.Start();

                        var listaConfirmacoes = contextoConfirmacao.GetByProperty("GUID_DOCUMENTO", mudado.GuidDocumento).OrderBy(x => x.ORDENADOR).ToList();


                        if (listaConfirmacoes.Count > 0)
                        {
                            var ultimaRevisaoCadastrada = listaRevisoesNoConfirm.Last();

                            if (listaConfirmacoes.Exists(x => x.INDICE_REV == ultimaRevisaoCadastrada.INDICE))
                            {
                                var conf_a_alterar = listaConfirmacoes.First(x => x.INDICE_REV == ultimaRevisaoCadastrada.INDICE);
                                conf_a_alterar.INDICE_REV = mudado.Nome;
                                contextoConfirmacao.Update(conf_a_alterar);
                                contextoConfirmacao.Commit();
                            }



                        }

                    }

                    listaVerificacao.MudaIndiceUltimaRevisao(mudado.Nome, listaRevisoesNoConfirm);

                    contextoListaVerificacao.Update(listaVerificacao);
                    contextoListaVerificacao.Commit();

                    _pemitido = true;

                }
                

            }

            

            if (_pemitido)
            {
                TempData["LayoutUsuario"] = "_LayoutAddRevisao";
                //return RedirectToAction("IndexLD", "ListaDocumento", new { guidDocumento = guidDoc });

                var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri)
                {
                    Path = Url.Action("IndexLD", "ListaDocumento"),
                    Query = null,
                };

                //Uri uri = urlBuilder.Uri;
                string url = urlBuilder.ToString();

                string env = url + "?guidDocumento=" + mudado.GuidDocumento;
                return Content(env);




                
            }
            else
            {
                TempData["LayoutUsuario"] = "_LayoutAddRevisao";
                return Content("");
            }
            
            //TempData["LayoutUsuario"] = "_LayoutAddRevisao";
            //return RedirectToAction("IndexLD", "ListaDocumento", new { guidDocumento = guidDoc });
            //}

            //return View();
        }

        

        public ActionResult ValidaMudaRevisao(string GuidDocumento, string Nome)
        {


            var listaRevisoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>()
                .GetByProperty("GUID_DOC_VERIFICACAO", GuidDocumento).ToList();

            bool resp = false;

            if (listaRevisoes.Exists(x => x.INDICE == Nome))
            {
                resp = false;
            }
            else
            {
                resp = true;
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        private bool aindaNaoInseriuDesteIndice(MudaIndiceViewModel model, List<Revisao> listaRevisoes)
        {
            return !listaRevisoes.Exists(x => x.INDICE == model.Nome);
        }

    }
}