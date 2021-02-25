using Dapper;
using EntidadesRepositoriosLeitura;
using System.Linq;

namespace RepositorioLeitura.Consultas
{
    public class ConsultaListaVerificacao
    {

        public static ListaVerficacaoVM ObtemListaSemRevisoes(string guidDocumento)
        {
            ListaVerficacaoVM listaVerificacao = null;

            string qryLV = "SELECT "
                + "swp.lv_doc.guid AS guid_doc,"
                + "swp.lv_doc.objeto AS objeto,"
                + "swp.lv_doc.doc_verificado AS numerosnc,"
                + "swp.lv_doc.guid_projeto AS guid_projeto,"
                + "swp.lv_item_revisao.guid AS guid_item,"
                + "swp.lv_item_revisao.descricao AS item_desc,"
                + "swp.lv_item_revisao.ordenador AS item_ordenador,"
                + "swp.lv_grupo.nome AS grupo_nome,"
                + "swp.lv_grupo.ordenador AS grupo_ordenador,"
                + "swp.lv_grupo.guid AS guid_grupo,"
                + "swp.lv_planilha.verificador_unico AS verficador_unico,"
                + "swp.lv_planilha.revisao AS revisao_planilha,"
                + "swp.lv_planilha.funcao AS funcao,"
                + "swp.lv_planilha.nome AS nome_planilha,"
                + "swp.lv_planilha.guid AS guid_planilha,"
                + "swp.lv_planilha.descricao AS planilha_desc,"
                + "swp.lv_doc.numero AS numerodoc,"
                + "swp.lv_tipo.guid_config AS guid_config,"
                + "swp.lv_tipo.sigla AS sigla_disciplina,"
                + "swp.lv_tipo.nome AS nome_tipo,"
                + "swp.lv_configuracao.nome AS nome_cfg"
                + " FROM swp.lv_doc"
                + " INNER JOIN swp.lv_planilha ON swp.lv_doc.objeto = swp.lv_planilha.guid"
                + " INNER JOIN swp.lv_grupo ON swp.lv_grupo.guid_planilha = swp.lv_planilha.guid"
                + " INNER JOIN swp.lv_item_revisao ON swp.lv_item_revisao.guid_grupo = swp.lv_grupo.guid"
                + " INNER JOIN swp.lv_tipo ON swp.lv_planilha.guid_tipo = swp.lv_tipo.guid"
                + " INNER JOIN swp.lv_configuracao ON swp.lv_tipo.guid_config = swp.lv_configuracao.guid"
                + " WHERE "
                + "swp.lv_doc.guid = '" + guidDocumento + "'";


            using (var conexaoBD = new Conexao())
            {
                var respostaPlanilha = conexaoBD.OracleConnection.Query<ListaVerficacaoQry>(qryLV);

                if (respostaPlanilha.Count() > 0)
                {



                    var primeiro = respostaPlanilha.First();

                    CabecalhoVM cabecalhoVM = new CabecalhoVM()
                    {
                        Funcao = primeiro.FUNCAO,
                        Titulo = primeiro.NOME_PLANILHA,
                        Disciplina = primeiro.NOME_CFG,

                    };




                    listaVerificacao = new ListaVerficacaoVM()
                    {
                        GUID = primeiro.GUID_DOC,
                        NUMERODOC = primeiro.NUMEROSNC,
                        VERFICADOR_UNICO = primeiro.VERFICADOR_UNICO,
                        CabecalhoApp = cabecalhoVM
                    };

                    listaVerificacao.Confirmacoes = (from conf in respostaPlanilha
                                                     group conf by new
                                                     {
                                                         conf.CONFIRMACAO_GUID,
                                                         conf.CONFIRMACAO_INDICE,
                                                         conf.CONFIRMACAO_DATA,
                                                         conf.CONFIRMACAO_ID_USER1,
                                                         conf.CONFIRMACAO_SIGLA_USER1,
                                                         conf.CONFIRMACAO_NOME_USER1,
                                                         conf.CONFIRMACAO_ID_USER2,
                                                         conf.CONFIRMACAO_SIGLA_USER2,
                                                         conf.CONFIRMACAO_NOME_USER2,
                                                         conf.CONFIRMACAO_ORDENADOR
                                                     } into a
                                                     select new ConfirmacaoVM()
                                                     {
                                                         CONFIRMACAO_GUID = a.Key.CONFIRMACAO_GUID,
                                                         CONFIRMACAO_INDICE = a.Key.CONFIRMACAO_INDICE,
                                                         CONFIRMACAO_DATA = a.Key.CONFIRMACAO_DATA,
                                                         CONFIRMACAO_ID_USER1 = a.Key.CONFIRMACAO_ID_USER1,
                                                         CONFIRMACAO_SIGLA_USER1 = a.Key.CONFIRMACAO_SIGLA_USER1,
                                                         CONFIRMACAO_NOME_USER1 = a.Key.CONFIRMACAO_NOME_USER1,
                                                         CONFIRMACAO_ID_USER2 = a.Key.CONFIRMACAO_ID_USER2,
                                                         CONFIRMACAO_SIGLA_USER2 = a.Key.CONFIRMACAO_SIGLA_USER2,
                                                         CONFIRMACAO_NOME_USER2 = a.Key.CONFIRMACAO_NOME_USER2,
                                                         CONFIRMACAO_ORDENADOR = a.Key.CONFIRMACAO_ORDENADOR
                                                     }).OrderBy(x => x.CONFIRMACAO_ORDENADOR).ToList();



                    listaVerificacao.Colunas = (from col in respostaPlanilha
                                                group col by new { col.indice, col.ordenador } into a
                                                select new ColunaLVVM()
                                                {
                                                    INDICE_REV = a.Key.indice,
                                                    ORDENADOR = a.Key.ordenador
                                                }).OrderBy(x => x.ORDENADOR).ToList();

                    foreach (var coluna in listaVerificacao.Colunas)
                    {
                        coluna.LV_Grupos = (from grupo in respostaPlanilha
                                            where grupo.indice == coluna.INDICE_REV
                                            group grupo by new { grupo.GRUPO_ORDENADOR, grupo.GRUPO_NOME, grupo.GUID_GRUPO } into a
                                            select new LV_GrupoVM()
                                            {
                                                GUID = a.Key.GUID_GRUPO,
                                                ORDENADOR = a.Key.GRUPO_ORDENADOR,
                                                NOME = a.Key.GRUPO_NOME
                                            }).OrderBy(x => x.ORDENADOR).ToList();



                        foreach (var grp in coluna.LV_Grupos)
                        {
                            grp.Linhas = (from linha in respostaPlanilha
                                          where linha.GUID_GRUPO == grp.GUID && linha.indice == coluna.INDICE_REV
                                          group linha by new
                                          {
                                              linha.GUID_ITEM,
                                              linha.ITEM_ORDENADOR,
                                              linha.ITEM_DESC,
                                              linha.id_estado,
                                              linha.is_confirmado,
                                              linha.is_emitido,
                                              linha.guid_revisao
                                          } into a
                                          select new LinhaRevisaoVM()
                                          {
                                              GUID_REVISAO = a.Key.guid_revisao,
                                              GUID_ITEM = a.Key.GUID_ITEM,
                                              ORDENADOR = a.Key.ITEM_ORDENADOR,
                                              DESCRICAO = a.Key.ITEM_DESC,
                                              ID_ESTADO = a.Key.id_estado,
                                              EMITIDO = a.Key.is_emitido,
                                              CONFIRMADO = a.Key.is_confirmado
                                          }).OrderBy(x => x.ORDENADOR).ToList();
                        }
                    }





                }


            }


            return listaVerificacao;

        }

