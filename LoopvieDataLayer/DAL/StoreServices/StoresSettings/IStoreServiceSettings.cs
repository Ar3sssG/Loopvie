
namespace LoopvieDataLayer.DAL.StoreServices.StoresSettings
{
    public interface IStoreServiceSettings
    {
        string ConnectionString { get; set; }
        string StoreName { get; set; }
        string CollectionName { get; set; }
    }
}
