using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_DISCIPLINA
    {
        private int _id;
        private string _nome;
        private string _sigla;

        //public LV_DISCIPLINA(int id, string nome, string sigla)
        //{
        //    _id = id;
        //    _nome = nome;
        //    _sigla = sigla;
        //}

        [IDAtributo]
        public virtual int ID_DISCIPLINA { get => _id; set => _id = value; }
        public virtual string NOME { get => _nome; set => _nome = value; }
        public virtual string SIGLA { get => _sigla; set => _sigla = value; }
    }
}
