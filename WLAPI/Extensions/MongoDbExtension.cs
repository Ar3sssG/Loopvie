using WLDataLayer.DAL.StoreServices;
using WLDataLayer.DAL.StoreServices.StoresSettings;

namespace WLAPI.Extensions
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
