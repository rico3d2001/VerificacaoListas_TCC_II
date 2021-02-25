using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity;

namespace AppExcel
{


    public class LeitorArquivos
    {
        //public static void Ler(IEnumerable<FileInfo> arquivos, Disciplina disciplina) 
        //{
        //    var excelApp = new Application();
        //    excelApp.Visible = false;

        //    string guidConfiguracao = "";
        //    Configuracao cfg = null;
        //    var listaArquivos = new List<ArquivoListas>();

        //    using (var contextoConfiguracao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Configuracao>>())
        //    {
        //        contextoConfiguracao.Start();

        //        if (disciplina != null)//listaDisciplinas.Count > 0)
        //        {


        //            var listaConfiguracoes = contextoConfiguracao.Query().ToList();


        //            if (!listaConfiguracoes.Exists(x => x.NOME.Equals(disciplina.NOME)))
        //            {

        //                guidConfiguracao = Guid.NewGuid().ToString();

        //                cfg = new Configuracao()
        //                {
        //                    NOME = disciplina.NOME,
        //                    Disciplina = disciplina,
        //                    GUID = guidConfiguracao
        //                };

        //            }
        //            else
        //            {
        //                guidConfiguracao = listaConfiguracoes.Find(x => x.NOME.Equals(disciplina.NOME)).GUID;
        //                cfg = contextoConfiguracao.ReturnByGUID(guidConfiguracao);

        //            }

        //        }



        //        foreach (FileInfo file in arquivos)
        //        {

        //            if (file.Extension == ".xls" || file.Extension == ".XLS" || file.Extension == ".xlsx" || file.Extension == ".XLSX")
        //            {

        //                Worksheet workSheet = null;
        //                Workbook workBook = null;

        //                string nomeArquivo = file.Name.Split('.')[0].Trim();

        //                if (!nomeArquivo.Contains('$'))
        //                {
        //                    if (!(cfg.ListaArquivos.Count > 0) || !cfg.ListaArquivos.Distinct().ToList().Exists(x => x.NOME == nomeArquivo))
        //                    {
        //                        var livro = new ArquivoListas()
        //                        {
        //                            GUID = Guid.NewGuid().ToString(),
        //                            NOME = nomeArquivo,
        //                            Configuracao = cfg,
        //                            SIGLA = ""
        //                        };

        //                        cfg.ListaArquivos.Add(livro);

        //                    }

        //                    workBook = excelApp.Workbooks.Open(file.FullName);

        //                    var wsPlanilhas = workBook.Worksheets;

        //                    if (!(cfg.ListaArquivos.Last().ListaPlanilhas.Count > 0) || (cfg.ListaArquivos.Last().ListaPlanilhas.Distinct().ToList().Count > 0 && wsPlanilhas.Count > 0))
        //                    {
        //                        for (int i = 1; i < wsPlanilhas.Count + 1; i++)
        //                        {


        //                            workSheet = (Worksheet)wsPlanilhas.get_Item(i);

        //                            string nomeAba = workSheet.Name;

        //                            if (cfg.ListaArquivos.Last().ListaPlanilhas.Distinct().ToList().FirstOrDefault(x => x.NOME.ToUpper() == nomeAba.ToUpper()) == null)
        //                            {

        //                                string cell = getColuna(3, 1);
        //                                var funcao = workSheet.get_Range(cell, cell).Text;

        //                                cell = getColuna(4, 1);
        //                                var descricaoPlanilha = workSheet.get_Range(cell, cell).Text;

        //                                Planilha planilha = new Planilha()
        //                                {
        //                                    GUID = Guid.NewGuid().ToString(),
        //                                    NOME = workSheet.Name,
        //                                    FUNCAO = funcao,
        //                                    DESCRICAO = descricaoPlanilha
        //                                };
        //                                cfg.ListaArquivos.Last().ListaPlanilhas.Add(planilha);

        //                                LeitoraPlanilha.Ler(workSheet, cfg, contextoConfiguracao);
        //                            }
        //                            else
        //                            {
        //                                LeitoraPlanilha.Ler(workSheet, cfg, contextoConfiguracao);
        //                            }



        //                        }
        //                    }
        //                    else if (wsPlanilhas.Count > 0)
        //                    {
        //                        for (int i = 1; i < wsPlanilhas.Count + 1; i++)
        //                        {
        //                            workSheet = (Worksheet)wsPlanilhas.get_Item(i);

        //                            string cell = getColuna(3, 1);
        //                            var funcao = workSheet.get_Range(cell, cell).Text;

        //                            cell = getColuna(4, 1);
        //                            var descricaoPlanilha = workSheet.get_Range(cell, cell).Text;

        //                            Planilha planilha = new Planilha()
        //                            {
        //                                GUID = Guid.NewGuid().ToString(),
        //                                NOME = workSheet.Name,
        //                                FUNCAO = funcao,
        //                                DESCRICAO = descricaoPlanilha
        //                            };

        //                            cfg.ListaArquivos.Last().ListaPlanilhas.Add(planilha);


        //                            LeitoraPlanilha.Ler(workSheet, cfg, contextoConfiguracao);
        //                        }
        //                    }

        //                    workBook.Close(false);

        //                }

        //                workSheet = null;

        //            }

        //        }

        //        contextoConfiguracao.Insert(cfg);
        //        contextoConfiguracao.Commit();

        //    }


        //}

