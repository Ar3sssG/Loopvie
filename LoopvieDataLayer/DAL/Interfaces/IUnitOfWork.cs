using Npgsql;

namespace LoopvieDataLayer.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        #region RepositoryInterfaces
        IRefreshTokenRepository RefreshTokenRepository { get; }
        #endregion

        #region SAVE CHANGES

        Task<int> SaveChangesAsync();
        int SaveChanges();

        #endregion SAVE CHANGES

        #region TRANSACTION

        Task<NpgsqlTransaction> BeginTransaction();
        Task CommitTrasnactionAsync();
        Task RollBackTrasnactionAsync();

        #endregion TRANSACTION
    }
}
