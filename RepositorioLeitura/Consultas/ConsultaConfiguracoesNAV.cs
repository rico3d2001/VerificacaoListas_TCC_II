using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace RepositorioLeitura.Consultas
{
    public class ConsultaConfiguracoesNAV
    {

        public static List<ConfiguracaoNavDTO> ObtemListaCFGs()
        {



            List<ConfiguracaoNavDTO> lista = new List<ConfiguracaoNavDTO>();


            string qry = "SELECT"
                        + " swp.lv_configuracao.guid   AS guid,"
                        + " swp.lv_configuracao.nome   AS nome,"
                        + " swp.lv_disciplina.sigla    AS sigla_disciplina"
                        + " FROM"
                        + " swp.lv_configuracao"
                        + " INNER JOIN swp.lv_disciplina ON swp.lv_disciplina.id_disciplina = swp.lv_configuracao.id_disciplina";







            using (var conexaoBD = new Conexao())
            {
                lista = conexaoBD.OracleConnection.Query<ConfiguracaoNavDTO>(qry).ToList();

            }

            return lista;
        }
    }
}
