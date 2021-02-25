using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAWListaVerificacao.Models
{
    public static class SncUrlHelper
    {
        public static string GetUrl(this HtmlHelper helper, string Action, string Controller, object RouteValues)
        {
            UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return Url.Action(Action, Controller, RouteValues);
        }
    }
}