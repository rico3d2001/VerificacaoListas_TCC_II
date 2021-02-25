using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;

namespace LV_PresenterAPI.Consultas
{
    public class ValidaConfirmacao
    {
        public static StatusRevisoesLV StatusRevisoesLV(ListaVerficacaoVM lv)
        {
            StatusRevisoesLV statusLV = new StatusRevisoesLV();


            if (lv.Colunas.Count == 1 && string.IsNullOrEmpty(lv.Colunas.Last().INDICE_REV))
            {
                statusLV.ExistemRevisoesNesteDocumento = true;

                List<LinhaRevisaoVM> respostaListaLVs = new List<LinhaRevisaoVM>();

                foreach (var grupo in lv.Colunas.Last().LV_Grupos)
                {
                    foreach (var linha in grupo.Linhas)
                    {
                        respostaListaLVs.Add(linha);
                    }
                }



                if (respostaListaLVs.Where(x => x.ID_ESTADO > 5).Count() == 0)
                {
                    statusLV.NaoTemRevisoesIndefinidas = true;
                }

                if (respostaListaLVs.Where(x => x.CONFIRMADO == 0).Count() > 0)
                {
                    statusLV.PossuiRevisoesNaoConfirmadas = true;
                }


            }


            return statusLV;

        }

        public static StatusConfirmacoesLV StatusConfirmacoesLV(ListaVerficacaoVM lv)
        {
            StatusConfirmacoesLV statusLV = new StatusConfirmacoesLV();

           


          
                var respostaPlanilha = lv.Confirmacoes;

                if (respostaPlanilha.Count() < 1) //&& respostaPlanilha.Count() < 2)
                {
                    //    var primeiro = respostaPlanilha.FirstOrDefault();
                    //    if(!string.IsNullOrEmpty(primeiro.GUID))
                    //    {
                    statusLV.ListaSemConfirmacao = true;
                    // }

                }
                else
                {
                    var ultimaConfirmacao = respostaPlanilha.Distinct().OrderBy(x => x.CONFIRMACAO_ORDENADOR).ToList().Last();
                    statusLV.HouveSomentePrimeiraConfirmacaoColunaAtual = (!string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER1) && string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER2)) ? true : false;
                }

            

            return statusLV;

        }
    }
}