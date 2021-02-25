using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAWListaVerificacao.Models
{
    public interface ICrtlLista_Template_Doc: ICrtlLista
    {
        List<ColunaRevisaoViewModel> GetListaColunasRevisaoViewModel();
    }
}
