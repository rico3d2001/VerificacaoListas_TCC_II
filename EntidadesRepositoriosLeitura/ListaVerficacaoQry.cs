using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntidadesRepositoriosLeitura
{
    public class ListaVerficacaoQry
    {

        public string GUID_DOC { get; set; }
        public string OBJETO { get; set; }
        public string NUMEROSNC { get; set; }
        public string guid_projeto { get; set; }
        public string indice { get; set; }
        public int ordenador { get; set; }
        public int is_confirmado { get; set; }
        public string guid_confirmado { get; set; }
        public int id_estado { get; set; }
        public string data { get; set; }
        public string guid_revisao { get; set; }
        public string guid_verificador { get; set; }
        public int is_salvo { get; set; }
        public int is_emitido { get; set; }
        public string GUID_ITEM { get; set; }
        public string ITEM_DESC { get; set; }
        public int ITEM_ORDENADOR { get; set; }
        public string GRUPO_NOME { get; set; }
        public int GRUPO_ORDENADOR { get; set; }
        public string GUID_GRUPO { get; set; }
        public int VERFICADOR_UNICO { get; set; }
        public string REVISAO_PLANILHA { get; set; }
        public string FUNCAO { get; set; }
        public string NOME_PLANILHA { get; set; }
        public string GUID_PLANILHA { get; set; }
        public string PLANILHA_DESC { get; set; }
        public string NUMERODOC { get; set; }
        public string GUID_CONFIG { get; set; }
        public string SIGLA_DISCIPLINA { get; set; }
        public string NOME_TIPO { get; set; }
        public string NOME_CFG { get; set; }
        public string CONFIRMACAO_INDICE { get; set; }
        public DateTime CONFIRMACAO_DATA { get; set; }
        public int CONFIRMACAO_ORDENADOR { get; set; }
        public string CONFIRMACAO_ID_USER1 { get; set; }
        public string CONFIRMACAO_ID_USER2 { get; set; }
        public string CONFIRMACAO_GUID { get; set; }
        public string CONFIRMACAO_NOME_USER1 { get; set; }
        public string CONFIRMACAO_SIGLA_USER1 { get; set; }
        public string CONFIRMACAO_NOME_USER2 { get; set; }
        public string CONFIRMACAO_SIGLA_USER2 { get; set; }


    }
}