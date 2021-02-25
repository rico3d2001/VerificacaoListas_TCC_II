using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class BasePresenterController : Controller
    {
        //protected string _baseUrl;
       

        public BasePresenterController()
        {
            //_baseUrl = "http://sap/ApiLV/";
            //_baseUrl = "https://localhost:44355/";
        }

       

        //protected bool Usuario_Verificador()
        //{
        //    string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();
        //    var usuario = new QryUsuario(_baseUrl).ObtemUsuario(login);
        //    return (usuario != null && usuario.ISVERIFICADOR == 1);
        //}

       




    }
}