using WLCommon.Models.Manager;
using WLDataLayer.Identity;

namespace WLBusinessLogic.Interfaces
{
    public interface IIdentityManager
    {
        Task AddRefreshToken(RefreshTokenModel model);
        Task<User> GetUserByRefreshToken(string refreshToken);
    }
}
