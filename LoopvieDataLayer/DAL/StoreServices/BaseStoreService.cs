using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using LoopvieDataLayer.DAL.StoreServices.Interfaces;
using LoopvieDataLayer.DAL.StoreServices.StoresSettings;

namespace LoopvieDataLayer.DAL.StoreServices
{
    public class BaseStoreService<T, TSettings> : IBaseStoreService<T> where T : class  where TSettings : class, IStoreServiceSettings
    {
        public readonly IMongoCollection<T> _mongoCollection;

        public BaseStoreService(IOptions<TSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.StoreName);
            _mongoCollection = database.GetCollection<T>(options.Value.CollectionName);
        }

        #region CRUD

        public async Task<T> AddAsync(T entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _mongoCollection.InsertManyAsync(entities);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            await _mongoCollection.DeleteOneAsync(predicate);
        }

        public T Update(Expression<Func<T, bool>> filter, T entity)
        {
            _mongoCollection.ReplaceOne(filter, entity);
            return entity;
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            var filter = predicate != null ? Builders<T>.Filter.Where(predicate) : Builders<T>.Filter.Empty;
            var cursor = await _mongoCollection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        //public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        //{
        //    var filter = Builders<T>.Filter.Where(predicate);
        //    var queryable = _mongoCollection.AsQueryable().Where(predicate);
        //    return queryable;
        //}

        #endregion
    }
}
