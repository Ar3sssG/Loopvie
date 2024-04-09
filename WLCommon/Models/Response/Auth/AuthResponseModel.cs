
namespace WLCommon.Models.Response
{
    public class AuthResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        public bool IsAuthorized { get; set; }
    }

    public class UnauthorizedResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
