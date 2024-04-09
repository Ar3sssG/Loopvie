using Npgsql;
using WLDataLayer.DAL.DBContext;
using WLDataLayer.DAL.Entities;
using WLDataLayer.DAL.Interfaces;

namespace WLDataLayer.DAL.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(NpgsqlTransaction transaction, WLDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
