using ConsumidorLV_Oracle.Comandos;
using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Consultas;
using LVModel;
using RepositorioMongoDB;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApiLV.Eventos;

namespace LV_PresenterAPI.Comandos
{
    public class CmdConfirmacaoRevisao:CmdGeral
    {

        //private string _baseURL;
        private static CmdConfirmacaoRevisao _instancia;
        private int _confirmado = 0;

        private CmdConfirmacaoRevisao()
        {
            //_baseURL = baseURL;
        }

        public static CmdConfirmacaoRevisao Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new CmdConfirmacaoRevisao();
            }

            return _instancia;
        }

        private bool segundaConfirmacao(string guidLV)
        {
            var lista = QryListaVerificacao.Instancia(guidLV).ListaVerificacaoApp;

            var ultimaConfirmacao = lista.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).Last();

            var t = !string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER2);

            return t;
        }

        public void EmiteViewModel(string guidLV,string guidEmissor)
        {
            var valor = new ValoresConfirma(guidLV, guidEmissor);



            string api = "api/ApiConfirmacao";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(_baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.PostAsJsonAsync(api, valor);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    //tentativa = 2;
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                }

            }
        }

        public void ConfirmaViewModel(ValoresConfirma valor)//, bool houveSomenteAprimeira)
        {
           


            if (QryListaVerificacao.Instancia(valor.GUID_LV).ObtemEstadoRevisoes().NaoTemRevisoesIndefinidas && segundaConfirmacao(valor.GUID_LV))
            {

                ////teste
                ///
                //for (int i = 0; i < 5; i++)
                //{
                //    var cols = new LV_NoSQL().BuscarLV_ViewModel(valor.GUID_LV).Colunas.OrderBy(x => x.ORDENADOR).Last();

                //    if (cols != null && cols.LV_Grupos.Last().Linhas.Last().CONFIRMADO != 1)
                //    {
                //        valor.Coluna = new LV_NoSQL().ConfirmacaoRevisaoVM(valor).Colunas.OrderBy(x => x.ORDENADOR).Last();
                //        cols = valor.Coluna;
                //        var enviodoDispara = new Envio<ValoresConfirma>(valor, new int[] { 2 });
                //        //CmdsOraConfirmacaoRevisao.Confirma(enviodoDispara.MSG);
                //    }
                //}




                ///fim do teste



                string api = "api/ApiConfirmacao";
                var hndlr = new HttpClientHandler();
                hndlr.UseDefaultCredentials = true;

                using (var client = new HttpClient(hndlr))
                {
                    client.BaseAddress = new Uri(_baseURL);

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseTask = client.PostAsJsonAsync(api, valor);

                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {

                        //tentativa = 2;
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var str = readTask.Result;

                    }

                }


            }

            //return tentativa;
        }

        public int Confirma(ValoresConfirma valor, int tentativa)//, bool houveSomenteAprimeira)
        {

            //if (_confirmado < 2)
            //{

            string api = "api/ApiConfirmacao";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(_baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.PostAsJsonAsync(api, valor);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    tentativa = 2;
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                }

            }
            //}

            return tentativa;
        }

        public void Reset()
        {
            if (_instancia != null)
                _confirmado = 0;
        }























        //private string _baseURL;
        //private static CmdConfirmacaoRevisao _unicaInstancia;
        //private HttpClient _client;
        //private string _api;
        //private string _indice;
        //private CmdConfirmacaoRevisao(string baseURL)
        //{
        //    _baseURL = baseURL;
        //    _indice = "INICIAL";
        //    var hndlr = new HttpClientHandler();
        //    hndlr.UseDefaultCredentials = true;
        //    _client = new HttpClient(hndlr);
        //    _client.BaseAddress = new Uri(_baseURL);
        //}

        //public static CmdConfirmacaoRevisao Instance(string baseURL)
        //{

        //    if (_unicaInstancia == null)

        //    {

        //        _unicaInstancia = new CmdConfirmacaoRevisao(baseURL);

        //    }

        //    return _unicaInstancia;

        //}

        ////public void Confirma(SendConfirmarVM valor)
        ////{
        ////    _api = "api/ApiConfirmacao";

        ////    _client.DefaultRequestHeaders.Clear();

        ////    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        ////    var responseTask = _client.PostAsJsonAsync(_api, valor);

        ////    responseTask.Wait();

        ////    var result = responseTask.Result;

        ////    if (result.IsSuccessStatusCode)
        ////    {
        ////        var readTask = result.Content.ReadAsStringAsync();
        ////        readTask.Wait();

        ////        var str = readTask.Result;

        ////    }

        ////}

        //public void Confirma(SendConfirmarVM valor, string indice)//async Task Confirma(SendConfirmarVM valor, string indice)
        //{
        //    //if (indice == "INICIAL")
        //    //{
        //        _api = "api/ApiConfirmacao";

        //        _client.DefaultRequestHeaders.Clear();

        //        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var responseTask = _client.PostAsJsonAsync(_api, valor);

        //        responseTask.Wait();

        //        var result = responseTask.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsStringAsync();
        //            readTask.Wait();

        //            var str = readTask.Result;

        //        }

        //        //_indice = indice;
        //    //}

        //}

        //public void ClienteDispose()
        //{
        //    _client.Dispose();
        //}

        ////public static void Reset()
        ////{
        ////    if (_unicaInstancia != null)
        ////    {
        ////        _unicaInstancia.ClienteDispose();
        ////        _unicaInstancia = null;
        ////    }

        ////}










    }
}