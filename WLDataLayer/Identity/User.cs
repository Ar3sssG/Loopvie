using Microsoft.AspNetCore.Identity;

namespace WLDataLayer.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
