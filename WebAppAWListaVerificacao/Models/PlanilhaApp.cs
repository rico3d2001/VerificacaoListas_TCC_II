using AppExcel.AppWeb;
using LVModel;

namespace WebAppAWListaVerificacao.Models
{
    public class PlanilhaApp
    {
        

        
        ListaRegistroVerificacoes _listaRegistroVerificacoes;
        //ListaColunasRevisaoViewModel _listaColunasRevisaoViewModel;

        string _numeroDocumento;
        bool _isVerificador;
        bool _documentoCarregado;
        bool _totalmentePreenchidaUltimaListaRevisoes;
        int _nivel;
        bool _planilhaEscolhida;

        ListaRegistrosPorColunas _listaRegistrosPorRevisaoViewModel;


        //protected string paginaDocumentoVerificado;
        //protected ListaRegistroVerificacoes listaRegistroVerificacoes;
        //protected List<ColunaRevisaoViewModel> listaColunas;
        //private Navegador navegador;
        //protected UsuarioApresentacao usuarioViewModel;
        //private ListaRegistroRevisao listaRegistroRevisao;
        //public ListaRegistroRevisao ListaRegistroRevisao { get => listaRegistroRevisao; set => listaRegistroRevisao = value; }
        //public List<RegistroRevisao> ListaRegistroRevisao { get => listaRegistroRevisao; set => listaRegistroRevisao = value; }
        //public PlanilhaVerificacao(DocumentoApresentacao documento)
        //{
        //    this.documento = documento;
        //    this.imagePath = "~/imagens/logo_snc_lavalin.png";
        //    this.imagePathV = "~/imagens/V.png";
        //    this.imagePathX = "~/imagens/X.png";
        //    this.imagePathNA = "~/imagens/NA.png";
        //    this.imagePathND = "~/imagens/ND.png";
        //    this.imagePathI = "~/imagens/I.png";

        //    this.paginaDocumentoVerificado = ""; // "PAGINA DOC VERFICADO";

        //    listaColunas = new List<ColunaRevisaoViewModel>();
        //    this.listaRegistroVerificacoes = new ListaRegistroVerificacoes();
        //}

        #region campos private

        //private DocumentoApp documento;
        Planilha _templateEscolhido;
        PlanilhaViewModel _planilhaViewModel;
        //parei aqui vou cancelar este campo
        //private List<RegistroRevisao> listaRegsRevSession;
        //private UsuarioCorrente usuarioCorrente;
        ListaRevisoesDBPorColunas _listaColunas;

        //private readonly List<ColunaRevisaoViewModel> _listaColunasRevisaoViewModel;
        ListaRevisoesDBPorColunas _listaRevisoes;

        

        #endregion

        #region propriedades

        //public List<RegistroRevisao> ListaRegsRevSession { get => listaRegsRevSession; set => listaRegsRevSession = value; }

        //public dynamic PlanilhaEscolhida { get { return _templateEscolhido == null ? false : true; } }

        //public PlanilhaViewModel PlanilhaViewModel { get => planilhaViewModel; set => planilhaViewModel = value; }

        //public dynamic DocumentoCarregado { get { return this.documento == null ? false : true; } }

        public dynamic TotalmentePreenchidaUltimaListaRevisoes { get; internal set; }

        #endregion

        #region construtores

        public PlanilhaApp(Planilha planilhaEscolhida)//, ListaColunas listaRevisoes) //DocumentoApp doc, UsuarioCorrente usuarioCorrente)
        {


            //var listaColunasRevisaoView = new ListaColunasRevisaoViewModel(navegadorSession.PlanilhaEscolhida, listaRegsRevSession);

            //_listaRevisoes = listaRevisoes;
            //listaRegsRevSession = new List<RegistroRevisao>();

            //this.usuarioCorrente = usuarioCorrente;

            //this.documento = doc;

            _templateEscolhido = planilhaEscolhida;

            //string ERRO_INTRODUZIDO = "?";
            //if (planilhaEscolhida != null)
            //{

            //    this.documento.GuidTipoDocCorrente = _planilhaEscolhida.Guid;
            //}

            //this.planilhaViewModel = new PlanilhaViewModel(this.usuarioCorrente, _templateEscolhido)
            //{
            //    IsVerificador = usuarioCorrente.IsVerificador, //isVerificador(),
            //    DocumentoCarregado = this.documento == null ? false : true,
            //    TotalmentePreenchidaUltimaListaRevisoes = this.documento.TotalmentePreenchidaUltimaListaRevisoes,
            //    PlanilhaEscolhida = _templateEscolhido == null ? false : true
            //};
        }

        

