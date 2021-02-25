using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using Microsoft.Office.Interop.Excel;
using Unity;

namespace AppExcel
{

    public class LeitorDiretorios
    {
        private static List<Disciplina> listaDisciplinas;

        public static void Ler(string diretorioBase)
        {

            listaDisciplinas = new List<Disciplina>();

            Application excelApp = new Application();
            excelApp.Visible = false;

            DirectoryInfo dir = new DirectoryInfo(diretorioBase);

            foreach (var pasta in dir.EnumerateDirectories())
            {
                string sigla = pasta.Name.Split('-')[0].Trim();
                string nomePasta = pasta.Name.Split('-')[1].Trim();
                Disciplina disciplina;
                using (var contextoDisciplina = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>())
                {
                    contextoDisciplina.Start();

                    listaDisciplinas = contextoDisciplina.Query().ToList();

                    if (listaDisciplinas.Exists(x => x.SIGLA.Equals(sigla)) && listaDisciplinas.Exists(x => x.NOME.Equals(nomePasta)))
                    {

                        disciplina = listaDisciplinas.Find(x => x.SIGLA.Equals(sigla) && x.NOME.Equals(nomePasta));

                    }
                    else
                    {
                        int id = listaDisciplinas.OrderBy(x => x.ID_DISCIPLINA).Last().ID_DISCIPLINA + 1;
                        disciplina = new Disciplina()
                        { ID_DISCIPLINA = id, NOME = nomePasta, SIGLA = sigla };

                        contextoDisciplina.Insert(disciplina);
                        contextoDisciplina.Commit();
                    }
                }
                var arquivos = pasta.EnumerateFiles();


                foreach (FileInfo file in arquivos)
                {

                    if (file.Extension == ".xls" || file.Extension == ".XLS" || file.Extension == ".xlsx" || file.Extension == ".XLSX")
                    {

                        //Worksheet workSheet = null;
                        //Workbook workBook = null;

                        string nomeArquivo = file.Name.Split('.')[0].Trim();

                        if (!nomeArquivo.Contains('$'))
                        {
                            LeitorArquivos.LerUnico(file, disciplina);//, listaDisciplinas);
                        }

                    }

                }

            }

            excelApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);


        }
    }


}
