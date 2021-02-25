using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Validator
{
    public class AddRevisaoValidator : AbstractValidator<AddRevisaoViewModel>
    {

        public AddRevisaoValidator()
        {
            RuleFor(x => x.Nome).NotNull().WithMessage("Campo sem preenchimento");
            RuleFor(x => x.Nome).Matches(@"[A-Z,0-9]{1,2}$").WithMessage("Use quatro numeros inteiros");
            
        }
    }
}


//public ActionResult ValidaAddRevisao(string GuidDocumento, string Nome)
//{
//    bool resp = false;


//    List<Revisao> listaRevisoes = null;
//    using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>())
//    {
//        contextoObjeto.Start();
//        listaRevisoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>()
//        .GetByProperty("GUID_DOC_VERIFICACAO", GuidDocumento).ToList();

//    }

//    if (listaRevisoes.Exists(x => x.INDICE == Nome))
//    {
//        resp = false;
//    }
//    else
//    {
//        resp = true;
//    }

//    return Json(resp, JsonRequestBehavior.AllowGet);
//}