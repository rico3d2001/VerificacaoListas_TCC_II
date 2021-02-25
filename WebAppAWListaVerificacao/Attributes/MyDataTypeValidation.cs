using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebAppAWListaVerificacao.Attributes
{
    public class MyDataTypeValidationAttribute : ValidationAttribute
    {
        private Regex _regex = new Regex(@"^[\w\s.-_]+$");

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_regex.IsMatch(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Solo se permite letras, numeros y puntuaciones(- _ .)");
        }
    }
}