        public static ListaVerficacaoVM ObtemListaSemConfirmacoes(string guidDocumento)
        {
            ListaVerficacaoVM listaVerificacao = null;

            string qryLV = "SELECT "
                                + "swp.lv_doc.guid AS guid_doc,"
                                + "swp.lv_doc.objeto AS objeto,"
                                + "swp.lv_doc.doc_verificado AS numerosnc,"
                                + "swp.lv_doc.guid_projeto AS guid_projeto,"
                                + "swp.lv_revisao.indice AS indice,"
                                + "swp.lv_revisao.ordenador AS ordenador,"
                                + "swp.lv_revisao.confirmado AS is_confirmado,"
                                + "swp.lv_revisao.guid_confirmado AS guid_confirmado,"
                                + "swp.lv_revisao.id_estado AS id_estado,"
                                + "swp.lv_revisao.data_vericacao AS data,"
                                + "swp.lv_revisao.guid AS guid_revisao,"
                                + "swp.lv_revisao.guid_lv_verificador AS guid_verificador,"
                                + "swp.lv_revisao.salvo AS is_salvo,"
                                + "swp.lv_revisao.emitido AS is_emitido,"
                                + "swp.lv_item_revisao.guid AS guid_item,"
                                + "swp.lv_item_revisao.descricao AS item_desc,"
                                + "swp.lv_item_revisao.ordenador AS item_ordenador,"
                                + "swp.lv_grupo.nome AS grupo_nome,"
                                + "swp.lv_grupo.ordenador AS grupo_ordenador,"
                                + "swp.lv_grupo.guid AS guid_grupo,"
                                + "swp.lv_planilha.verificador_unico AS verficador_unico,"
                                + "swp.lv_planilha.revisao AS revisao_planilha,"
                                + "swp.lv_planilha.funcao AS funcao,"
                                + "swp.lv_planilha.nome AS nome_planilha,"
                                + "swp.lv_planilha.guid AS guid_planilha,"
                                + "swp.lv_planilha.descricao AS planilha_desc,"
                                + "swp.lv_doc.numero AS numerodoc,"
                                + "swp.lv_tipo.guid_config AS guid_config,"
                                + "swp.lv_tipo.sigla AS sigla_disciplina,"
                                + "swp.lv_tipo.nome AS nome_tipo,"
                                + "swp.lv_configuracao.nome AS nome_cfg"
                                + " FROM swp.lv_doc"
                                + " INNER JOIN swp.lv_revisao ON swp.lv_revisao.guid_doc_verificacao = swp.lv_doc.guid"
                                + " INNER JOIN swp.lv_item_revisao ON swp.lv_revisao.guid_lv_item = swp.lv_item_revisao.guid"
                                + " INNER JOIN swp.lv_grupo ON swp.lv_item_revisao.guid_grupo = swp.lv_grupo.guid"
                                + " INNER JOIN swp.lv_planilha ON swp.lv_grupo.guid_planilha = swp.lv_planilha.guid"
                                + " INNER JOIN swp.lv_tipo ON swp.lv_planilha.guid_tipo = swp.lv_tipo.guid"
                                + " INNER JOIN swp.lv_configuracao ON swp.lv_tipo.guid_config = swp.lv_configuracao.guid"
                                + " WHERE "
                                + "swp.lv_doc.guid = '" + guidDocumento + "'";


            using (var conexaoBD = new Conexao())
            {
                var respostaPlanilha = conexaoBD.OracleConnection.Query<ListaVerficacaoQry>(qryLV);

                if (respostaPlanilha.Count() > 0)
                {



                    var primeiro = respostaPlanilha.First();

                    CabecalhoVM cabecalhoVM = new CabecalhoVM()
                    {
                        Funcao = primeiro.FUNCAO,
                        Titulo = primeiro.NOME_PLANILHA,
                        Disciplina = primeiro.NOME_CFG,

                    };




                    listaVerificacao = new ListaVerficacaoVM()
                    {
                        GUID = primeiro.GUID_DOC,
                        NUMERODOC = primeiro.NUMEROSNC,
                        VERFICADOR_UNICO = primeiro.VERFICADOR_UNICO,
                        CabecalhoApp = cabecalhoVM
                    };

                    listaVerificacao.Confirmacoes = (from conf in respostaPlanilha
                                                     group conf by new
                                                     {
                                                         conf.CONFIRMACAO_GUID,
                                                         conf.CONFIRMACAO_INDICE,
                                                         conf.CONFIRMACAO_DATA,
                                                         conf.CONFIRMACAO_ID_USER1,
                                                         conf.CONFIRMACAO_SIGLA_USER1,
                                                         conf.CONFIRMACAO_NOME_USER1,
                                                         conf.CONFIRMACAO_ID_USER2,
                                                         conf.CONFIRMACAO_SIGLA_USER2,
                                                         conf.CONFIRMACAO_NOME_USER2,
                                                         conf.CONFIRMACAO_ORDENADOR
                                                     } into a
                                                     select new ConfirmacaoVM()
                                                     {
                                                         CONFIRMACAO_GUID = a.Key.CONFIRMACAO_GUID,
                                                         CONFIRMACAO_INDICE = a.Key.CONFIRMACAO_INDICE,
                                                         CONFIRMACAO_DATA = a.Key.CONFIRMACAO_DATA,
                                                         CONFIRMACAO_ID_USER1 = a.Key.CONFIRMACAO_ID_USER1,
                                                         CONFIRMACAO_SIGLA_USER1 = a.Key.CONFIRMACAO_SIGLA_USER1,
                                                         CONFIRMACAO_NOME_USER1 = a.Key.CONFIRMACAO_NOME_USER1,
                                                         CONFIRMACAO_ID_USER2 = a.Key.CONFIRMACAO_ID_USER2,
                                                         CONFIRMACAO_SIGLA_USER2 = a.Key.CONFIRMACAO_SIGLA_USER2,
                                                         CONFIRMACAO_NOME_USER2 = a.Key.CONFIRMACAO_NOME_USER2,
                                                         CONFIRMACAO_ORDENADOR = a.Key.CONFIRMACAO_ORDENADOR
                                                     }).OrderBy(x => x.CONFIRMACAO_ORDENADOR).ToList();



                    listaVerificacao.Colunas = (from col in respostaPlanilha
                                                group col by new { col.indice, col.ordenador } into a
                                                select new ColunaLVVM()
                                                {
                                                    INDICE_REV = a.Key.indice,
                                                    ORDENADOR = a.Key.ordenador
                                                }).OrderBy(x => x.ORDENADOR).ToList();

                    foreach (var coluna in listaVerificacao.Colunas)
                    {
                        coluna.LV_Grupos = (from grupo in respostaPlanilha
                                            where grupo.indice == coluna.INDICE_REV
                                            group grupo by new { grupo.GRUPO_ORDENADOR, grupo.GRUPO_NOME, grupo.GUID_GRUPO } into a
                                            select new LV_GrupoVM()
                                            {
                                                GUID = a.Key.GUID_GRUPO,
                                                ORDENADOR = a.Key.GRUPO_ORDENADOR,
                                                NOME = a.Key.GRUPO_NOME
                                            }).OrderBy(x => x.ORDENADOR).ToList();



                        foreach (var grp in coluna.LV_Grupos)
                        {
                            grp.Linhas = (from linha in respostaPlanilha
                                          where linha.GUID_GRUPO == grp.GUID && linha.indice == coluna.INDICE_REV
                                          group linha by new
                                          {
                                              linha.GUID_ITEM,
                                              linha.ITEM_ORDENADOR,
                                              linha.ITEM_DESC,
                                              linha.id_estado,
                                              linha.is_confirmado,
                                              linha.is_emitido,
                                              linha.guid_revisao
                                          } into a
                                          select new LinhaRevisaoVM()
                                          {
                                              GUID_REVISAO = a.Key.guid_revisao,
                                              GUID_ITEM = a.Key.GUID_ITEM,
                                              ORDENADOR = a.Key.ITEM_ORDENADOR,
                                              DESCRICAO = a.Key.ITEM_DESC,
                                              ID_ESTADO = a.Key.id_estado,
                                              EMITIDO = a.Key.is_emitido,
                                              CONFIRMADO = a.Key.is_confirmado
                                          }).OrderBy(x => x.ORDENADOR).ToList();
                        }
                    }





                }


            }


            return listaVerificacao;

        }

