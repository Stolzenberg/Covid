
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Stolzenberg.Commands.Base;
using Stolzenberg.Models;
using System.Linq;
using System.Text;
using Stolzenberg.Services.Base;

namespace Stolzenberg.Commands
{
    public class GetArticlesCommand : IAsyncCommand<List<Article>>
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly Source _source;
        private readonly IKeywordService _keywordService;

        public GetArticlesCommand(Source source, IKeywordService keywordService)
        {
            _source = source;
            _keywordService = keywordService;
        }

        public async Task<List<Article>> Execute()
        {
            try
            {
                var webRequestResponse = await _client.GetAsync(_source.Link);
                webRequestResponse.EnsureSuccessStatusCode();
                string responseBody = await webRequestResponse.Content.ReadAsStringAsync();

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(responseBody);

                var articles = new List<Article>();

                // Build up our query search string in xpath format.  
                var stringBuilder = new StringBuilder();

                var keywords = _keywordService.GetAllKeywords();

                for (int i = 0; i < keywords.Count; i++)
                {
                    stringBuilder.Append($"contains(@href, '{keywords[i]}')");

                    if (i < keywords.Count - 1) 
                    {
                        stringBuilder.Append(" or ");
                    }
                }

                // Query the entire html document and find every node that has an a (link) tag with the href that contains one of the keywords.
                var nodes = htmlDocument.DocumentNode.SelectNodes($"//a[{stringBuilder.ToString()}]");

                if (nodes == null) 
                {
                    return articles;
                }

                // Go through all html nodes and execute the get articles command.
                foreach (var node in nodes)
                {
                    var getArticleCommand = new GetArticleCommand(_source, node);
                    var article = getArticleCommand.Execute();

                    if (article == null) 
                    {
                        continue;
                    }

                    // Check if we have the same link already in our articles list. 
                    if (articles.Any(a => a.Link == article.Link)) 
                    {
                        continue;
                    }

                    articles.Add(article);
                }

                return articles;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}