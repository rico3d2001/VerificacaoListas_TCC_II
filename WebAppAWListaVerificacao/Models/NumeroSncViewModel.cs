using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using LVModel;
using System.Web.Mvc;
using WebAppAWListaVerificacao.Attributes;

namespace WebAppAWListaVerificacao.Models
{
    public class NumeroSncViewModel
    {
        [Display(Name = "Documento")]
        public string NUMERO { get; set; }
        
        [Display(Name = "Ultima Revisão")]
        public string IndiceUltimaRevisao { get; set; }

        [Display(Name = "Verificador 1")]
        public string Confirmacao1 { get; set; }

        [Display(Name = "Verificador 2")]
        public string Confirmacao2 { get; set; }

        [Display(Name = "Data")]
        public string Data { get; set; }
    }
}