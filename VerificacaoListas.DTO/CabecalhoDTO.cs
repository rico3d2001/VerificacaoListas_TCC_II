using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificacaoListas.DTO
{
    public class CabecalhoDTO
    {
        //LV_VIEW_PLANILHA

        public virtual string GUID { get; set; }
        public virtual string NOME_DISCIPLINA { get; set; }
        public virtual string NOME_CONFIG { get; set; }
        public virtual string NOME_TIPO { get; set; }
        public virtual string NOME { get; set; }
        public virtual string FUNCAO { get; set; }
        public virtual string DESCRICAO { get; set; }
        public virtual string SIGLA_DISCIPLINA { get; set; }



        //public string Funcao { get => _funcao; set => _funcao = value; }
        //public string Titulo { get => _titulo; set => _titulo = value; }
        //public string Disciplina { get => _disciplina; set => _disciplina = value; }
        //public string NumeroDocumento { get => numeroDocumento; set => numeroDocumento = value; }
        //public string PaginaDocumentoVerificado { get => _paginaDocumentoVerificado; set => _paginaDocumentoVerificado = value; }



    }
}
