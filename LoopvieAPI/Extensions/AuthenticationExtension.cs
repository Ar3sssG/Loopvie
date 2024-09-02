using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using WLCommon.Constants;
using WLCommon.Models.Response;
using WLDataLayer.Identity;

namespace WLAPI.Extensions
{
    public static class AuthenticationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = 401;
                        context.Response.Headers.Append("Content-Type", "application/json");

                        var jsonResponse = JsonConvert.SerializeObject(new UnauthorizedResponseModel { StatusCode = 401, Message = AuthErrorConstants.Unatuhorized });
                        var data = Encoding.UTF8.GetBytes(jsonResponse);

                        await context.Response.Body.WriteAsync(data, 0, data.Length);

                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthConstants.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthConstants.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
