using System.Collections.Generic;
using System.Threading.Tasks;
using Stolzenberg.Commands.Base;
using Stolzenberg.Models;
using System.Linq;
using Stolzenberg.Services.Base;

namespace Stolzenberg.Commands
{
    public class GetArticlesCommand : IAsyncCommand<List<Article>>
    {
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
                var articles = new List<Article>();

                var getHtmlDocumentCommand = new GetHtmlDocumentCommand(_source.Link);
                var htmlDocument = await getHtmlDocumentCommand.Execute();

                var createXPathQueryCommand = new CreateXPathQueryCommand(_keywordService);
                string searchQuery = createXPathQueryCommand.Execute();

                // Query the entire html document and find every node that has an a (link) tag with the href that contains one of the keywords.
                var nodes = htmlDocument.DocumentNode.SelectNodes($"//a[{searchQuery}]");

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