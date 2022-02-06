using System.Net.Http;
using HtmlAgilityPack;
using Stolzenberg.Commands.Base;
using Stolzenberg.Models;
using Stolzenberg.Extensions;
using System.Linq;
using Stolzenberg.Services.Base;
using System.Threading.Tasks;

namespace Stolzenberg.Commands
{
    public class GetArticleCommand : ICommand<Article>
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly Source _source;
        private readonly HtmlNode _node;
        private readonly IKeywordService _keywordService;

        public GetArticleCommand(Source source, HtmlNode node, IKeywordService keywordService)
        {
            _source = source;
            _node = node;
            _keywordService = keywordService;
        }

        public Article Execute()
        {
            try
            {
                var hrefAttribute = _node.Attributes.FirstOrDefault(a => a.Name == "href");

                string header = _node.InnerText.RemoveSpecialCharacters().Trim();

                if (string.IsNullOrEmpty(header) || hrefAttribute == null) 
                {
                    return null;
                }

                string link = hrefAttribute.Value.StartsWith("http") ? hrefAttribute.Value : _source.HrefLink + hrefAttribute.Value;
            
                return new Article() 
                {
                    Header = header,
                    ImgUrl = "",
                    Link = link,
                    Source = _source.SourceName
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}