        public static ListaVerficacaoVM ObtemListaCompleta(string guidDocumento)
        {
            ListaVerficacaoVM listaVerificacao = null;

            string qryLV = "SELECT "
                        + "swp.lv_doc.guid AS GUID_DOC,"
                        + "swp.lv_doc.objeto AS OBJETO,"
                        + "swp.lv_doc.doc_verificado AS NUMEROSNC,"
                        + "swp.lv_doc.guid_projeto AS guid_projeto,"
                        + "swp.lv_revisao.indice AS indice,"
                        + "swp.lv_revisao.ordenador AS ordenador,"
                        + "swp.lv_revisao.confirmado AS is_confirmado,"
                        + "swp.lv_revisao.guid_confirmado AS guid_confirmado,"
                        + "swp.lv_revisao.id_estado AS id_estado,"
                        + "swp.lv_revisao.data_vericacao AS data,"
                        + "swp.lv_revisao.guid AS guid_revisao,"
                        + "swp.lv_revisao.guid_lv_verificador AS guid_verificador,"
                        + "swp.lv_revisao.salvo AS is_salvo,"
                        + "swp.lv_revisao.emitido AS is_emitido,"
                        + "swp.lv_item_revisao.guid AS GUID_ITEM,"
                        + "swp.lv_item_revisao.descricao AS ITEM_DESC,"
                        + "swp.lv_item_revisao.ordenador AS ITEM_ORDENADOR,"
                        + "swp.lv_grupo.nome AS GRUPO_NOME,"
                        + "swp.lv_grupo.ordenador AS GRUPO_ORDENADOR,"
                        + "swp.lv_grupo.guid AS GUID_GRUPO,"
                        + "swp.lv_planilha.verificador_unico AS VERFICADOR_UNICO,"
                        + "swp.lv_planilha.revisao AS REVISAO_PLANILHA,"
                        + "swp.lv_planilha.funcao AS FUNCAO,"
                        + "swp.lv_planilha.nome AS NOME_PLANILHA,"
                        + "swp.lv_planilha.guid AS GUID_PLANILHA,"
                        + "swp.lv_planilha.descricao AS PLANILHA_DESC,"
                        + "swp.lv_doc.numero AS NUMERODOC,"
                        + "swp.lv_tipo.guid_config AS GUID_CONFIG,"
                        + "swp.lv_tipo.sigla AS SIGLA_DISCIPLINA,"
                        + "swp.lv_tipo.nome AS NOME_TIPO,"
                        + "swp.lv_configuracao.nome AS NOME_CFG,"
                        + "swp.lv_confirmacao.indice_rev AS CONFIRMACAO_INDICE,"
                        + "swp.lv_confirmacao.data AS CONFIRMACAO_DATA,"
                        + "swp.lv_confirmacao.ordenador AS CONFIRMACAO_ORDENADOR,"
                        + "swp.lv_confirmacao.guid_usuario1 AS CONFIRMACAO_ID_USER1,"
                        + "swp.lv_confirmacao.guid_usuario2 AS CONFIRMACAO_ID_USER2,"
                        + "swp.lv_confirmacao.guid AS CONFIRMACAO_GUID,"
                        + "swp.lv_usuario.nome AS CONFIRMACAO_NOME_USER1,"
                        + "swp.lv_usuario.sigla AS CONFIRMACAO_SIGLA_USER1,"
                        + "lv_usuario1.nome AS CONFIRMACAO_NOME_USER2,"
                        + "lv_usuario1.sigla AS CONFIRMACAO_SIGLA_USER2"
                        + " FROM swp.lv_doc"
                        + " INNER JOIN swp.lv_revisao ON swp.lv_revisao.guid_doc_verificacao = swp.lv_doc.guid"
                        + " INNER JOIN swp.lv_item_revisao ON swp.lv_revisao.guid_lv_item = swp.lv_item_revisao.guid"
                        + " INNER JOIN swp.lv_grupo ON swp.lv_item_revisao.guid_grupo = swp.lv_grupo.guid"
                        + " INNER JOIN swp.lv_planilha ON swp.lv_grupo.guid_planilha = swp.lv_planilha.guid"
                        + " INNER JOIN swp.lv_tipo ON swp.lv_planilha.guid_tipo = swp.lv_tipo.guid"
                        + " INNER JOIN swp.lv_configuracao ON swp.lv_tipo.guid_config = swp.lv_configuracao.guid"
                        + " INNER JOIN swp.lv_confirmacao ON swp.lv_confirmacao.guid_documento = swp.lv_doc.guid"
                        + " INNER JOIN swp.lv_usuario ON swp.lv_usuario.guid = swp.lv_confirmacao.guid_usuario1"
                        + " INNER JOIN swp.lv_usuario lv_usuario1 ON lv_usuario1.guid = swp.lv_confirmacao.guid_usuario2"
                        + " WHERE "
                        + "swp.lv_doc.guid = '" + guidDocumento + "'";


            using (var conexaoBD = new Conexao())
            {
                var respostaPlanilha = conexaoBD.OracleConnection.Query<ListaVerficacaoQry>(qryLV);

                if (respostaPlanilha.Count() > 0)
                {



                    var primeiro = respostaPlanilha.First();

                    CabecalhoVM cabecalhoVM = new CabecalhoVM()
                    {
                        Funcao = primeiro.FUNCAO,
                        Titulo = primeiro.NOME_PLANILHA,
                        Disciplina = primeiro.NOME_CFG,

                    };




                    listaVerificacao = new ListaVerficacaoVM()
                    {
                        GUID = primeiro.GUID_DOC,
                        NUMERODOC = primeiro.NUMEROSNC,
                        VERFICADOR_UNICO = primeiro.VERFICADOR_UNICO,
                        CabecalhoApp = cabecalhoVM
                    };

                    listaVerificacao.Confirmacoes = (from conf in respostaPlanilha
                                                     group conf by new
                                                     {
                                                         conf.CONFIRMACAO_GUID,
                                                         conf.CONFIRMACAO_INDICE,
                                                         conf.CONFIRMACAO_DATA,
                                                         conf.CONFIRMACAO_ID_USER1,
                                                         conf.CONFIRMACAO_SIGLA_USER1,
                                                         conf.CONFIRMACAO_NOME_USER1,
                                                         conf.CONFIRMACAO_ID_USER2,
                                                         conf.CONFIRMACAO_SIGLA_USER2,
                                                         conf.CONFIRMACAO_NOME_USER2,
                                                         conf.CONFIRMACAO_ORDENADOR
                                                     } into a
                                                     select new ConfirmacaoVM()
                                                     {
                                                         CONFIRMACAO_GUID = a.Key.CONFIRMACAO_GUID,
                                                         CONFIRMACAO_INDICE = a.Key.CONFIRMACAO_INDICE,
                                                         CONFIRMACAO_DATA = a.Key.CONFIRMACAO_DATA,
                                                         CONFIRMACAO_ID_USER1 = a.Key.CONFIRMACAO_ID_USER1,
                                                         CONFIRMACAO_SIGLA_USER1 = a.Key.CONFIRMACAO_SIGLA_USER1,
                                                         CONFIRMACAO_NOME_USER1 = a.Key.CONFIRMACAO_NOME_USER1,
                                                         CONFIRMACAO_ID_USER2 = a.Key.CONFIRMACAO_ID_USER2,
                                                         CONFIRMACAO_SIGLA_USER2 = a.Key.CONFIRMACAO_SIGLA_USER2,
                                                         CONFIRMACAO_NOME_USER2 = a.Key.CONFIRMACAO_NOME_USER2,
                                                         CONFIRMACAO_ORDENADOR = a.Key.CONFIRMACAO_ORDENADOR
                                                     }).OrderBy(x => x.CONFIRMACAO_ORDENADOR).ToList();



                    listaVerificacao.Colunas = (from col in respostaPlanilha
                                                group col by new { col.indice, col.ordenador } into a
                                                select new ColunaLVVM()
                                                {
                                                    INDICE_REV = a.Key.indice,
                                                    ORDENADOR = a.Key.ordenador
                                                }).OrderBy(x => x.ORDENADOR).ToList();

                    foreach (var coluna in listaVerificacao.Colunas)
                    {
                        coluna.LV_Grupos = (from grupo in respostaPlanilha
                                            where grupo.indice == coluna.INDICE_REV
                                            group grupo by new { grupo.GRUPO_ORDENADOR, grupo.GRUPO_NOME, grupo.GUID_GRUPO } into a
                                            select new LV_GrupoVM()
                                            {
                                                GUID = a.Key.GUID_GRUPO,
                                                ORDENADOR = a.Key.GRUPO_ORDENADOR,
                                                NOME = a.Key.GRUPO_NOME
                                            }).OrderBy(x => x.ORDENADOR).ToList();



                        foreach (var grp in coluna.LV_Grupos)
                        {
                            grp.Linhas = (from linha in respostaPlanilha
                                          where linha.GUID_GRUPO == grp.GUID && linha.indice == coluna.INDICE_REV
                                          group linha by new
                                          {
                                              linha.GUID_ITEM,
                                              linha.ITEM_ORDENADOR,
                                              linha.ITEM_DESC,
                                              linha.id_estado,
                                              linha.is_confirmado,
                                              linha.is_emitido,
                                              linha.guid_revisao
                                          } into a
                                          select new LinhaRevisaoVM()
                                          {
                                              GUID_REVISAO = a.Key.guid_revisao,
                                              GUID_ITEM = a.Key.GUID_ITEM,
                                              ORDENADOR = a.Key.ITEM_ORDENADOR,
                                              DESCRICAO = a.Key.ITEM_DESC,
                                              ID_ESTADO = a.Key.id_estado,
                                              EMITIDO = a.Key.is_emitido,
                                              CONFIRMADO = a.Key.is_confirmado
                                          }).OrderBy(x => x.ORDENADOR).ToList();
                        }
                    }





                }


            }


            return listaVerificacao;

        }

