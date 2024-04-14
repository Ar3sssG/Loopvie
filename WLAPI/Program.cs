using WLDataLayer.DAL;
using WLAPI.Extensions;
using WLDataLayer.DAL.Interfaces;

namespace WLAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureAspNetServices(builder.Configuration);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddManagerServices();

            builder.Services.ConfigureAuthentication();

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwagger();

            builder.Services.ConfigureAutoMapper();

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseCors();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
