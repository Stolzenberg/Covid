using System.Text.Json.Serialization;

public class Article 
{
    [JsonPropertyName("header")]
    public string Header { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; }
}