        public static ListaVerficacaoVM ObtemListaSimples(NumeroSNCLV numero)
        {
            ListaVerficacaoVM listaVerificacao = null;

            string qryLV = "SELECT "
                         + "swp.lv_doc.guid AS GUID_DOC,"
                         + "swp.lv_doc.doc_verificado AS NUMEROSNC"
                         + " FROM swp.lv_doc WHERE "
                         + "swp.lv_doc.doc_verificado = '" + numero.NUMERO + "'";


            using (var conexaoBD = new Conexao())
            {
                var respostaPlanilha = conexaoBD.OracleConnection.Query<ListaVerficacaoQry>(qryLV);

                if (respostaPlanilha.Count() > 0)
                {



                    var primeiro = respostaPlanilha.First();

                    CabecalhoVM cabecalhoVM = new CabecalhoVM()
                    {
                        Funcao = primeiro.FUNCAO,
                        Titulo = primeiro.NOME_PLANILHA,
                        Disciplina = primeiro.NOME_CFG,

                    };




                    listaVerificacao = new ListaVerficacaoVM()
                    {
                        GUID = primeiro.GUID_DOC,
                        NUMERODOC = primeiro.NUMEROSNC,
                        VERFICADOR_UNICO = primeiro.VERFICADOR_UNICO,
                        CabecalhoApp = cabecalhoVM
                    };

                    //listaVerificacao.Confirmacoes = (from conf in respostaPlanilha
                    //                                 group conf by new
                    //                                 {
                    //                                     conf.CONFIRMACAO_GUID,
                    //                                     conf.CONFIRMACAO_INDICE,
                    //                                     conf.CONFIRMACAO_DATA,
                    //                                     conf.CONFIRMACAO_ID_USER1,
                    //                                     conf.CONFIRMACAO_SIGLA_USER1,
                    //                                     conf.CONFIRMACAO_NOME_USER1,
                    //                                     conf.CONFIRMACAO_ID_USER2,
                    //                                     conf.CONFIRMACAO_SIGLA_USER2,
                    //                                     conf.CONFIRMACAO_NOME_USER2,
                    //                                     conf.CONFIRMACAO_ORDENADOR
                    //                                 } into a
                    //                                 select new ConfirmacaoVM()
                    //                                 {
                    //                                     CONFIRMACAO_GUID = a.Key.CONFIRMACAO_GUID,
                    //                                     CONFIRMACAO_INDICE = a.Key.CONFIRMACAO_INDICE,
                    //                                     CONFIRMACAO_DATA = a.Key.CONFIRMACAO_DATA,
                    //                                     CONFIRMACAO_ID_USER1 = a.Key.CONFIRMACAO_ID_USER1,
                    //                                     CONFIRMACAO_SIGLA_USER1 = a.Key.CONFIRMACAO_SIGLA_USER1,
                    //                                     CONFIRMACAO_NOME_USER1 = a.Key.CONFIRMACAO_NOME_USER1,
                    //                                     CONFIRMACAO_ID_USER2 = a.Key.CONFIRMACAO_ID_USER2,
                    //                                     CONFIRMACAO_SIGLA_USER2 = a.Key.CONFIRMACAO_SIGLA_USER2,
                    //                                     CONFIRMACAO_NOME_USER2 = a.Key.CONFIRMACAO_NOME_USER2,
                    //                                     CONFIRMACAO_ORDENADOR = a.Key.CONFIRMACAO_ORDENADOR
                    //                                 }).OrderBy(x => x.CONFIRMACAO_ORDENADOR).ToList();



                    //listaVerificacao.Colunas = (from col in respostaPlanilha
                    //                            group col by new { col.indice, col.ordenador } into a
                    //                            select new ColunaLVVM()
                    //                            {
                    //                                INDICE_REV = a.Key.indice,
                    //                                ORDENADOR = a.Key.ordenador
                    //                            }).OrderBy(x => x.ORDENADOR).ToList();

                    //foreach (var coluna in listaVerificacao.Colunas)
                    //{
                    //    coluna.LV_Grupos = (from grupo in respostaPlanilha
                    //                        where grupo.indice == coluna.INDICE_REV
                    //                        group grupo by new { grupo.GRUPO_ORDENADOR, grupo.GRUPO_NOME, grupo.GUID_GRUPO } into a
                    //                        select new LV_GrupoVM()
                    //                        {
                    //                            GUID = a.Key.GUID_GRUPO,
                    //                            ORDENADOR = a.Key.GRUPO_ORDENADOR,
                    //                            NOME = a.Key.GRUPO_NOME
                    //                        }).OrderBy(x => x.ORDENADOR).ToList();



                    //foreach (var grp in coluna.LV_Grupos)
                    //{
                    //    grp.Linhas = (from linha in respostaPlanilha
                    //                  where linha.GUID_GRUPO == grp.GUID && linha.indice == coluna.INDICE_REV
                    //                  group linha by new
                    //                  {
                    //                      linha.GUID_ITEM,
                    //                      linha.ITEM_ORDENADOR,
                    //                      linha.ITEM_DESC,
                    //                      linha.id_estado,
                    //                      linha.is_confirmado
                    //                      linha.is_emitido,
                    //                      linha.guid_revisao
                    //                  } into a
                    //                  select new LinhaRevisaoVM()
                    //                  {
                    //                      GUID_REVISAO = a.Key.guid_revisao,
                    //                      GUID_ITEM = a.Key.GUID_ITEM,
                    //                      ORDENADOR = a.Key.ITEM_ORDENADOR,
                    //                      DESCRICAO = a.Key.ITEM_DESC,
                    //                      ID_ESTADO = a.Key.id_estado,
                    //                      EMITIDO = a.Key.is_emitido,
                        //                      CONFIRMADO = a.Key.is_confirmado
                        //                  }).OrderBy(x => x.ORDENADOR).ToList();
                        //}
                    }





                //}


            }


            return listaVerificacao;

        }

