using Db4objects.Db4o;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class RepositoryID<T> : Repository<T>, IRepositoryID<T>
    {
        public virtual T ReturnById(int id)
        {

            Type tipo = typeof(T);

            string nomeProprieade = getNomePropriedadeID<T>();

            var instance = defineInstanciaID(id, tipo, nomeProprieade);

            IObjectContainer db = Db4oFactory.OpenFile(nomeBanco);
            try
            {
                IObjectSet result = db.QueryByExample(instance);
                return (T)result.Next();
            }
            finally
            {

                db.Close();
            }
        }

        


    }
}
