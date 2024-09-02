
using System.Text.Json.Serialization;

namespace LoopvieCommon.Models.Request
{
    public class WordRequestModel
    {
        [JsonPropertyName("word")]
        public string Word { get; set; }
        [JsonPropertyName("wrong_variants")]
        public List<string> WrongVariants { get; set; }
        [JsonPropertyName("correct_answer")]
        public string CorrectAnswer { get; set; }
        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }
    }
}
