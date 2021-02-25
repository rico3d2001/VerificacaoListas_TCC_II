using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppConfigLV.Models
{
    public class DisciplinaViewModel
    {
        Disciplina disciplina;

        public DisciplinaViewModel(Disciplina disciplina)
        {
            this.disciplina = disciplina;
        }

        public int IdDisciplina { get => this.disciplina.ID_DISCIPLINA; } //set => this.disciplina.ID = value; }
        public string NomeDisciplina { get => this.disciplina.NOME; } //set => nomeDisciplina = value; }
        public string SiglaDisciplina { get => this.disciplina.SIGLA; } //set => siglaDisciplina = value; }
    }
}