        #endregion

        #region metodos publicos

        

        //private void listaSemRevisoes(Template planilha)
        //{
        //    _listaColunasRevisaoViewModel.Add(new ColunaRevisaoViewModel("0", "00/00/00", "XXX", "XXX", 0, "XXX"));

        //    foreach (var coluna in _listaColunasRevisaoViewModel)
        //    {
        //        foreach (var gp in planilha.ListaGrupos)
        //        {
        //            coluna.AddGrupo(new GrupoRevisoes(
        //                gp.Ordenador.ToString(),
        //                gp.Nome,
        //                geraListaLinhas(gp.ListaItens, gp.Ordenador, coluna.IndiceRevisao)
        //                ));
        //        }
        //    }
        //}

        //public DocumentoApp GetDocumentoToSession(AddRevisaoViewModel addRevisaoViewModel, DocumentoApp documentoApp, string indiceRevisao)
        //{
        //    if (_listaColunas.IniciouListaRevisoes())//documento.IniciouListaRevisoes())
        //    {
        //        if (_listaColunas.IniciouListaRegistros() && _listaColunas.UltimaRevisaoConfirmada())//documento.IniciouListaRegistros() && documento.UltimaRevisaoConfirmada())
        //        {

        //            //documento.AddRevisao(addRevisaoViewModel.Nome);
        //            ListaRegistrosPorColunas listaRegistrosPorColunas = documentoApp.AddRevisao(addRevisaoViewModel.Nome, _listaColunas.GetUltimaColuna());

        //            //if (documento.UltimosRegistrosCopiaveis(this.listaRegsRevSession))//listaRegistrosView))
        //            if (_listaColunas.UltimosRegistrosCopiaveis(this.listaRegsRevSession))
        //            {
        //                this.listaRegsRevSession.ForEach(x => x.Status = _listaColunas.GetStatusRegistroUltimaRevisao(x.GuidTipoRev));
        //                //listaRegistrosView.ForEach(x => x.Status = documento.GetStatusRegistroUltimaRevisao(x.GuidTipoRev));

        //                foreach (var rgv in this.listaRegsRevSession)
        //                {
        //                    rgv.Guid = Guid.NewGuid().ToString();
        //                    EstadoRevisao tipoRevisao = new EstadoRevisao(rgv.Status);

        //                    Revisao revisao = new Revisao(rgv.GuidTipoRev,
        //                        this.usuarioCorrente.Guid,
        //                        tipoRevisao,
        //                        documento.Documento,
        //                        addRevisaoViewModel.Nome,
        //                        documento.OrdenadorRevCorrente,
        //                        rgv.Guid);
        //                }

        //                _listaColunas.AddRevisoesDocumento(this.listaRegsRevSession, indiceRevisao);
        //            }
        //        }
        //        //Session["Doc"] = documento;
        //        return documento;
        //    }
        //    else
        //    {
        //        documentoApp.IniciaRevisao(addRevisaoViewModel.Nome);
        //        _listaColunas.IniciaListaRevisoes(addRevisaoViewModel.Nome);
        //        //Session["Doc"] = documento;
        //        return documento;

        //    }
        //}

        //public void SetListaRegsRevSession(Template planilha)
        //{
        //    //listaRegsRevisao = new List<RegistroRevisao>();
        //    //if (!Revisao.IndiceRegistrado(indiceRev, numeroDoc))
        //    //{
        //    //if(numeroDoc == string.Empty)
        //    //{
        //        foreach (var grupo in planilha.ListaGrupos)
        //        {
        //            foreach (var item in grupo.ListaItens)
        //            {
        //                this.listaRegsRevSession.Add(new RegistroRevisao(item.Guid));
        //            }
        //        }
        //    //}

        //    //}


        //}

        //internal bool JaInseriu(string numeroDocumento, string indiceRev)
        //{
        //    return Revisao.IndiceRegistrado(indiceRev, numeroDocumento);
        //}

        //public void RegistraRevisoesIniciais(Template planilha, string indiceRev, string numeroDoc)
        //{
            
        //    if (!Revisao.IndiceRegistrado(indiceRev, numeroDoc))
        //    {
        //        Documento documento = new Documento(numeroDoc);
        //        ListaRevisoesDocumento listaRevisoesDocumento = new ListaRevisoesDocumento(documento);
        //        //listaRevisoesDocumento.SetConformeNumeroDocumento(numeroDoc);

