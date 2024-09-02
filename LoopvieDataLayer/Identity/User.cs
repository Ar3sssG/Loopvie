using Microsoft.AspNetCore.Identity;

namespace LoopvieDataLayer.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
