using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LVModel;

namespace WebAppConfigLV.Models
{
    public class UserDisciplinaViewModel
    {
        string guidUsuario;
        string nomeUsuario;
        string siglaUsuario;
        List<CheckDisciplina> listaChecks;
        

        public UserDisciplinaViewModel(string guidUser)
        {
            listaChecks = new List<CheckDisciplina>();
            List<RelacaoUsuarioDisciplina> listaRelacoes = RelacaoUsuarioDisciplina.ListaByUser(guidUser);
            List<Disciplina> listaDisciplinas = new ListaDisciplinas().Disciplinas();

            foreach (var item in listaDisciplinas)
            {
                bool check = false;

                var d = listaRelacoes.FirstOrDefault(x => x.GuidUsuario == guidUser && x.IdDisciplina == item.ID_DISCIPLINA);

                if (d != null) check = true;

                listaChecks.Add(new CheckDisciplina(check, item.NOME, guidUser, item.ID_DISCIPLINA, item.SIGLA));


            }


        }


        public string GuidUsuario { get => guidUsuario; set => guidUsuario = value; }
        public string NomeUsuario { get => nomeUsuario; set => nomeUsuario = value; }
        public string SiglaUsuario { get => siglaUsuario; set => siglaUsuario = value; }
        public List<CheckDisciplina> ListaChecks { get => listaChecks; set => listaChecks = value; }

        internal void Altera(int idDisciplina)
        {
            var check = listaChecks.Find(x => x.IdDisciplina == idDisciplina);

            RelacaoUsuarioDisciplina.Altera(check.IdDisciplina, check.GuidUsuario, check.Check);

        }
    }
}