        //        if (listaRevisoesDocumento.ContemRevisoes(numeroDoc))
        //        {
        //            foreach (var grupo in planilha.ListaGrupos)
        //            {
        //                foreach (var item in grupo.ListaItens)
        //                {
        //                    string novoGuid = Guid.NewGuid().ToString();
        //                    Revisao revisaoAnterior = listaRevisoesDocumento.GetRevisao(item.Guid);
        //                    //Documento documentoVerificacao = new Documento(this.documento.NumeroDocumento);
        //                    Revisao revisao = new Revisao(item.Guid, usuarioCorrente.Guid, revisaoAnterior.EstadoRevisao,
        //                        documento, indiceRev, revisaoAnterior.Ordenador, novoGuid);
        //                    revisao.Insere(documento);

        //                }
        //            }
        //        }
        //        else
        //        {
        //            foreach (var grupo in planilha.ListaGrupos)
        //            {
        //                foreach (var item in grupo.ListaItens)
        //                {
        //                    EstadoRevisao tipoRevisao = new EstadoRevisao(5);
        //                    //Documento documentoVerificacao = new Documento(this.documento.NumeroDocumento);
        //                    int ordeandor = 0;//grupo.Ordenador * 10 + item.Oredenador;
        //                    string novoGuid = Guid.NewGuid().ToString();
        //                    this.documento.IndiceRevCorrente = indiceRev;
        //                    Revisao revisao = new Revisao(item.Guid, usuarioCorrente.Guid, tipoRevisao,
        //                        documento, this.documento.IndiceRevCorrente, ordeandor, novoGuid);

        //                    revisao.Insere(documento);

        //                }
        //            }
        //        }

                
        //    }


            
        //}


        //public void DefineLinhasApresentacao()
        //{
        //    if (_templateEscolhido == null)
        //    {
        //        this.listaRegsRevSession = new List<RegistroRevisao>();
        //        definirDoNada();
                
        //    }
        //    else
        //    {
        //        definePorListaPlanilhaEscolhida();
        //    }
        //}

        #endregion


        #region metodos private

        //private void definirDoNada()
        //{
        //    var linhas1 = new List<LinhaRevisao>()
        //    {
        //        new LinhaRevisao("1.1","Descricao item","null","0")
        //    };

        //    if (planilhaViewModel.ListaColunas.Count == 0)
        //    {
        //        planilhaViewModel.ListaColunas.Add(new ColunaRevisaoViewModel("", "00/00/00", "XXX", "XXX", 0, "XXX"));
        //    }

        //    foreach (var coluna in planilhaViewModel.ListaColunas)
        //    {
        //        coluna.AddGrupo(new GrupoRevisoes("1", "NOME GRUPO", linhas1));
        //    }
        //}

        //private bool documentoCarregado()
        //{
        //    if (this.documento == null || this.documento.NumeroDocumento == null)
        //        return false;

        //    return true;
        //}

        

        //private void listaSemRevisoes(Template planilha)
        //{
        //    planilhaViewModel.ListaColunas.Add(new ColunaRevisaoViewModel("0", "00/00/00", "XXX", "XXX", 0, "XXX"));

        //    foreach (var coluna in planilhaViewModel.ListaColunas)
        //    {
        //        foreach (var gp in planilha.ListaGrupos)
        //        {
        //            coluna.AddGrupo(new GrupoRevisoes(
        //                gp.Ordenador.ToString(),
        //                gp.Nome,
        //                geraListaLinhas(gp.ListaItens, gp.Ordenador, coluna.IndiceRevisao)
        //                ));
        //        }
        //    }
        //}

        //private List<LinhaRevisao> geraListaLinhas(List<ItemVerificacao> listaItens, int ordenadorGrupo, string indiceRevisao)
        //{
        //    List<LinhaRevisao> lista = new List<LinhaRevisao>();

        //    foreach (var ln in listaItens)
        //    {
        //        string itemLinha = ordenadorGrupo.ToString() + "." + ln.Oredenador.ToString();
        //        lista.Add(new LinhaRevisao(itemLinha, ln.Descricao, ln.Guid, indiceRevisao));
        //    }

        //    return lista;
        //}

        //private void definePorListaPlanilhaEscolhida()
        //{
        //    //var listaRegistrosPorColunas = new List<ListaRegistrosPorColunas>();

