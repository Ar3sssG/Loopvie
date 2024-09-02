using Npgsql;
using LoopvieDataLayer.DAL.DBContext;
using LoopvieDataLayer.DAL.Entities;
using LoopvieDataLayer.DAL.Interfaces;

namespace LoopvieDataLayer.DAL.Repositories
{
    public class AchievementRepository : BaseRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(NpgsqlTransaction transaction, LoopvieDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
