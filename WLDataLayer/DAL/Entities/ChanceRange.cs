
namespace WLDataLayer.DAL.Entities
{
    public class ChanceRange : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ChanceRangeValue> Values { get; set; }
    }

}
