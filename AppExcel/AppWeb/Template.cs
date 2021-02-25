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
    public class Template
    {


        private string disciplina;
        private string nome;
        private string funcao;
        private string descricao;
        private string config;
        private string lv;
        //private List<Grupo> listaGrupos;
        private string guid;
        private string siglaDiscliplina;
        private bool verificadorUnico;

        //public Template(string guidPlanilha)
        //{
        //    this.guid = guidPlanilha;

        //    //listaGrupos = new List<Grupo>();

            


        //    var planilha = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().ReturnByGUID(guidPlanilha);
        //    //var planilha = new Repository<LV_PLANILHA>().ReturnByGUID(guid);


        //    this.nome = planilha.NOME;
        //    this.funcao = planilha.FUNCAO;
        //    this.disciplina = "DEFINIR";
        //    this.descricao = planilha.DESCRICAO;

        //    //var lista = new Repository<LV_VIEW_PLANILHA>().GetByProperty("GUID", this.guid).ToList();
        //    //var lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ViewPlanilha>>().GetByProperty("GUID", guidPlanilha).ToList();
        //    //var viewPlanilha = lista[0];



        //    var lis = DIContainer.Instance.AppContainer.Resolve<ViewAppServiceBase<ViewPlanilha>>().GetByProperty(guidPlanilha); //"GUID", guidPlanilha); 

        //    ViewPlanilha viewPlanilha = lis.FirstOrDefault();//new ViewPlanilha(guidPlanilha);

        //    this.siglaDiscliplina = viewPlanilha.SIGLA_DISCIPLINA;
        //    this.verificadorUnico = planilha.VERIFICADOR_UNICO == 1 ? true : false;


        //    //var listaItens = new Repository<LV_VIEW_ITENS_REV>().GetByProperty("GUID_PLANILHA", this.guid) as List<LV_VIEW_ITENS_REV>;
        //    //var listaItens = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ViewItensRev>>().GetByProperty("GUID_PLANILHA", this.guid).ToList();

        //    //if (listaItens != null && listaItens.Count > 0)
        //    //{
        //    //    ListagemGrupos(listaItens);
        //    //}



        //    var viewItensRev  = DIContainer.Instance.AppContainer.Resolve<ViewAppServiceBase<ViewItensRev>>().GetByProperty(guidPlanilha);//"GUID", guidPlanilha);//= new ViewItensRev(guidPlanilha);



        //}

        //public void Preenche()
        //{

        //    var lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ViewPlanilha>>().GetByProperty("GUID", this.guid).ToList();
        //    //var lista = new Repository<LV_VIEW_PLANILHA>().GetByProperty("GUID", this.guid).ToList();


        //    var viewPlanilha = lista[0];
        //    this.disciplina = viewPlanilha.NOME;
        //    this.siglaDiscliplina = viewPlanilha.SIGLA_DISCIPLINA;
        //    this.funcao = viewPlanilha.DESCRICAO;
        //    this.descricao = viewPlanilha.NOME_DISCIPLINA;
        //    this.config = viewPlanilha.NOME_CONFIG;
        //    this.lv = viewPlanilha.NOME_TIPO;

        //    ViewItensRev viewItensRev = new ViewItensRev()

        //    var listaItens = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ViewItensRev>>().GetByProperty("GUID_PLANILHA", this.guid).ToList();
        //    //var listaItens = new Repository<LV_VIEW_ITENS_REV>().GetByProperty("GUID_PLANILHA", this.guid) as List<LV_VIEW_ITENS_REV>;

        //    ListagemGrupos(this.guid); //listaItens);

        //}

        //private void ListagemGrupos(string guidPlanilha)//List<ViewItensRev> listaItens)//List<LV_VIEW_ITENS_REV> listaItens)
        //{
        //    ViewItensRev viewItensRev = new ViewItensRev(guidPlanilha);

        //    //var results = listaItens.GroupBy(
        //    //                p => p.GUID_GRUPO,
        //    //                p => p.ORDENADOR_GRUPO,
        //    //                (key, g) => new { guid = key, ordenador = g });






        //    foreach (var grupo in viewItensRev.Planilha.GetListaGrupos())//results)
        //    {
        //        //var listaItensGrupo = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ViewItensRev>>().GetByProperty("GUID_GRUPO", grupo.guid).ToList();
        //        //var listaItensGrupo = new Repository<LV_VIEW_ITENS_REV>().GetByProperty("GUID_GRUPO", grupo.guid) as List<LV_VIEW_ITENS_REV>;

        //        listaGrupos.Add(new Grupo(listaItensGrupo.First().ORDENADOR_GRUPO, listaItensGrupo.First().NOME_GRUPO, listaItensGrupo));

        //    }
        //}

        //public int ContaItens()
        //{
        //    var listaItens = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ViewItensRev>>().GetByProperty("GUID_PLANILHA", this.guid).ToList();
        //    //var listaItens = new Repository<LV_VIEW_ITENS_REV>().GetByProperty("GUID_PLANILHA", this.guid) as List<LV_VIEW_ITENS_REV>;



        //    return listaItens.Count;
        //}

        public string Guid { get => this.guid; }


        public string Nome { get => this.nome; }
        public string Funcao { get => this.funcao; }
        public string Descicao { get => this.descricao; }
        public string Disciplina { get => this.disciplina; }
        public string Config { get => this.config; }
        public string LV { get => this.lv; }
        //public List<Grupo> ListaGrupos { get => this.listaGrupos; }
        public string SiglaDiscliplina { get => this.siglaDiscliplina; }
        public string SiglaTipoDocumento { get; set; }
        public bool VerificadorUnico { get => verificadorUnico; }






    }
}
