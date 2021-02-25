using LVModel.ObjetosValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using LV14FluentNHB;

namespace LVModel
{

    public class ListaVerificacao
    {
        NumeroDocSNCLavalin _numeroDocSNCLavalin;
        Planilha _planilha;
        Projeto _projeto;
        OS _os;
        Area _area;
        //NumeroSncLavalin _numeroSncLavalin;

        IList<Confirmacao> _listaConfirmacoes;
        IList<Revisao> _listaRevisoes;

        string doc_Verificado;
        string _guid;
        string _numero;



        public virtual string GUID { get => _guid; set => _guid = value; }
        public virtual string DOC_VERIFICADO { get => doc_Verificado; set => doc_Verificado = value; }
        public virtual string NUMERO { get => _numero; set => _numero = value; }

        public virtual NumeroDocSNCLavalin NumeroDocSNCLavalin { get => _numeroDocSNCLavalin; set => _numeroDocSNCLavalin = value; }
        public virtual Planilha Planilha { get => _planilha; set => _planilha = value; }
        public virtual Projeto Projeto { get => _projeto; set => _projeto = value; }
        public virtual OS OS { get => _os; set => _os = value; }
        public virtual Area Area { get => _area; set => _area = value; }

        public virtual bool IsListaConfimacaoDupla()
        {
            return _planilha.VERIFICADOR_UNICO == 1 ? false : true;
        }

        public virtual IList<Confirmacao> ListaConfirmacoes { get => _listaConfirmacoes; set => _listaConfirmacoes = value; }
        public virtual IList<Revisao> ListaRevisoes { get => _listaRevisoes; set => _listaRevisoes = value; }
       

        public ListaVerificacao()
        {
            _listaConfirmacoes = new List<Confirmacao>();
            _listaRevisoes = new List<Revisao>();
        }

       
        public virtual bool PodeAcrescentarRevisao(string indice)
        {
            if (naoPossuiRevisoes())
            {

                return true;
            }
            else if (naoExisteMesmoIndice(indice) && todasRevisoesConfirmadas())
            {
   
               return true;
             
            }

            return false;
        }

        protected virtual bool naoPossuiRevisoes()
        {
            return _listaRevisoes.Distinct().ToList().Count < 1;
        }

        public virtual bool NaoTemRevisoesIndefinidas()
        {
            return !_listaRevisoes.Distinct().ToList().Exists(x => x.ID_ESTADO == StatusRevisao.Indefinido.Id);
        }

        protected virtual bool todasRevisoesConfirmadas()
        {
            return _listaRevisoes.Distinct().ToList().Exists(x => x.CONFIRMADO == 1);
        }


        public virtual bool HouveSomentePrimeiraConfiramcao()
        {
            var ultimaConfirmacao = _listaConfirmacoes.Distinct().OrderBy(x => x.ORDENADOR).ToList().Last();
            return (!string.IsNullOrEmpty(ultimaConfirmacao.GUID_USUARIO1) && string.IsNullOrEmpty(ultimaConfirmacao.GUID_USUARIO2)) ? true : false;
        }

        public virtual bool HouveConfimacaoNesteDocumento()
        {
            if(_listaConfirmacoes == null)
            {
                _listaConfirmacoes = new List<Confirmacao>();
            }

            return _listaConfirmacoes.Count > 0;
        }

        protected virtual bool naoExisteMesmoIndice(string indice)
        {
            return _listaRevisoes.Distinct().ToList().FirstOrDefault(x => x.INDICE.Equals(indice)) == null ? true : false;
        }


