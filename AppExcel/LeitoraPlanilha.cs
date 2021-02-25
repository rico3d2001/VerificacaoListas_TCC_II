using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Unity;
using LV_DI;
using LVModel;
using AppListaVerificacao.Interface;
using LV14FluentNHB.Service;

namespace AppExcel
{



    public class LeitoraPlanilha
    {


        public static void Ler(Worksheet wsPlanilha, Planilha planilha, AppServiceBase<Configuracao> contextoConfiguracao)
        {

            Grupo grupo = null;
            int ordenarGrupoItem = 0;
            int ordenarItemItem = 0;

            int colIndex = 1;

            string cell;

            for (int rowIndex = 7; rowIndex < 80; rowIndex++)
            {
                cell = getColuna(rowIndex, colIndex);
                string texto = wsPlanilha.get_Range(cell, cell).Text;

                if (texto != "")
                {
                    int ordenadorGrupo = 0;
                    if (int.TryParse(texto, out ordenadorGrupo))
                    {
                        if (grupo == null || grupo.ORDENADOR < ordenadorGrupo)
                        {

                            cell = getColuna(rowIndex, colIndex + 1);
                            string nomeGrupo = wsPlanilha.get_Range(cell, cell).Text;

                            var listagrupos = planilha.ListaGrupos.Distinct().ToList();

                            if (listagrupos.FirstOrDefault(x => x.NOME == nomeGrupo) == null)
                            {
                                grupo = new Grupo()
                                {
                                    GUID = Guid.NewGuid().ToString(),
                                    ORDENADOR = ordenadorGrupo,
                                    NOME = nomeGrupo,
                                    Planilha = planilha
                                };

                                planilha.ListaGrupos.Add(grupo);
                            }
                            //else
                            //{
                            //    grupo = listagrupos.FirstOrDefault(x => x.NOME == nomeGrupo);
                            //}

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


                                var listaItens = planilha
                               .ListaGrupos.Last()
                               .ListaItens.Distinct().ToList();

                                if (listaItens.FirstOrDefault(x => x.DESCRICAO == descricaoRevisao) == null)
                                {
                                    ItemRevisao itemRevisao = new ItemRevisao()
                                    {
                                        GUID = Guid.NewGuid().ToString(),
                                        DESCRICAO = descricaoRevisao,
                                        ORDENADOR = ordenarItemItem,
                                        Grupo = grupo

                                    };

                                    planilha
                                        .ListaGrupos.Last()
                                        .ListaItens.Add(itemRevisao);

                                   

                                }
                                
                              
                            }


                        }



                    }
                }

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
