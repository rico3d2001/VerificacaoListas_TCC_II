using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace RepositorioMySQL.Consultas
{

    public class MySQLConsultaConfiguracoesNAV
    {

        public static List<ConfiguracaoNavDTO> ObtemListaCFGs()
        {



            List<ConfiguracaoNavDTO> lista = new List<ConfiguracaoNavDTO>();


            string qry = "SELECT"
                        + " lv_configuracao.guid as GUID,"
                        + " lv_configuracao.nome as NOME,"
                        + " lv_disciplina.sigla as SIGLA_DISCIPLINA"
                        + " FROM"
                        + " listaverificacao.lv_configuracao"
                        + " INNER JOIN lv_disciplina ON lv_configuracao.ID_DISCIPLINA = lv_disciplina.ID_DISCIPLINA;";

            using (var conexaoBD = new ConexaoMySQL())
            {
                lista = conexaoBD.MySqlConnection.Query<ConfiguracaoNavDTO>(qry).ToList();

            }

            return lista;
        }
    }
}
