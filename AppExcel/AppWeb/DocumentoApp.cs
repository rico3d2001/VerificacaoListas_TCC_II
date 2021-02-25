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
    public class DocumentoApp
    {
        //NumeroSNCApp _numeroSNCApp;
        //ListaTiposDocumentos _listaTiposDocumentos;
        NumeroDocSNCLavalin _numeroDocSNCLavalin;
        private bool _ativo;
        ListaVerificacao _documento;


        public DocumentoApp(NumeroDocSNCLavalin numeroDocSNCLavalin)
        {



            _numeroDocSNCLavalin = numeroDocSNCLavalin;


            //_listaTiposDocumentos = listaTiposDocumentos;
            //_numeroSNCApp = numeroSNCApp;

            _ativo = abrir();


        }

        public dynamic NumeroDocumentoCorrente { get => _numeroDocSNCLavalin.ToString(); }
        public bool Ativo { get => _ativo;  }
        public ListaVerificacao Documento { get => _documento; set => _documento = value; }

        private bool abrir()
        {
            var lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>().GetByProperty("DOC_VERIFICADO", _numeroDocSNCLavalin.ToString());

            if (lista.Count > 0 && lista.Count < 2)
            {
                _documento = lista.First();
                return true;
            }


            return false;

        }




        //public void SetGuids()//ProjetoApp projetoApp)
        //{
        //    setTemplate();

        //    //this.guidPlanilha = guidPlanilha; //projetoApp.GetGuidPlanilha();

        //    Planilha planilha =
        //        DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().GetByProperty("GUID", _guidtipo).First();

        //    //this.viewPlanilhaCorrente = new Template(this.guidPlanilha);

        //    //_guidListaVericicacao = projetoApp.GetGuidLV();

        //    _guidListaVericicacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().
        //        GetByProperty("GUID", _viewPlanilhaCorrente.).First();

        //    _guidConfiguracao = projetoApp.GetGuidConfig(this.guidListaVericicacao);
        //}



        //private NumeroDocumento iniciaNumeroSncLavalin(
        //    string guidProjeto, string guidos, string guidArea, string idDisciplina,
        //    string guidtipo, string sequencial)
        //{






        //}














        //public Documento(NumeroDocumento numeroDocumentoVerificado)
        //{
        //    _tipoNumeroDocumento = numeroDocumentoVerificado;

        //    setByNumeroDoc();

        //}


        //private void setByNumeroDoc()
        //{
        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", this.numeroDocumentoVerificado);

        //    if (documentos.Count > 0)
        //    {

        //        var lv = documentos.FirstOrDefault(x => x.DOC_VERIFICADO.Equals(this.numeroDocumentoVerificado));
        //        if (lv != null)
        //        {
        //            _guid = lv.GUID;
        //            this._guidTipoCorrente = lv.OBJETO;

        //        }
        //    }
        //}

        //public void CriaSeNaoExisteDocumento()
        //{
        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", this.numeroDocumentoVerificado);

        //    if (documentos.Count < 1)
        //    {
        //        string[] partesNumeroDoc = this.numeroDocumentoVerificado.Split('-');

        //        string numeroProjeto = partesNumeroDoc[0];
        //        string numeroOS = partesNumeroDoc[1];
        //        string numeroArea = partesNumeroDoc[2];
        //        string siglaDisciplina = partesNumeroDoc[3].Substring(0, 2);
        //        string siglaTipoDoc = partesNumeroDoc[3].Substring(2, 2);
        //        int sequencial = int.Parse(partesNumeroDoc[4]);

        //        string guidProjeto = criaSeNaoExisteProjeto(numeroProjeto, numeroArea, numeroOS);


        //        this._guid = Guid.NewGuid().ToString();

        //        var documento = new Documento()
        //        {
        //            GUID = this._guid,
        //            DOC_VERIFICADO = numeroDocumentoVerificado,
        //            NUMERO = "1",
        //            OBJETO = _guidTipoCorrente,
        //            GUID_PROJETO = guidProjeto
        //        };

        //        new Repository<Documento>().Insert(documento);
        //    }
        //}

        //public bool ConfirmadaRevisaoAtual(string indiceRevisao)
        //{
        //    var lvRevs = new Repository<LV_REVISAO>().GetByProperty("GUID_DOC_VERIFICACAO", _guid);

        //    var ultimasRevisoes = lvRevs.Where(x => x.INDICE.Equals(indiceRevisao));

        //    var rev = ultimasRevisoes.FirstOrDefault(x => x.CONFIRMADO.Equals(0));

        //    return rev == null ? true : false;


        //}

        //public bool UltimaRevisaoPronta(string indiceRevisao)
        //{
        //    var lvRevs = new Repository<LV_REVISAO>().GetByProperty("GUID_DOC_VERIFICACAO", _guid);

        //    var ultimasRevisoes = lvRevs.Where(x => x.INDICE.Equals(indiceRevisao));



        //    var rev = ultimasRevisoes.FirstOrDefault(x => x.ID_ESTADO.Equals(5));

        //    return rev == null ? true : false;

        //}

        //public void ConfirmaRegistros(string indiceRevisao, int ordenador)
        //{
        //    List<Revisao> lista = new List<Revisao>();

        //    var lvRevs = new Repository<LV_REVISAO>().GetByProperty("GUID_DOC_VERIFICACAO", this._guid);

        //    var ultimasRevisoes = lvRevs.Where(x => x.INDICE.Equals(indiceRevisao)).ToList();

        //    ultimasRevisoes.ForEach(x => lista.Add(new Revisao(x.GUID, ordenador)));

        //    lista.ForEach(x => x.Confirma());


        //}

        //private string criaSeNaoExisteProjeto(string numeroProjeto, string numeroArea, string numeroOS)
        //{
        //    var projetos = new Repository<LV_PROJETO>().GetByProperty("NUMERO", numeroProjeto);

        //    if (projetos.Count < 1)
        //    {
        //        string guidProjeto = Guid.NewGuid().ToString();
        //        string guidOS = Guid.NewGuid().ToString();
        //        string guidArea = Guid.NewGuid().ToString();



        //        LV_PROJETO lvProjeto = new LV_PROJETO()
        //        {
        //            GUID = Guid.NewGuid().ToString(),
        //            NUMERO = numeroProjeto
        //        };

        //        new Repository<LV_PROJETO>().Insert(lvProjeto);

        //        criaSeNaoExisteOS(lvProjeto.GUID, numeroOS);

        //        criaSeNaoExisteArea(lvProjeto.GUID, numeroArea);

        //        return lvProjeto.GUID;

        //    }
        //    else
        //    {

        //        var projeto = projetos.First() as LV_PROJETO;

        //        criaSeNaoExisteArea(projeto.GUID, numeroArea);

        //        criaSeNaoExisteOS(projeto.GUID, numeroOS);



        //        return projeto.GUID;

        //    }

        //}

        //public static List<Documento> GetLista(string projeto, string disciplina, string tipoDoc)
        //{
        //    throw new NotImplementedException();
        //}

        //private void criaSeNaoExisteOS(string gUIDProjeto, string numeroOS)
        //{
        //    var oss = new Repository<LV_OS>().GetByProperty("GUID_PROJETO", gUIDProjeto);

        //    if (oss.Count < 1)
        //    {
        //        LV_OS lvOS = new LV_OS()
        //        {
        //            GUID = Guid.NewGuid().ToString(),
        //            NUMERO = numeroOS,
        //            GUID_PROJETO = gUIDProjeto
        //        };

        //        new Repository<LV_OS>().Insert(lvOS);

        //    }
        //    else if (oss.FirstOrDefault(x => x.NUMERO.Equals(numeroOS)) == null)
        //    {
        //        LV_OS lvOS = new LV_OS()
        //        {
        //            GUID = Guid.NewGuid().ToString(),
        //            NUMERO = numeroOS,
        //            GUID_PROJETO = gUIDProjeto
        //        };

        //        new Repository<LV_OS>().Insert(lvOS);
        //    }
        //}

        //private void criaSeNaoExisteArea(string gUIDProjeto, string numeroArea)
        //{
        //    var areas = new Repository<LV_AREA>().GetByProperty("GUID_PROJETO", gUIDProjeto);

        //    if (areas.Count < 1)
        //    {
        //        LV_AREA lvArea = new LV_AREA()
        //        {
        //            GUID = Guid.NewGuid().ToString(),
        //            NUMERO = numeroArea,
        //            GUID_PROJETO = gUIDProjeto
        //        };

        //        new Repository<LV_AREA>().Insert(lvArea);

        //    }
        //    else if (areas.FirstOrDefault(x => x.NUMERO.Equals(numeroArea)) == null)
        //    {
        //        LV_AREA lvArea = new LV_AREA()
        //        {
        //            GUID = Guid.NewGuid().ToString(),
        //            NUMERO = numeroArea,
        //            GUID_PROJETO = gUIDProjeto
        //        };

        //        new Repository<LV_AREA>().Insert(lvArea);
        //    }
        //}

        //public string GetGuidConfig(string guidListaVericicacao)
        //{
        //    var lvTipo = new Repository<LV_TIPO>().GetByProperty("GUID", guidListaVericicacao).First() as LV_TIPO;

        //    return lvTipo.GUID_CONFIG;
        //}

        //public string GetGuidPlanilha()
        //{
        //    return _guidTipoCorrente;
        //}

        //public string GetGuidLV()
        //{
        //    var lvPlan = new Repository<LV_PLANILHA>().GetByProperty("GUID", this._guidTipoCorrente).First() as LV_PLANILHA;

        //    return lvPlan.GUID_TIPO;
        //}

        //public string GetTipoRev(string numeroDesenhoCorrente)
        //{
        //    string guidTipo = string.Empty;

        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDesenhoCorrente);

        //    if (documentos.Count > 0)
        //    {
        //        var doc = documentos.First() as Documento;

        //        guidTipo = doc.OBJETO;


        //    }

        //    return guidTipo;


        //}

        //public static bool ContemRegistrosPreenchidos(string indiceRevisao, string numeroDesenhoCorrente)
        //{

        //    var documento = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDesenhoCorrente).FirstOrDefault();

        //    if (documento == null) return false;


        //    var lista = new Repository<LV_REVISAO>().GetByProperty("GUID_DOC_VERIFICACAO", documento.GUID);

        //    if (lista.Count > 0)
        //    {
        //        return true;
        //    }

        //    return false;

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

        //public static bool DocumentoExiste(string numeroDocumentoVerificado)
        //{
        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDocumentoVerificado);

        //    if (documentos.Count > 0 && documentos.Count < 2)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public bool AbrePelaBusca()
        //{
        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDocumentoVerificado);

        //    if (documentos.Count < 1)
        //    {
        //        return false;
        //    }
        //    else
        //    {


        //        var doc = documentos.First() as Documento;
        //        this._guid = doc.GUID;
        //        this.numeroDocumentoVerificado = doc.DOC_VERIFICADO;
        //        this._guidTipoCorrente = doc.OBJETO;


        //        return true;

        //    }
        //}



        //public bool Abre()
        //{
        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDocumentoVerificado);

        //    if (documentos.Count < 1)
        //    {
        //        return false;
        //    }
        //    else
        //    {

        //        if (documentos.FirstOrDefault(x => x.OBJETO == _guidTipoCorrente) != null)
        //        {
        //            var doc = documentos.First(x => x.OBJETO == _guidTipoCorrente);
        //            this._guid = doc.GUID;
        //            this.numeroDocumentoVerificado = doc.DOC_VERIFICADO;
        //            this._guidTipoCorrente = doc.OBJETO;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //        return true;

        //    }
        //}

        //public string GUID
        //{

        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDocumentoVerificado);

        //    return documentos.First().GUID;  //this.guid; //this.documento.GUID;
        //}




        //public static bool ExisteNesteTipoDeLista(string numeroDoc, string guidTipoDoc)
        //{
        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDoc);


        //    if (documentos.Count > 0)
        //    {
        //        if (documentos.FirstOrDefault(x => x.OBJETO == guidTipoDoc) != null)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;


        //}

        //public static bool NaoExiste(string numeroDocumento)
        //{
        //    var documentos = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDocumento);

        //    if (documentos.Count > 0)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public bool Cria()
        //{

        //    try
        //    {
        //        this._guid = Guid.NewGuid().ToString();

        //        var documento = new Documento()
        //        {
        //            GUID = this._guid,
        //            DOC_VERIFICADO = numeroDocumentoVerificado,
        //            NUMERO = "1",
        //            OBJETO = _guidTipoCorrente
        //        };

        //        new Repository<Documento>().Insert(documento);

        //        return true;
        //    }
        //    catch
        //    {

        //        return false;
        //    }


        //}

        //public static bool ConfirmaConfirmados(string numeroDocumento)
        //{
        //    var doc = new Repository<Documento>().GetByProperty("NUMERO", numeroDocumento).FirstOrDefault();

        //    if (doc != null)
        //    {
        //        var lista = new Repository<LV_REVISAO>().GetByProperty("GUID_DOC_VERIFICACAO", doc.GUID).ToList();

        //        if (lista.Count < 1)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            var naoConfirmados = lista.Where(x => x.CONFIRMADO == 0);
        //            if (naoConfirmados.Count() > 0)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }

        //    }

        //    return false;
        //}

        //public static void SalvaConfirmados(string numeroDocumento)
        //{
        //    var doc = new Repository<Documento>().GetByProperty("DOC_VERIFICADO", numeroDocumento).FirstOrDefault();

        //    var lista = new Repository<LV_REVISAO>().GetByProperty("GUID_DOC_VERIFICACAO", doc.GUID).ToList();

        //    if (lista.Count > 0)
        //    {
        //        foreach (var item in lista)
        //        {

        //            Revisao revisao = new Revisao(item.GUID);

        //            revisao.SalvaDocumento();
        //        }
        //    }
        //}

    }
}
