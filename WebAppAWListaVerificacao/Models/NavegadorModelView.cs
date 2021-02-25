using AppExcel.AppWeb;
using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace WebAppAWListaVerificacao.Models
{
    /// <summary>Arvore de navegação lateral</summary>
    public sealed class Navegador
    {
        private int nivel;
        private string guidConfiguracao;
        private string guidListaVericicacao;
        private string guidPlanilha;

        private Planilha _planilhaCorrente;


        private string guid;
        #pragma warning disable CS0169 
        ProjetoViewModel projetoViewModel;
        #pragma warning restore CS0169 



        public Navegador()
        {
            this.guid = "";
            this.nivel = 0;
            _planilhaCorrente = null;


        }

    


        public void OperaNivel(int nivel, string guid)
        {
            if (nivel == 0)
            {
                guid = "";
                reset();

            }
            else
            {
                if (this.nivel < nivel)
                {
                    avanca(guid);
                }
                else if (this.nivel > nivel)
                {
                    if ((this.nivel - nivel) == 1)
                    {
                        recua(guid);
                    }

                }
            }


        }

        

        public void reset()
        {
            this.nivel = 0;
            this.guidConfiguracao = "";
            this.guidListaVericicacao = "";
            this.guidPlanilha = "";
        }

        public Planilha PlanilhaEscolhida
        {
            get => _planilhaCorrente;

            set => _planilhaCorrente = value;
        }

        public string Guid { get => guid; set => guid = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        
        public void SetPlanilhaEscolhaNull()
        {
            this._planilhaCorrente = null;
        }

        public List<TreeViewModel> ListaConfiguracoes()
        {
            List<TreeViewModel> arvoreNavegacao = new List<TreeViewModel>();

            using (var contextoLista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Configuracao>>())
            {
                contextoLista.Start();
                var listaConfig = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Configuracao>>().Query().ToList();





                //List<Configuracao> listaConfig = configuracoes;

                if (this.guidConfiguracao != null && this.guidConfiguracao != "")
                {
                    listaConfig = listaConfig.Where(x => x.GUID.Equals(this.guidConfiguracao)).ToList();
                }

                

                foreach (var configuracao in listaConfig)
                {
                    var cfg = new TreeViewModel()
                    {
                        Nivel = 1,
                        ID = configuracao.GUID,

                        Nome = configuracao.NOME
                    };

                    if (this.nivel >= 1)
                    {

                        var cfg_query = listaConfig.FirstOrDefault(x => x.GUID == configuracao.GUID); //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Configuracao>>().ReturnByGUID(configuracao.GUID);

                        var listaLVs = cfg_query.ListaArquivos.Distinct().ToList();


                        foreach (var arquivoVerifc in listaLVs)
                        {


                            if (this.nivel >= 2)
                            {

                                var arquivo = cfg_query.ListaArquivos.First(x => x.GUID == this.guidListaVericicacao);

                                var lv = new TreeViewModel()
                                {
                                    Nivel = 2,
                                    ID = arquivo.GUID,
                                    Tipo = arquivo.NOME,
                                    Nome = arquivo.NOME
                                };

                                cfg.Childs.Add(lv);

                                var listaPlanilhas = arquivo.ListaPlanilhas.Distinct().ToList(); //listaVerifc.ListaPlanilhas.Distinct().ToList();


                                if (this.guidPlanilha != null && this.guidPlanilha != "")
                                {

                                    this._planilhaCorrente = listaPlanilhas.First(x => x.GUID == this.guidPlanilha);

                                    listaPlanilhas = listaPlanilhas.Where(x => x.GUID.Equals(this.guidPlanilha)).ToList();
                                }

                                foreach (var planilha in listaPlanilhas)
                                {
                                    var plan = new TreeViewModel()
                                    {
                                        Nivel = 3,
                                        ID = planilha.GUID,
                                        Tipo = planilha.GUID,
                                        Nome = planilha.NOME
                                    };

                                    lv.Childs.Add(plan);
                                }



                                break;
                            }
                            else
                            {
                                var lv = new TreeViewModel()
                                {
                                    Nivel = 2,
                                    ID = arquivoVerifc.GUID,
                                    Tipo = arquivoVerifc.NOME,
                                    Nome = arquivoVerifc.NOME
                                };

                                cfg.Childs.Add(lv);
                            }


                        }
                    }

                    arvoreNavegacao.Add(cfg);
                }
            }

            return arvoreNavegacao;
        }

        public void avanca(string guid)
        {
            this.nivel++;
            switch (this.nivel)
            {
                case 1:
                    this.guidConfiguracao = guid;
                    break;
                case 2:
                    this.guidListaVericicacao = guid;
                    break;
                case 3:
                    this.guidPlanilha = guid;
                    break;
                default:
                    {
                        this.guidConfiguracao = "";
                        this.guidListaVericicacao = "";
                        this.guidPlanilha = "";
                    }
                    break;
            }
        }

        public void recua(string guid)
        {

            _planilhaCorrente = null;

            this.nivel--;
            switch (this.nivel)
            {
                case 1:
                    {
                        this.guidConfiguracao = guid;
                        this.guidListaVericicacao = "";
                        this.guidPlanilha = "";
                    }
                    break;
                case 2:
                    {
                        this.guidListaVericicacao = guid;
                        this.guidPlanilha = "";
                    }
                    break;
                case 3:
                    this.guidPlanilha = guid;
                    break;
                default:
                    {
                        this.guidConfiguracao = "";
                        this.guidListaVericicacao = "";
                        this.guidPlanilha = "";
                    }
                    break;
            }
        }

        public void SetGuids(string guidPlan, ListaVerificacao documento)
        {

            this.guidListaVericicacao = documento.Planilha.GUID; 

            this._planilhaCorrente = new Planilha(this.guidPlanilha); 

        
            this.guidConfiguracao = documento.Planilha.Tipo.GUID; 
        }

   

    }
}