using LoopvieCommon.Models.Manager;
using LoopvieCommon.Models.Request;
using LoopvieCommon.Models.Response;
using LoopvieDataLayer.Identity;

namespace LoopvieBusinessLogic.Interfaces
{
    public interface IIdentityManager
    {
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task<AuthResponseModel> AuthenticateUserAsync(AuthRequestModel requestModel);
        Task<AuthResponseModel> RegisterUserAsync(RegisterRequestModel requestModel);
        Task<AuthResponseModel> AuthenticateByRefreshTokenAsync(RefreshTokenRequestModel requestModel);
    }
}
