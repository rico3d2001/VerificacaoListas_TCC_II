using EntidadesRepositoriosLeitura;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LV_PresenterAPI.Comandos
{
    public class CmdUpdateRevisao
    {
        private string _baseURL;
        private static CmdUpdateRevisao _unicaInstancia;
        private HttpClient _client;
        private string _api;
        private int _tentativa;
   
        private CmdUpdateRevisao(string baseURL)
        {
            _baseURL = baseURL;
           
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;
            _client = new HttpClient(hndlr);
            _client.BaseAddress = new Uri(_baseURL);
        }

        public static CmdUpdateRevisao Instance(string baseURL)
        {

            if (_unicaInstancia == null)

            {

                _unicaInstancia = new CmdUpdateRevisao(baseURL);

            }

            return _unicaInstancia;

        }

       


        public int MudaEstado(RevisaoVM valor, int tentativa)
        {

            //if (_mudado == false)
            //{


                //using (var client = new HttpClient(hndlr))
                //{
                _api = "api/ApiRevisao/MudarEstado";

                _client.DefaultRequestHeaders.Clear();

                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = _client.PutAsJsonAsync(_api, valor);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;
                tentativa = 2;
                }

            //}

            return tentativa;
        
        }


        public void ClienteDispose()
        {
            _client.Dispose();
        }

        public static void Reset()
        {
            if (_unicaInstancia != null)
            {
                _unicaInstancia.ClienteDispose();
                _unicaInstancia = null;
            }

        }
    }
}