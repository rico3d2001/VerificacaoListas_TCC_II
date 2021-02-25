using System.Collections.Generic;
using System.Linq;

namespace LVModel
{
    public class Projeto
    {


        protected string _guidProjeto;

        protected string _numeroProjeto;

        protected IList<Area> _listaAreas;
        protected IList<OS> _listaOSs;
        //protected IList<ListaVerificacao> _listaDocumentos;
        protected IList<NumeroDocSNCLavalin> _listaNumerosSNC;

        public Projeto(string numeroProjeto, string guidProjeto)
        {
            _listaAreas = new List<Area>();
            _listaOSs = new List<OS>();
            _numeroProjeto = numeroProjeto;
            _guidProjeto = guidProjeto;

            //_listaDocumentos = new List<ListaVerificacao>();
            _listaNumerosSNC = new List<NumeroDocSNCLavalin>();
        }


        public Projeto()
        {
            _listaAreas = new List<Area>();
            _listaOSs = new List<OS>();
            //_listaDocumentos = new List<ListaVerificacao>();
            _listaNumerosSNC = new List<NumeroDocSNCLavalin>();
        }

        


        public virtual string GUID { get => _guidProjeto; set => _guidProjeto = value; }
        public virtual string NUMERO { get => _numeroProjeto; set => _numeroProjeto = value; }
        public virtual IList<Area> ListaAreas { get => _listaAreas; set => _listaAreas = value; }
        public virtual IList<OS> ListaOSs { get => _listaOSs; set => _listaOSs = value; }
        //public virtual IList<ListaVerificacao> ListaDocumentos { get => _listaDocumentos; set => _listaDocumentos = value; }
        public virtual IList<NumeroDocSNCLavalin> ListaNumerosSNC { get => _listaNumerosSNC; set => _listaNumerosSNC = value; }

        public virtual bool ChecaSeDocumentoEstaSalvo(string num)
        {
            return _listaNumerosSNC.Distinct().FirstOrDefault(x => x.NUMERO == num) != null ? true : false;

            //_listaDocumentos.Distinct().ToList().Count > 0 ? true : false;
        }

        public virtual void AddArea(Area area)
        {

           

            _listaAreas.Add(area);
        }

        public virtual void AddOS(OS os)
        {
            _listaOSs.Add(os);
        }

        public virtual List<TipoDocumento> TiposDocumentos
        {
            get{

                List<TipoDocumento> listaTipoDocumentos = new List<TipoDocumento>();

                //string numeroProjeto = projeto.NUMERO;

                //List<NumeroDocSNCLavalin> listaNumeroDocSNCLavalin = null;
                //using (var contextoLista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
                //{
                //    contextoLista.Start();
                //    listaNumeroDocSNCLavalin = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().GetByProperty("PROJETO", numeroProjeto).ToList();
                //}

                List<string> listaStr = new List<string>();

                var codAgrup = _listaNumerosSNC.Distinct();

                foreach (var item in codAgrup)
                {
                    var numero = item.NUMERO;
                    var strarray = numero.ToString().Split('-');
                    var str = strarray[3];
                    str = str.Substring(2, 2);
                    listaStr.Add(item.TIPO);
                }

                var agrupado = listaStr.Distinct().OrderBy(x => x).ToList();

                for (int i = 0; i < agrupado.Count; i++)
                {
                    listaTipoDocumentos.Add(new TipoDocumento(agrupado[i], i.ToString()));
                }



                return listaTipoDocumentos;
            }
        }


    }
        
}
