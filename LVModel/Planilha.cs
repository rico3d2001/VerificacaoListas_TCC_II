using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVModel
{
    public class Planilha
    {
        string _guid;
        string _nome;
        string _funcao;
        string _descricao;
        int _verificador_unico;
        IList<Grupo> _listaGrupos;

        ArquivoListas _livroListasVerificacao;

        public Planilha(string guidPlanilha)
        {
            _guid = guidPlanilha;
            _listaGrupos = new List<Grupo>();
        }

        public Planilha()
        {
            _listaGrupos = new List<Grupo>();
        }


      

        public virtual string GUID { get => _guid; set => _guid = value; }
        public virtual string NOME { get => _nome; set => _nome = value; }



        public virtual string FUNCAO { get => _funcao; set => _funcao = value; }
        public virtual string DESCRICAO { get => _descricao; set => _descricao = value; }
        public virtual int VERIFICADOR_UNICO { get => _verificador_unico; set => _verificador_unico = value; }
        public virtual ArquivoListas Tipo { get => _livroListasVerificacao; set => _livroListasVerificacao = value; }
        public virtual IList<Grupo> ListaGrupos { get => _listaGrupos; }
        

       
    }
}
