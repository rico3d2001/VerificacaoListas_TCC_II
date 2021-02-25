using AppListaVerificacao.Interface;
using AppUtils;
using EntidadesRepositoriosLeitura;
using LV_DI;
using LVModel;
using RepositorioMongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace ConsumidorLV_Oracle.Comandos
{
    public class CmdsOraConfirmacaoRevisao
    {
        public static bool Confirma(string message)
        {

            try
            {
                var value = MeuJson.ConverteJSonParaObject<ValoresConfirma>(message);

                var lv = new LV_NoSQL().BuscarLV_ViewModel(value.GUID_LV);

                var listaRevisoesNaoConfirmadas = new List<Revisao>();


                var coluna = lv.Colunas.OrderBy(x => x.ORDENADOR).Last();
                var confirmacaoVM = lv.Confirmacoes.First(x => x.CONFIRMACAO_INDICE == coluna.INDICE_REV);

                if (coluna != null)
                {
                    foreach (var grupo in coluna.LV_Grupos)
                    {
                        foreach (var linha in grupo.Linhas)
                        {

                            listaRevisoesNaoConfirmadas.Add(new Revisao()
                            {
                                GUID = linha.GUID_REVISAO,
                                GUID_LV_ITEM = linha.GUID_ITEM,
                                DATA_VERICACAO = DateTime.Now,
                                CONFIRMADO = 1,
                                EMITIDO = 1,
                                SALVO = 0,
                                GUID_CONFIRMADO = confirmacaoVM.CONFIRMACAO_GUID,
                                GUID_DOC_VERIFICACAO = lv.GUID,
                                GUID_LV_VERIFICADOR = confirmacaoVM.CONFIRMACAO_ID_USER2,
                                ID_ESTADO = linha.ID_ESTADO,
                                INDICE = confirmacaoVM.CONFIRMACAO_INDICE,
                                ORDENADOR = linha.ORDENADOR
                            });
                        }
                    }
                }


                Confirmacao confirmacao = new Confirmacao()
                {
                    GUID = confirmacaoVM.CONFIRMACAO_GUID,
                    GUID_DOCUMENTO = lv.GUID,
                    GUID_USUARIO1 = confirmacaoVM.CONFIRMACAO_ID_USER1,
                    GUID_USUARIO2 = confirmacaoVM.CONFIRMACAO_ID_USER2,
                    INDICE_REV = confirmacaoVM.CONFIRMACAO_INDICE,
                    ORDENADOR = confirmacaoVM.CONFIRMACAO_ORDENADOR,
                    DATA = confirmacaoVM.CONFIRMACAO_DATA
                };

                bool salvo = false;
                if (listaRevisoesNaoConfirmadas.Count > 0)
                {

                    using (var contextoDocumento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                    {
                        contextoDocumento.Start();
                        var documento = contextoDocumento.ReturnByGUID(value.GUID_LV);

                        bool alterdoDocumento = documento.Salva(confirmacao, listaRevisoesNaoConfirmadas);

                        if (alterdoDocumento)
                        {
                            contextoDocumento.Update(documento);

                            contextoDocumento.Commit();
                            salvo = true;
                        }

                    }

                    if (salvo)
                    {
                        using (var contextoNumeroDocSNCLavalin = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
                        {
                            contextoNumeroDocSNCLavalin.Start();
                            var numeroDoc = contextoNumeroDocSNCLavalin.ReturnByGUID(lv.GUID);
                            numeroDoc.GUID_ULTIMA_CONFIRMACAO = confirmacaoVM.CONFIRMACAO_GUID;
                            contextoNumeroDocSNCLavalin.Update(numeroDoc);
                            contextoNumeroDocSNCLavalin.Commit();
                        }
                    }

                    new LV_NoSQL().EmitirRevisaoVM(lv.GUID);

                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                return false;
            }
}
    }
}