using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class RegistroRevisao
    {
        private string status;
        private string guidTipoRev;
        private string guid;
        private bool confirmado = false;
        private bool salvo = false;
        private bool emitido = false;
        private string guidConfirmacao;


        public string Status { get => status; set => status = value; }
        public string GuidTipoRev { get => guidTipoRev; set => guidTipoRev = value; }
        public string Guid { get => guid; set => guid = value; }
        public bool Confirmado { get => confirmado; set => confirmado = value; }
        public bool Salvo { get => salvo; set => salvo = value; }
        public bool Emitido { get => emitido; set => emitido = value; }
        public string GuidConfirmacao { get => guidConfirmacao; set => guidConfirmacao = value; }

        public RegistroRevisao(string guidTipoRev)
        {
            this.guidTipoRev = guidTipoRev;
        }

        public RegistroRevisao(string guidTipoRev, string status)
        {
            this.guidTipoRev = guidTipoRev;
            this.status = status;
        }

        //public void AtualizarStatus(string guid, int status)
        //{
        //    Revisao revisao = new Revisao(guid);
        //    revisao.SalvarStatus(status);
        //}



        //internal bool IsConfirmado()
        //{
        //    return Revisao.ChecaConfirmado(this.guid);
        //}
    }
}