using System.Net.Http;
using System.Text;
using Stolzenberg.Commands.Base;
using Stolzenberg.Services.Base;

namespace Stolzenberg.Commands
{
    public class CreateXPathQueryCommand : ICommand<string>
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly IKeywordService _keywordService;

        public CreateXPathQueryCommand(IKeywordService keywordService)
        {
            _keywordService = keywordService;
        }

        public string Execute()
        {
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

            return stringBuilder.ToString();
        }
    }
}