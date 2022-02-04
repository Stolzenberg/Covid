using System.Collections.Generic;
using Stolzenberg.Repositories.Base;

namespace Stolzenberg.Repositories
{
    public class KeywordRepository : IKeywordRepository
    {
        public List<string> GetAllKeywords()
        {
            // We could connect a database here but for now lets just use a hardcoded list.

            return new List<string>() {
                "covid",
                "corona",
                "vaccine",
                "pfizer",
                "biontech",
                "moderna",
                "sars-cov-2",
                "rna",
                "mrna-1273",
            };
        }
    }
}