        public static void LerUnico(FileInfo file, Disciplina disciplina)
        {
            var excelApp = new Application();
            excelApp.Visible = false;

            string guidConfiguracao = "";
            Configuracao cfg = null;
            var listaArquivos = new List<ArquivoListas>();

            using (var contextoConfiguracao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Configuracao>>())
            {
                contextoConfiguracao.Start();

                if (disciplina != null)//listaDisciplinas.Count > 0)
                {


                    var listaConfiguracoes = contextoConfiguracao.Query().ToList();


                    if (!listaConfiguracoes.Exists(x => x.NOME.Equals(disciplina.NOME)))
                    {

                        guidConfiguracao = Guid.NewGuid().ToString();

                        cfg = new Configuracao()
                        {
                            NOME = disciplina.NOME,
                            Disciplina = disciplina,
                            GUID = guidConfiguracao
                        };

                    }
                    else
                    {
                        guidConfiguracao = listaConfiguracoes.Find(x => x.NOME.Equals(disciplina.NOME)).GUID;
                        cfg = contextoConfiguracao.ReturnByGUID(guidConfiguracao);

                    }

                }



                //foreach (FileInfo file in arquivos)
                //{

                //if (file.Extension == ".xls" || file.Extension == ".XLS" || file.Extension == ".xlsx" || file.Extension == ".XLSX")
                //{

                Worksheet workSheet = null;
                Workbook workBook = null;

                string nomeArquivo = file.Name.Split('.')[0].Trim();

                //if (!nomeArquivo.Contains('$'))
                //{

                bool existeArquivo = cfg.ListaArquivos.Distinct().ToList().Exists(x => x.NOME == nomeArquivo);
                ArquivoListas livro = null;
                if (!(cfg.ListaArquivos.Count > 0) || !cfg.ListaArquivos.Distinct().ToList().Exists(x => x.NOME == nomeArquivo))
                {
                    livro = new ArquivoListas()
                    {
                        GUID = Guid.NewGuid().ToString(),
                        NOME = nomeArquivo,
                        Configuracao = cfg,
                        SIGLA = ""
                    };

                    cfg.ListaArquivos.Add(livro);

                }
                else
                {
                    livro = cfg.ListaArquivos.Distinct().First(x => x.NOME == nomeArquivo);
                }

                workBook = excelApp.Workbooks.Open(file.FullName);

                var wsPlanilhas = workBook.Worksheets;

                if (wsPlanilhas.Count > 0)
                {
                    for (int i = 1; i < wsPlanilhas.Count + 1; i++)
                    {


                        workSheet = (Worksheet)wsPlanilhas.get_Item(i);

                        string nomeAba = workSheet.Name;

                        if (livro.ListaPlanilhas.Distinct().ToList().FirstOrDefault(x => x.NOME.ToUpper() == nomeAba.ToUpper()) == null)
                        {


                            string cell = getColuna(3, 1);
                            string funcao = workSheet.get_Range(cell, cell).Text;

                            cell = getColuna(4, 1);
                            string descricaoPlanilha = workSheet.get_Range(cell, cell).Text;

                            Planilha planilha = new Planilha()
                            {
                                GUID = Guid.NewGuid().ToString(),
                                NOME = workSheet.Name,
                                FUNCAO = funcao,
                                DESCRICAO = descricaoPlanilha,
                                VERIFICADOR_UNICO = 1,
                                Tipo = livro
                                
                            };
                            livro.ListaPlanilhas.Add(planilha);

                            LeitoraPlanilha.Ler(workSheet, planilha, contextoConfiguracao);
                        }
                        //else
                        //{
                        //    LeitoraPlanilha.Ler(workSheet, cfg, contextoConfiguracao);
                        //}



                    }
                }
                //else if (wsPlanilhas.Count > 0)
                //{
                //    for (int i = 1; i < wsPlanilhas.Count + 1; i++)
                //    {
                //        workSheet = (Worksheet)wsPlanilhas.get_Item(i);

                //        string cell = getColuna(3, 1);
                //        var funcao = workSheet.get_Range(cell, cell).Text;

                //        cell = getColuna(4, 1);
                //        var descricaoPlanilha = workSheet.get_Range(cell, cell).Text;

                //        Planilha planilha = new Planilha()
                //        {
                //            GUID = Guid.NewGuid().ToString(),
                //            NOME = workSheet.Name,
                //            FUNCAO = funcao,
                //            DESCRICAO = descricaoPlanilha
                //        };

                //        cfg.ListaArquivos.Last().ListaPlanilhas.Add(planilha);


                //        LeitoraPlanilha.Ler(workSheet, cfg, contextoConfiguracao);
                //    }
                //}

                workBook.Close(false);

                //}

                workSheet = null;

                //}

                //}

                contextoConfiguracao.Update(cfg);
                contextoConfiguracao.Commit();

            }


        }


        private static string getColuna(int iLin, int iCol)
        {
            String[] letras = new String[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            if (iCol >= 703)
            {
                int iCol1 = 0;
                int iCol2 = 0;
                int iCol3 = 0;

                iCol2 = iCol / 26;
                if (iCol % 26 == 0)
                {
                    iCol2 -= 1;
                    iCol3 = 26;
                }
                else
                {
                    iCol3 = iCol % 26;
                }

                iCol1 = iCol2 / 26;
                iCol2 = iCol2 % 26;

                return letras[iCol1] + letras[iCol2] + letras[iCol3] + iLin.ToString();
            }
            else
            {
                int iCol1 = 0;
                int iCol2 = 0;

                iCol1 = iCol / 26;
                if (iCol % 26 == 0)
                {
                    iCol1 -= 1;
                    iCol2 = 26;
                }
                else
                {
                    iCol2 = iCol % 26;
                }

                return letras[iCol1] + letras[iCol2] + iLin.ToString();
            }
        }
    }

}
