using Db4objects.Db4o;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class RepositoryGUID<T> : Repository<T>, IRepositoryGUID<T>
    {


        public virtual T ReturnByGUID(string guid)
        {
            Type tipo = typeof(T);

            string nomeProprieade = getNomePropriedadeGUID<T>();
            var instance = defineInstanciaGuid(guid, tipo, nomeProprieade);

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
