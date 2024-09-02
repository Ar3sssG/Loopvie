using System.Linq.Expressions;

namespace LoopvieDataLayer.DAL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] includes);
        IQueryable<T> Get(params string[] includes);
        IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] includes);
        IQueryable<T> GetNoTracking(params string[] includes);
        void Add(T entity);
        Task AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        void UpdateWithNavigation(T entity, params string[] updatedProperties);
        void Delete(T entity);
        void Detach(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
