using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Validator
{
    public class DocViewModelValidator: AbstractValidator<DocViewModel>
    {
        public DocViewModelValidator()
        {

            //Somente inteiro
            RuleFor(x => x.Projeto).NotNull().WithMessage("Campo sem preenchimento");
            RuleFor(x => x.Projeto).Matches(@"^[0-9]{4}$").WithMessage("Use quatro numeros inteiros");

            //Somente inteiro
            RuleFor(x => x.OS).NotNull().WithMessage("Campo sem preenchimento");
            RuleFor(x => x.OS).Matches(@"^[0-9]{3}$").WithMessage("Use tres numeros inteiros");

            //Area aceita letras
            RuleFor(x => x.Area).NotNull().WithMessage("Campo sem preenchimento");
            RuleFor(x => x.Area).Length(4).WithMessage("Use quatro caracteres");

            //Pode letra
            RuleFor(x => x.TipoDocumento).NotNull().WithMessage("Campo sem preenchimento");
            RuleFor(x => x.TipoDocumento).Length(2).WithMessage("Use dois caracteres");

            //Pode letra
            RuleFor(x => x.SiglaDisciplina).NotNull().WithMessage("Campo sem preenchimento");
            RuleFor(x => x.SiglaDisciplina).Length(2).WithMessage("Use dois caracteres");

            //Pode possuir letra do idioma no fim
            RuleFor(x => x.Sequencial).NotNull().WithMessage("Campo sem preenchimento");
            RuleFor(x => x.Sequencial).Length(4,6).WithMessage("Use de quatro a seis caracteres");
        }
    }
}