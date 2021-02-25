using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.Threading.Tasks;
using LV14FluentNHB;
using LVModel;

namespace VerificacaoListas.LeExcel
{
    public class LeitorArquivos
    {
        public static void Ler(IEnumerable<FileInfo> arquivos,Disciplina strDisciplina) //LV_DISCIPLINA disciplina)
        {
            var listaDisciplinas = new Repository<LV_DISCIPLINA>().GetByProperty("NOME", strDisciplina);

            if (listaDisciplinas.Count > 0)
            {

                LV_DISCIPLINA disciplina = listaDisciplinas.First() as LV_DISCIPLINA;

                var excelApp = new Excel.Application();
                // Make the object visible.
                excelApp.Visible = false;


                Repository<LV_CONFIGURACAO> repositoryConfiguracao = new Repository<LV_CONFIGURACAO>();
                var listaConfiguracoes = repositoryConfiguracao.Query() as List<LV_CONFIGURACAO>;

                string guidConfiguracao = "";

                if (!listaConfiguracoes.Exists(x => x.NOME.Equals(disciplina.NOME)))
                {

                    guidConfiguracao = Guid.NewGuid().ToString();

                    LV_CONFIGURACAO configuracao = new LV_CONFIGURACAO()
                    {
                        NOME = disciplina.NOME,
                        ID_DISCIPLINA = disciplina.ID_DISCIPLINA,
                        GUID = guidConfiguracao
                    };

                    repositoryConfiguracao.Insert(configuracao);
                }
                else
                {
                    guidConfiguracao = listaConfiguracoes.Find(x => x.NOME.Equals(disciplina.NOME)).GUID;
                }


                Repository<LV_TIPO> repositoryLV = new Repository<LV_TIPO>();




                foreach (FileInfo file in arquivos)
                {


                    if (file.Extension == ".xls" || file.Extension == ".XLS" || file.Extension == ".xlsx" || file.Extension == ".XLSX")
                    {


                        string nomeArquivo = file.Name.Split('.')[0].Trim();


                        LV_TIPO tipo = new LV_TIPO()
                        {
                            GUID = Guid.NewGuid().ToString(),
                            GUID_CONFIG = guidConfiguracao,
                            NOME = nomeArquivo
                        };

                        repositoryLV.Insert(tipo);






                        Excel.Workbook workBook = excelApp.Workbooks.Open(file.FullName);

                        var wsPlanilhas = workBook.Worksheets; //.get_Item(1);

                        Excel.Worksheet workSheet = null;

                        for (int i = 1; i < wsPlanilhas.Count + 1; i++)
                        {





                            workSheet = (Excel.Worksheet)wsPlanilhas.get_Item(i);





                            LeitoraPlanilha.Ler(workSheet, tipo.GUID);

                        }

                        workSheet = null;
                        workBook.Close(false);
                    }


                }


            }

        }
    }
}
