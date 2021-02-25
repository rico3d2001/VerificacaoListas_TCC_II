using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using System.Linq;
using System.Web.Mvc;
using Unity;

namespace WebAppAWListaVerificacao.Controllers
{
    public class RetomarEdicaoController : DefaultController
    {
        // GET: RetomarEdicao
        public ActionResult Index(string guidDoc)
        {
            string indiceUltimaConfirmacao = "";

            using (var contextoConfirmacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>())
            {
                contextoConfirmacao.Start();

                var listaConfirmacoes = contextoConfirmacao.GetByProperty("GUID_DOCUMENTO", guidDoc).OrderBy(x => x.ORDENADOR).ToList();

                var ultimaConfirmacao = listaConfirmacoes.Last();

                indiceUltimaConfirmacao = ultimaConfirmacao.INDICE_REV;

                contextoConfirmacao.Delete(ultimaConfirmacao);

                contextoConfirmacao.Commit();
            }

            using (var contextoLV = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
            {
                contextoLV.Start();
                //var listaRevisoes = contextoRevisao.GetByProperty("GUID_DOC_VERIFICACAO", guidDoc).ToList();
                var listaVerificacao = contextoLV.ReturnByGUID(guidDoc);
                var listaRevisoesIndiceAtual = listaVerificacao.ListaRevisoes.Distinct().Where(x => x.INDICE == indiceUltimaConfirmacao).ToList();

                foreach (var rev in listaRevisoesIndiceAtual)
                {
                    rev.GUID_CONFIRMADO = "";
                    rev.CONFIRMADO = 0;
                    //contextoRevisao.Update(rev);
                }

                contextoLV.Update(listaVerificacao);
                contextoLV.Commit();
                //contextoRevisao.Commit();

            }

            TempData["LayoutUsuario"] = "_LayoutAddRevisao";
            return RedirectToAction("IndexLD", "ListaDocumento", new { guidDocumento = guidDoc });

        }
    }
}