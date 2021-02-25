using Dapper;
using EntidadesRepositoriosLeitura;
using System.Linq;

namespace RepositorioLeitura.Consultas
{
    public class ConsultaPlanilha
    {
        public static PlanilhaLVVM ObtemPlanilha(string guidPlanilha)
        {
            PlanilhaLVVM planilhaLVVM = null;

            string qryPlanilha = "SELECT "
                    + "swp.lv_disciplina.nome AS NOME_DISICPLINA,"
                    + "swp.lv_configuracao.nome AS CONFIGURACAO,"
                    + "swp.lv_tipo.nome AS TIPO_DOCUMENTO,"
                    + "swp.lv_planilha.nome AS NOME_PLANILHA,"
                    + "swp.lv_planilha.funcao AS FUNCAO,"
                    + "swp.lv_planilha.descricao AS DECRICAO,"
                    + "swp.lv_planilha.guid AS GUID_PLANILHA,"
                    + "swp.lv_disciplina.sigla AS SIGLA_DICIPLINA,"
                    + "swp.lv_grupo.nome AS NOME_GRUPO,"
                    + "swp.lv_grupo.ordenador AS ORDENADOR_GRUPO,"
                    + "swp.lv_item_revisao.guid AS GUID_ITEM,"
                    + "swp.lv_item_revisao.descricao AS DECRICAO_ITEM,"
                    + "swp.lv_item_revisao.ordenador AS ORDENADOR_ITEM,"
                    + "swp.lv_grupo.guid AS GUID_GRUPO"
                    + " FROM "
                    + "swp.lv_disciplina,"
                    + "swp.lv_configuracao,"
                    + "swp.lv_tipo,"
                    + "swp.lv_planilha,"
                    + "swp.lv_grupo,"
                    + "swp.lv_item_revisao"
                    + " WHERE "
                    + "swp.lv_configuracao.id_disciplina = swp.lv_disciplina.id_disciplina"
                    + " AND swp.lv_configuracao.guid = swp.lv_tipo.guid_config"
                    + " AND swp.lv_planilha.guid_tipo = swp.lv_tipo.guid"
                    + " AND swp.lv_grupo.guid_planilha = swp.lv_planilha.guid"
                    + " AND swp.lv_item_revisao.guid_grupo = swp.lv_grupo.guid"
                    + " AND swp.lv_planilha.guid = '"
                    + guidPlanilha + "'";


            using (var conexaoBD = new Conexao())
            {
                var respostaPlanilha = conexaoBD.OracleConnection.Query<PlanilhaQry>(qryPlanilha);

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
