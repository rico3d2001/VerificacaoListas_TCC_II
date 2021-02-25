using EntidadesRepositoriosLeitura;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LV_PresenterAPI.Consultas
{
    public class QryRevisaoUnitaria: QryConsulta
    {
        //private string _baseURL;
        private static QryRevisaoUnitaria _instancia;
        //private HttpClientHandler _hndlr = new HttpClientHandler();
        //private HttpClient _client;

        private QryRevisaoUnitaria()//string baseURL)
        {
            //_baseURL = baseURL;
            //_hndlr = new HttpClientHandler();
            //_hndlr.UseDefaultCredentials = true;
            //_client = new HttpClient(hndlr);
            //_client.BaseAddress = new Uri(_baseURL);
        }


        public static QryRevisaoUnitaria Instancia()//string baseURL)
        {
            if (_instancia == null)
            {
                _instancia = new QryRevisaoUnitaria();//baseURL);
            }

            return _instancia;
        }


        public RevUnitQuery ObtemRevisao(string guid)
        {
            RevUnitQuery rev = null;
            //api/ApiRevisao/{guidRevisao}
           
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                string api = "api/ApiRevisao/" + guid;

                client.BaseAddress = new Uri(_baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.GetAsync(api);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                    rev = JsonConvert.DeserializeObject<RevUnitQuery>(str);

                }
            }

            return rev;
        }

        //protected string consultar<T>(string api, HttpClientHandler hndlr, T objeto)
        //{
        //    string str_json = "";

        //    using (var client = new HttpClient(hndlr))
        //    {
        //        client.BaseAddress = new Uri(_baseURL);

        //        client.DefaultRequestHeaders.Clear();

        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var responseTask = client.GetAsync(api);

        //        responseTask.Wait();

        //        var result = responseTask.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsStringAsync();
        //            readTask.Wait();

        //            str_json = readTask.Result;
        //        }


        //    }

        //    return str_json;
        //}


    }
}