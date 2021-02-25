using EntidadesRepositoriosLeitura;
using Newtonsoft.Json;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System.Linq;
using System.Net.Http;

namespace LV_PresenterAPI.Consultas
{
    public class QryListaVerificacao : QryConsulta
    {

        //private string _baseURL;
        //private string _guidDocumento;
        private bool _listaPossuiRevisoes;
        private ListaVerficacaoVM _lv;
        private static QryListaVerificacao _instancia = null;


        public bool ListaPossuiRevisoes { get => _listaPossuiRevisoes; }
        public ListaVerficacaoVM ListaVerificacaoApp { get => _lv; set => _lv = value; }

        private QryListaVerificacao(string guidDocumento)//(string baseURL, string guidDocumento)
        {
            //_guidDocumento = guidDocumento;
            //_baseURL = baseURL;
            setLV(guidDocumento);
        }

        public static QryListaVerificacao Instancia(string guidDocumento)//string baseURL, string guidDocumento)
        {
             //if (_instancia == null)
                //{
                    _instancia = new QryListaVerificacao(guidDocumento);//baseURL, guidDocumento);
                                                                        //}
            return _instancia;
            
        }


        public static void Reset()
        {
            _instancia = null;
        }

        public void setLV(string guidDocumento)
        {
            if (_lv == null)
            {
                var noSQL = new LV_NoSQL();

                _lv = noSQL.BuscarLV_ViewModel(guidDocumento);

                if (_lv == null)
                {
                    _lv = MySQLConsultaListaVerificacao.ObtemListaCompleta(guidDocumento);
                    if(_lv == null)
                    {
                        _lv = MySQLConsultaListaVerificacao.ObtemListaPrimeiraConfirmacao(guidDocumento);
                        if(_lv == null)
                        {
                            _lv = MySQLConsultaListaVerificacao.ObtemListaSemConfirmacoes(guidDocumento);
                            if(_lv == null)
                            {
                                _lv = MySQLConsultaListaVerificacao.ObtemListaSemRevisoes(guidDocumento);
                            }
                        }
                    }

                    noSQL.CriarLV_ViewModel(_lv);
                }
            }
        }


        //public ListaVerficacaoVM RecuperaLV_ViewModel(string guidDocumento)
        //{

        //    //string api = "api/LVCompleta/" + guidDocumento;
        //    //var hndlr = new HttpClientHandler();
        //    //hndlr.UseDefaultCredentials = true;

        //    ////ListaVerficacaoVM listaVerificacaoApp = null;


        //    //string str_json = consultar(api, hndlr, listaVerificacaoApp);

        //    //listaVerificacaoApp = JsonConvert.DeserializeObject<ListaVerficacaoVM>(str_json);



        //    return listaVerificacaoApp;
        //}




        public ListaVerficacaoVM RecuperaLV(string guidDocumento)
        {

         

            StatusRevisoesLV statusRev = ObtemEstadoRevisoes();

            if (statusRev.ExistemRevisoesNesteDocumento)
            {
                _listaPossuiRevisoes = true;


                StatusConfirmacoesLV statusConfirm = ObtemEstadoConfirmacoes();

                if (statusConfirm.ListaSemConfirmacao)
                {
                    return obtemListaSemConfirmacoes(guidDocumento);
                }
                else
                {
                    if (statusConfirm.HaColunaConfirmada && statusConfirm.HouveSomentePrimeiraConfirmacaoColunaAtual)
                    {
                        return obtemListaCompleta(guidDocumento);
                    }
                    else if(statusConfirm.HouveSomentePrimeiraConfirmacaoColunaAtual)
                    {
                        return obtemListaPrimeiraConfirmacao(guidDocumento);
                    }
                    else
                    {
                        return obtemListaCompleta(guidDocumento);
                    }
                }




            }
            else
            {
                //_listaPossuiRevisoes = true;

                return obtemListaSemRevisoes(guidDocumento);
            }
        }

        //"api/LVPrimeiraConfirmacao/{guidDocumento}"
        private ListaVerficacaoVM obtemListaPrimeiraConfirmacao(string guidDOc)
        {



            string api = "api/LVPrimeiraConfirmacao/" + guidDOc;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            ListaVerficacaoVM listaVerificacaoApp = null;


            string str_json = consultar(api, hndlr, listaVerificacaoApp);

            listaVerificacaoApp = JsonConvert.DeserializeObject<ListaVerficacaoVM>(str_json);



            return listaVerificacaoApp;

        }

