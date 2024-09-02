using Microsoft.EntityFrameworkCore;
using Npgsql;
using LoopvieDataLayer.DAL.DBContext;
using LoopvieDataLayer.DAL.Interfaces;
using LoopvieDataLayer.DAL.Repositories;

namespace LoopvieDataLayer.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private LoopvieDBContext _dbContext;
        private NpgsqlTransaction _transaction;

        public UnitOfWork(LoopvieDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Repositories

        private IRefreshTokenRepository _refreshTokenRepository;
        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository != null ? _refreshTokenRepository : new RefreshTokenRepository(_transaction, _dbContext);
        
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

        #region Transaction
        public async Task<NpgsqlTransaction> BeginTransaction()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_dbContext.Database.GetConnectionString());
            connection.Open();

            _transaction = await connection.BeginTransactionAsync();

            return _transaction;
        }

        public async Task CommitTrasnactionAsync()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("There is no created transaction");
            }

            await _transaction.CommitAsync();
        }

        public async Task RollBackTrasnactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        #endregion
    }
}
