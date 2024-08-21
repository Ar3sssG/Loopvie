using WLCommon.Models.Manager;
using WLCommon.Models.Request;
using WLCommon.Models.Response;
using WLDataLayer.Identity;

namespace WLBusinessLogic.Interfaces
{
    public interface IIdentityManager
    {
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task<AuthResponseModel> AuthenticateUserAsync(AuthRequestModel requestModel);
        Task<AuthResponseModel> RegisterUserAsync(RegisterRequestModel requestModel);
        Task<AuthResponseModel> AuthenticateByRefreshTokenAsync(RefreshTokenRequestModel requestModel);
        Task<bool> CheckTokenAsync(string accessToken);
    }
}
