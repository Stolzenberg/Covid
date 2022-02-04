using System.IO;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Stolzenberg.Covid
{
    public class GetCovidInfo
    {
        private readonly ILogger<GetCovidInfo> _logger;
        private readonly HttpClient _client = new HttpClient();

        private readonly Source[] _sources = new Source[] {
            new Source { Link = "https://edition.cnn.com/health", HrefLink = "https://edition.cnn.com" },
            new Source { Link = "https://www.medicalnewstoday.com", HrefLink = "https://www.medicalnewstoday.com"},
            new Source { Link = "https://www.news-medical.net", HrefLink = "https://www.news-medical.net"},
            new Source { Link = "https://www.sciencenews.org", HrefLink = "https://www.sciencenews.org"},
            new Source { Link = "https://scitechdaily.com", HrefLink = "https://scitechdaily.com"},
        };

        private readonly string[] _keywords = new string[] {
            "covid",
            "corona",
            "vaccine",
            "pfizer",
            "biontech",
            "moderna",
            "sars-cov-2",
            "RNA",
            "mRNA-1273",
        };

        public GetCovidInfo(ILogger<GetCovidInfo> log)
        {
            _logger = log;
        }

        [FunctionName("GetCovidInfo")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("Get covid info was triggerd executing get infos from sources now.");

            var articles = await GetInfosFromSources();

            _logger.LogInformation($"Return {articles.Count} articles as json.");

            string data = JsonSerializer.Serialize(articles);
            return new OkObjectResult(data);
        }

        private async Task<List<Article>> GetInfosFromSources() 
        {
            try
            {
                var articles = new List<Article>();

                foreach (var source in _sources)
                {
                    var articlesFromSource = await GetSource(source);
                    articles.AddRange(articlesFromSource);
                }    

                _logger.LogInformation($"Successfully searched {_sources.Length + 1} sources.");

                return articles;
            }
            catch (System.Exception e)
            {
                _logger.LogError("Erorr in GetInfosFromSources: " + e.Message);
                throw;
            }
        }

        private async Task<List<Article>> GetSource(Source source) 
        {
            try
            {
                var webRequestResponse = await _client.GetAsync(source.Link);
                webRequestResponse.EnsureSuccessStatusCode();
                string responseBody = await webRequestResponse.Content.ReadAsStringAsync();

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(responseBody);

                var articles = new List<Article>();

                string searchQuery = string.Empty;
                for (int i = 0; i < _keywords.Length; i++)
                {
                    searchQuery += $"contains(@href, '{_keywords[i]}')";

                    if (i < _keywords.Length - 1) {
                        searchQuery += " or ";
                    }
                }

                var nodes = htmlDocument.DocumentNode.SelectNodes($"//a[{searchQuery}]");

                if (nodes == null) 
                {
                    return articles;
                }

                foreach (var node in nodes)
                {
                    var attribute = node.Attributes.FirstOrDefault(a => a.Name == "href");

                    string header = RemoveSpecialCharacters(node.InnerText.Trim());

                    if (string.IsNullOrEmpty(header) || attribute == null) 
                    {
                        continue;
                    }

                    string link = attribute.Value.StartsWith("http") ? attribute.Value : source.HrefLink + attribute.Value;

                    if (articles.Any(a => a.Link == link)) 
                    {
                        continue;
                    }

                    articles.Add(new Article() {
                        Header = header,
                        Link = link
                    });
                }

                return articles;
            }
            catch (System.Exception e)
            {
                _logger.LogError("Erorr in GetSource: " + e.Message);
                throw;
            }
        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled);
        }
    }
}