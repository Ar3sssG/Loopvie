using Microsoft.IdentityModel.Tokens;
using System.Text;
using WLCommon.Constants;

namespace WLDataLayer.Identity
{
    public static class AuthOptions
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.KEY));
        public static SymmetricSecurityKey GetSymmetricSecurityRefreshKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.REFRESH_TOKEN_KEY));
    }
}
