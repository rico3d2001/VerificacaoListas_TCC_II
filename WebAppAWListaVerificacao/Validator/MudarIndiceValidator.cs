using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Validator
{
    public class MudarIndiceValidator : AbstractValidator<MudaIndiceViewModel>
    {
        public MudarIndiceValidator()
        {
            RuleFor(x => x.Nome).NotNull().WithMessage("Campo sem preenchimento");
            RuleFor(x => x.Nome).Matches(@"[A-Z,0-9]{1,2}$").WithMessage("Formato não permitido.");

        }


     
    }
}