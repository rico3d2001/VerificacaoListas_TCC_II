using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppConfigLV.Models
{
    public class ListaDisciplinasViewModel
    {

#pragma warning disable CS0649 // Field 'ListaDisciplinasViewModel.listaViewModel' is never assigned to, and will always have its default value null
        List<DisciplinaViewModel> listaViewModel;
#pragma warning restore CS0649 // Field 'ListaDisciplinasViewModel.listaViewModel' is never assigned to, and will always have its default value null

        public ListaDisciplinasViewModel()
        {
            List<Disciplina> listaNegocio = new ListaDisciplinas().Disciplinas();

            listaNegocio.ForEach(x => listaViewModel.Add(new DisciplinaViewModel(x)));

        }

        public List<DisciplinaViewModel> Lista { get => listaViewModel; } //set => lista = value; }

        internal List<DisciplinaViewModel> GetTodas()
        {
            return listaViewModel;
        }
    }
}