

using System.Collections.Generic;

namespace LVModel
{
    public class ArquivoListas
    {
        

        private string _guid;
        private string _nome;
        //private string _guid_Config;
        private string _sigla;
        private Configuracao _configuracao;
        private IList<Planilha> _listaPlanilhas;

        public ArquivoListas(string guid)
        {



            _guid = guid;

            _listaPlanilhas = new List<Planilha>();

        }

        public ArquivoListas() {

            _listaPlanilhas = new List<Planilha>();
        }


  

      

        public virtual string GUID { get => _guid; set => _guid = value; }
        public virtual string NOME { get => _nome; set => _nome = value; }

        //public virtual string GUID_CONFIG { get => _guid_Config; set => _guid_Config = value; }
        public virtual string SIGLA { get => _sigla; set => _sigla = value; }
        public virtual Configuracao Configuracao { get => _configuracao; set => _configuracao = value; }

        public virtual IList<Planilha> ListaPlanilhas { get => _listaPlanilhas; set => _listaPlanilhas = value; }


    }
}
