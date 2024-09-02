using Npgsql;
using LoopvieDataLayer.DAL.DBContext;
using LoopvieDataLayer.DAL.Entities;
using LoopvieDataLayer.DAL.Interfaces;

namespace LoopvieDataLayer.DAL.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(NpgsqlTransaction transaction,LoopvieDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
