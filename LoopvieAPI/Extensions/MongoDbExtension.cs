using LoopvieDataLayer.DAL.StoreServices;
using LoopvieDataLayer.DAL.StoreServices.StoresSettings;

namespace LoopvieAPI.Extensions
{
    public static class MongoDbExtension
    {
        public static void ConfigureMongoDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            //settings
            services.Configure<WordStoreServiceSettings>(configuration.GetSection("Stores:WordStoreServiceSettings"));
            services.Configure<AnswerStoreServiceSettings>(configuration.GetSection("Stores:AnswerStoreServiceSettings"));

            services.AddSingleton<WordStoreService>();
            services.AddSingleton<AnswerStoreService>();
        }
    }
}
