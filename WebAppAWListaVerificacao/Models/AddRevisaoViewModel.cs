﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAWListaVerificacao.Models
{
    public class AddRevisaoViewModel
    {
     

        //[Required(ErrorMessage = "O caracter da nova revisão deve ser informado.")]
        //[RegularExpression(@"[A-Z,0-9]{1,2}$", ErrorMessage = "Formato não permitido.")]
        [Display(Name = "Caracteres: ")]
        //[Remote("ValidaAddRevisao", "Lista", AdditionalFields = "GuidDocumento", ErrorMessage = "Coloque uma sigla não usada anteriormente.")]
        public string Nome { get; set; }
        public string GuidDocumento { get; set; }
    }
}