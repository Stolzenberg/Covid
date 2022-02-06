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
        private readonly string _attribute;

        public CreateXPathQueryCommand(string attribute, IKeywordService keywordService)
        {
            _attribute = attribute;
            _keywordService = keywordService;
        }

        public string Execute()
        {
            var stringBuilder = new StringBuilder();

            var keywords = _keywordService.GetAllKeywords();

            for (int i = 0; i < keywords.Count; i++)
            {
                stringBuilder.Append($"contains(@{_attribute}, '{keywords[i]}')");

                if (i < keywords.Count - 1) 
                {
                    stringBuilder.Append(" or ");
                }
            }

            return stringBuilder.ToString();
        }
    }
}