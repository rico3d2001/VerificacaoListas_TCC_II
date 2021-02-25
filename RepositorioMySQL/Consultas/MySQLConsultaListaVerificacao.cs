
using Dapper;
using EntidadesRepositoriosLeitura;
using System;
using System.Linq;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaListaVerificacao
    {

        public static ListaVerficacaoVM ObtemListaSemRevisoes(string guidDocumento)
        {
            ListaVerficacaoVM listaVerificacao = null;

            string qryLV = "SELECT "
                + "lv_doc.guid AS guid_doc,"
                + "lv_doc.objeto AS objeto,"
                + "lv_doc.doc_verificado AS numerosnc,"
                + "lv_doc.guid_projeto AS guid_projeto,"
                + "lv_item_revisao.guid AS guid_item,"
                + "lv_item_revisao.descricao AS item_desc,"
                + "lv_item_revisao.ordenador AS item_ordenador,"
                + "lv_grupo.nome AS grupo_nome,"
                + "lv_grupo.ordenador AS grupo_ordenador,"
                + "lv_grupo.guid AS guid_grupo,"
                + "lv_planilha.verificador_unico AS verficador_unico,"
                + "lv_planilha.revisao AS revisao_planilha,"
                + "lv_planilha.funcao AS funcao,"
                + "lv_planilha.nome AS nome_planilha,"
                + "lv_planilha.guid AS guid_planilha,"
                + "lv_planilha.descricao AS planilha_desc,"
                + "lv_doc.numero AS numerodoc,"
                + "lv_tipo.guid_config AS guid_config,"
                + "lv_tipo.sigla AS sigla_disciplina,"
                + "lv_tipo.nome AS nome_tipo,"
                + "lv_configuracao.nome AS nome_cfg"
                + " FROM lv_doc"
                + " INNER JOIN lv_planilha ON lv_doc.objeto = lv_planilha.guid"
                + " INNER JOIN lv_grupo ON lv_grupo.guid_planilha = lv_planilha.guid"
                + " INNER JOIN lv_item_revisao ON lv_item_revisao.guid_grupo = lv_grupo.guid"
                + " INNER JOIN lv_tipo ON lv_planilha.guid_tipo = lv_tipo.guid"
                + " INNER JOIN lv_configuracao ON lv_tipo.guid_config = lv_configuracao.guid"
                + " WHERE "
                + "lv_doc.guid = '" + guidDocumento + "'";


            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaPlanilha = conexaoBD.MySqlConnection.Query<ListaVerficacaoQry>(qryLV);

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
                         + "lv_doc.guid AS GUID_DOC,"
                         + "lv_doc.doc_verificado AS NUMEROSNC"
                         + " FROM listaverificacao.lv_doc WHERE "
                         + "lv_doc.doc_verificado = '" + numero.NUMERO + "'";


            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaPlanilha = conexaoBD.MySqlConnection.Query<ListaVerficacaoQry>(qryLV);

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
                    //                      linha.is_confirmado,
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

        public static ListaVerficacaoVM ObtemListaSemConfirmacoes(string guidDocumento)
        {
            ListaVerficacaoVM listaVerificacao = null;

            string qryLV = "SELECT "
                                + "lv_doc.guid AS guid_doc,"
                                + "lv_doc.objeto AS objeto,"
                                + "lv_doc.doc_verificado AS numerosnc,"
                                + "lv_doc.guid_projeto AS guid_projeto,"
                                + "lv_revisao.indice AS indice,"
                                + "lv_revisao.ordenador AS ordenador,"
                                + "lv_revisao.confirmado AS is_confirmado,"
                                + "lv_revisao.guid_confirmado AS guid_confirmado,"
                                + "lv_revisao.id_estado AS id_estado,"
                                + "lv_revisao.data_vericacao AS data,"
                                + "lv_revisao.guid AS guid_revisao,"
                                + "lv_revisao.guid_lv_verificador AS guid_verificador,"
                                + "lv_revisao.salvo AS is_salvo,"
                                + "lv_revisao.emitido AS is_emitido,"
                                + "lv_item_revisao.guid AS guid_item,"
                                + "lv_item_revisao.descricao AS item_desc,"
                                + "lv_item_revisao.ordenador AS item_ordenador,"
                                + "lv_grupo.nome AS grupo_nome,"
                                + "lv_grupo.ordenador AS grupo_ordenador,"
                                + "lv_grupo.guid AS guid_grupo,"
                                + "lv_planilha.verificador_unico AS verficador_unico,"
                                + "lv_planilha.revisao AS revisao_planilha,"
                                + "lv_planilha.funcao AS funcao,"
                                + "lv_planilha.nome AS nome_planilha,"
                                + "lv_planilha.guid AS guid_planilha,"
                                + "lv_planilha.descricao AS planilha_desc,"
                                + "lv_doc.numero AS numerodoc,"
                                + "lv_tipo.guid_config AS guid_config,"
                                + "lv_tipo.sigla AS sigla_disciplina,"
                                + "lv_tipo.nome AS nome_tipo,"
                                + "lv_configuracao.nome AS nome_cfg"
                                + " FROM lv_doc"
                                + " INNER JOIN lv_revisao ON lv_revisao.guid_doc_verificacao = lv_doc.guid"
                                + " INNER JOIN lv_item_revisao ON lv_revisao.guid_lv_item = lv_item_revisao.guid"
                                + " INNER JOIN lv_grupo ON lv_item_revisao.guid_grupo = lv_grupo.guid"
                                + " INNER JOIN lv_planilha ON lv_grupo.guid_planilha = lv_planilha.guid"
                                + " INNER JOIN lv_tipo ON lv_planilha.guid_tipo = lv_tipo.guid"
                                + " INNER JOIN lv_configuracao ON lv_tipo.guid_config = lv_configuracao.guid"
                                + " WHERE "
                                + "lv_doc.guid = '" + guidDocumento + "'";


            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaPlanilha = conexaoBD.MySqlConnection.Query<ListaVerficacaoQry>(qryLV);

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
                        + "lv_doc.guid AS GUID_DOC,"
                        + "lv_doc.objeto AS OBJETO,"
                        + "lv_doc.doc_verificado AS NUMEROSNC,"
                        + "lv_doc.guid_projeto AS guid_projeto,"
                        + "lv_revisao.indice AS indice,"
                        + "lv_revisao.ordenador AS ordenador,"
                        + "lv_revisao.confirmado AS is_confirmado,"
                        + "lv_revisao.guid_confirmado AS guid_confirmado,"
                        + "lv_revisao.id_estado AS id_estado,"
                        + "lv_revisao.data_vericacao AS data,"
                        + "lv_revisao.guid AS guid_revisao,"
                        + "lv_revisao.guid_lv_verificador AS guid_verificador,"
                        + "lv_revisao.salvo AS is_salvo,"
                        + "lv_revisao.emitido AS is_emitido,"
                        + "lv_item_revisao.guid AS GUID_ITEM,"
                        + "lv_item_revisao.descricao AS ITEM_DESC,"
                        + "lv_item_revisao.ordenador AS ITEM_ORDENADOR,"
                        + "lv_grupo.nome AS GRUPO_NOME,"
                        + "lv_grupo.ordenador AS GRUPO_ORDENADOR,"
                        + "lv_grupo.guid AS GUID_GRUPO,"
                        + "lv_planilha.verificador_unico AS VERFICADOR_UNICO,"
                        + "lv_planilha.revisao AS REVISAO_PLANILHA,"
                        + "lv_planilha.funcao AS FUNCAO,"
                        + "lv_planilha.nome AS NOME_PLANILHA,"
                        + "lv_planilha.guid AS GUID_PLANILHA,"
                        + "lv_planilha.descricao AS PLANILHA_DESC,"
                        + "lv_doc.numero AS NUMERODOC,"
                        + "lv_tipo.guid_config AS GUID_CONFIG,"
                        + "lv_tipo.sigla AS SIGLA_DISCIPLINA,"
                        + "lv_tipo.nome AS NOME_TIPO,"
                        + "lv_configuracao.nome AS NOME_CFG,"
                        + "lv_confirmacao.indice_rev AS CONFIRMACAO_INDICE,"
                        + "lv_confirmacao.data AS CONFIRMACAO_DATA,"
                        + "lv_confirmacao.ordenador AS CONFIRMACAO_ORDENADOR,"
                        + "lv_confirmacao.guid_usuario1 AS CONFIRMACAO_ID_USER1,"
                        + "lv_confirmacao.guid_usuario2 AS CONFIRMACAO_ID_USER2,"
                        + "lv_confirmacao.guid AS CONFIRMACAO_GUID,"
                        + "lv_usuario.nome AS CONFIRMACAO_NOME_USER1,"
                        + "lv_usuario.sigla AS CONFIRMACAO_SIGLA_USER1,"
                        + "lv_usuario1.nome AS CONFIRMACAO_NOME_USER2,"
                        + "lv_usuario1.sigla AS CONFIRMACAO_SIGLA_USER2"
                        + " FROM lv_doc"
                        + " INNER JOIN lv_revisao ON lv_revisao.guid_doc_verificacao = lv_doc.guid"
                        + " INNER JOIN lv_item_revisao ON lv_revisao.guid_lv_item = lv_item_revisao.guid"
                        + " INNER JOIN lv_grupo ON lv_item_revisao.guid_grupo = lv_grupo.guid"
                        + " INNER JOIN lv_planilha ON lv_grupo.guid_planilha = lv_planilha.guid"
                        + " INNER JOIN lv_tipo ON lv_planilha.guid_tipo = lv_tipo.guid"
                        + " INNER JOIN lv_configuracao ON lv_tipo.guid_config = lv_configuracao.guid"
                        + " INNER JOIN lv_confirmacao ON lv_confirmacao.guid_documento = lv_doc.guid"
                        + " INNER JOIN lv_usuario ON lv_usuario.guid = lv_confirmacao.guid_usuario1"
                        + " INNER JOIN lv_usuario lv_usuario1 ON lv_usuario1.guid = lv_confirmacao.guid_usuario2"
                        + " WHERE "
                        + "lv_doc.guid = '" + guidDocumento + "'";


            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaPlanilha = conexaoBD.MySqlConnection.Query<ListaVerficacaoQry>(qryLV);

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



        public static ListaVerficacaoVM ObtemListaPrimeiraConfirmacao(string guidDocumento)
        {
            //fc16ae5a-5fa9-41c5-9655-659f076bed7f
            ListaVerficacaoVM listaVerificacao = null;

            string qryLV = "SELECT "
                        + "lv_doc.guid AS guid_doc,"
                        + "lv_doc.objeto AS objeto,"
                        + "lv_doc.doc_verificado AS numerosnc,"
                        + "lv_doc.guid_projeto AS guid_projeto,"
                        + "lv_revisao.indice AS indice,"
                        + "lv_revisao.ordenador AS ordenador,"
                        + "lv_revisao.confirmado AS is_confirmado,"
                        + "lv_revisao.guid_confirmado AS guid_confirmado,"
                        + "lv_revisao.id_estado AS id_estado,"
                        + "lv_revisao.data_vericacao AS data,"
                        + "lv_revisao.guid AS guid_revisao,"
                        + "lv_revisao.guid_lv_verificador AS guid_verificador,"
                        + "lv_revisao.salvo AS is_salvo,"
                        + "lv_revisao.emitido AS is_emitido,"
                        + "lv_item_revisao.guid AS guid_item,"
                        + "lv_item_revisao.descricao AS item_desc,"
                        + "lv_item_revisao.ordenador AS item_ordenador,"
                        + "lv_grupo.nome AS grupo_nome,"
                        + "lv_grupo.ordenador AS grupo_ordenador,"
                        + "lv_grupo.guid AS guid_grupo,"
                        + "lv_planilha.verificador_unico AS verficador_unico,"
                        + "lv_planilha.revisao AS revisao_planilha,"
                        + "lv_planilha.funcao AS funcao,"
                        + "lv_planilha.nome AS nome_planilha,"
                        + "lv_planilha.guid AS guid_planilha,"
                        + "lv_planilha.descricao AS planilha_desc,"
                        + "lv_doc.numero AS numerodoc,"
                        + "lv_tipo.guid_config AS guid_config,"
                        + "lv_tipo.sigla AS sigla_disciplina,"
                        + "lv_tipo.nome AS nome_tipo,"
                        + "lv_configuracao.nome AS nome_cfg,"
                        + "lv_confirmacao.indice_rev AS confirmacao_indice,"
                        + "lv_confirmacao.data AS confirmacao_data,"
                        + "lv_confirmacao.ordenador AS confirmacao_ordenador,"
                        + "lv_confirmacao.guid_usuario1 AS confirmacao_id_user1,"
                        + "lv_confirmacao.guid AS confirmacao_guid,"
                        + "lv_usuario.nome AS confirmacao_nome_user1,"
                        + "lv_usuario.sigla AS confirmacao_sigla_user1"
                        + " FROM lv_doc"
                        + " INNER JOIN lv_revisao ON lv_revisao.guid_doc_verificacao = lv_doc.guid"
                        + " INNER JOIN lv_item_revisao ON lv_revisao.guid_lv_item = lv_item_revisao.guid"
                        + " INNER JOIN lv_grupo ON lv_item_revisao.guid_grupo = lv_grupo.guid"
                        + " INNER JOIN lv_planilha ON lv_grupo.guid_planilha = lv_planilha.guid"
                        + " INNER JOIN lv_tipo ON lv_planilha.guid_tipo = lv_tipo.guid"
                        + " INNER JOIN lv_configuracao ON lv_tipo.guid_config = lv_configuracao.guid"
                        + " INNER JOIN lv_confirmacao ON lv_confirmacao.guid_documento = lv_doc.guid"
                        + " INNER JOIN lv_usuario ON lv_usuario.guid = lv_confirmacao.guid_usuario1"
                        + " WHERE "
                       + "lv_doc.guid = '" + guidDocumento + "'";

            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaPlanilha = conexaoBD.MySqlConnection.Query<ListaVerficacaoQry>(qryLV);

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
                + "lv_doc.guid as guid,"
                + "lv_planilha.verificador_unico as verificador_unico"
                + " FROM lv_doc"
                + " INNER JOIN lv_planilha ON lv_doc.objeto = lv_planilha.guid"
                + " WHERE "
                + "lv_doc.guid  = '" + guidDocumento + "'";

            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaListaLVs = conexaoBD.MySqlConnection.Query<StatusLVQry>(qryListaParaAnalise);
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

            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaListaLVs = conexaoBD.MySqlConnection.Query<RevisaoVM>(qryListaParaAnalise);

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
                            + "lv_confirmacao.guid AS guid,"
                            + "lv_confirmacao.guid_usuario1 as guid_usuario1,"
                            + "lv_confirmacao.guid_usuario2 as guid_usuario2,"
                            + "lv_confirmacao.ordenador as ordenador,"
                            + "lv_confirmacao.indice_rev as indice"
                            + " FROM "
                            + "lv_confirmacao"
                            + " WHERE "
                            + "lv_confirmacao.guid_documento = '" + guidDocumento + "'";


            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaPlanilha = conexaoBD.MySqlConnection.Query<ConfirmacaoQry>(qryListaParaAnalise);

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
