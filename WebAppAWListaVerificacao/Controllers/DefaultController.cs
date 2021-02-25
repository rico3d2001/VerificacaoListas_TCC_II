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

namespace WebAppAWListaVerificacao.Controllers
{
    public class DefaultController : Controller
    {
        

        

        protected Usuario getUsuario(string login)
        {

            using (var contextoUsuario = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>())
            {
                contextoUsuario.Start();

                var listaUser = contextoUsuario.GetByProperty("SIGLA", login);

                return listaUser.Count > 0 ? listaUser.First() : new Usuario();
            }
        }

        

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Exception exception = filterContext.Exception;

        //    filterContext.ExceptionHandled = true;

        //    var resultado = this.View("Error", 
        //        new HandleErrorInfo(exception, 
        //        filterContext.RouteData.Values["controller"].ToString(),
        //        filterContext.RouteData.Values["action"].ToString()));

        //    filterContext.Result = resultado;
        //}


    }
}