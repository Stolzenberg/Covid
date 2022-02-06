using System.Net;
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
using System.Collections.Generic;
using Stolzenberg.Commands;
using Stolzenberg.Models;
using Stolzenberg.Services.Base;

namespace Stolzenberg.AzureFunctions
{
    public class GetCovidInfo
    {
        private readonly ILogger<GetCovidInfo> _logger;
        private readonly ISourceService _sourceService;
        private readonly IKeywordService _keywordService;

        public GetCovidInfo(ILogger<GetCovidInfo> logger, ISourceService sourceService, IKeywordService keywordService)
        {
            _logger = logger;
            _sourceService = sourceService;
            _keywordService = keywordService;
        }

        [FunctionName("GetCovidInfo")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("Get covid info was triggerd executing get infos from sources now.");

            var articles = new List<Article>();

            foreach (var source in _sourceService.GetAllSources())
            {
                var getArticlesCommand = new GetArticlesCommand(source, _keywordService);
                articles.AddRange(await getArticlesCommand.Execute());
            }    

            _logger.LogInformation($"Return {articles.Count} articles as json.");

            return new OkObjectResult(JsonSerializer.Serialize(articles));
        }
    }
}