using FluentValidation.Attributes;
using LVModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAWListaVerificacao.Attributes;
using WebAppAWListaVerificacao.Validator;

namespace WebAppAWListaVerificacao.Models
{
    [Validator(typeof(DocViewModelValidator))]
    public class DocViewModel
    {
        private string _numeroDocumento;
        private string _siglaDisciplina;
        private string _guid_planilha;
        private string _guidDocumento;


        

        //Projeto projeto;

        //[Required(ErrorMessage = "O numero do documento deve ser informado.")]
        [Display(Name = "Número do Documento")]
        //[RegularExpression(@"^[A-Z,0-9]{4}-[A-Z,0-9]{3}-[A-Z,0-9]{4}-[A-Z,0-9]{4}-[A-Z,0-9]{5}$", ErrorMessage = "Numero com formato inadequado.")]
        public string NumeroDocumento { get => _numeroDocumento; set => _numeroDocumento = value; }

        //[Required(ErrorMessage = "O numero do projeto deve ser informado.")]
        [Display(Name = "Projeto")]
        //[RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Digite número inteiro de 4 digitos.")]
        //[Remote("ValidaBuscaDocumento_Projeto", "ValidaBuscaDocumento", HttpMethod = "POST", ErrorMessage = "Selecione um documento existente.")]
        public string Projeto { get; set; }

        //[Required(ErrorMessage = "O numero da OS deve ser informado.")]
        [Display(Name = "OS")]
        //[RegularExpression(@"^[0-9]{3}$", ErrorMessage = "Digite número inteiro de 3 digitos.")]
        //[Remote("ValidaBuscaDocumento_OS", "ValidaBuscaDocumento", HttpMethod = "POST", ErrorMessage = "Selecione um documento existente.")]
        public string OS { get; set; }

        //[Required(ErrorMessage = "O numero da área deve ser informado.")]
        [Display(Name = "Área")]
        //[RegularExpression(@"^[A-Z,0-9]{4}$", ErrorMessage = "Numero com formato inadequado.")]
        //[Remote("ValidaBuscaDocumento_Area", "ValidaBuscaDocumento", HttpMethod = "POST", ErrorMessage = "Selecione um documento existente.")]
        public string Area { get; set; }

        //[Required(ErrorMessage = "a sigla da disciplina deve ser informada.")]
        [Display(Name = "Disciplina")]
        //[RegularExpression(@"^[A-Z,0-9]{2}$", ErrorMessage = "Digite 2 caracteres.")]
        //[Remote("ValidaBuscaDocumento_SiglaDisciplina", "ValidaBuscaDocumento", HttpMethod = "POST", ErrorMessage = "Selecione um documento existente.")]
        public string SiglaDisciplina { get => _siglaDisciplina; set => _siglaDisciplina = value; }

       
        //[Required(ErrorMessage = "A sigla do tipo de documento deve ser informada.")]
        [Display(Name = "Tipo")]
        //[RegularExpression(@"^[A-Z,0-9]{2}$", ErrorMessage = "Digite 2 caracteres.")]
        //[Remote("ValidaBuscaDocumento_TipoDocumento", "ValidaBuscaDocumento", HttpMethod = "POST", ErrorMessage = "Selecione um documento existente.")]
        public string TipoDocumento { get; set; }

        //[Required(ErrorMessage = "O número sequencial deve ser informado.")]
        [Display(Name = "Sequencial")]
        //[RegularExpression(@"^[A-Z,0-9]{4,6}$", ErrorMessage = "Digite número inteiro de 4 a 6 digitos.")]
        //[Remote("ValidaBuscaDocumento", "ValidaBuscaDocumento", HttpMethod = "POST", ErrorMessage = "Preencha numero de documento existente. Somente verificadores criam numeros novos.")]
        public string Sequencial { get; set; }


        public string GuidPlanilha { get => _guid_planilha; set => _guid_planilha = value; }
        public string GuidDocumento { get => _guidDocumento; set => _guidDocumento = value; }
    }
}