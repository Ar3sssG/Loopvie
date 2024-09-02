using LoopvieDataLayer.DAL;
using LoopvieAPI.Extensions;
using LoopvieDataLayer.DAL.Interfaces;

namespace LoopvieAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureAspNetServices(builder.Configuration);

            builder.Services.ConfigureMongoDbServices(builder.Configuration);

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

            app.UseSwaggerAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
