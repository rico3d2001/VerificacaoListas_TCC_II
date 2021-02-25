using LVModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppConfigLV.Models
{

    

    public class UsuarioViewModel
    {
        private string sigla;

        private string nome;
        private bool isVerificador;
        private bool isGestor;
        private bool isConfigurador;
        private string guid;
        private string senha;

        




        public UsuarioViewModel(string guid)
        {
            var user = new Usuario();
            user.SetByGuid(guid);

            this.sigla = user.SIGLA;

            this.nome = user.NOME;
            this.isVerificador = user.IsVerificador;
            this.isGestor = user.IsGestor;
            this.isConfigurador = user.IsConfigurador;
            this.guid = guid;
            this.senha = user.SENHA;

        }

        public UsuarioViewModel()
        { }

        public UsuarioViewModel(Usuario usuario)
        {

            this.nome = usuario.NOME;
            this.sigla = usuario.SIGLA;
            this.isVerificador = usuario.IsVerificador;
            this.isGestor = usuario.IsGestor;
            this.isConfigurador = usuario.IsConfigurador;
            this.guid = usuario.GUID;
            this.senha = usuario.SENHA;
        }

        [Required(ErrorMessage = "O nome do usuário deve ser informado.")]
        public string Nome { get => this.nome; set => this.nome = value; }

        [Required(ErrorMessage = "A sigla do usuário deve ser informada.")]
        public string Sigla { get => this.sigla; set => this.sigla = value; }

        public bool IsVerificador { get => this.isVerificador; set => this.isVerificador = value; }
        public bool IsConfigurador { get => this.isConfigurador; set => this.isConfigurador = value; }
        public bool IsGestor { get => this.isGestor; set => this.isGestor = value; }
        public string GuidUsuario { get => this.guid; set => this.guid = value; }
        public string Senha { get => senha; set => senha = value; }


        internal void PersisteNovo()
        {


            new Usuario(
                this.sigla,
                this.nome,
                this.isVerificador,
                this.isGestor,
                this.isConfigurador);


        }


        internal void Update()
        {
            new Usuario(
                this.sigla,
                this.nome,
                this.isVerificador,
                this.isGestor,
                this.isConfigurador,
                this.guid,
                this.senha);
  
        }




    }
}