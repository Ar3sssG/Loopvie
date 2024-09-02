
namespace LoopvieCommon.Models.Response
{
    public class PagedResponseModel<T> where T : class
    {
        public List<T> Data { get; set; }
        public int TotalItemCount { get; set; }
        public int Count { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
