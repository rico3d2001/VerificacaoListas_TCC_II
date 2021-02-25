using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaRegistroConfirmacoesViewModel
    {
        List<ConfirmacaoViewModel> _listaConfirmacoesViewModel;

        public ListaRegistroConfirmacoesViewModel()
        {
            _listaConfirmacoesViewModel = new List<ConfirmacaoViewModel>();
        }

        public void Add(ConfirmacaoViewModel confirmacaoViewModel)
        {
            _listaConfirmacoesViewModel.Add(confirmacaoViewModel);
        }

        public ConfirmacaoViewModel this[int index]    
        {
            get => _listaConfirmacoesViewModel[index];
        }

        public int Comprimento
        {
            get => _listaConfirmacoesViewModel.Count;
        }

    }
}