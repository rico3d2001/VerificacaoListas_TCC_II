using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LV_PresenterAPI.Models
{
    public class TreeViewModel
    {
        List<TreeViewModel> childs;

        public TreeViewModel()
        {
            this.childs = new List<TreeViewModel>();
        }

        public string ID { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int Nivel { get; set; }

        public virtual List<TreeViewModel> Childs { get => this.childs; }
    }
}