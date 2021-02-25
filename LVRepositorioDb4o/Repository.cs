using Db4objects.Db4o;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class Repository<T> : IRepository<T>
    {

        protected string nomeBanco = @"C:\Trabalho-Ricardo\Db4OBancos\ListaVerificacao.yap";

        public Repository()
        {

        }

        #region métodos virtuais

        public virtual void Delete(T entidade)
        {

            Type tipo = typeof(T);

            string nomeProprieadeID = getNomePropriedadeID<T>();
            string nomeProprieadeGUID = getNomePropriedadeGUID<T>();



            if (!string.IsNullOrEmpty(nomeProprieadeID))
            {
                var properties = tipo.GetProperties();

                PropertyInfo propertyInfo = properties.FirstOrDefault(x => x.Name.Equals(nomeProprieadeID));

                var id = (int)propertyInfo.GetValue(entidade, null);

                var instanceQuery = (T)defineInstanciaID(id, tipo, nomeProprieadeID);

                apagar(instanceQuery);
            }
            else if (!string.IsNullOrEmpty(nomeProprieadeGUID))
            {

                var properties = tipo.GetProperties();

                PropertyInfo propertyInfo = properties.FirstOrDefault(x => x.Name.Equals(nomeProprieadeGUID));

                var guid = (string)propertyInfo.GetValue(entidade, null);


                var instanceQuery = (T)defineInstanciaGuid(guid, tipo, nomeProprieadeGUID);

                apagar(instanceQuery);
            }


        }

        public virtual void Insert(T entidade)
        {
            if (entidadeNova(entidade))
            {

                IObjectContainer db = Db4oFactory.OpenFile(nomeBanco);
                try
                {


                    db.Store(entidade);


                }
                catch (Exception ex)
                {
                    db.Rollback();

                    throw new Exception("Erro ao inserir entidade: " + ex.Message);
                }
                finally
                {

                    db.Close();
                }
            }
        }

        public virtual void Update(T entidade)
        {
            Type tipo = typeof(T);

            string nomeProprieadeID = getNomePropriedadeID<T>();
            string nomeProprieadeGUID = getNomePropriedadeGUID<T>();

            IObjectContainer db = Db4oFactory.OpenFile(nomeBanco);
            try
            {

                if (!string.IsNullOrEmpty(nomeProprieadeID))
                {
                    var properties = tipo.GetProperties();

                    PropertyInfo propertyInfo = properties.FirstOrDefault(x => x.Name.Equals(nomeProprieadeID));

                    var id = (int)propertyInfo.GetValue(entidade, null);

                    var instanceQuery = (T)defineInstanciaID(id, tipo, nomeProprieadeID);

                    //atualizar(entidade, properties, instanceQuery);

                    IObjectSet result = db.QueryByExample(instanceQuery);
                    var found = (T)result.Next();

                    foreach (var propInfo in properties)
                    {
                        var v = propInfo.GetValue(entidade, null);
                        propInfo.SetValue(found, v, null);
                    }

                    db.Store(found);
                }
                else if (!string.IsNullOrEmpty(nomeProprieadeGUID))
                {

                    var properties = tipo.GetProperties();

                    PropertyInfo propertyInfo = properties.FirstOrDefault(x => x.Name.Equals(nomeProprieadeGUID));

                    var guid = (string)propertyInfo.GetValue(entidade, null);


                    var instanceQuery = (T)defineInstanciaGuid(guid, tipo, nomeProprieadeGUID);

                    IObjectSet result = db.QueryByExample(instanceQuery);
                    var found = (T)result.Next();

                    foreach (var propInfo in properties)
                    {
                        var v = propInfo.GetValue(entidade, null);
                        propInfo.SetValue(found, v, null);
                    }

                    db.Store(found);

                }
            }
            catch (Exception ex)
            {
                db.Rollback();

                throw new Exception("Erro ao inserir entidade: " + ex.Message);
            }
            finally
            {

                db.Close();
            }


        }

        public virtual IList<T> Query()
        {
            IObjectContainer db = Db4oFactory.OpenFile(nomeBanco);
            try
            {

                return db.Query<T>().ToList();
            }
            finally
            {

                db.Close();
            }
        }

        public virtual IList<T> GetByProperty(string propertyName, object value)
        {
            Type tipo = typeof(T);

            var instance = Activator.CreateInstance(tipo);

            var properties = tipo.GetProperties();

            //var instanceQuery = (T)defineInstanciaGuid(guid, tipo, propertyName);

            foreach (PropertyInfo property in properties)
            {
                if (property.Name.Equals(propertyName))
                {
                    property.SetValue(instance, value, null);

                }
            }


            IObjectContainer db = Db4oFactory.OpenFile(nomeBanco);
            try
            {
                IObjectSet result = db.QueryByExample(instance);
                return result.Cast<T>().ToList();
            }
            finally
            {

                db.Close();
            }

        }

        public virtual bool IsSaved(string guid)
        {
            Type tipo = typeof(T);

            string nomeProprieade = getNomePropriedadeGUID<T>();
            var instance = defineInstanciaGuid(guid, tipo, nomeProprieade);

            IObjectContainer db = Db4oFactory.OpenFile(nomeBanco);
            try
            {
                IObjectSet result = db.QueryByExample(instance);

                return result.Next() != null ? true : false;
            }
            finally
            {

                db.Close();
            }
        }

        #endregion


        #region métodos protected




        //protected void atualizar(T entidade, PropertyInfo[] properties, T instanceQuery)
        //{
        //    //IObjectContainer db = Db4oFactory.OpenFile(nomeBanco);
        //    //try
        //    //{


        //    //}
        //    //finally
        //    //{

        //    //    db.Close();
        //    //}
        //}

        protected bool entidadeNova(T entidade)
        {


            Type tipo = typeof(T);

            var instanceByQuery = Activator.CreateInstance(tipo);

            string nomeProprieade1 = getNomePropriedadeID<T>();
            string nomeProprieade2 = getNomePropriedadeGUID<T>();




            if (!string.IsNullOrEmpty(nomeProprieade1))
            {
                return testaSeNaoExisteNoBanco(entidade, tipo, nomeProprieade1);
            }
            else if (!string.IsNullOrEmpty(nomeProprieade2))
            {
                return testaSeNaoExisteNoBanco(entidade, tipo, nomeProprieade2);
            }
            else
            {
                return false;
            }


        }

        protected bool testaSeNaoExisteNoBanco(T entidade, Type tipo, string nomeProprieade1)
        {
            var properties = tipo.GetProperties();

            PropertyInfo propertyInfo = properties.FirstOrDefault(x => x.Name.Equals(nomeProprieade1));
            var val = propertyInfo.GetValue(entidade, null);

            var teste = GetByProperty(nomeProprieade1, val);

            return teste.Count < 1 ? true : false;
        }


        protected void apagar(T instanceQuery)
        {
            IObjectContainer db = Db4oFactory.OpenFile(nomeBanco);
            try
            {
                IObjectSet result = db.QueryByExample(instanceQuery);
                var found = (T)result.Next();

                db.Delete(found);

            }
            finally
            {

                db.Close();
            }
        }

        protected static object defineInstanciaGuid(string guid, Type tipo, string nomeProprieade)
        {
            var instance = Activator.CreateInstance(tipo);

            var properties = tipo.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name.Equals(nomeProprieade))
                {
                    property.SetValue(instance, guid, null);
                }
            }

            return instance;
        }

        protected string getNomePropriedadeGUID<TT>()
        {


            Type tipo = typeof(TT);
            MemberInfo[] members = tipo.GetMembers();

            foreach (MemberInfo member in members)
            {
                //if (member.CustomAttributes.Count() > 0)
                //{
                if (member.MemberType == MemberTypes.Property)
                {
                    if (member.CustomAttributes.FirstOrDefault(x => x.ToString().Equals("[LVRepositorioDb4o.GUIDAtributo()]")) != null)
                    {



                        return member.Name;
                    }
                }


                //}


            }

            return string.Empty;
        }

        protected static object defineInstanciaID(int id, Type tipo, string nomeProprieade)
        {
            var instance = Activator.CreateInstance(tipo);

            var properties = tipo.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name.Equals(nomeProprieade))
                {
                    property.SetValue(instance, id, null);
                }
            }

            return instance;
        }

        protected string getNomePropriedadeID<TT>()
        {


            Type tipo = typeof(TT);
            MemberInfo[] members = tipo.GetMembers();

            foreach (MemberInfo member in members)
            {
                //if (member.CustomAttributes.Count() > 0)
                //{
                if (member.MemberType == MemberTypes.Property)
                {
                    if (member.CustomAttributes.FirstOrDefault(x => x.ToString().Equals("[LVRepositorioDb4o.IDAtributo()]")) != null)
                    {



                        return member.Name;
                    }
                }


                //}


            }

            return string.Empty;
        }



        #endregion








    }
}
