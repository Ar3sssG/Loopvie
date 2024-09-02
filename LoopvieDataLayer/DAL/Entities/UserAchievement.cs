using WLDataLayer.Identity;

namespace WLDataLayer.DAL.Entities
{
    public class UserAchievement : BaseEntity
    {
        public int UserId { get; set; }
        public int AchievementId { get; set; }

        public virtual User User { get; set; }
        public virtual Achievement Achievement { get; set; }
    }
}
