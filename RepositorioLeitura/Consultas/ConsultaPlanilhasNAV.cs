using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace RepositorioLeitura.Consultas
{
    public class ConsultaPlanilhasNAV
    {
        public static List<PlanilhaNavDTO> ObtemPlanilhasNAV(string guidArquivo)
        {
            List<PlanilhaNavDTO> lista = null;

            string qryUser = "SELECT "
                            + "swp.lv_planilha.GUID as GUID,"
                            + "swp.lv_planilha.NOME as NOME,"
                            + "swp.lv_planilha.GUID_TIPO as GUID_TIPO,"
                            + "swp.lv_planilha.REVISAO as REV"
                            + " FROM "
                            + "swp.lv_planilha"
                            + " WHERE "
                            + "swp.lv_planilha.GUID_TIPO = '" + guidArquivo + "'";

            using (var conexaoBD = new Conexao())
            {
                lista = conexaoBD.OracleConnection.Query<PlanilhaNavDTO>(qryUser).ToList();
            }

            return lista;



        }
    }
}
