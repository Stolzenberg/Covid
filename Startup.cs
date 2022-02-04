using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Stolzenberg.Services.Base;
using Stolzenberg.Services;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using Stolzenberg.Repositories.Base;
using Stolzenberg.Repositories;

[assembly: FunctionsStartup(typeof(Stolzenberg.Startup))]

namespace Stolzenberg
{
    public class Startup : FunctionsStartup
    {
        private IConfiguration _configuration;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureSettings(builder);

            builder.Services.AddSingleton<ISourceRepository, SourceRepository>();
            builder.Services.AddSingleton<IKeywordRepository, KeywordRepository>();
            builder.Services.AddSingleton<ISourceService, SourceService>();
            builder.Services.AddSingleton<IKeywordService, KeywordService>();
        }

        private void ConfigureSettings(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _configuration = config;
        }
    }
}