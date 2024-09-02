
namespace WLDataLayer.DAL.StoreServices.StoresSettings
{
    public class AnswerStoreServiceSettings : IStoreServiceSettings
    {
        public string ConnectionString { get; set; }
        public string StoreName { get; set; }
        public string CollectionName { get; set; }
    }
}
