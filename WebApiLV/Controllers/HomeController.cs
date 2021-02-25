using EntidadesRepositoriosLeitura;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApiLV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        public ActionResult TesteMudarEstado()
        {


            RevisaoVM valor = new RevisaoVM()
            {
                GUID = "41242004-db81-45e2-985e-8abc75c132e2",
                GUID_DOC_VERIFICACAO = "caef570e-af51-4b59-9e0b-4065b05f0ab4",
                ID_ESTADO = 3
            };



            string baseURL = "https://localhost:44355/";

            string api = "api/ApiRevisao/MudarEstado";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.PutAsJsonAsync(api, valor);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                }
            }





            return View();
        }


        public ActionResult TesteCriarLV()
        {
            ValoresComandoCriaLV valoresComandoCriaLV = new ValoresComandoCriaLV();
            valoresComandoCriaLV.GuidPlanilha = "98ef3089-f6d4-459f-9fe1-0728ae6343e2";
            valoresComandoCriaLV.NovoGuidLV = "kkkc7f7a-d0ab-4417-9955-kkk";
            valoresComandoCriaLV.NumeroSNC = "9999-999-9999-46YY-96000";

            string baseURL = "https://localhost:44355/";

            string api = "api/CriaLV";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask =
                    client.PostAsJsonAsync(api, valoresComandoCriaLV);


                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                }
            }






            //bool teste = CmdAcrescimoRevisao.Acrescenta(valoresCriaColunaRevisao);



            return View();

        }




        public async Task<ActionResult> TesteCmdAcrescimoRevisao()
        {
            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            string baseURL = "https://localhost:44355/";

            ValoresColunasRev valoresCriaColunaRevisao = new ValoresColunasRev(
                    "420c7f7a-d0ab-4417-9955-bf92f4252eb7", "PA", "RRP");
            //"7e5e3cf4-dd1f-4fda-865e-9a7d3d884651"



            string api = "api/ApiAddRevisao";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask =
                    client.PostAsJsonAsync(api, valoresCriaColunaRevisao);


                ///responseTask.Wait();

                var result = responseTask.Result;

                //if (result.IsSuccessStatusCode)
                //{
                //    var readTask = result.Content.ReadAsStringAsync();
                //    readTask.Wait();

                //    var str = readTask.Result;

                //}
            }






            //bool teste = CmdAcrescimoRevisao.Acrescenta(valoresCriaColunaRevisao);



            return View();


        }


        public ActionResult TesteLeConfiguracoes()
        {

            var lista = new ConfiguracoesListaMDB().Buscar();

            return View();
        }


        public ActionResult TesteListaVerificacaoSemConfirmacoes()
        {

            var lv = MySQLConsultaListaVerificacao.ObtemListaSemConfirmacoes("7e5e3cf4-dd1f-4fda-865e-9a7d3d884651");

            return View();
        }

        public ActionResult TesteListaAlteraRevisao()
        {
            RevisaoVM revisaoVM = new RevisaoVM();
            revisaoVM.GUID = "d1c08b8b-9315-4dea-95ed-fc55fcc31d1b";
            revisaoVM.GUID_DOC_VERIFICACAO = "420c7f7a-d0ab-4417-9955-bf92f4252eb7";
            revisaoVM.ID_ESTADO = 2;

            string baseURL = "https://localhost:44355/";

            string api = "api/ApiRevisao/MudarEstado";


            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.PutAsJsonAsync(api, revisaoVM);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                }
            }





            return View();
        }

        public ActionResult TestaMudaIndice()
        {




            ValoresMudaIndice valor = new ValoresMudaIndice(true, "7e5e3cf4-dd1f-4fda-865e-9a7d3d884651", "Z");


            //ValoresColunaRevisao valoresCriaColunaRevisao = new ValoresColunaRevisao(
            //       "7e5e3cf4-dd1f-4fda-865e-9a7d3d884651", "PA", "RRP");

            string baseURL = "https://localhost:44355/";

            string api = "api/ApiMudaIndice";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.PutAsJsonAsync(api, valor);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                }
            }





            return View();
        }

        public ActionResult TestaStatusRevisoesLV()
        {
            string api = "api/StatusRevisoesLV/5efa3953-7238-4b2d-92a4-981bf7973fcc";

            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            string baseURL = "https://localhost:44355/";

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.GetAsync(api);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str_json = readTask.Result;
                }


            }

            return View();
        }



        public ActionResult TestaConfirma()
        {

            ValoresConfirma valor = new ValoresConfirma("fdbdc45e-b124-436d-ba91-52bb2932c498", "RRP");
                 
            string baseURL = "https://localhost:44355/";

            string api = "api/ApiConfirmacao";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.PostAsJsonAsync(api, valor);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                }
            }





            return View();
        }

        //public ActionResult TesteListaConfirmaRevisao()
        //{


        //    string baseURL = "https://localhost:44355/";



        //    SendConfirmarVM valor = new SendConfirmarVM(
        //        "7e5e3cf4-dd1f-4fda-865e-9a7d3d884651", false, "RRP", Guid.NewGuid().ToString(), 1);

        //    //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44355/api/ApiConfirmacao/Confirmar");




        //    //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //    //{
        //    //    var json = JsonConvert.SerializeObject(valor);

        //    //    streamWriter.Write(json);
        //    //}





        //    string api = "api/ApiConfirmacao/";  
        //    var hndlr = new HttpClientHandler();
        //    hndlr.UseDefaultCredentials = true;



        //    using (var client = new HttpClient(hndlr))
        //    {
        //        client.BaseAddress = new Uri(baseURL);

        //        client.DefaultRequestHeaders.Clear();

        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var dataAsString = JsonConvert.SerializeObject(valor);
        //        //var content = new StringContent(dataAsString);

        //        string GUID_LV = "7e5e3cf4-dd1f-4fda-865e-9a7d3d884651";
        //        bool IsConfiguarcaoDupla = false;
        //    string GUID_USUARIO = "RRP";
        //    string GUID_CONFIRMACAO = Guid.NewGuid().ToString();
        //    int ORDENADOR = 2;

        //        string json = "{\"GUID_LV\":\""
        //            + GUID_LV + "\","

        //            + "\"IsConfiguarcaoDupla\":\""
        //            + IsConfiguarcaoDupla.ToString() + "\","

        //            + "\"GUID_USUARIO\":\""
        //            + GUID_USUARIO + "\","

        //            + "\"GUID_CONFIRMACAO\":\""
        //            + Guid.NewGuid().ToString() + "\","


        //            + "\"GUID_USUARIO\":\""
        //            + GUID_USUARIO
        //            + "\"}";

        //        //string json = "bunda";

        //        //api = api + "bunda";

        //        var content = new StringContent(dataAsString);

        //        var responseTask = client.PostAsJsonAsync(api, content);


        //        responseTask.Wait();

        //        var result = responseTask.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsStringAsync();
        //            readTask.Wait();

        //            var str = readTask.Result;

        //        }
        //    }




        //    return View();
        //}




    }
}
