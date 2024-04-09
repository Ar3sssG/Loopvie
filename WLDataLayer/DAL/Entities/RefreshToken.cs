using WLDataLayer.Identity;
using System.ComponentModel.DataAnnotations;

namespace WLDataLayer.DAL.Entities
{
    public class RefreshToken : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }

        public virtual User User { get; set; }
    }
}
