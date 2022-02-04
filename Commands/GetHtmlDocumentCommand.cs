using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Stolzenberg.Commands.Base;

namespace Stolzenberg.Commands
{
    public class GetHtmlDocumentCommand : IAsyncCommand<HtmlDocument>
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _link;

        public GetHtmlDocumentCommand(string link)
        {
            _link = link;
        }

        public async Task<HtmlDocument> Execute()
        {
            var webRequestResponse = await _client.GetAsync(_link);
            webRequestResponse.EnsureSuccessStatusCode();
            string responseBody = await webRequestResponse.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(responseBody);

            return htmlDocument;
        }
    }
}