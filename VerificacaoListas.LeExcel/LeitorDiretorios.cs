using LV14FluentNHB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace VerificacaoListas.LeExcel
{
    public class LeitorDiretorios
    {
        public static void Ler(string diretorioBase)
        {

            Repository<LV_DISCIPLINA> repositoryDisciplina = new Repository<LV_DISCIPLINA>();
            var listaDisciplinas = repositoryDisciplina.Query() as List<LV_DISCIPLINA>;

            


            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            

            DirectoryInfo dir = new DirectoryInfo(diretorioBase);

            foreach (var pasta in dir.EnumerateDirectories())
            {
                string sigla = pasta.Name.Split('-')[0].Trim();
                string nomePasta = pasta.Name.Split('-')[1].Trim();

                if (listaDisciplinas.Exists(x => x.SIGLA.Equals(sigla)) && listaDisciplinas.Exists(x => x.NOME.Equals(nomePasta)))
                {

                    var disciplina = listaDisciplinas.Find(x => x.SIGLA.Equals(sigla) && x.NOME.Equals(nomePasta));

                    var arquivos = pasta.EnumerateFiles();

                    LeitorArquivos.Ler(arquivos, disciplina.NOME);
                }
                

            }

            //excelApp.Workbooks.Close();
            excelApp.Quit();

            
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);

        }
    }
}
