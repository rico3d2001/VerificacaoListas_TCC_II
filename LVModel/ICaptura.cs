using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVModel
{
    public interface ICaptura
    {
        List<string> ListaNomes();
        object GetObjectByName(string nome);
    }
}
