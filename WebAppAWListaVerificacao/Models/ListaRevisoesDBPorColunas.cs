using AppExcel.AppWeb;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaRevisoesDBPorColunas
    {
        private ListaVerificacao _documento;
        private Template _planilhaEscolhida;
        private List<ListaRegistrosPorColunas> listaRevisoes;


        //public ListaRevisoesDBPorColunas(string numeroDocumento)//Template planilhaEscolhida, string numeroDocumento)
        //{

        //    //_planilhaEscolhida = planilhaEscolhida;
        //    this.listaRevisoes = new List<ListaRegistrosPorColunas>();


        //    //if (planilhaEscolhida != null)
        //    //{

        //    if (!string.IsNullOrEmpty(numeroDocumento))
        //    {
        //        _documento = new Documento(numeroDocumento);

        //        this.listaRevisoes = getListaRevisoes();
        //    }



        //    //}







        //}



        //public int Comprimento
        //{
        //    get => this.listaRevisoes.Count;
        //}

        //public bool IniciouListaRevisoes()
        //{
        //    return this.listaRevisoes.Count < 1 ? false : true;
        //}





        //public List<RegistroRevisao> ListaRegsRevSession { get => Models.ListaRegsRevSession; set => listaRegsRevSession = value; }


        public string GetUltimoIndiceRevisaoLista()
        {
            return this.listaRevisoes.Last().IndiceRevisao;
        }

        public bool UltimaRevisaoConfirmada()
        {
            if (this.listaRevisoes.Count < 1) return false;

            var listaOrdenada = this.listaRevisoes.OrderBy(x => x.Ordenador);

            var lista = listaOrdenada.Last().ListaRegistros;

            if (lista.FirstOrDefault(x => x.Confirmado == false) == null)
            {
                return true;
            }

            return false;
        }

        public ListaRegistrosPorColunas this[int index]  
        {
            get => this.listaRevisoes[index];
        }



        //private List<ListaRegistrosPorColunas> getListaRevisoes()
        //{
        //    List<ListaRegistrosPorColunas> lista = new List<ListaRegistrosPorColunas>();

        //    List<Revisao> listaRevisoes = Revisao.GetRevisoesDocumento(_documento);

        //    if (listaRevisoes.Count < 1)
        //    {
        //        return lista;
        //    }

        //    var queryIndices =
        //        from rev in listaRevisoes
        //        group rev by new { indice = rev.INDICE, ordenador = rev.ORDENADOR };

        //    foreach (var q in queryIndices.OrderBy(x => x.Key.ordenador))
        //    {
        //        lista.Add(new ListaRegistrosPorColunas(q.Key.indice, q.Key.ordenador));
        //    }

        //    foreach (var rev in lista)
        //    {
        //        var registros = listaRevisoes.Where(x => x.INDICE == rev.IndiceRevisao);
        //        foreach (var reg in registros)
        //        {
        //            rev.ListaRegistros.Add(new RegistroVerificacao(reg.GUID_LV_ITEM, reg.INDICE, reg.ORDENADOR, reg.GUID, reg.GetEstadoRevisao(), reg.DATA_VERICACAO, reg.GUID_LV_VERIFICADOR, reg.CONFIRMADO, reg.SALVO, reg.EMITIDO));
        //        }
        //    }

        //    return lista;
        //}

        internal bool IsVazia()
        {
            return listaRevisoes.Count < 1 ? true : false;//throw new NotImplementedException();
        }

        internal void Add(ListaRegistrosPorColunas listaRegistrosPorColunas)
        {
            throw new NotImplementedException();
        }

        public RegistroVerificacao RetornaRegistro(string guidTipo, string indiceRevisao)
        {
            if (this.listaRevisoes.Count == 0)
                return null;

            return listaRevisoes.Find(x => x.IndiceRevisao == indiceRevisao).ListaRegistros.Find(x => x.GetGuidTipo() == guidTipo);
        }

        public List<ListaRegistrosPorColunas> ListaRevisoes { get => this.listaRevisoes.OrderBy(x => x.Ordenador).ToList(); }

        public int Comprimento { get => this.listaRevisoes.Count; }

        public bool Vazia()
        {
            return this.listaRevisoes.Count > 0 ? false : true;
        }

        public bool ExisteIndice(string nome)
        {
            return this.listaRevisoes.Exists(x => x.IndiceRevisao == nome);
        }

        

        public bool IniciouListaRevisoes()
        {
            return this.listaRevisoes.Count < 1 ? false : true;
        }

        public bool IniciouListaRegistros()
        {
            return this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.Count() < 1 ? false : true;
        }

        private void atualizaConfirmacaoRev(string guidRev)
        {
            var l = this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.FirstOrDefault(x => x.GuidVerificacao == guidRev);
            l.Confirmado = true;
        }



        public void IniciaListaRevisoes(string caracter)
        {
            this.listaRevisoes = new List<ListaRegistrosPorColunas>();

            int order = 0;

            

            this.listaRevisoes.Add(new ListaRegistrosPorColunas(caracter, order));

           
        }

        //internal void AddRevisoesDocumento(ListaRegsRevSession listaRegistrosView, string indiceRevCorrente)
        //{

        //    var l = this.listaRevisoes.FirstOrDefault(x => x.IndiceRevisao == indiceRevCorrente).ListaRegistros;

        //    if (l.Count < 1)
        //    {
        //        for (int i = 0; i < listaRegistrosView.Count; i++)
        //        {
                   

        //            var registroVerificacaoViewModel =
        //                new RegistroVerificacao(listaRegistrosView[i].GuidTipoRev, indiceRevCorrente, 0, "");

        //            registroVerificacaoViewModel.GuidVerificacao = listaRegistrosView[i].Guid;
        //            registroVerificacaoViewModel.Confirmado = false;
        //            registroVerificacaoViewModel.Emitido = listaRegistrosView[i].Emitido;
        //            registroVerificacaoViewModel.Salvo = listaRegistrosView[i].Salvo;
        //            registroVerificacaoViewModel.TipoRevisao = new EstadoRevisao(listaRegistrosView[i].Status);

        //            l.Add(registroVerificacaoViewModel);
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < listaRegistrosView.Count; i++)
        //        {
        //            l[i].GuidVerificacao = listaRegistrosView[i].Guid;
        //            l[i].Confirmado = false;
        //            l[i].Emitido = listaRegistrosView[i].Emitido;
        //            l[i].Salvo = listaRegistrosView[i].Salvo;
        //            l[i].TipoRevisao = new EstadoRevisao(listaRegistrosView[i].Status);

        //        }
        //    }
        //}

        //internal string GetStatusRegistroUltimaRevisao(string guidTipoRev)
        //{
        //    int indicePenultima = (this.listaRevisoes.Count() - 2);
        //    var penultimaListaRegistros = this.listaRevisoes[indicePenultima].ListaRegistros;

        //    string status = penultimaListaRegistros.Find(x => x.GetGuidTipo().Equals(guidTipoRev)).TipoRevisao.Nome;

        //    return status;
        //}

        

        internal bool UltimosRegistrosCopiaveis(ListaRegsRevSession listaRegsRevSession)//List<RegistroRevisao> listaRegistrosView)
        {
            int indicePenultima = (this.listaRevisoes.Count() - 2);
            var penultimaListaRegistros = this.listaRevisoes[indicePenultima].ListaRegistros;

            return penultimaListaRegistros.Count() == listaRegsRevSession.Comprimento ? true : false;
        }

        internal string GetUltimoIndice()
        {
           return this.listaRevisoes.Last().IndiceRevisao;
        }

       


        

        internal ListaRegistrosPorColunas GetUltimaColuna()
        {
            return this.listaRevisoes.OrderBy(x => x.Ordenador).Last();
        }

        //public bool UltimaRevisaoEstaCompletaSalva()
        //{
        //    if (this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.Last().TipoRevisao != null)
        //    {

        //        if (this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.Last().IsSaved())
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}


        //public bool ValidaRegistros(Template planilhaEscolhida)
        //{
        //    bool validados = false;

        //    if (this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.Count < planilhaEscolhida.ContaItens()) //this.navegador.PlanilhaEscolhida.ContaItens())
        //    {
        //        return false;
        //    }

        //    foreach (var reg in this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros)
        //    {
        //        if (reg.TipoRevisao.NOME == "V" || reg.TipoRevisao.NOME == "ND" || reg.TipoRevisao.NOME == "NA" || reg.TipoRevisao.NOME == "X")
        //        {
        //            validados = true;
        //        }
        //        else
        //        {
        //            validados = false;
        //            break;
        //        }
        //    };

        //    return validados;
        //}



    }
}