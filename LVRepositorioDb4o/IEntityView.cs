using System.Collections.Generic;

namespace LVRepositorioDb4o
{
    public interface IEntityView
    {
        List<IEntityView> GetByProperty(string propertyName, object value);   
    }
}