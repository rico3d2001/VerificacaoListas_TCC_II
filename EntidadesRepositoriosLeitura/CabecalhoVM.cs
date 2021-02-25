using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class CabecalhoVM
    {
        string _funcao;
        string _titulo;
        string _disciplina;
        string _siglaDisciplina;
        public string Funcao { get => _funcao; set => _funcao = value; }
        public string Titulo { get => _titulo; set => _titulo = value; }
        public string Disciplina { get => _disciplina; set => _disciplina = value; }
        public string SiglaDisciplina { get => _siglaDisciplina; set => _siglaDisciplina = value; }




    }
}