        public static ListaVerficacaoVM ObtemListaPrimeiraConfirmacao(string guidDocumento)
        {
            //fc16ae5a-5fa9-41c5-9655-659f076bed7f
            ListaVerficacaoVM listaVerificacao = null;

            string qryLV = "SELECT "
                        + "swp.lv_doc.guid AS guid_doc,"
                        + "swp.lv_doc.objeto AS objeto,"
                        + "swp.lv_doc.doc_verificado AS numerosnc,"
                        + "swp.lv_doc.guid_projeto AS guid_projeto,"
                        + "swp.lv_revisao.indice AS indice,"
                        + "swp.lv_revisao.ordenador AS ordenador,"
                        + "swp.lv_revisao.confirmado AS is_confirmado,"
                        + "swp.lv_revisao.guid_confirmado AS guid_confirmado,"
                        + "swp.lv_revisao.id_estado AS id_estado,"
                        + "swp.lv_revisao.data_vericacao AS data,"
                        + "swp.lv_revisao.guid AS guid_revisao,"
                        + "swp.lv_revisao.guid_lv_verificador AS guid_verificador,"
                        + "swp.lv_revisao.salvo AS is_salvo,"
                        + "swp.lv_revisao.emitido AS is_emitido,"
                        + "swp.lv_item_revisao.guid AS guid_item,"
                        + "swp.lv_item_revisao.descricao AS item_desc,"
                        + "swp.lv_item_revisao.ordenador AS item_ordenador,"
                        + "swp.lv_grupo.nome AS grupo_nome,"
                        + "swp.lv_grupo.ordenador AS grupo_ordenador,"
                        + "swp.lv_grupo.guid AS guid_grupo,"
                        + "swp.lv_planilha.verificador_unico AS verficador_unico,"
                        + "swp.lv_planilha.revisao AS revisao_planilha,"
                        + "swp.lv_planilha.funcao AS funcao,"
                        + "swp.lv_planilha.nome AS nome_planilha,"
                        + "swp.lv_planilha.guid AS guid_planilha,"
                        + "swp.lv_planilha.descricao AS planilha_desc,"
                        + "swp.lv_doc.numero AS numerodoc,"
                        + "swp.lv_tipo.guid_config AS guid_config,"
                        + "swp.lv_tipo.sigla AS sigla_disciplina,"
                        + "swp.lv_tipo.nome AS nome_tipo,"
                        + "swp.lv_configuracao.nome AS nome_cfg,"
                        + "swp.lv_confirmacao.indice_rev AS confirmacao_indice,"
                        + "swp.lv_confirmacao.data AS confirmacao_data,"
                        + "swp.lv_confirmacao.ordenador AS confirmacao_ordenador,"
                        + "swp.lv_confirmacao.guid_usuario1 AS confirmacao_id_user1,"
                        + "swp.lv_confirmacao.guid AS confirmacao_guid,"
                        + "swp.lv_usuario.nome AS confirmacao_nome_user1,"
                        + "swp.lv_usuario.sigla AS confirmacao_sigla_user1"
                        + " FROM swp.lv_doc"
                        + " INNER JOIN swp.lv_revisao ON swp.lv_revisao.guid_doc_verificacao = swp.lv_doc.guid"
                        + " INNER JOIN swp.lv_item_revisao ON swp.lv_revisao.guid_lv_item = swp.lv_item_revisao.guid"
                        + " INNER JOIN swp.lv_grupo ON swp.lv_item_revisao.guid_grupo = swp.lv_grupo.guid"
                        + " INNER JOIN swp.lv_planilha ON swp.lv_grupo.guid_planilha = swp.lv_planilha.guid"
                        + " INNER JOIN swp.lv_tipo ON swp.lv_planilha.guid_tipo = swp.lv_tipo.guid"
                        + " INNER JOIN swp.lv_configuracao ON swp.lv_tipo.guid_config = swp.lv_configuracao.guid"
                        + " INNER JOIN swp.lv_confirmacao ON swp.lv_confirmacao.guid_documento = swp.lv_doc.guid"
                        + " INNER JOIN swp.lv_usuario ON swp.lv_usuario.guid = swp.lv_confirmacao.guid_usuario1"
                        + " WHERE "
                       + "swp.lv_doc.guid = '" + guidDocumento + "'";

            using (var conexaoBD = new Conexao())
            {
                var respostaPlanilha = conexaoBD.OracleConnection.Query<ListaVerficacaoQry>(qryLV);

                if (respostaPlanilha.Count() > 0)
                {



                    var primeiro = respostaPlanilha.First();

                    CabecalhoVM cabecalhoVM = new CabecalhoVM()
                    {
                        Funcao = primeiro.FUNCAO,
                        Titulo = primeiro.NOME_PLANILHA,
                        Disciplina = primeiro.NOME_CFG,

                    };




                    listaVerificacao = new ListaVerficacaoVM()
                    {
                        GUID = primeiro.GUID_DOC,
                        NUMERODOC = primeiro.NUMEROSNC,
                        VERFICADOR_UNICO = primeiro.VERFICADOR_UNICO,
                        CabecalhoApp = cabecalhoVM
                    };

                    listaVerificacao.Confirmacoes = (from conf in respostaPlanilha
                                                     group conf by new
                                                     {
                                                         conf.CONFIRMACAO_GUID,
                                                         conf.CONFIRMACAO_INDICE,
                                                         conf.CONFIRMACAO_DATA,
                                                         conf.CONFIRMACAO_ID_USER1,
                                                         conf.CONFIRMACAO_SIGLA_USER1,
                                                         conf.CONFIRMACAO_NOME_USER1,
                                                         conf.CONFIRMACAO_ID_USER2,
                                                         conf.CONFIRMACAO_SIGLA_USER2,
                                                         conf.CONFIRMACAO_NOME_USER2,
                                                         conf.CONFIRMACAO_ORDENADOR
                                                     } into a
                                                     select new ConfirmacaoVM()
                                                     {
                                                         CONFIRMACAO_GUID = a.Key.CONFIRMACAO_GUID,
                                                         CONFIRMACAO_INDICE = a.Key.CONFIRMACAO_INDICE,
                                                         CONFIRMACAO_DATA = a.Key.CONFIRMACAO_DATA,
                                                         CONFIRMACAO_ID_USER1 = a.Key.CONFIRMACAO_ID_USER1,
                                                         CONFIRMACAO_SIGLA_USER1 = a.Key.CONFIRMACAO_SIGLA_USER1,
                                                         CONFIRMACAO_NOME_USER1 = a.Key.CONFIRMACAO_NOME_USER1,
                                                         CONFIRMACAO_ID_USER2 = a.Key.CONFIRMACAO_ID_USER2,
                                                         CONFIRMACAO_SIGLA_USER2 = a.Key.CONFIRMACAO_SIGLA_USER2,
                                                         CONFIRMACAO_NOME_USER2 = a.Key.CONFIRMACAO_NOME_USER2,
                                                         CONFIRMACAO_ORDENADOR = a.Key.CONFIRMACAO_ORDENADOR
                                                     }).OrderBy(x => x.CONFIRMACAO_ORDENADOR).ToList();



                    listaVerificacao.Colunas = (from col in respostaPlanilha
                                                group col by new { col.indice, col.ordenador } into a
                                                select new ColunaLVVM()
                                                {
                                                    INDICE_REV = a.Key.indice,
                                                    ORDENADOR = a.Key.ordenador
                                                }).OrderBy(x => x.ORDENADOR).ToList();

                    foreach (var coluna in listaVerificacao.Colunas)
                    {
                        coluna.LV_Grupos = (from grupo in respostaPlanilha
                                            where grupo.indice == coluna.INDICE_REV
                                            group grupo by new { grupo.GRUPO_ORDENADOR, grupo.GRUPO_NOME, grupo.GUID_GRUPO } into a
                                            select new LV_GrupoVM()
                                            {
                                                GUID = a.Key.GUID_GRUPO,
                                                ORDENADOR = a.Key.GRUPO_ORDENADOR,
                                                NOME = a.Key.GRUPO_NOME
                                            }).OrderBy(x => x.ORDENADOR).ToList();



                        foreach (var grp in coluna.LV_Grupos)
                        {
                            grp.Linhas = (from linha in respostaPlanilha
                                          where linha.GUID_GRUPO == grp.GUID && linha.indice == coluna.INDICE_REV
                                          group linha by new
                                          {
                                              linha.GUID_ITEM,
                                              linha.ITEM_ORDENADOR,
                                              linha.ITEM_DESC,
                                              linha.id_estado,
                                              linha.is_confirmado,
                                              linha.is_emitido,
                                              linha.guid_revisao
                                          } into a
                                          select new LinhaRevisaoVM()
                                          {
                                              GUID_REVISAO = a.Key.guid_revisao,
                                              GUID_ITEM = a.Key.GUID_ITEM,
                                              ORDENADOR = a.Key.ITEM_ORDENADOR,
                                              DESCRICAO = a.Key.ITEM_DESC,
                                              ID_ESTADO = a.Key.id_estado,
                                              EMITIDO = a.Key.is_emitido,
                                              CONFIRMADO = a.Key.is_confirmado
                                          }).OrderBy(x => x.ORDENADOR).ToList();
                        }
                    }





                }


            }


            return listaVerificacao;

        }




