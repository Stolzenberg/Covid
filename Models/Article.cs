using System.Text.Json.Serialization;

namespace Stolzenberg.Models
{
    public class Article 
    {
        [JsonPropertyName("header")]
        public string Header { get; set; }

        [JsonPropertyName("imgUrl")]
        public string ImgUrl { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
    }
}