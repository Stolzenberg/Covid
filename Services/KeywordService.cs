using System.Collections.Generic;
using Stolzenberg.Repositories;
using Stolzenberg.Repositories.Base;
using Stolzenberg.Services.Base;

namespace Stolzenberg.Services
{
    public class KeywordService : IKeywordService
    {
        private readonly IKeywordRepository _repository;

        public KeywordService(IKeywordRepository repository)
        {
            _repository = repository;
        }

        public List<string> GetAllKeywords()
        {
            return _repository.GetAllKeywords();
        }
    }
}