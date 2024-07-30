
namespace WLCommon.Models.Response.Word
{
    public class WordResponseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<string> Variants { get; set; }
    }
}
