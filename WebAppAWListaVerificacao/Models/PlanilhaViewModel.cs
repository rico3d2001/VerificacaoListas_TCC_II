using LVModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class PlanilhaViewModel //: PlanilhaVerificacao
    {
        //private string imagePath;
        //private string imagePathV;
        //private string imagePathX;
        //private string imagePathNA;
        //private string imagePathND;
        //private string imagePathI;

        //private string paginaDocumentoVerificado;
        //private ListaRegistroVerificacoes listaRegistroVerificacoes;
        //private ListaColunasRevisaoViewModel _listaColunasRevisaoViewModel;

        //private List<ColunaRevisaoViewModel> _listaColunasRevisaoViewModel;
        //private CabecalhoViewModel _cabecalhoViewModel;

        //private string _paginaDocumentoVerificado;

        //private Navegador navegador;
        //private UsuarioCorrente usuarioViewModel;

        //private DocumentoApp documento;

        //private string numeroDocumento;
        //private bool isVerificador;
        //private bool documentoCarregado;
        //private bool totalmentePreenchidaUltimaListaRevisoes;
        //private int nivel;
        //private bool planilhaEscolhida;


        private RegistroVerificacao registro;

        #pragma warning disable CS0169 // The field 'PlanilhaViewModel.listaRegistrosPorRevisaoViewModel' is never used
        //private ListaRegistrosPorColunas listaRegistrosPorRevisaoViewModel;
        #pragma warning restore CS0169 // The field 'PlanilhaViewModel.listaRegistrosPorRevisaoViewModel' is never used

        

       // public PlanilhaViewModel(UsuarioCorrente usuarioViewModel, Template template) //: base(documento)
        //{

            //this.documento = documento;
            //this.imagePath = "~/imagens/logo_snc_lavalin.png";
            //this.imagePathV = "~/imagens/V.png";
            //this.imagePathX = "~/imagens/X.png";
            //this.imagePathNA = "~/imagens/NA.png";
            //this.imagePathND = "~/imagens/ND.png";
            //this.imagePathI = "~/imagens/I.png";

            //this.paginaDocumentoVerificado = ""; // "PAGINA DOC VERFICADO";

            //var listaColunas = new List<ColunaRevisaoViewModel>();
            //this.listaRegistroVerificacoes = new ListaRegistroVerificacoes();


            //acima construtor


            //this.usuarioViewModel = usuarioViewModel;

            //this.numeroDocumento = "NUMERO DOC. VERIFICADO";

            //this.listaRegistroVerificacoes = new ListaRegistroVerificacoes();

            //if (template != null)
            //{
            //    cabecalho = new CabecalhoViewModel(template);
            //}
            //else
            //{
            //    cabecalho = new CabecalhoViewModel();
            //}

            

            
        //}

        //public List<ColunaRevisaoViewModel> ListaColunas { get => _listaColunas; set => _listaColunas = value; }
        //public ListaColunasRevisaoViewModel ListaColunasRevisaoViewModel { get => _listaColunasRevisaoViewModel; set => this._listaColunasRevisaoViewModel = value; }
        //public List<ColunaRevisaoViewModel> ListaColunasRevisaoViewModel { get; set; }
        //public string Funcao { get { return _cabecalhoViewModel.Funcao; } }
        //public string NumeroDocumento { get { return _cabecalhoViewModel.NumeroDocumento; } set => _cabecalhoViewModel.NumeroDocumento = value; }
        //public string ImagePath { get { return this.imagePath; } }
        //public string ImagePathV { get { return this.imagePathV; } }
        //public string ImagePathX { get { return this.imagePathX; } }
        //public string ImagePathNA { get { return this.imagePathNA; } }
        //public string ImagePathND { get { return this.imagePathND; } }
        //public string ImagePathI { get { return this.imagePathI; } }
        //public string Title { get { return this._cabecalhoViewModel.Titulo; } }
        //public string Disciplina { get { return this._cabecalhoViewModel.Disciplina; } }
        //public string PaginaDocumentoVerificado { get { return _paginaDocumentoVerificado; } }
        //public List<RegistroVerificacao> ListaRevisoes { get { return this.listaRegistroVerificacoes.ListaRevisoes; } }
        //public Navegador Navegador { get => navegador; set => this.navegador = value; }
        //public bool IsVerificador { get => this.isVerificador; set => this.isVerificador = value; }
        //public bool DocumentoCarregado { get => documentoCarregado; set => documentoCarregado = value; }
        //public bool TotalmentePreenchidaUltimaListaRevisoes { get => totalmentePreenchidaUltimaListaRevisoes; set => totalmentePreenchidaUltimaListaRevisoes = value; }
        //public int Nivel { get => nivel; set => nivel = value; }
        //public bool PlanilhaEscolhida { get => planilhaEscolhida; set => planilhaEscolhida = value; }
        //public RegistroVerificacao Registro { get => registro; set => registro = value; }
        //public UsuarioCorrente UsuarioViewModel => usuarioViewModel;

       



        [Required(ErrorMessage = "O numero do projeto deve ser informado.")]
        [Display(Name = "Projeto")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Digite número inteiro de 4 digitos.")]
        public string Projeto { get; set; }

        [Required(ErrorMessage = "O numero da OS deve ser informado.")]
        [Display(Name = "OS")]
        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "Digite número inteiro de 3 digitos.")]
        public string OS { get; set; }

        [Required(ErrorMessage = "O numero da área deve ser informado.")]
        [Display(Name = "Área")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Digite número inteiro de 4 digitos.")]
        public string Area { get; set; }

        [Required(ErrorMessage = "a sigla da disciplina deve ser informada.")]
        [Display(Name = "Disciplina")]
        [RegularExpression(@"^[A-Z,0-9]{2}$", ErrorMessage = "Digite 2 caracteres.")]
        public string SiglaDisciplina { get; set; }

        [Required(ErrorMessage = "A sigla do tipo de documento deve ser informada.")]
        [Display(Name = "Tipo")]
        [RegularExpression(@"^[A-Z,0-9]{2}$", ErrorMessage = "Digite 2 caracteres.")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "O número sequencial deve ser informado.")]
        [Display(Name = "Sequencial")]
        [RegularExpression(@"^[0-9]{4,5}$", ErrorMessage = "Digite número inteiro de 4 ou 5 digitos.")]
        public string Sequencial { get; set; }

        //public CabecalhoViewModel CabecalhoViewModel { get; set; }

        //public ImagemStatusViewModel ImagemStatusViewModel { get; set; }


        public IEnumerable<ConfirmacaoViewModel> ListaRegistroConfirmacoesViewModel { get; set; }

        //public bool ContemRegistrosPreenchidos(string indiceRevisao, string numeroDoc)
        //{


        //    return Documento.ContemRegistrosPreenchidos(indiceRevisao, numeroDoc);



        //    //if (this.documento.ListaRevisoes.Count == 0)
        //    //{
        //    //    return false;
        //    //}
        //    //else
        //    //{
        //    //    var lista = this.documento.ListaRevisoes.Find(x => x.IndiceRevisao == indiceRevisao).ListaRegistros;

        //    //    if (lista.Count > 0)
        //    //    {
        //    //        return true;
        //    //    }
        //    //    else
        //    //    {
        //    //        return false;
        //    //    }
        //    //}
        //}

        //public RegistroVerificacao GetRegistro(string guidTipo, string indiceRevisao, string numeroDoc)
        //{
        //    if (string.IsNullOrEmpty(numeroDoc) || numeroDoc.Equals("NUMERO DOC. VERIFICADO")) return null;

            

        //    Revisao rev =  Revisao.GetRegistro(guidTipo, indiceRevisao, numeroDoc);

            

        //    RegistroVerificacao reg =
        //        new RegistroVerificacao(guidTipo, indiceRevisao, rev.ORDENADOR, rev.GUID, rev.GetEstadoRevisao(),
        //        rev.DATA_VERICACAO, rev.GUID_LV_VERIFICADOR, rev.CONFIRMADO, rev.SALVO, rev.EMITIDO);
            

        //    return reg;
        //    //if (this.documento.ListaRevisoes.Count == 0)
        //    //{
        //    //    return null;
        //    //}
        //    //else
        //    //{
        //    //    return this.documento.ListaRevisoes.Find(x => x.IndiceRevisao == indiceRevisao).ListaRegistros.Find(x => x.GetGuidTipo() == guidTipo);
        //    //}
        //}

        //internal void DefColunas(ListaColunas listaParaColunas)//List<ListaRegistrosPorColunas> listaParaColunas)
        //{
        //    for (int i = 0; i < listaParaColunas.Comprimento; i++)
        //    {
        //        this.listaColunas.Add(new ColunaRevisaoViewModel(
        //            listaParaColunas[i].IndiceRevisao,
        //            listaParaColunas[i].DataRevisaoExistente,
        //            listaParaColunas[i].NomeVerificadorRevisaoExistente,
        //            listaParaColunas[i].SiglaVerificadorRevisaoExistente,
        //            listaParaColunas[i].Ordenador,
        //            listaParaColunas[i].IdVerificador));
        //    }

            
        //}
    }
}