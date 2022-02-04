using System.Text.Json.Serialization;

namespace Stolzenberg.Models
{
    public class Article 
    {
        [JsonPropertyName("header")]
        public string Header { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}