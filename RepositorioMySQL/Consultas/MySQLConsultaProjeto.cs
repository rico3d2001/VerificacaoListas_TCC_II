
using EntidadesRepositoriosLeitura;
using System.Linq;
using Dapper;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaProjeto
    {
        public static ProjetoVM ObtemProjeto(string guidProjeto)
        {
            ProjetoVM projetoApp = null;

            string qryProjeto = "SELECT"
                     + " " + "lv_projeto.guid AS guid,"
                     + "lv_projeto.numero AS numero_projeto,"
                     + "lv_os.guid AS guid_os,"
                     + "lv_os.numero AS numero_os,"
                     + "lv_area.guid AS guid_area,"
                     + "lv_area.numero AS numero_area"
                     + " " + "FROM"
                     + " " + "lv_projeto"
                     + " " + "INNER JOIN lv_os ON lv_os.guid_projeto = lv_projeto.guid"
                     + " " + "INNER JOIN lv_area ON lv_area.guid_projeto = lv_projeto.guid"
                     + " " + "WHERE"
                     + " " + "lv_projeto.guid = '"
                     + guidProjeto
                     + "'";

            string qryTipos = "SELECT lv_numero_snc.tipo as GUID, lv_numero_snc.tipo as CODIGO FROM lv_numero_snc GROUP BY lv_numero_snc.tipo";

            string qryDisciplinas = "SELECT lv_disciplina.id_disciplina AS ID_DISCIPLINA,lv_disciplina.nome AS NOME,lv_disciplina.sigla AS SIGLA FROM lv_disciplina";


            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaProjeto = conexaoBD.MySqlConnection.Query<ProjetoQry>(qryProjeto);

                if (respostaProjeto.Count() > 0)
                {

                    var primeiro = respostaProjeto.First();

                    projetoApp = new ProjetoVM()
                    {
                        GUID = primeiro.guid,
                        NUMERO = primeiro.numero_projeto
                    };

                    //var areas = resp.GroupBy(x => x.guid_area).ToList();

                    projetoApp.Areas = (from area in respostaProjeto
                                        group area by new { area.guid_area, area.numero_area } into a
                                        select new AreaVM()
                                        {
                                            GUID = a.Key.guid_area,
                                            NUMERO = a.Key.numero_area
                                        }).OrderBy(x => x.NUMERO).ToList();

                    projetoApp.OSs = (from os in respostaProjeto
                                      group os by new { os.guid_os, os.numero_os } into o
                                      select new OSVM()
                                      {
                                          GUID = o.Key.guid_os,
                                          NUMERO = o.Key.numero_os
                                      }).OrderBy(x => x.NUMERO).ToList();



                    projetoApp.Tipos = conexaoBD.MySqlConnection.Query<TipoLVVM>(qryTipos).OrderBy(x => x.CODIGO).ToList();

                    projetoApp.Disciplinas = conexaoBD.MySqlConnection.Query<DisciplinaVM>(qryDisciplinas).OrderBy(x => x.SIGLA).ToList();

                    //var listaContexto = TipoLVVM.GetTiposDocumentos(projeto);

                }
            }





            return projetoApp;
        }



    }
}
