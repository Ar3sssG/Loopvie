
namespace WLDataLayer.DAL.Entities
{
    public class ChanceRangeValue : BaseEntity
    {
        public int Priority { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public int ChanceRangeId { get; set; }
        public virtual ChanceRange ChanceRange { get; set; }
    }
}
