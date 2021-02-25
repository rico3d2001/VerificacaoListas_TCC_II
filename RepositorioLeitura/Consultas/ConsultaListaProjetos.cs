using Dapper;
using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;

namespace RepositorioLeitura.Consultas
{
    public class ConsultaListaProjetos
    {

     

        public static List<ProjetoToListDTO> ObtemListaProjetos()
        {

            

            List<ProjetoToListDTO> lista = new List<ProjetoToListDTO>();


            string qry = "SELECT"
                        + " swp.lv_projeto.guid as GUID,"
                                + " swp.lv_projeto.numero AS NUMERO"
                                + " FROM"
                        + " swp.lv_projeto";

            using (var conexaoBD = new Conexao())
            {
                    lista = conexaoBD.OracleConnection.Query<ProjetoToListDTO>(qry).ToList();

            }

            return lista;



        }
    }
}
