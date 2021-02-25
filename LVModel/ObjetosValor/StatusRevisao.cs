using LVModel.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVModel.ObjetosValor
{
    public class StatusRevisao: Enumeration
    {
        public static StatusRevisao Correto = new StatusRevisao(1,"V");
        public static StatusRevisao NaoDisponivel = new StatusRevisao(2, "ND");
        public static StatusRevisao NaoAplicavel = new StatusRevisao(3, "NA");
        public static StatusRevisao Errado = new StatusRevisao(4, "X");
        public static StatusRevisao Indefinido = new StatusRevisao(5, "I");

        public StatusRevisao(int id, string name) : base(id, name)
        {

        }

        public static StatusRevisao ObtemStatusRevisao(int idStatus)
        {
            switch (idStatus)
            {
                case 1:
                    return StatusRevisao.Correto;
                case 2:
                    return StatusRevisao.NaoDisponivel;
                case 3:
                    return StatusRevisao.NaoAplicavel;
                case 4:
                    return StatusRevisao.Errado;
                case 5:
                    return StatusRevisao.Indefinido;
                default:
                    return StatusRevisao.Indefinido;
            }
        }

        public static StatusRevisao ObtemStatusRevisao(string nome)
        {
            switch (nome)
            {
                case "V":
                    return StatusRevisao.Correto;
                case "ND":
                    return StatusRevisao.NaoDisponivel;
                case "NA":
                    return StatusRevisao.NaoAplicavel;
                case "X":
                    return StatusRevisao.Errado;
                case "I":
                    return StatusRevisao.Indefinido;
                default:
                    return StatusRevisao.Indefinido;
            }
        }


    }
}
