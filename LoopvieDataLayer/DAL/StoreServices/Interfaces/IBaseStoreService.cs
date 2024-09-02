
using MongoDB.Driver;
using System.Linq.Expressions;

namespace LoopvieDataLayer.DAL.StoreServices.Interfaces
{
    public interface IBaseStoreService<T>
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate = null);
        //Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        T Update(Expression<Func<T, bool>> filter, T entity);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);


    }
}
