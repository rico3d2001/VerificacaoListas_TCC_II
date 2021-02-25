using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AppExcel.AppWeb
{
    public class ProjetoAppExcel
    {

        //string numeroProjeto;
        ////string _numeroDocumentoCorrente;
        //Projeto projetoCorrente;
        ////string guidProjeto;

        //List<Area> listaAreas;
        //List<OS> listaOSs;
        ////List<Disciplina> _listaDisciplinas;

        ////List<Projeto> listalocal;

        //List<string> listaNum;

        //List<string> listaStrTipos;
        //private string guidos;
        //private string guidarea;
        //private string iddisciplina;
        //private string guidtipo;
        //private string sequencial;
        //private string guidProjeto;

        //private NumeroDocSNCLavalin _numeroDocSNCLavalin;
        

        ////private Documento documento;
        ////Usuario _usuario;

        ////public ProjetoApp(string login, NumeroDocSNCLavalin _numeroDocSNCLavalin)
        ////{
        ////    _numeroDocSNCLavalin = new NumeroDocSNCLavalin();
        ////    //_usuario = new Usuario();
        ////    //_usuario.SetByLogin(login);
        ////}

        ////public ProjetoApp()
        ////{
            
        ////}

        ////public void SetProjeto(string guidProjeto)
        ////{

        ////    this.projetoCorrente = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>().ReturnByGUID(guidProjeto);
        ////    //new Projeto(guidProjeto);
        ////}

        ////public void SetProjeto(string guidprojeto, string guidos, string guidarea, string iddisciplina, string guidtipo, string sequencial)
        ////{
        ////    listaNum = new List<string> { "proj", "-", "oss", "-", "area", "-", "DI", "TI", "-", "sequen" };


        ////    //this.projetoCorrente = new Projeto(guidprojeto);
        ////    this.projetoCorrente = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>().ReturnByGUID(guidProjeto);
        ////    this.guidos = guidos;
        ////    this.iddisciplina = iddisciplina;
        ////    this.guidtipo = guidtipo;
        ////    this.sequencial = sequencial;
        ////    this.guidarea = guidarea;

        ////}

        //public IEnumerable<Projeto> GetListaProjetos()
        //{
        //    List<Projeto> lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>().Query().ToList();
        //    var ordenada = lista.OrderBy(x => x.NUMERO);
        //    return ordenada;
        //}






        ////public void SetCorrente(string guidProjeto)
        ////{
        ////    this.projetoCorrente = new Projeto(guidProjeto);
        ////}

        ////public Projeto GetProjetoCorrente()
        ////{
        ////    return this.projetoCorrente;
        ////}

        ////public List<OS> GetListaOSs()
        ////{
        ////    var lista =  this.projetoCorrente.GetListaOSs();
        ////    var ordenada = lista.OrderBy(x => x.Numero).ToList();
        ////    return ordenada;
        ////}

        ////public List<Area> GetListaAreas()
        ////{
        ////    var lista = this.projetoCorrente.GetListaAreas();
        ////    var ordenada = lista.OrderBy(x => x.Numero).ToList();
        ////    return ordenada; //this.projetoCorrente.GetListaAreas();
        ////}

        ////public List<Disciplina> GetListaDisciplinas()
        ////{
        ////    //var lista = this.projetoCorrente.GetListaDisciplinas();
        ////    var lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>().Query().ToList();
        ////    var ordenada = lista.OrderBy(x => x.SIGLA).ToList();
        ////    return ordenada;
        ////    //return this.projetoCorrente.GetListaDisciplinas();
        ////}

        ////public List<TipoDocumento> GetTiposDocumentos()
        ////{
        ////    this.listaStrTipos = this.projetoCorrente.GetStrListaTipos();

        ////    var agrupado = this.listaStrTipos.Distinct().ToList();

        ////    List<TipoDocumento> listaTipoDocumentoViewModels = new List<TipoDocumento>();

        ////    agrupado.ForEach(x => listaTipoDocumentoViewModels.Add(new TipoDocumento(x, x)));

        ////    return listaTipoDocumentoViewModels;
        ////}


        


        ////public string GetGuidLV()
        ////{
        ////    return this.documento.GetGuidLV();
        ////}

        ////public string GetGuidConfig(string guidListaVericicacao)
        ////{
        ////    return this.documento.GetGuidConfig(guidListaVericicacao);
        ////}


        ////public string NumeroProjeto { get => numeroProjeto; set => numeroProjeto = value; }
        ////public string NumeroDocumentoCorrente { get => _numeroDocumentoCorrente; set => _numeroDocumentoCorrente = value; }

        //public List<string> ListaStrTipos { get => this.projetoCorrente.GetStrListaTipos(); } //set => listaStrTipos = value; }
        //public string GuidOs { get; set; }
        //public string GuidArea { get; set; }
        //public string GuidDisciplina { get; set; }
        //public string GuidTipo { get; set; }
        //public string Sequencial { get; set; }

        //public string GuidProjeto => this.projetoCorrente.GUID;

        ////public NumeroDocSNCLavalin NumeroDocSNCLavalin { get => _numeroDocSNCLavalin; } //set => _numeroDocSNCLavalin = value; }

        ////public string NomeUsuario { get => _usuario.Nome; }

       



        ////public bool Abre()
        ////{
        ////    this.documento = new Documento(_numeroDocumentoCorrente);

        ////    return documento.AbrePelaBusca();
        ////}

        //public string GetGuidPlanilha()
        //{
        //    return this.documento.GetGuidPlanilha();
        //}


    }
}
