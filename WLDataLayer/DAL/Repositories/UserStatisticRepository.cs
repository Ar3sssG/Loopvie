using Npgsql;
using WLDataLayer.DAL.DBContext;
using WLDataLayer.DAL.Entities;
using WLDataLayer.DAL.Interfaces;

namespace WLDataLayer.DAL.Repositories
{
    public class UserStatisticRepository : BaseRepository<UserStatistic>, IUserStatisticRepository
    {
        public UserStatisticRepository(NpgsqlTransaction transaction, WLDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
