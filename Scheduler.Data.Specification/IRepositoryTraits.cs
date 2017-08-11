using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    #region Data Modification

    public interface ICanRemove<TEntity>
    {
        void Remove(TEntity entity);
    }

    public interface ICanCreate<T>
    {
        T Create(T entity);
    }

    public interface ICanUpdate<T>
    {
        void Update(T entity);
    }

    #endregion

    #region Data Reading

    public interface ICanGetAll<T>
    {
        List<T> GetAll();
    }

    public interface ICanGetById<TEntity, TKey>
    {
        TEntity GetById(TKey id);
    }

    public interface ICanFindAll<T>
    {
        IEnumerable<T> FindAll(IDictionary<string, object> propertyValuePairs);
    }

    public interface ICanFindOne<T>
    {
        T FindAll(IDictionary<string, object> propertyValuePairs);
    }

    #endregion

    #region Aggregation

    public interface ICanGetCount
    {
        int GetCount();
    }

    #endregion
}
