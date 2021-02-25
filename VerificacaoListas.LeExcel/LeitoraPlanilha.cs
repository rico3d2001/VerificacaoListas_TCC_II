using LV14FluentNHB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace VerificacaoListas.LeExcel
{
    public class LeitoraPlanilha
    {
        

        public static void Ler(Excel.Worksheet wsPlanilha, string tipoGUID)
        {

            
            Repository<LV_PLANILHA> repositoryPLanilha = new Repository<LV_PLANILHA>();

            string cell = getColuna(3, 1);
            var funcao = wsPlanilha.get_Range(cell, cell).Text;

            cell = getColuna(4, 1);
            var descricaoPlanilha = wsPlanilha.get_Range(cell, cell).Text;

            LV_PLANILHA planilha = new LV_PLANILHA()
            {
                GUID = Guid.NewGuid().ToString(),
                GUID_TIPO = tipoGUID,
                NOME = wsPlanilha.Name,
                FUNCAO = funcao,
                DESCRICAO = descricaoPlanilha
            };

            
            //if(planilha.NOME.Equals("Prédios - BAS CONSOLIDADO"))
            //{
            //string pare = "Prédios - BAS CONSOLIDADO";
            //}

            repositoryPLanilha.Insert(planilha);


            Repository<LV_GRUPO> repositoryGrupo = new Repository<LV_GRUPO>();


            LV_GRUPO grupo = null;
            int ordenarGrupoItem = 0;
            int ordenarItemItem = 0;


            //for (int colIndex = 1; colIndex < 5; colIndex++)
            //{

            int colIndex = 1;

            for (int rowIndex = 7; rowIndex < 80; rowIndex++)
            {
                cell = getColuna(rowIndex, colIndex);
                string texto = wsPlanilha.get_Range(cell, cell).Text;

                if (texto != "")
                {
                    int ordenadorGrupo = 0;
                    if (int.TryParse(texto, out ordenadorGrupo))
                    {
                        if(grupo == null || grupo.ORDENADOR < ordenadorGrupo)
                        {

                            cell = getColuna(rowIndex, colIndex + 1);
                            string nomeGrupo = wsPlanilha.get_Range(cell, cell).Text;

                            grupo = new LV_GRUPO()
                            {
                                GUID = Guid.NewGuid().ToString(),
                                ORDENADOR = ordenadorGrupo,
                                GUID_PLANILHA = planilha.GUID,
                                NOME = nomeGrupo
                            };

                            repositoryGrupo.Insert(grupo);
                        }
                        
                    }
                    else if (int.TryParse(texto.Split('.')[0], out ordenarGrupoItem) && int.TryParse(texto.Split('.')[1], out ordenarItemItem))
                    {
                        if (grupo.ORDENADOR.Equals(ordenarGrupoItem))
                        {

                            

                            cell = getColuna(rowIndex, colIndex + 1);
                            string descricaoRevisao = wsPlanilha.get_Range(cell, cell).Text;

                            if (descricaoRevisao != "")
                            {
                                Repository<LV_ITEM_REVISAO> repositoryItemRevisao = new Repository<LV_ITEM_REVISAO>();

                                LV_ITEM_REVISAO itemRevisao = new LV_ITEM_REVISAO()
                                {
                                    GUID = Guid.NewGuid().ToString(),
                                    GUID_GRUPO = grupo.GUID,
                                    DESCRICAO = descricaoRevisao,
                                    ORDENADOR = ordenarItemItem
                                };

                                repositoryItemRevisao.Insert(itemRevisao);
                            }



                            


                        }
                    }

                }


            }
            //}






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
