using System.ComponentModel.DataAnnotations;

namespace WLDataLayer.DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public int? UpdatedById { get; set; }
    }
}
