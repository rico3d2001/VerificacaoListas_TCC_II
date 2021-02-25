using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using LVModel.ObjetosValor;
using System;
using Unity;

namespace WebAppAWListaVerificacao.Models
{
    public class RegistroVerificacao
    {
        private string indiceRevisao;
        private string guidTipoItem;
        private string guidVerificacao;
        private DateTime data;
        private int ordenador;
        private StatusRevisao tipoRevisao;
        private string guidVerificador;
        private bool confirmado;
        private bool salvo;
        private bool emitido;
        private string _nomeVerificador;


        public RegistroVerificacao(string guidTipoItem, string indiceRevisao, int ordenador, string guidRevisao)
        {
           
            this.guidTipoItem = guidTipoItem;
            guidVerificacao = guidRevisao;
            this.indiceRevisao = indiceRevisao;
            this.ordenador = ordenador;
        }

    

        public RegistroVerificacao(string guidTipoItem, string indiceRevisao, int ordenador, string guidVerificacao, StatusRevisao tipoRevisao, DateTime dateTimeRev, string guidVerificador, int confirmado, int salvo, int emitido)
        {
            this.data = dateTimeRev;
            this.guidVerificador = guidVerificador;
            
            this.guidTipoItem = guidTipoItem;
            this.guidVerificacao = guidVerificacao;
            this.indiceRevisao = indiceRevisao;
            this.ordenador = ordenador;
            this.tipoRevisao = tipoRevisao;

            this.confirmado = confirmado == 0 ? false : true;
            this.salvo = salvo == 0 ? false : true;
            this.emitido = emitido == 0 ? false : true;
        }

        public void SetOrdenador(int ordenador)
        {
            this.ordenador = ordenador;
        }

        public void SetDateTime(DateTime dateTime)
        {
            this.data = dateTime;
        }

        public string GetDataApresenta()
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("pt-BR", true);
            return this.data.ToString("d",culture);
        }

        public string GetLetraStatus()
        {
           return this.tipoRevisao.Name;
        }



        public string nomeVerificadorPelaGuid()
        {
            if (this.guidVerificador == null)
                return "XXX";

            string nome = "XXX";

            using (var contextoRevisao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>())
            {
                contextoRevisao.Start();
                nome = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>().ReturnByGUID(this.guidVerificador).NOME;
            }

            return nome;
        }

        public string GetGuidTipo()
        {
            return this.guidTipoItem;
        }

        public string Verificador { get; set; }

        public string IndiceRevisao { get => this.indiceRevisao; set => this.indiceRevisao = value; }
        public StatusRevisao TipoRevisao { get => this.tipoRevisao; set => this.tipoRevisao = value; }
        public string GuidVerificador { get => guidVerificador; set => guidVerificador = value; }
        public int Ordenador { get => this.ordenador; set => ordenador = value; }
        public bool Confirmado { get => confirmado; set => confirmado = value; }
        public bool Salvo { get => salvo; set => salvo = value; }
        public bool Emitido { get => emitido; set => emitido = value; }
        public string GuidVerificacao { get => guidVerificacao; set => guidVerificacao = value; }
        public string NomeVerificador { get => nomeVerificadorPelaGuid();  }
    }
}