using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Consultas;
using LVModel;
using System.Collections.Generic;
using System.Linq;
using VerificacaoListas.DTO;

namespace LV_PresenterAPI.Models.Navegacao
{
    public sealed class Navegador
    {
        private int _nivel;
        private string _guidConfiguracao;
        private string _guidListaVericicacao;
        private string _guidPlanilha;
        private PlanilhaNavDTO _planilhaCorrente;
        private string _guid;

        #region Construtores

        public Navegador()
        {
            _guid = "";
            _nivel = 0;
            _planilhaCorrente = null;


        }

        #endregion


        #region Propriedades

        public PlanilhaNavDTO PlanilhaEscolhida
        {
            get => _planilhaCorrente;

            set => _planilhaCorrente = value;
        }

        public string Guid { get => _guid; set => _guid = value; }

        public int Nivel { get => _nivel; set => _nivel = value; }

        #endregion


        #region  Métodos Públicos

        public List<TreeViewModel> GetDadosArvore(string baseURL)
        {
            List<TreeViewModel> arvoreNavegacao = new List<TreeViewModel>();

            var listaConfig = QryTree.GetConfiguracoes(baseURL).OrderBy(x => x.SIGLA_DISCIPLINA).ToList();

            if (_guidConfiguracao != null && _guidConfiguracao != "")
            {
                listaConfig = listaConfig.Where(x => x.GUID.Equals(_guidConfiguracao)).ToList();
            }

            foreach (var configuracao in listaConfig)
            {
                var cfg = new TreeViewModel()
                {
                    Nivel = 1,
                    ID = configuracao.GUID,

                    Nome = configuracao.SIGLA_DISCIPLINA + "-" +  configuracao.NOME
                };


                if (_nivel >= 1)
                {

                    var listaArquivos = QryTree.GetArquivosNav(configuracao.GUID, baseURL);

                    foreach (var arquivoVerifc in listaArquivos)
                    {
                        if (_nivel >= 2)
                        {
                            var arquivo = listaArquivos.First(x => x.GUID == _guidListaVericicacao);

                            var lv = new TreeViewModel()
                            {
                                Nivel = 2,
                                ID = arquivo.GUID,
                                Tipo = arquivo.NOME,
                                Nome = arquivo.NOME
                            };

                            cfg.Childs.Add(lv);

                            var listaPlanilhasGeral = QryTree.GetPlanilhasNav(arquivo.GUID,baseURL);
                            //var listaPlanilhasUltimaRevisao = new List<PlanilhaNavDTO>();
                            //foreach (var pl in listaPlanilhasGeral)
                            //{
                            //    if (!(listaPlanilhasUltimaRevisao.Where(x => x.NOME == pl.NOME).Count() > 0))
                            //    {
                            //        var lista = listaPlanilhasGeral.Where(x => x.NOME == pl.NOME).ToList();
                            //        var ultimaPL = lista.OrderBy(x => x.REV).Last();
                            //        listaPlanilhasUltimaRevisao.Add(ultimaPL);
                            //    }

                            //}

                            if (_guidPlanilha != null && _guidPlanilha != "")
                            {
                                _planilhaCorrente = listaPlanilhasGeral.First(x => x.GUID == _guidPlanilha);
                                listaPlanilhasGeral = listaPlanilhasGeral.Where(x => x.GUID.Equals(_guidPlanilha)).ToList();
                            }

                            foreach (var planilha in listaPlanilhasGeral)
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

            return arvoreNavegacao;
        }

        public void SetPlanilhaEscolhaNull()
        {
            this._planilhaCorrente = null;
        }

        public void OperaNivel(int? nivel, string guid)
        {
           


            if (nivel == 0)
            {
                guid = "";
                reset();

            }
            else
            {
                if (_nivel < nivel)
                {
                    avanca(guid);
                }
                else if (_nivel > nivel)
                {
                    if ((_nivel - nivel) == 1)
                    {
                        recua(guid);
                    }

                }
            }


        }

        public void reset()
        {
            _nivel = 0;
            _guidConfiguracao = "";
            _guidListaVericicacao = "";
            _guidPlanilha = "";
        }

        public void avanca(string guid)
        {
            _nivel++;
            switch (_nivel)
            {
                case 1:
                    _guidConfiguracao = guid;
                    break;
                case 2:
                    _guidListaVericicacao = guid;
                    break;
                case 3:
                    _guidPlanilha = guid;
                    break;
                default:
                    {
                        _guidConfiguracao = "";
                        _guidListaVericicacao = "";
                        _guidPlanilha = "";
                    }
                    break;
            }
        }

        public void recua(string guid)
        {

            _planilhaCorrente = null;

            _nivel--;
            switch (_nivel)
            {
                case 1:
                    {
                        _guidConfiguracao = guid;
                        _guidListaVericicacao = "";
                        _guidPlanilha = "";
                    }
                    break;
                case 2:
                    {
                        _guidListaVericicacao = guid;
                        _guidPlanilha = "";
                    }
                    break;
                case 3:
                    _guidPlanilha = guid;
                    break;
                default:
                    {
                        _guidConfiguracao = "";
                        _guidListaVericicacao = "";
                        _guidPlanilha = "";
                    }
                    break;
            }
        }

        public void SetGuids(string guidPlan, ListaVerificacao documento)
        {

            _guidListaVericicacao = documento.Planilha.GUID;

            _planilhaCorrente = new PlanilhaNavDTO(_guidPlanilha);


            _guidConfiguracao = documento.Planilha.Tipo.GUID;
        }

        #endregion

    }
}