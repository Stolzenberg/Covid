using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Stolzenberg.Models;
using Stolzenberg.Repositories.Base;
using Stolzenberg.Services.Base;

namespace Stolzenberg.Services
{
    public class SourceService : ISourceService
    {
        private readonly ILogger<SourceService> _logger;
        private readonly ISourceRepository _repository;

        public SourceService(ILogger<SourceService> logger, ISourceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public List<Source> GetAllSources()
        {
            return _repository.GetAllSources();
        }
    }
}