using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class CabecalhoViewModel
    {
        string _funcao;
        string _titulo;
        string _disciplina;
        string _imagePath;
       
        string _paginaDocumentoVerificado;
        string numeroDocumento;

        //public CabecalhoViewModel(Template template)
        //{
        //    //var plan = new Repository<LV_VIEW_PLANILHA>().ReturnByGUID(template.Guid);

        //    //this.funcao = template.Nome;
        //    //this.titulo = template.Descicao;

        //    ////MELHORAR
        //    //this.disciplina = plan.NOME_DISCIPLINA;

        //    //this.numeroDocumento = "NUMERO DOC. VERIFICADO";
        //}

        //public CabecalhoViewModel()
        //{
        //    _funcao = "FUNÇÃO";
        //    this._titulo = "NOME PLANILHA VERIFICACAO";
        //    this._disciplina = "DISCIPLINA";
        //}



        public string ImagePath { get { return _imagePath; } set { _imagePath = value; }  } 
       

        public string Funcao { get => _funcao; set => _funcao = value; }
        public string Titulo { get => _titulo; set => _titulo = value; }
        public string Disciplina { get => _disciplina; set => _disciplina = value; }
        public string NumeroDocumento { get => numeroDocumento; set => numeroDocumento = value; }
        public string PaginaDocumentoVerificado { get => _paginaDocumentoVerificado; set => _paginaDocumentoVerificado = value; }
    }
}