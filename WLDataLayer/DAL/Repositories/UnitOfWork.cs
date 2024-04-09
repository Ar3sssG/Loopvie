using Microsoft.EntityFrameworkCore;
using Npgsql;
using WLDataLayer.DAL.DBContext;
using WLDataLayer.DAL.Interfaces;

namespace WLDataLayer.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private WLDBContext _dbContext;
        private NpgsqlTransaction _transaction;

        public UnitOfWork(WLDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Repositories

        private IRefreshTokenRepository _refreshTokenRepository;
        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository != null ? _refreshTokenRepository : new RefreshTokenRepository(_transaction,_dbContext);

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
