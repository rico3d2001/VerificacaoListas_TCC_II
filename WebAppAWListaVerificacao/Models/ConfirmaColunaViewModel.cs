using LVModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAWListaVerificacao.Models
{
    public class ConfirmaColunaViewModel
    {

       

        [Required(ErrorMessage = "O caracter da nova revisão deve ser informado.")]
        [RegularExpression(@"[A-Z,0-9]{1,2}$", ErrorMessage = "Formato não permitido.")]
        [Display(Name = "Caracteres: ")]
        [Remote("ValidaConfirmacaoColuna_PreenchimentoCampos", "ConfirmaRevisoes", AdditionalFields = "GuidDocumento", HttpMethod = "POST", ErrorMessage = "Clique duplo! Aguarde.")]
        //public string Indice { get; set;} 
        public string GuidDocumento { get; set; }

        //public string GuidUsuario { get; set; }

        public bool CriaConfirmacaoUnica { get; set; }

        public bool CriaPrimeiraConfirmacaoDupla { get; set; }
        public bool CriaSegundaConfirmacaoDupla { get; set; }
        public int Ordenador { get; set; }

        public string GuidUltimaConfirmacao { get; set; }

        public bool IsListaConfimacaoDupla { get; set; }

        //public List<Revisao> ListaRevisoesNaoConfirmadas { get; set; }

    }
}