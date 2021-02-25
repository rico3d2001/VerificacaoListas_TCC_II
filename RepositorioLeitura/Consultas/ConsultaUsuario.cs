using Dapper;
using LVModel;
using System.Linq;

namespace RepositorioLeitura.Consultas
{
    public class ConsultaUsuario
    {
        public static Usuario ObtemUsuarioPorLogin(string login)
        {
            Usuario usuario = null;

            string qryUser = "SELECT "
                            + "lv_usuario.guid,"
                            + "lv_usuario.nome,"
                            + "lv_usuario.isconfigurador,"
                            + "lv_usuario.isverificador,"
                            + "lv_usuario.isgestor,"
                            + "lv_usuario.sigla,"
                            + "lv_usuario.senha"
                            + " FROM "
                            + "lv_usuario"
                            + " WHERE "
                            + "lv_usuario.sigla = '" + login + "'";

            using (var conexaoBD = new Conexao())
            {
                usuario = conexaoBD.OracleConnection.Query<Usuario>(qryUser).FirstOrDefault();
            }

            return usuario;



        }
    }
}
