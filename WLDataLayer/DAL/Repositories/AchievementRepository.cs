using Npgsql;
using WLDataLayer.DAL.DBContext;
using WLDataLayer.DAL.Entities;
using WLDataLayer.DAL.Interfaces;

namespace WLDataLayer.DAL.Repositories
{
    public class AchievementRepository : BaseRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(NpgsqlTransaction transaction, WLDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
