
using System.Net.Http;
using HtmlAgilityPack;
using Stolzenberg.Commands.Base;
using Stolzenberg.Models;
using Stolzenberg.Extensions;
using System.Linq;

namespace Stolzenberg.Commands
{
    public class GetArticleCommand : ICommand<Article>
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly Source _source;
        private readonly HtmlNode _node;

        public GetArticleCommand(Source source, HtmlNode node)
        {
            _source = source;
            _node = node;
        }

        public Article Execute()
        {
            try
            {
                var attribute = _node.Attributes.FirstOrDefault(a => a.Name == "href");

                string header = _node.InnerText.RemoveSpecialCharacters().Trim();

                if (string.IsNullOrEmpty(header) || attribute == null) 
                {
                    return null;
                }

                string link = attribute.Value.StartsWith("http") ? attribute.Value : _source.HrefLink + attribute.Value;

                return new Article() {
                    Header = header,
                    Link = link
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}