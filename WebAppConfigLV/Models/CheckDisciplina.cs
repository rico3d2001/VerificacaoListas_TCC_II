using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppConfigLV.Models
{
    public class CheckDisciplina
    {
        bool check;
        string nome;
        string sigla;
        string guidUsuario;
        int idDisciplina;

        public CheckDisciplina(bool check, string nome, string guidUser, int idDisciplina, string sigla)
        {
            this.check = check;
            this.nome = nome;
            this.guidUsuario = guidUser;
            this.idDisciplina = idDisciplina;
            this.sigla = sigla;
        }

        public bool Check { get => check; set => check = value; }
        public string Nome { get => nome; set => nome = value; }
        public string GuidUsuario { get => guidUsuario; set => guidUsuario = value; }
        public int IdDisciplina { get => idDisciplina; set => idDisciplina = value; }
        public string Sigla { get => sigla; set => sigla = value; }
    }
}