        //    ListaColunas listaRegistrosPorColunas = new ListaColunas(_templateEscolhido);

        //    if (documento.DocumentoCarregado())
        //    {
        //        listaRegistrosPorColunas = _listaColunas; //documento.ListaRevisoes;

        //        if (listaRegistrosPorColunas.Vazia())//listaRegistrosPorColunas.Count < 1)
        //        {
        //            listaSemRevisoes(_templateEscolhido);
        //        }
        //        else
        //        {

        //            //setListaRevisoesColunasViewModel(listaRegistrosPorColunas);
        //            planilhaViewModel.DefColunas(listaRegistrosPorColunas);

        //            setColunasRevisaoViewModel();
        //            //UA
        //            for (int i = 0; i < listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros.Count; i++)//documento.ListaRevisoes.Last().ListaRegistros.Count; i++)
        //            {
        //                listaRegsRevSession[i].Guid = listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].GuidVerificacao;//documento.ListaRevisoes.Last().ListaRegistros[i].GuidVerificacao;
        //                if (listaRegsRevSession[i].Confirmado && !listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].Confirmado)//documento.ListaRevisoes.Last().ListaRegistros[i].Confirmado)
        //                {
        //                    //documento.ListaRevisoes.Last().ListaRegistros[i].Confirmado = listaRegsRevSession[i].Confirmado;
        //                    listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].Confirmado = listaRegsRevSession[i].Confirmado;
        //                }
        //                else
        //                {
        //                    //listaRegsRevSession[i].Confirmado = documento.ListaRevisoes.Last().ListaRegistros[i].Confirmado;
        //                    listaRegsRevSession[i].Confirmado = listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].Confirmado;
        //                }

        //                //listaRegsRevSession[i].Emitido = documento.ListaRevisoes.Last().ListaRegistros[i].Emitido;
        //                //listaRegsRevSession[i].Salvo = documento.ListaRevisoes.Last().ListaRegistros[i].Salvo;
        //                //listaRegsRevSession[i].Status = documento.ListaRevisoes.Last().ListaRegistros[i].GetLetraStatus();
        //                listaRegsRevSession[i].Emitido = listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].Emitido;
        //                listaRegsRevSession[i].Salvo = listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].Salvo;
        //                listaRegsRevSession[i].Status = listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].GetLetraStatus();

        //                var lastCol = planilhaViewModel.ListaColunas.Last();

        //                foreach (var grp in lastCol.ListaGrupos)
        //                {
        //                    //var resp = grp.ListaLinhas.FirstOrDefault(x => x.GuidTipo == documento.ListaRevisoes.Last().ListaRegistros[i].GetGuidTipo());
        //                    var resp = grp.ListaLinhas.FirstOrDefault(x => x.GuidTipo == listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].GetGuidTipo());
        //                    if (resp != null)
        //                    {
        //                        //resp.GuidRevisao = documento.ListaRevisoes.Last().ListaRegistros[i].GuidVerificacao;
        //                        resp.GuidRevisao = listaRegistrosPorColunas.GetUltimaColuna().ListaRegistros[i].GuidVerificacao;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        listaSemRevisoes(_templateEscolhido);
        //    }

        //    //return listaRegsRevisao;
        //}

        //private void setColunasRevisaoViewModel()
        //{
        //    foreach (var coluna in planilhaViewModel.ListaColunas)
        //    {
        //        foreach (var gp in _templateEscolhido.ListaGrupos)
        //        {
        //            coluna.AddGrupo(new GrupoRevisoes(
        //                gp.Ordenador.ToString(),
        //                gp.Nome,
        //                geraListaLinhas(gp.ListaItens, gp.Ordenador, coluna.IndiceRevisao)
        //                ));
        //        }
        //    }
        //}

        //private void setListaRevisoesColunasViewModel(List<ListaRegistrosPorColunas> listaParaColunas)
        //{
        //    planilhaViewModel.DefColunas(listaParaColunas);

        //    //foreach (var col in listaParaColunas)
        //    //{
        //    //planilhaViewModel.ListaColunas.Add(new ColunaRevisaoViewModel(col.IndiceRevisao, col.DataRevisaoExistente, col.NomeVerificadorRevisaoExistente, col.SiglaVerificadorRevisaoExistente, col.Ordenador, col.IdVerificador));
        //    //}
        //}

        

        #endregion

    }
}