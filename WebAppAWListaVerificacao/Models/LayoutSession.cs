using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class LayoutSession
    {
        private bool _exibeBUSCAR_DOCUMENTO;
        private bool _exibeACRESCENTAR_REVISAO;
        private bool _exibeSALVAR_ARDOCUMENTO;
        private bool _exibeCONFIRMA_REVISAO;
        private bool _exibeNUMERO_DOCUMENTO;
        private bool _exibeNUMERO_DOCUMENTO_CARREGADO;

        public LayoutSession()
        {
            _exibeBUSCAR_DOCUMENTO = true;
            _exibeACRESCENTAR_REVISAO = false;
            _exibeSALVAR_ARDOCUMENTO = false;
            _exibeCONFIRMA_REVISAO = false;
            _exibeNUMERO_DOCUMENTO = false;
        }

        public void SetConfirmado()
        {
            _exibeCONFIRMA_REVISAO = false;
            _exibeACRESCENTAR_REVISAO = true;
        }

        public void SetPlanilhaSemDocumentoCarregada()
        {
            _exibeNUMERO_DOCUMENTO = true;
            _exibeNUMERO_DOCUMENTO_CARREGADO = false;
        }

        public void SetDocumentoCarregado()
        {
            _exibeNUMERO_DOCUMENTO = false;
            _exibeNUMERO_DOCUMENTO_CARREGADO = true;
        }

        public void SetDocumentoDescarregado()
        {
            _exibeBUSCAR_DOCUMENTO = true;
            _exibeACRESCENTAR_REVISAO = false;
            _exibeSALVAR_ARDOCUMENTO = false;
            _exibeCONFIRMA_REVISAO = false;
            _exibeNUMERO_DOCUMENTO = false;
        }


        public void SetEditandoRevisao()
        {
            _exibeCONFIRMA_REVISAO = false;
            _exibeACRESCENTAR_REVISAO = false;
        }

        public bool ExibeBUSCAR_DOCUMENTO { get => _exibeBUSCAR_DOCUMENTO;  }
        public bool ExibeACRESCENTAR_REVISAO { get => _exibeACRESCENTAR_REVISAO;  }
        public bool ExibeSALVAR_ARDOCUMENTO { get => _exibeSALVAR_ARDOCUMENTO;  }
        public bool ExibeCONFIRMA_REVISAO { get => _exibeCONFIRMA_REVISAO;  }
        public bool ExibeNUMERO_DOCUMENTO { get => _exibeNUMERO_DOCUMENTO;  }
        public bool ExibeNUMERO_DOCUMENTO_CARREGADO { get => _exibeNUMERO_DOCUMENTO_CARREGADO;}
    }
}