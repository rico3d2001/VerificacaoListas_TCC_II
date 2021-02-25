using EntidadesRepositoriosLeitura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace LV_PresenterAPI.Consultas
{
    public abstract class QryConsulta
    {

        

        protected string _baseURL;
        protected string _guidDocumento;
        protected bool _listaPossuiRevisoes;
        protected ListaVerficacaoVM _lv;
        public QryConsulta()
        {
            //_baseURL = "http://sap/ApiLV/";
            _baseURL = "https://localhost:44355/";

        }




        protected string consultar<T>(string api, HttpClientHandler hndlr, T objeto)
        {
            string str_json = "";

            using (var client = new HttpClient(hndlr))
            {
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

                    str_json = readTask.Result;
                }


            }

            return str_json;
        }


    }
}