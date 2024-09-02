
namespace WLCommon.Models.Response
{
    public class WordResponseModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<string> Variants { get; set; }
    }

    public class WordCreateResponseModel
    {
        public string Word { get; set; }
        public string Message { get; set; }
    }
}
