using WLDataLayer.Identity;

namespace WLDataLayer.DAL.Entities
{
    public class UserStatistic : BaseEntity
    {
        public int UserId { get; set; }
        public int Rating { get; set; }
        public int WordCount { get; set; }
        public virtual User User { get; set; }
    }
}