        public static StatusLV StatusLV(string guidDocumento)
        {
            StatusLV statusLV = new StatusLV();


            string qryListaParaAnalise = "SELECT "
                + "swp.lv_doc.guid as guid,"
                + "swp.lv_planilha.verificador_unico as verificador_unico"
                + " FROM swp.lv_doc"
                + " INNER JOIN swp.lv_planilha ON swp.lv_doc.objeto = swp.lv_planilha.guid"
                + " WHERE "
                + "swp.lv_doc.guid  = '" + guidDocumento + "'";

            using (var conexaoBD = new Conexao())
            {
                var respostaListaLVs = conexaoBD.OracleConnection.Query<StatusLVQry>(qryListaParaAnalise);
                if (respostaListaLVs.Count() > 0)
                {
                    statusLV.ConfirmacaoDupla = true;
                    if (respostaListaLVs.Last().VERFICADOR_UNICO == 1)
                    {
                        statusLV.ConfirmacaoDupla = false;
                    }
                }





            }

            return statusLV;

        }

        public static StatusRevisoesLV StatusRevisoesLV(string guidDocumento)
        {
            StatusRevisoesLV statusLV = new StatusRevisoesLV();

            string qryListaParaAnalise = "SELECT "
                + "lv_revisao.guid as guid,"
                + "lv_revisao.indice as indice,"
                + "lv_revisao.ordenador as ordenador,"
                + "lv_revisao.confirmado as confirmado,"
                + "lv_revisao.id_estado as id_estado"
                + " FROM lv_revisao"
                + " WHERE "
                + "lv_revisao.guid_doc_verificacao = '" + guidDocumento + "'";

            using (var conexaoBD = new Conexao())
            {
                var respostaListaLVs = conexaoBD.OracleConnection.Query<RevisaoVM>(qryListaParaAnalise);

                if (respostaListaLVs.Count() > 0)
                {
                    statusLV.ExistemRevisoesNesteDocumento = true;

                    if (respostaListaLVs.Where(x => x.ID_ESTADO == 5).Count() == 0)
                    {
                        statusLV.NaoTemRevisoesIndefinidas = true;
                    }

                    if (respostaListaLVs.Where(x => x.CONFIRMADO == 0).Count() > 0)
                    {
                        statusLV.PossuiRevisoesNaoConfirmadas = true;
                    }

                    var results = (from p in respostaListaLVs.OrderBy(x => x.ORDENADOR)
                                   group p.ORDENADOR by p.ORDENADOR into g
                                   select new { Ordenador = g.Key }).ToList();

                    foreach (var item in results)
                    {
                        statusLV.Indices.Add(respostaListaLVs.First(x => x.ORDENADOR == item.Ordenador).INDICE);
                    }


                    //foreach (var ord in results)
                    //{
                    //    statusLV.Indices.Add(respostaListaLVs.FirstOrDefault(x => x.Ordenador == ord));
                    //}


                }




            }

            return statusLV;

        }

