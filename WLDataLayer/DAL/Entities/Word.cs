
namespace WLDataLayer.DAL.Entities
{
    public class Word : BaseEntity
    {
        public string MainWord { get; set; }
        public string Transcription { get; set; }
        public string Translation_RU { get; set; }
        public int DifficultyLevel { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
