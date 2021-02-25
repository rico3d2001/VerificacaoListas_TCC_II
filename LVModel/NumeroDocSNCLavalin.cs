using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVModel
{
    public class NumeroDocSNCLavalin
    {
        string _numeroCompleto;
        string _guid;
        string _sequencial;
        string _numerProjeto;
        string _numeroOs;
        string _numeroArea;
        string _siglaDisciplina;
        string _codigoTipo;
        string _guid_ultima_confirmacao;

        public NumeroDocSNCLavalin(string numeroDoc)
        {
            _numeroCompleto = numeroDoc;
            _numerProjeto = numeroDoc.Split('-')[0];
            _numeroOs = numeroDoc.Split('-')[1];
            _numeroArea = numeroDoc.Split('-')[2];
            _siglaDisciplina = numeroDoc.Split('-')[3].Substring(0, 2);
            _codigoTipo = numeroDoc.Split('-')[3].Substring(2, 2);
            _sequencial = numeroDoc.Split('-')[4];

        }

        public NumeroDocSNCLavalin(Projeto projeto, OS os, Area area, Disciplina disciplina, string codigoGambiarra, string sequencial)
        {
            _numerProjeto = projeto.NUMERO;
            _numeroOs = os.NUMERO;
            _numeroArea = area.NUMERO;
            _siglaDisciplina = disciplina.SIGLA;
            _codigoTipo = codigoGambiarra;
            _sequencial = sequencial;

            _numeroCompleto = _numerProjeto
                + "-" + _numeroOs
                + "-" + _numeroArea
                + "-" + _siglaDisciplina + _codigoTipo
                + "-" + _sequencial;

        }

        public NumeroDocSNCLavalin(string projeto, string os, string area,
            string disciplina, string tipo, string sequencial)
        {
            _numerProjeto = projeto;
            _numeroOs = os;
            _numeroArea = area;
            _siglaDisciplina = disciplina;
            _codigoTipo = tipo;
            _sequencial = sequencial;

            _numeroCompleto = _numerProjeto
                + "-" + _numeroOs
                + "-" + _numeroArea
                + "-" + _siglaDisciplina + _codigoTipo
                + "-" + _sequencial;
        }

        public NumeroDocSNCLavalin() { }

        public virtual string GUID { get => _guid; set => _guid = value; }
        public virtual string NUMERO { get => _numeroCompleto; set => _numeroCompleto = value; }
        public virtual string SEQUENCIAL { get => _sequencial; set => _sequencial = value; }
        public virtual string PROJETO { get => _numerProjeto; set => _numerProjeto = value; }
        public virtual string OS { get => _numeroOs; set => _numeroOs = value; }
        public virtual string AREA { get => _numeroArea; set => _numeroArea = value; }
        public virtual string DISCIPLINA { get => _siglaDisciplina; set => _siglaDisciplina = value; }
        public virtual string TIPO { get => _codigoTipo; set => _codigoTipo = value; }
        public virtual string GUID_ULTIMA_CONFIRMACAO { get => _guid_ultima_confirmacao; set => _guid_ultima_confirmacao = value; }

        public override string ToString()
        {


            string nm = _numerProjeto
                + "-" + _numeroOs
                + "-" + _numeroArea
                + "-" + _siglaDisciplina + _codigoTipo
                + "-" + _sequencial;
            return nm;
        }

       

    }
}