        public static StatusConfirmacoesLV StatusConfirmacoesLV(string guidDocumento)
        {
            StatusConfirmacoesLV statusLV = new StatusConfirmacoesLV();

            string qryListaParaAnalise = "SELECT "
                            + "swp.lv_confirmacao.guid AS guid,"
                            + "swp.lv_confirmacao.guid_usuario1 as guid_usuario1,"
                            + "swp.lv_confirmacao.guid_usuario2 as guid_usuario2,"
                            + "swp.lv_confirmacao.ordenador as ordenador,"
                            + "swp.lv_confirmacao.indice_rev as indice"
                            + " FROM "
                            + "swp.lv_confirmacao"
                            + " WHERE "
                            + "swp.lv_confirmacao.guid_documento = '" + guidDocumento + "'";


            using (var conexaoBD = new Conexao())
            {
                var respostaPlanilha = conexaoBD.OracleConnection.Query<ConfirmacaoQry>(qryListaParaAnalise);

                if (respostaPlanilha.Count() < 1) //&& respostaPlanilha.Count() < 2)
                {
                    //    var primeiro = respostaPlanilha.FirstOrDefault();
                    //    if(!string.IsNullOrEmpty(primeiro.GUID))
                    //    {
                    statusLV.ListaSemConfirmacao = true;
                    // }

                }
                else
                {
                    var ultimaConfirmacao = respostaPlanilha.Distinct().OrderBy(x => x.ORDENADOR).ToList().Last();
                    statusLV.HouveSomentePrimeiraConfirmacaoColunaAtual = (!string.IsNullOrEmpty(ultimaConfirmacao.GUID_USUARIO1) && string.IsNullOrEmpty(ultimaConfirmacao.GUID_USUARIO2)) ? true : false;


                    statusLV.HaColunaConfirmada = respostaPlanilha.Distinct().Count() > 1 ? true : false;
                }

            }

            return statusLV;

        }








    }
}
