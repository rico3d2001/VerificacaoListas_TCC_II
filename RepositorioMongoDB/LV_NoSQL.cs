using EntidadesRepositoriosLeitura;
using LVModel;
using MongoDB.Driver;
using RepositorioLeitura.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositorioMongoDB
{
    public class LV_NoSQL
    {
        private readonly string _servidorMDB;

        public LV_NoSQL()
        {
            _servidorMDB = "mongodb://localhost";
            //_servidorMDB = "mongodb://sli3117";
        }

        public List<ConfiguracaoNavDTO> PegaConfiguracaoNavDTO()
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("lv_template");
            IMongoCollection<ConfiguracaoNavDTO> col =
                database.GetCollection<ConfiguracaoNavDTO>("ConfiguracaoNavDTO");

            return col.AsQueryable<ConfiguracaoNavDTO>().ToList();
        }

        public List<ProjetoToListDTO> PegaProjetoToListDTO()
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("lv");
            IMongoCollection<ProjetoToListDTO> col =
                database.GetCollection<ProjetoToListDTO>("ProjetoToListDTO");

            return col.AsQueryable<ProjetoToListDTO>().ToList();
        }

        public List<ArquivoNavDTO> PegaArquivoNavDTO(string guidConfiguracao)
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("lv_template");
            IMongoCollection<ArquivoNavDTO> colNews =
                database.GetCollection<ArquivoNavDTO>("ArquivoNavDTO");

            var filter = Builders<ArquivoNavDTO>.Filter.Eq("GUID", guidConfiguracao);
            return colNews.Find(filter).ToList();
        }

        

        public List<PlanilhaNavDTO> PegaPlanilhaNavDTO(string guidConfiguracao)
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("lv_template");
            IMongoCollection<PlanilhaNavDTO> colNews =
                database.GetCollection<PlanilhaNavDTO>("PlanilhaNavDTO");

            var filter = Builders<PlanilhaNavDTO>.Filter.Eq("GUID", guidConfiguracao);
            return colNews.Find(filter).ToList();
        }

        public bool CriarLV_ViewModel(ListaVerficacaoVM listaVerficacaoVM)
        {




            try
            {
                IMongoClient client = new MongoClient(_servidorMDB);
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colNews = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");
                colNews.InsertOne(listaVerficacaoVM);

                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }



        }

        public List<ListaVerficacaoVM> ListaLVS()
        {
            List<ListaVerficacaoVM> lista = new List<ListaVerficacaoVM>();

            try
            {
                IMongoClient client = new MongoClient("mongodb://localhost");
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colNews = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");

                lista = colNews.AsQueryable<ListaVerficacaoVM>().ToList();
 
            }
            catch (Exception)
            {

                throw;
            }


            return lista;
        }

        public ListaVerficacaoVM BuscarLV_ViewModel(string guidDocumento)
        {
            ListaVerficacaoVM lv;

            try
            {
                IMongoClient client = new MongoClient("mongodb://localhost");
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colNews = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");



                var filter = Builders<ListaVerficacaoVM>.Filter.Eq("GUID", guidDocumento);
                var lista = colNews.Find(filter).ToList();
                if (lista.Count() > 0)
                {
                    lv = lista.First();
                }
                else
                {
                    return null;
                }



            }
            catch (Exception)
            {

                throw;
            }


            return lv;
        }



        public ListaVerficacaoVM AcrescentarRevisoes_ViewModel(ValoresColunasRev valores)
        {
            ListaVerficacaoVM lv = null;

            try
            {
                IMongoClient client = new MongoClient(_servidorMDB);
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colecao = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");



                var filterLV = Builders<ListaVerficacaoVM>.Filter.Eq("GUID", valores.Guid_LV);

                lv = colecao.Find(filterLV).FirstOrDefault();

                if (lv != null)
                {


                    var ultimaColuna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();
                    if (lv.Colunas.Count() == 1 && string.IsNullOrEmpty(ultimaColuna.INDICE_REV))
                    {
                        ultimaColuna.INDICE_REV = valores.IndiceRevisao;
                        foreach (var grupo in ultimaColuna.LV_Grupos)
                        {
                            foreach (var linha in grupo.Linhas)
                            {
                                linha.GUID_REVISAO = Guid.NewGuid().ToString();
                            }
                        }
                    }
                    else
                    {
                        var novaColuna = new ColunaLVVM()
                        {
                            INDICE_REV = valores.IndiceRevisao,
                            ORDENADOR = ultimaColuna.ORDENADOR + 1,
                            LV_Grupos = new List<LV_GrupoVM>()
                        };

                        foreach (var grupo in ultimaColuna.LV_Grupos)
                        {

                            var novoGrupo = new LV_GrupoVM()
                            {
                                GUID = grupo.GUID,
                                NOME = grupo.NOME,
                                Linhas = new List<LinhaRevisaoVM>()
                            };

                            foreach (var linha in grupo.Linhas)
                            {
                                novoGrupo.Linhas.Add(new LinhaRevisaoVM()
                                {
                                    GUID_ITEM = linha.GUID_ITEM,
                                    GUID_REVISAO = Guid.NewGuid().ToString(),
                                    ID_ESTADO = linha.ID_ESTADO,
                                    DESCRICAO = linha.DESCRICAO,
                                    ORDENADOR = linha.ORDENADOR,
                                    CONFIRMADO = 0
                                });
                            }

                            novaColuna.LV_Grupos.Add(novoGrupo);
                        }

                        lv.Colunas.Add(novaColuna);
                    }

                }

                ReplaceOneResult result = colecao.ReplaceOne(filterLV, lv);

            }
            catch (Exception)
            {

                throw;
            }

            return lv;

        }

        public void MudaIndice(string guidlv, string novoIndice)
        {
            ListaVerficacaoVM lv = null;

            try
            {
                IMongoClient client = new MongoClient(_servidorMDB);
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colecao = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");



                var filterLV = Builders<ListaVerficacaoVM>.Filter.Eq("GUID", guidlv);

                lv = colecao.Find(filterLV).FirstOrDefault();



                if (lv.Colunas.Count() > 0)
                {
                    var ultimaColuna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

                    ultimaColuna.INDICE_REV = novoIndice;

                }

                ReplaceOneResult result = colecao.ReplaceOne(filterLV, lv);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RetomarVM(string guidlv)
        {

            ListaVerficacaoVM lv = null;

            try
            {
                IMongoClient client = new MongoClient(_servidorMDB);
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colecao = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");



                var filterLV = Builders<ListaVerficacaoVM>.Filter.Eq("GUID", guidlv);

                lv = colecao.Find(filterLV).FirstOrDefault();



                if (lv.Colunas.Count() > 0)
                {

                    var ultimaColuna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

                    foreach (var grupo in ultimaColuna.LV_Grupos)
                    {
                        foreach (var linha in grupo.Linhas)
                        {
                            linha.CONFIRMADO = 0;
                        }
                    }

                    var confirmacao = lv.Confirmacoes.First(x => x.CONFIRMACAO_INDICE == ultimaColuna.INDICE_REV);

                    lv.Confirmacoes.Remove(confirmacao);

                }

                ReplaceOneResult result = colecao.ReplaceOne(filterLV, lv);

            }
            catch (Exception)
            {

                throw;
            }

          
        }

        public void ConfirmacaoRevisaoVM(string guidlv, Usuario ususario)
        {


            ListaVerficacaoVM lv;

            try
            {
                IMongoClient client = new MongoClient(_servidorMDB);
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colecao = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");



                var filterLV = Builders<ListaVerficacaoVM>.Filter.Eq("GUID", guidlv);

                lv = colecao.Find(filterLV).FirstOrDefault();



                if (lv.Colunas.Count() > 0) 
                {
                    var ultimaColuna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

                    if (lv.VERFICADOR_UNICO == 1)
                    {
                        if(lv.Colunas.Count() == 1 && lv.Confirmacoes.Count() == 1)
                        {
                            var ultimaConfirmacao = lv.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).Last();
                            ultimaConfirmacao.CONFIRMACAO_GUID = Guid.NewGuid().ToString();
                            ultimaConfirmacao.CONFIRMACAO_DATA = DateTime.Now;
                            ultimaConfirmacao.CONFIRMACAO_ORDENADOR = ultimaColuna.ORDENADOR;
                            ultimaConfirmacao.CONFIRMACAO_ID_USER1 = ususario.GUID;
                            ultimaConfirmacao.CONFIRMACAO_NOME_USER1 = ususario.NOME;
                            ultimaConfirmacao.CONFIRMACAO_SIGLA_USER1 = ususario.SIGLA;
                            ultimaConfirmacao.CONFIRMACAO_ID_USER2 = ususario.GUID;
                            ultimaConfirmacao.CONFIRMACAO_NOME_USER2 = ususario.NOME;
                            ultimaConfirmacao.CONFIRMACAO_SIGLA_USER2 = ususario.SIGLA;
                            ultimaConfirmacao.CONFIRMACAO_INDICE = ultimaColuna.INDICE_REV;
                        }
                        else
                        {
                            lv.Confirmacoes.Add(new ConfirmacaoVM()
                            {
                                CONFIRMACAO_GUID = Guid.NewGuid().ToString(),
                                CONFIRMACAO_DATA = DateTime.Now,
                                CONFIRMACAO_ORDENADOR = ultimaColuna.ORDENADOR,
                                CONFIRMACAO_ID_USER1 = ususario.GUID,
                                CONFIRMACAO_NOME_USER1 = ususario.NOME,
                                CONFIRMACAO_SIGLA_USER1 = ususario.SIGLA,
                                CONFIRMACAO_ID_USER2 = ususario.GUID,
                                CONFIRMACAO_NOME_USER2 = ususario.NOME,
                                CONFIRMACAO_SIGLA_USER2 = ususario.SIGLA,
                                CONFIRMACAO_INDICE = ultimaColuna.INDICE_REV
                            });
                        }
                        



                        foreach (var grupo in ultimaColuna.LV_Grupos)
                        {
                            foreach (var linha in grupo.Linhas)
                            {
                                linha.CONFIRMADO = 1;
                            }
                        }
                    }
                    else
                    {

                        if (lv.Colunas.Count() == 1 && lv.Confirmacoes.Count() == 1)
                        {
                            var conf = lv.Confirmacoes.FirstOrDefault(x => x.CONFIRMACAO_INDICE == ultimaColuna.INDICE_REV);
                            if (conf == null)
                            {
                                var ultimaConfirmacao = lv.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).Last();
                                ultimaConfirmacao.CONFIRMACAO_GUID = Guid.NewGuid().ToString();
                                ultimaConfirmacao.CONFIRMACAO_DATA = DateTime.Now;
                                ultimaConfirmacao.CONFIRMACAO_ORDENADOR = ultimaColuna.ORDENADOR;
                                ultimaConfirmacao.CONFIRMACAO_ID_USER1 = ususario.GUID;
                                ultimaConfirmacao.CONFIRMACAO_NOME_USER1 = ususario.NOME;
                                ultimaConfirmacao.CONFIRMACAO_SIGLA_USER1 = ususario.SIGLA;
                                ultimaConfirmacao.CONFIRMACAO_INDICE = ultimaColuna.INDICE_REV;
                            }
                            else
                            {
                                conf.CONFIRMACAO_ID_USER2 = ususario.GUID;
                                conf.CONFIRMACAO_NOME_USER2 = ususario.NOME;
                                conf.CONFIRMACAO_SIGLA_USER2 = ususario.SIGLA;

                                var coluna = lv.Colunas.First(x => x.INDICE_REV == conf.CONFIRMACAO_INDICE);

                                foreach (var grupo in coluna.LV_Grupos)
                                {
                                    foreach (var linha in grupo.Linhas)
                                    {
                                        linha.CONFIRMADO = 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var conf = lv.Confirmacoes.FirstOrDefault(x => x.CONFIRMACAO_INDICE == ultimaColuna.INDICE_REV);

                            if (conf == null)
                            {
                                lv.Confirmacoes.Add(new ConfirmacaoVM()
                                {
                                    CONFIRMACAO_GUID = Guid.NewGuid().ToString(),
                                    CONFIRMACAO_DATA = DateTime.Now,
                                    CONFIRMACAO_ORDENADOR = ultimaColuna.ORDENADOR,
                                    CONFIRMACAO_ID_USER1 = ususario.GUID,
                                    CONFIRMACAO_NOME_USER1 = ususario.NOME,
                                    CONFIRMACAO_SIGLA_USER1 = ususario.SIGLA,
                                    CONFIRMACAO_INDICE = ultimaColuna.INDICE_REV
                                });
                            }
                            else
                            {
                                conf.CONFIRMACAO_ID_USER2 = ususario.GUID;
                                conf.CONFIRMACAO_NOME_USER2 = ususario.NOME;
                                conf.CONFIRMACAO_SIGLA_USER2 = ususario.SIGLA;

                                var coluna = lv.Colunas.First(x => x.INDICE_REV == conf.CONFIRMACAO_INDICE);

                                foreach (var grupo in coluna.LV_Grupos)
                                {
                                    foreach (var linha in grupo.Linhas)
                                    {
                                        linha.CONFIRMADO = 1;
                                    }
                                }
                            }
                        }

                    }
                }

                ReplaceOneResult result = colecao.ReplaceOne(filterLV, lv);

            }
            catch (Exception)
            {

                throw;
            }

            //return lv;
        }


        //public ListaVerficacaoVM ConfirmacaoRevisaoVM(ValoresConfirma valores)
        //{
        //    ListaVerficacaoVM lv = null;

        //    try
        //    {
        //        IMongoClient client = new MongoClient(_servidorMDB);
        //        IMongoDatabase database = client.GetDatabase("lv");
        //        IMongoCollection<ListaVerficacaoVM> colecao = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");



        //        var filterLV = Builders<ListaVerficacaoVM>.Filter.Eq("GUID", valores.GUID_LV);

        //        lv = colecao.Find(filterLV).FirstOrDefault();

        //        if (lv != null)
        //        {

        //            if (lv.Colunas.Count() > 1 && !string.IsNullOrEmpty(lv.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).Last().CONFIRMACAO_INDICE))
        //            {
        //                if (lv.VERFICADOR_UNICO == 1)
        //                {
        //                    lv.Confirmacoes.Add(new ConfirmacaoVM()
        //                    {
        //                        CONFIRMACAO_GUID = valores.GUID_CONFIRMACAO,
        //                        CONFIRMACAO_DATA = DateTime.Now,
        //                        CONFIRMACAO_ORDENADOR = lv.Confirmacoes.Last().CONFIRMACAO_ORDENADOR + 1,
        //                        CONFIRMACAO_ID_USER1 = valores.GUID_USUARIO1,
        //                        CONFIRMACAO_NOME_USER1 = valores.NOME_USUARIO1,
        //                        CONFIRMACAO_SIGLA_USER1 = valores.GUID_USUARIO1,
        //                        CONFIRMACAO_ID_USER2 = valores.GUID_USUARIO2,
        //                        CONFIRMACAO_NOME_USER2 = valores.NOME_USUARIO2,
        //                        CONFIRMACAO_SIGLA_USER2 = valores.GUID_USUARIO2,
        //                        CONFIRMACAO_INDICE = valores.INDICE
        //                    });

        //                    var coluna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

        //                    foreach (var grupo in coluna.LV_Grupos)
        //                    {
        //                        foreach (var linha in grupo.Linhas)
        //                        {
        //                            linha.CONFIRMADO = 1;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (lv.Colunas.Count() > lv.Confirmacoes.Count())
        //                    {
        //                        lv.Confirmacoes.Add(new ConfirmacaoVM()
        //                        {
        //                            CONFIRMACAO_GUID = valores.GUID_CONFIRMACAO,
        //                            CONFIRMACAO_DATA = DateTime.Now,
        //                            CONFIRMACAO_ORDENADOR = lv.Confirmacoes.Last().CONFIRMACAO_ORDENADOR + 1,
        //                            CONFIRMACAO_ID_USER1 = valores.GUID_USUARIO1,
        //                            CONFIRMACAO_NOME_USER1 = valores.NOME_USUARIO1,
        //                            CONFIRMACAO_SIGLA_USER1 = valores.GUID_USUARIO1,
        //                            CONFIRMACAO_INDICE = valores.INDICE
        //                        });
        //                    }
        //                    else
        //                    {
        //                        var ultima = lv.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).Last();
        //                        if (!string.IsNullOrEmpty(ultima.CONFIRMACAO_ID_USER1) && string.IsNullOrEmpty(ultima.CONFIRMACAO_ID_USER2))
        //                        {
        //                            ultima.CONFIRMACAO_ID_USER2 = valores.GUID_USUARIO2;
        //                            ultima.CONFIRMACAO_NOME_USER2 = valores.NOME_USUARIO1;
        //                            ultima.CONFIRMACAO_SIGLA_USER2 = valores.GUID_USUARIO1;

        //                            var coluna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

        //                            foreach (var grupo in coluna.LV_Grupos)
        //                            {
        //                                foreach (var linha in grupo.Linhas)
        //                                {
        //                                    linha.CONFIRMADO = 1;
        //                                }
        //                            }


        //                        }
        //                        else if (string.IsNullOrEmpty(ultima.CONFIRMACAO_ID_USER1) && string.IsNullOrEmpty(ultima.CONFIRMACAO_ID_USER2))
        //                        {
        //                            lv.Confirmacoes.Add(new ConfirmacaoVM()
        //                            {
        //                                CONFIRMACAO_GUID = valores.GUID_CONFIRMACAO,
        //                                CONFIRMACAO_DATA = DateTime.Now,
        //                                CONFIRMACAO_ORDENADOR = lv.Confirmacoes.Last().CONFIRMACAO_ORDENADOR + 1,
        //                                CONFIRMACAO_ID_USER1 = valores.GUID_USUARIO1,
        //                                CONFIRMACAO_NOME_USER1 = valores.NOME_USUARIO1,
        //                                CONFIRMACAO_SIGLA_USER1 = valores.GUID_USUARIO1,
        //                                CONFIRMACAO_INDICE = valores.INDICE
        //                            });
        //                        }

        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (lv.VERFICADOR_UNICO == 1)
        //                {

        //                    var coluna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

        //                    var unica = lv.Confirmacoes.First();

        //                    unica.CONFIRMACAO_GUID = valores.GUID_CONFIRMACAO;
        //                    unica.CONFIRMACAO_DATA = DateTime.Now;
        //                    unica.CONFIRMACAO_ORDENADOR = coluna.ORDENADOR;
        //                    unica.CONFIRMACAO_ID_USER1 = valores.GUID_USUARIO1;
        //                    unica.CONFIRMACAO_NOME_USER1 = valores.NOME_USUARIO1;
        //                    unica.CONFIRMACAO_SIGLA_USER1 = valores.GUID_USUARIO1;
        //                    unica.CONFIRMACAO_ID_USER2 = valores.GUID_USUARIO2;
        //                    unica.CONFIRMACAO_NOME_USER2 = valores.NOME_USUARIO2;
        //                    unica.CONFIRMACAO_SIGLA_USER2 = valores.GUID_USUARIO2;
        //                    unica.CONFIRMACAO_INDICE = valores.INDICE;




        //                    foreach (var grupo in coluna.LV_Grupos)
        //                    {
        //                        foreach (var linha in grupo.Linhas)
        //                        {
        //                            linha.CONFIRMADO = 1;
        //                        }
        //                    }

        //                }
        //                else
        //                {
        //                    var unica = lv.Confirmacoes.First();

        //                    if (!string.IsNullOrEmpty(unica.CONFIRMACAO_ID_USER1) && string.IsNullOrEmpty(unica.CONFIRMACAO_ID_USER2))
        //                    {
        //                        unica.CONFIRMACAO_ID_USER2 = valores.GUID_USUARIO2;
        //                        unica.CONFIRMACAO_NOME_USER2 = valores.NOME_USUARIO2;
        //                        unica.CONFIRMACAO_SIGLA_USER2 = valores.GUID_USUARIO2;

        //                        var coluna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

        //                        foreach (var grupo in coluna.LV_Grupos)
        //                        {
        //                            foreach (var linha in grupo.Linhas)
        //                            {
        //                                linha.CONFIRMADO = 1;
        //                            }
        //                        }

        //                    }
        //                    else if (string.IsNullOrEmpty(unica.CONFIRMACAO_ID_USER1) && string.IsNullOrEmpty(unica.CONFIRMACAO_ID_USER2))
        //                    {
        //                        var coluna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

        //                        unica.CONFIRMACAO_GUID = valores.GUID_CONFIRMACAO;
        //                        unica.CONFIRMACAO_DATA = DateTime.Now;
        //                        unica.CONFIRMACAO_ORDENADOR = coluna.ORDENADOR;
        //                        unica.CONFIRMACAO_ID_USER1 = valores.GUID_USUARIO1;
        //                        unica.CONFIRMACAO_NOME_USER1 = valores.NOME_USUARIO1;
        //                        unica.CONFIRMACAO_SIGLA_USER1 = valores.GUID_USUARIO1;
        //                        unica.CONFIRMACAO_INDICE = valores.INDICE;

        //                    }
        //                }
        //            }



        //        }

        //        //Aguuardar codigo
        //        ReplaceOneResult result = colecao.ReplaceOne(filterLV, lv);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return lv;
        //}


        public void EmitirRevisaoVM(string guidLV)
        {
            ListaVerficacaoVM lv = null;

            try
            {
                IMongoClient client = new MongoClient(_servidorMDB);
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colecao = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");



                var filterLV = Builders<ListaVerficacaoVM>.Filter.Eq("GUID", guidLV);

                lv = colecao.Find(filterLV).FirstOrDefault();

                if (lv != null)
                {
                    var ultimaConfirmacao = lv.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).Last();
                    if (!string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER2))
                    {
                        var coluna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();

                        foreach (var grupo in coluna.LV_Grupos)
                        {
                            foreach (var linha in grupo.Linhas)
                            {
                                linha.EMITIDO = 1;
                            }
                        }
                    }



                }

                //Aguuardar codigo
                ReplaceOneResult result = colecao.ReplaceOne(filterLV, lv);

            }
            catch (Exception)
            {

                throw;
            }

         
        }


        public ListaVerficacaoVM MudaEstadoRevisao_ViewModel(RevisaoVM valores)
        {
            ListaVerficacaoVM lv = null;

            try
            {
                IMongoClient client = new MongoClient(_servidorMDB);
                IMongoDatabase database = client.GetDatabase("lv");
                IMongoCollection<ListaVerficacaoVM> colecao = database.GetCollection<ListaVerficacaoVM>("ListaVerficacaoVM");



                var filterLV = Builders<ListaVerficacaoVM>.Filter.Eq("GUID", valores.GUID_DOC_VERIFICACAO);

                lv = colecao.Find(filterLV).FirstOrDefault();

                if (lv != null)
                {
                    var coluna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();
                    foreach (var grupo in coluna.LV_Grupos)
                    {
                        var linha = grupo.Linhas.FirstOrDefault(x => x.GUID_REVISAO == valores.GUID);

                        if (linha != null)
                        {
                            linha.ID_ESTADO = valores.ID_ESTADO;
                            break;
                        }



                        //foreach (var linha in grupo.Linhas)
                        //{
                        //    linha.GUID_REVISAO = Guid.NewGuid().ToString();
                        //}
                    }
                }

                ReplaceOneResult result = colecao.ReplaceOne(filterLV, lv);

            }
            catch (Exception)
            {

                throw;
            }

            return lv;

        }


    }
}
