using System.Collections.Generic;

namespace Stolzenberg.Repositories.Base
{
    public interface IKeywordRepository
    {
        List<string> GetAllKeywords();
    }
}