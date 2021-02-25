using Dapper;
using EntidadesRepositoriosLeitura;
using System.Linq;

namespace RepositorioLeitura.Consultas
{
    public class ConsultaProjeto
    {
        public static ProjetoVM ObtemProjeto(string guidProjeto)
        {
            ProjetoVM projetoApp = null;

            string qryProjeto = "SELECT"
                     + " " + "swp.lv_projeto.guid AS guid,"
                     + "swp.lv_projeto.numero AS numero_projeto,"
                     + "swp.lv_os.guid AS guid_os,"
                     + "swp.lv_os.numero AS numero_os,"
                     + "swp.lv_area.guid AS guid_area,"
                     + "swp.lv_area.numero AS numero_area"
                     + " " + "FROM"
                     + " " + "swp.lv_projeto"
                     + " " + "INNER JOIN swp.lv_os ON swp.lv_os.guid_projeto = swp.lv_projeto.guid"
                     + " " + "INNER JOIN swp.lv_area ON swp.lv_area.guid_projeto = swp.lv_projeto.guid"
                     + " " + "WHERE"
                     + " " + "swp.lv_projeto.guid = '"
                     + guidProjeto
                     + "'";

            string qryTipos = "SELECT lv_numero_snc.tipo as GUID, lv_numero_snc.tipo as CODIGO FROM lv_numero_snc GROUP BY lv_numero_snc.tipo";

            string qryDisciplinas = "SELECT swp.lv_disciplina.id_disciplina AS ID_DISCIPLINA,swp.lv_disciplina.nome AS NOME,swp.lv_disciplina.sigla AS SIGLA FROM swp.lv_disciplina";


            using (var conexaoBD = new Conexao())
            {
                var respostaProjeto = conexaoBD.OracleConnection.Query<ProjetoQry>(qryProjeto);

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



                    projetoApp.Tipos = conexaoBD.OracleConnection.Query<TipoLVVM>(qryTipos).OrderBy(x => x.CODIGO).ToList();

                    projetoApp.Disciplinas = conexaoBD.OracleConnection.Query<DisciplinaVM>(qryDisciplinas).OrderBy(x => x.SIGLA).ToList();

                    //var listaContexto = TipoLVVM.GetTiposDocumentos(projeto);

                }
            }





            return projetoApp;
        }



    }
}
