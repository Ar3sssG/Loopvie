using LoopvieDataLayer.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LoopvieDataLayer.DAL.DBContext;

namespace LoopvieDataLayer.DAL
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected LoopvieDBContext _dbContext;
        public BaseRepository(LoopvieDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region CRUD Operations
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            EntityEntry<T> dbEntityEntry = _dbContext.Entry(entity);

            _dbContext.Set<T>().Attach(entity);

            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate).AsQueryable();
            foreach (T obj in query)
            {
                Delete(obj);
            }
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            if (predicate == null)
            {
                return set;
            }
            return set.Where(predicate);
        }

        public IQueryable<T> Get(params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            return set;
        }

        public IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            if (predicate == null)
            {
                return set;
            }

            return set.AsNoTracking().Where(predicate);
        }

        public IQueryable<T> GetNoTracking(params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            return set.AsNoTracking();
        }

        public void UpdateWithNavigation(T entity, params string[] navigationProperties)
        {
            var dbEntityEntry = _dbContext.Entry(entity);

            _dbContext.Set<T>().Attach(entity);
            dbEntityEntry.State = EntityState.Modified;

            if (navigationProperties != null && navigationProperties.Any())
            {
                foreach (var property in navigationProperties)
                {
                    dbEntityEntry.Navigation(property).IsModified = true;
                }
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var dbEntityEntry = _dbContext.Entry(entity);
            _dbContext.Set<T>().Attach(entity);
            dbEntityEntry.State = EntityState.Modified;
            await Task.FromResult(0);
        }

        public void Detach(T entity)
        {
            var dbEntityEntry = _dbContext.Entry(entity);

            if (dbEntityEntry != null)
            {
                dbEntityEntry.State = EntityState.Detached;
            }
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
                set = set.Include(include);

            return set.FirstOrDefault(filter);
        }

        #endregion

        #region SaveChanges
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
