
using EntidadesRepositoriosLeitura;
using System.Linq;
using Dapper;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaPlanilha
    {
        public static PlanilhaLVVM ObtemPlanilha(string guidPlanilha)
        {
            PlanilhaLVVM planilhaLVVM = null;

            string qryPlanilha = "SELECT "
                    + "lv_disciplina.nome AS NOME_DISICPLINA,"
                    + "lv_configuracao.nome AS CONFIGURACAO,"
                    + "lv_tipo.nome AS TIPO_DOCUMENTO,"
                    + "lv_planilha.nome AS NOME_PLANILHA,"
                    + "lv_planilha.funcao AS FUNCAO,"
                    + "lv_planilha.descricao AS DECRICAO,"
                    + "lv_planilha.guid AS GUID_PLANILHA,"
                    + "lv_disciplina.sigla AS SIGLA_DICIPLINA,"
                    + "lv_grupo.nome AS NOME_GRUPO,"
                    + "lv_grupo.ordenador AS ORDENADOR_GRUPO,"
                    + "lv_item_revisao.guid AS GUID_ITEM,"
                    + "lv_item_revisao.descricao AS DECRICAO_ITEM,"
                    + "lv_item_revisao.ordenador AS ORDENADOR_ITEM,"
                    + "lv_grupo.guid AS GUID_GRUPO"
                    + " FROM "
                    + "lv_disciplina,"
                    + "lv_configuracao,"
                    + "lv_tipo,"
                    + "lv_planilha,"
                    + "lv_grupo,"
                    + "lv_item_revisao"
                    + " WHERE "
                    + "lv_configuracao.id_disciplina = lv_disciplina.id_disciplina"
                    + " AND lv_configuracao.guid = lv_tipo.guid_config"
                    + " AND lv_planilha.guid_tipo = lv_tipo.guid"
                    + " AND lv_grupo.guid_planilha = lv_planilha.guid"
                    + " AND lv_item_revisao.guid_grupo = lv_grupo.guid"
                    + " AND lv_planilha.guid = '"
                    + guidPlanilha + "'";


            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaPlanilha = conexaoBD.MySqlConnection.Query<PlanilhaQry>(qryPlanilha);

                if (respostaPlanilha.Count() > 0)
                {



                    var primeiro = respostaPlanilha.First();

                    CabecalhoVM cabecalhoVM = new CabecalhoVM()
                    {
                        Funcao = primeiro.FUNCAO,
                        Titulo = primeiro.NOME_PLANILHA,
                        Disciplina = primeiro.NOME_DISICPLINA,
                        SiglaDisciplina = primeiro.SIGLA_DICIPLINA
                    };

                    planilhaLVVM = new PlanilhaLVVM()
                    {
                        GUID = primeiro.GUID_PLANILHA,
                        CabecalhoApp = cabecalhoVM
                    };

                    planilhaLVVM.Grupos = (from grupo in respostaPlanilha
                                           group grupo by new { grupo.ORDENADOR_GRUPO, grupo.NOME_GRUPO, grupo.GUID_GRUPO } into a
                                           select new GrupoVM()
                                           {
                                               GUID = a.Key.GUID_GRUPO,
                                               ORDENADOR = a.Key.ORDENADOR_GRUPO,
                                               NOME = a.Key.NOME_GRUPO
                                           }).ToList();

                    foreach (var grp in planilhaLVVM.Grupos)
                    {
                        grp.Itens = (from item in respostaPlanilha
                                     where item.GUID_GRUPO == grp.GUID
                                     group item by new { item.GUID_ITEM, item.ORDENADOR_ITEM, item.DECRICAO_ITEM } into a
                                     select new ItemVM()
                                     {
                                         GUID = a.Key.GUID_ITEM,
                                         ORDENADOR = a.Key.ORDENADOR_ITEM,
                                         DESCRICAO = a.Key.DECRICAO_ITEM
                                     }).ToList();
                    }

                }


            }

            return planilhaLVVM;




        }
    }
}
