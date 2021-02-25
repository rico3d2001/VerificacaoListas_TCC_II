using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Comandos;
using LV_PresenterAPI.Consultas;
using RepositorioMongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LV_PresenterAPI.Controllers
{
    public class CorfirmarRevisoes
    {
        private bool abriu_e_nao_confirmou_ainda;
        private static CorfirmarRevisoes _instancia;

        private CorfirmarRevisoes()
        {
            abriu_e_nao_confirmou_ainda = true;
        }

        public static CorfirmarRevisoes Instancia {
            get{
                if(_instancia == null)
                {
                    _instancia = new CorfirmarRevisoes();
                }
                return _instancia;
            }
        }


        public void Corfirmar(string login, bool verificadorUnico, string guidLV)
        {

            

            int tentativa = 0;
            if (verificadorUnico && abriu_e_nao_confirmou_ainda)
            {


                var usuario = new QryUsuario().ObtemUsuario(login);

                new LV_NoSQL().ConfirmacaoRevisaoVM(guidLV, usuario);

                //CmdConfirmacaoRevisao.Instancia().ConfirmaViewModel(new ValoresConfirma(
                //guidLV,
                //verificadorUnico,
                //usuario.GUID,
                //Guid.NewGuid().ToString(),
                //QryListaVerificacao.Instancia(guidLV).ObtemEstadoRevisoes().Indices.Last(),
                //usuario.NOME));


                abriu_e_nao_confirmou_ainda = false;

            }
            else if (!verificadorUnico && abriu_e_nao_confirmou_ainda)
            {

                var lista = QryListaVerificacao.Instancia(guidLV).ListaVerificacaoApp;

                var ultimaConfirmacao = lista.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).Last();



                if (string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER1))
                {
                    var usuario = new QryUsuario().ObtemUsuario(login);
                    new LV_NoSQL().ConfirmacaoRevisaoVM(guidLV, usuario);
                    //  var cols = new LV_NoSQL().ConfirmacaoRevisaoVM(new ValoresConfirma(
                    //guidLV,
                    //verificadorUnico,
                    //usuario.GUID,
                    //Guid.NewGuid().ToString(),
                    //QryListaVerificacao.Instancia(guidLV).ObtemEstadoRevisoes().Indices.Last(),
                    //usuario.NOME)).Colunas.OrderBy(x => x.ORDENADOR).Last();

                    abriu_e_nao_confirmou_ainda = false;

                }
                else if (!string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER1)
                    && string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER2)
                    && abriu_e_nao_confirmou_ainda)
                {

                    var usuario = new QryUsuario().ObtemUsuario(login);
                    new LV_NoSQL().ConfirmacaoRevisaoVM(guidLV, usuario);
                    //var cols = new LV_NoSQL().ConfirmacaoRevisaoVM(new ValoresConfirma(
                    //      guidLV,
                    //      verificadorUnico,
                    //      usuario.GUID,
                    //      Guid.NewGuid().ToString(),
                    //      QryListaVerificacao.Instancia(guidLV).ObtemEstadoRevisoes().Indices.Last(),
                    //      usuario.NOME)).Colunas.OrderBy(x => x.ORDENADOR).Last();


                    //CmdConfirmacaoRevisao.Instancia().ConfirmaViewModel(new ValoresConfirma(
                    //guidLV,
                    //verificadorUnico,
                    //usuario.GUID,
                    //Guid.NewGuid().ToString(),
                    //QryListaVerificacao.Instancia(guidLV).ObtemEstadoRevisoes().Indices.Last(),
                    //usuario.NOME));
                }









            }

            
        }
    }
}