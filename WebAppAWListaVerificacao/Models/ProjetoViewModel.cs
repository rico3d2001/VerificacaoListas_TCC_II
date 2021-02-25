using AutoMapper;
using LVModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAWListaVerificacao.Models
{
    public class ProjetoViewModel
    {


        string guidProjeto;
        string guidOs;
        string guidArea;
        string guidDisciplina;
        string guidTipo;
        string numeroProjeto;
        string _numeroDesenhoCorrente;
        string _guidDocumento;
        NumeroDocSNCLavalin _numeroDocSNCLavalin;
        //Documento _documento;

        //Documento documentoVerificacao;



        public ProjetoViewModel()
        {

        }

        public ProjetoViewModel(string guidProjeto, string guidOs, string guidArea, string guidDisciplina, string guidTipo)
        {



            this.guidProjeto = guidProjeto;
            this.guidOs = guidOs;
            this.guidArea = guidArea;
            this.guidDisciplina = guidDisciplina;
            this.guidTipo = guidTipo;
        }

        [Display(Name = "OS")]
        public string GuidOs { get => guidOs; set => guidOs = value; }

        [Display(Name = "Projeto")]
        public string GuidProjeto { get => guidProjeto; set => guidProjeto = value; }

        [Display(Name = "Área")]
        public string GuidArea { get => guidArea; set => guidArea = value; }

        [Display(Name = "Disciplina")]
        public string GuidDisciplina { get => guidDisciplina; set => guidDisciplina = value; }

        [Display(Name = "Tipo")]
        public string GuidTipo { get => guidTipo; set => guidTipo = value; }

        [Display(Name = "Sequencial")]
        [MaxLength(5)]
        public string Sequencial { get; set; }

        //public string GetGuidTipo()
        //{
        //    return this.guidTipo;
        //}

        public string NUMERO { get => numeroProjeto; set => numeroProjeto = value; }
        public string GUID { get => guidProjeto; set => guidProjeto = value; }
        public string NumeroDocumentoCorrente { get => _numeroDesenhoCorrente; set => _numeroDesenhoCorrente = value; }
        public string GuidDocumento { get => _guidDocumento; set => _guidDocumento = value; }
        //public Documento Documento { get => _documento; set => _documento = value; }

        //public string GetNumeroDocumentoCorrente()
        //{
        //    return _numeroDesenhoCorrente;
        //}

        //public void SetNumeroDocumentoCorrente(string numero)
        //{
        //     _numeroDesenhoCorrente = numero;
        //}



        //public bool ExistemRevisoesNaoConfimadas { get; set; }
        //public bool ExistemRevisoesNesteDocumento { get; set; }
        public NumeroDocSNCLavalin NumeroDocSNCLavalin { get => _numeroDocSNCLavalin; set => _numeroDocSNCLavalin = value; }
    }




}