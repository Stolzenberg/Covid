using System.Collections.Generic;
using Stolzenberg.Models;
using Stolzenberg.Repositories.Base;

namespace Stolzenberg.Repositories
{
    public class SourceRepository : ISourceRepository
    {
        public List<Source> GetAllSources()
        {
            // We could connect a database here but for now lets just use a hardcoded list.

            return new List<Source> {
                new Source { Link = "https://edition.cnn.com/health", HrefLink = "https://edition.cnn.com" },
                new Source { Link = "https://www.medicalnewstoday.com", HrefLink = "https://www.medicalnewstoday.com"},
                new Source { Link = "https://www.news-medical.net", HrefLink = "https://www.news-medical.net"},
                new Source { Link = "https://www.sciencenews.org", HrefLink = "https://www.sciencenews.org"},
                new Source { Link = "https://scitechdaily.com", HrefLink = "https://scitechdaily.com"},
            };
        }
    }
}