        public virtual bool AddRevisao(string indiceRevisao, string guidUsuarioCorrente)
        {

         
            if (aindaNaoJaInseriuDesteIndice(indiceRevisao, _listaRevisoes.Distinct().ToList()))
            {

                if (_listaRevisoes.Distinct().ToList().Count > 0 && !_listaRevisoes.Distinct().ToList().Exists(x => x.CONFIRMADO == 0))
                {
                    var ultimo_ordenador = _listaRevisoes.Distinct().ToList().OrderBy(x => x.ORDENADOR).Last().ORDENADOR;
                    var ultimaColunaRevisao = _listaRevisoes.Distinct().ToList().Where(x => x.ORDENADOR == ultimo_ordenador).ToList();

                    foreach (var grupo in this.Planilha.ListaGrupos.Distinct().OrderBy(x => x.ORDENADOR))
                    {
                        foreach (var item in grupo.ListaItens.Distinct().OrderBy(x => x.ORDENADOR))
                        {


                            Revisao revisaoAnterior = ultimaColunaRevisao.First(x => x.GUID_LV_ITEM == item.GUID);

                            Revisao revisao =
                                new Revisao()
                                {
                                    DATA_VERICACAO = DateTime.Now,
                                    GUID = Guid.NewGuid().ToString(),
                                    GUID_LV_ITEM = item.GUID,
                                    GUID_LV_VERIFICADOR = guidUsuarioCorrente,
                                    ID_ESTADO = revisaoAnterior.ID_ESTADO,
                                    GUID_DOC_VERIFICACAO = _guid,
                                    INDICE = indiceRevisao,
                                    ORDENADOR = revisaoAnterior.ORDENADOR + 1,
                                    GUID_CONFIRMADO = string.Empty,
                                    CONFIRMADO = 0,
                                    EMITIDO = 0,
                                    SALVO = 0

                                };

                            _listaRevisoes.Add(revisao);


                        }
                    }

                    return true;
                }
                else
                {
                    foreach (var grupo in this.Planilha.ListaGrupos.Distinct().OrderBy(x => x.ORDENADOR))
                    {
                        foreach (var item in grupo.ListaItens.Distinct().OrderBy(x => x.ORDENADOR))
                        {

                            int ordeandor = 0;

                            int estadoIndefinido = 5;

                            Revisao revisao =
                              new Revisao()
                              {
                                  DATA_VERICACAO = DateTime.Now,
                                  GUID = Guid.NewGuid().ToString(),
                                  GUID_LV_ITEM = item.GUID,
                                  GUID_LV_VERIFICADOR = guidUsuarioCorrente,
                                  ID_ESTADO = estadoIndefinido,
                                  GUID_DOC_VERIFICACAO = _guid,
                                  INDICE = indiceRevisao,
                                  ORDENADOR = ordeandor,
                                  GUID_CONFIRMADO = string.Empty,
                                  CONFIRMADO = 0,
                                  EMITIDO = 0,
                                  SALVO = 0

                              };

                            _listaRevisoes.Add(revisao);

                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public virtual bool Salva(Confirmacao confirmacao, List<Revisao> revisoes)
        {

                _listaConfirmacoes.Add(confirmacao);

                foreach (var rev in revisoes)
                {
                    _listaRevisoes.Add(rev);
                    
                }

            return true;
        }



        public virtual bool AddRevisoesConfirmadas(string guidUsuario, bool isListaConfimacaoDupla,
           string guidConfirmacao, int ordenador, string indice, List<Revisao> plistaRevisoes)//, string guidUltimaConfirmacao)
        {

            var listaConfirmacaoOrdenada = _listaConfirmacoes.Distinct().OrderBy(x => x.ORDENADOR).ToList();

            if (isListaConfimacaoDupla)
            {
                if (!(listaConfirmacaoOrdenada.Count > 0))
                {
                    ordenador = 1;

                    var confirmacaoLevaDados = new Confirmacao()
                    {
                        GUID_DOCUMENTO = _guid,
                        GUID_USUARIO1 = guidUsuario,
                        GUID_USUARIO2 = null,
                        ORDENADOR = ordenador,
                        INDICE_REV = indice,
                        DATA = DateTime.Now,
                        GUID = guidConfirmacao
                    };


                    _listaConfirmacoes.Add(confirmacaoLevaDados);

                }
                else if (HouveSomentePrimeiraConfiramcao())
                {

                    var confirmacao = listaConfirmacaoOrdenada.Last();

                  

                    confirmacao.GUID_USUARIO2 = guidUsuario;
                    confirmacao.DATA = DateTime.Now;

                    //var listaRevisoesNaoConfirmadas = this.ListaRevisoes.Distinct().Where(x => x.CONFIRMADO == 0).ToList();

                    string guidUltimaConfirmacao = confirmacao.GUID;

                    foreach (var rev in plistaRevisoes)
                    {

                        _listaRevisoes.Add(rev);
                        //rev.CONFIRMADO = 1;
                        //rev.GUID_CONFIRMADO = guidUltimaConfirmacao;
                    }



                }
                else
                {
                    if (HouveConfimacaoNesteDocumento())
                    {
                        var ultimoOrdenador = listaConfirmacaoOrdenada.OrderBy(x => x.ORDENADOR).Last().ORDENADOR;
                        ordenador = ultimoOrdenador + 1;
                    }


                    var confirmacaoLevaDados = new Confirmacao()
                    {
                        GUID_DOCUMENTO = _guid,
                        GUID_USUARIO1 = guidUsuario,
                        GUID_USUARIO2 = null,
                        ORDENADOR = ordenador,
                        INDICE_REV = indice,
                        DATA = DateTime.Now,
                        GUID = guidConfirmacao
                    };


                    this.ListaConfirmacoes.Add(confirmacaoLevaDados);

                }


            }
            else
            {

                if (HouveConfimacaoNesteDocumento())
                {
                    var ultimoOrdenador = listaConfirmacaoOrdenada.OrderBy(x => x.ORDENADOR).Last().ORDENADOR;
                    ordenador = ultimoOrdenador + 1;
                }


                var confirmacao = new Confirmacao()
                {
                    DATA = DateTime.Now,
                    GUID = guidConfirmacao,
                    GUID_DOCUMENTO = _guid,
                    INDICE_REV = indice,
                    ORDENADOR = ordenador,
                    GUID_USUARIO1 = guidUsuario,
                    GUID_USUARIO2 = guidUsuario
                };



                _listaConfirmacoes.Add(confirmacao);

                //var listaRevisoesNaoConfirmadas = _listaRevisoes.Distinct().Where(x => x.CONFIRMADO == 0).ToList();


                foreach (var rev in plistaRevisoes)
                {
                    _listaRevisoes.Add(rev);
                    //rev.CONFIRMADO = 1;
                    //rev.GUID_CONFIRMADO = guidConfirmacao;
                }


            }

            return true;
        }

        public virtual bool ConfirmaRevisoes(string guidUsuario, bool isListaConfimacaoDupla,
            string guidConfirmacao, int ordenador, string indice)//, string guidUltimaConfirmacao)
        {

            var listaConfirmacaoOrdenada = _listaConfirmacoes.Distinct().OrderBy(x => x.ORDENADOR).ToList();

            if (isListaConfimacaoDupla)
            {
                if (!(listaConfirmacaoOrdenada.Count > 0))
                {
                    ordenador = 1;

                    var confirmacaoLevaDados = new Confirmacao()
                    {
                        GUID_DOCUMENTO = _guid,
                        GUID_USUARIO1 = guidUsuario,
                        GUID_USUARIO2 = null,
                        ORDENADOR = ordenador,
                        INDICE_REV = indice,
                        DATA = DateTime.Now,
                        GUID = guidConfirmacao
                    };


                    _listaConfirmacoes.Add(confirmacaoLevaDados);

                }
                else if (HouveSomentePrimeiraConfiramcao())
                {

                    var confirmacao = listaConfirmacaoOrdenada.Last();

                    //_listaConfirmacoes.Distinct().FirstOrDefault(x => x.GUID == guidUltimaConfirmacao);   //contextoConfirmacao.ReturnByGUID(confirmaViewModel.GuidUltimaConfirmacao);

                    confirmacao.GUID_USUARIO2 = guidUsuario;
                    confirmacao.DATA = DateTime.Now;

                    var listaRevisoesNaoConfirmadas = this.ListaRevisoes.Distinct().Where(x => x.CONFIRMADO == 0).ToList();

                    string guidUltimaConfirmacao = confirmacao.GUID;

                    foreach (var rev in listaRevisoesNaoConfirmadas)
                    {
                        rev.CONFIRMADO = 1;
                        rev.GUID_CONFIRMADO = guidUltimaConfirmacao;
                    }


                }
                else
                {
                    if (HouveConfimacaoNesteDocumento())
                    {
                        var ultimoOrdenador = listaConfirmacaoOrdenada.OrderBy(x => x.ORDENADOR).Last().ORDENADOR;
                        ordenador = ultimoOrdenador + 1;
                    }


                    var confirmacaoLevaDados = new Confirmacao()
                    {
                        GUID_DOCUMENTO = _guid,
                        GUID_USUARIO1 = guidUsuario,
                        GUID_USUARIO2 = null,
                        ORDENADOR = ordenador,
                        INDICE_REV = indice,
                        DATA = DateTime.Now,
                        GUID = guidConfirmacao
                    };


                    this.ListaConfirmacoes.Add(confirmacaoLevaDados);

                }


            }
            else
            {

                if (HouveConfimacaoNesteDocumento())
                {
                    var ultimoOrdenador = listaConfirmacaoOrdenada.OrderBy(x => x.ORDENADOR).Last().ORDENADOR;
                    ordenador = ultimoOrdenador + 1;
                }


                var confirmacao = new Confirmacao()
                {
                    DATA = DateTime.Now,
                    GUID = guidConfirmacao,
                    GUID_DOCUMENTO = _guid,
                    INDICE_REV = indice,
                    ORDENADOR = ordenador,
                    GUID_USUARIO1 = guidUsuario,
                    GUID_USUARIO2 = guidUsuario
                };



                _listaConfirmacoes.Add(confirmacao);

                var listaRevisoesNaoConfirmadas = _listaRevisoes.Distinct().Where(x => x.CONFIRMADO == 0).ToList();


                foreach (var rev in listaRevisoesNaoConfirmadas)
                {
                    rev.CONFIRMADO = 1;
                    rev.GUID_CONFIRMADO = guidConfirmacao;
                }


            }

            return true;
        }

        protected virtual bool aindaNaoJaInseriuDesteIndice(string indiceRevisao, List<Revisao> listaRevisoes)
        {
            return !listaRevisoes.Exists(x => x.INDICE == indiceRevisao);
        }

        //public virtual void MudaIndiceUltimaRevisao(string novoIndice, List<Revisao> listaRevisoesNoConfirm)
        //{
        //    foreach (var rev in listaRevisoesNoConfirm)
        //    {
        //        rev.INDICE = novoIndice;

        //    }
        //}


    }
}
