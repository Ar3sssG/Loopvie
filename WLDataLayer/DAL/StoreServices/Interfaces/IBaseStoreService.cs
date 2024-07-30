
using System.Linq.Expressions;

namespace WLDataLayer.DAL.StoreServices.Interfaces
{
    public interface IBaseStoreService<T>
    {
        public Task<List<T>> GetAsync();
        public Task<T> GetAsync(string id);
        public Task CreateAsync(T newItem);
        public Task UpdateAsync(string id, T updatedItem);
        public Task RemoveAsync(string id);
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate);
       
    }
}