        public StatusRevisoesLV ObtemEstadoRevisoes()
        {
            

            StatusRevisoesLV status = new StatusRevisoesLV();

            //status.ExistemRevisoesNesteDocumento = true;

            if(!string.IsNullOrEmpty(_lv.Colunas.First().INDICE_REV))
            {
                status.ExistemRevisoesNesteDocumento = true;
            }

            if(status.ExistemRevisoesNesteDocumento)
            {
                var ultimaColuna = _lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

                status.NaoTemRevisoesIndefinidas = true;
                status.PossuiRevisoesNaoConfirmadas = false;
                status.LVEmitida = false;
                foreach (var grupo in ultimaColuna.LV_Grupos)
                {

                    if(status.NaoTemRevisoesIndefinidas == true)
                    {
                        if (grupo.Linhas.Where(x => x.ID_ESTADO == 5).Count() > 0)
                        {
                            status.NaoTemRevisoesIndefinidas = false;
                        }
                    }
                    
                    if(status.PossuiRevisoesNaoConfirmadas == false)
                    {
                        if (grupo.Linhas.Where(x => x.CONFIRMADO == 0).Count() > 0)
                        {
                            status.PossuiRevisoesNaoConfirmadas = true;
                        }
                    }

                    if(status.LVEmitida == false)
                    {
                        if (grupo.Linhas.Where(x => x.EMITIDO == 0).Count() < 1)
                        {
                            status.LVEmitida = true;
                        }
                    }
                    


                }

                var results = (from p in _lv.Colunas.OrderBy(x => x.ORDENADOR)
                               group p.ORDENADOR by p.ORDENADOR into g
                               select new { Ordenador = g.Key }).ToList();

                foreach (var item in results)
                {
                    status.Indices.Add(_lv.Colunas.First(x => x.ORDENADOR == item.Ordenador).INDICE_REV);
                }




            }

            return status;

        }

        //private string consultar<T>(string api, HttpClientHandler hndlr, T objeto)
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

        public StatusConfirmacoesLV ObtemEstadoConfirmacoes()
        {

            

            StatusConfirmacoesLV status = new StatusConfirmacoesLV();

            if (_lv.Confirmacoes.Count() < 1) //&& respostaPlanilha.Count() < 2)
            {
                //    var primeiro = respostaPlanilha.FirstOrDefault();
                //    if(!string.IsNullOrEmpty(primeiro.GUID))
                //    {
                status.ListaSemConfirmacao = true;
                // }

            }
            else
            {
                var ultimaConfirmacao = _lv.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).ToList().Last();
                status.HouveSomentePrimeiraConfirmacaoColunaAtual = 
                    (!string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER1) 
                    && string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER2)) ? true : false;


                status.HaColunaConfirmada = _lv.Confirmacoes.Count() > 1 ? true : false;
            }

            return status;

        }

        //public StatusRevisoesLV ObtemEstadoRevisoes(string guidDOc)
        //{
        //    string api = "api/StatusRevisoesLV/" + guidDOc;
        //    var hndlr = new HttpClientHandler();
        //    hndlr.UseDefaultCredentials = true;

        //    StatusRevisoesLV status = null;

        //    string str_json = consultar(api, hndlr, status);

        //    status = JsonConvert.DeserializeObject<StatusRevisoesLV>(str_json);

        //    return status;

        //}

        public StatusLV ObtemEstadoLV(string guidDOc)
        {
          

            string api = "api/StatusLV/" + guidDOc;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            StatusLV status = null;

            string str_json = consultar(api, hndlr, status);

            status = JsonConvert.DeserializeObject<StatusLV>(str_json);

            return status;

        }

        //public StatusConfirmacoesLV ObtemEstadoConfirmacoes(string guidDOc)
        //{
        //    string api = "api/StatusConfirmacoesLV/" + guidDOc;
        //    var hndlr = new HttpClientHandler();
        //    hndlr.UseDefaultCredentials = true;

        //    StatusConfirmacoesLV status = null;

        //    string str_json = consultar(api, hndlr, status);

        //    status = JsonConvert.DeserializeObject<StatusConfirmacoesLV>(str_json);

        //    return status;

        //}

      

        private ListaVerficacaoVM obtemListaCompleta(string guidDOc)
        {
            
            //    /api/ListaVerificacao/7e5e3cf4-dd1f-4fda-865e-9a7d3d884651


            string api = "api/LVCompleta/" + guidDOc;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            ListaVerficacaoVM listaVerificacaoApp = null;


            string str_json = consultar(api, hndlr, listaVerificacaoApp);

            listaVerificacaoApp = JsonConvert.DeserializeObject<ListaVerficacaoVM>(str_json);



            return listaVerificacaoApp;

        }

        private ListaVerficacaoVM obtemListaSemConfirmacoes(string guidDOc)
        {
           
            //    /api/ListaVerificacao/7e5e3cf4-dd1f-4fda-865e-9a7d3d884651


            string api = "api/LVSemConfirmacoes/" + guidDOc;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            ListaVerficacaoVM listaVerificacaoApp = null;


            string str_json = consultar(api, hndlr, listaVerificacaoApp);

            listaVerificacaoApp = JsonConvert.DeserializeObject<ListaVerficacaoVM>(str_json);



            return listaVerificacaoApp;

        }




        private ListaVerficacaoVM obtemListaSemRevisoes(string guidDOc)
        {
           

            string api = "api/LVInicial/" + guidDOc;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            ListaVerficacaoVM listaVerificacaoApp = null;

            string str_json = consultar(api, hndlr, listaVerificacaoApp);

            listaVerificacaoApp = JsonConvert.DeserializeObject<ListaVerficacaoVM>(str_json);

        

            return listaVerificacaoApp;
        }
    }
}