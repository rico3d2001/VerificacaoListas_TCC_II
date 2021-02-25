using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LV_PresenterAPI.Models
{
    public class DocViewModel
    {
        private string _numeroDocumento;
        private string _siglaDisciplina;
        private string _guid_planilha;
        private string _guidDocumento;
        private string _projeto;
        private string _os;
        private string _area;
        private string _tipo;
        private string _sequencial;

        [Display(Name = "Número do Documento")]
        public string NumeroDocumento { get => _numeroDocumento; set => _numeroDocumento = value; }

        [Display(Name = "Projeto")]
        public string Projeto { get => _projeto; set => _projeto = value; }

        [Display(Name = "OS")]
        [Required(ErrorMessage = "O numero da OS deve ser informado.")]
        [StringLength(3, MinimumLength = 3,ErrorMessage = "Campo deve ter 3 caracteres")]
        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "Use somente 3 números inteiros.")]
        public string OS { get => _os; set => _os = value; }

        [Display(Name = "Área")]
        [Required(ErrorMessage = "O numero da área deve ser informado.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Campo deve ter 4 caracteres")]
        [RegularExpression(@"^[A-Z,0-9]{4}$", ErrorMessage = "Use somente 4 maiusculas e/ou números inteiros.")]
        public string Area { get => _area; set => _area = value; }

        [Display(Name = "Disciplina")]
        public string SiglaDisciplina { get => _siglaDisciplina; set => _siglaDisciplina = value; }

        
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "O tipo de documento deve ser informado.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Campo deve ter 2 caracteres")]
        [RegularExpression(@"^[A-Z,0-9]{2}$", ErrorMessage = "Use somente 2 maiusculas e/ou números inteiros.")]
        public string TipoDocumento { get => _tipo; set => _tipo = value; }

        [Display(Name = "Sequencial")]
        [Required(ErrorMessage = "O número sequencial deve ser informado.")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "Campo deve ter de 4 a 6 caracteres")]
        public string Sequencial { get => _sequencial; set => _sequencial = value; }



        public string GuidPlanilha { get => _guid_planilha; set => _guid_planilha = value; }
        public string GuidDocumento { get => _guidDocumento; set => _guidDocumento = value; }
        
    }
}