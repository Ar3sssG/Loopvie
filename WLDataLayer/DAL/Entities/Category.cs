using WLCommon.Enums;

namespace WLDataLayer.DAL.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public CategoryLevel CategoryLevel { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<Word> Words { get; set; }
    }
}
