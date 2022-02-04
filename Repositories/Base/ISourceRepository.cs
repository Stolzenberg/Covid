using System.Collections.Generic;
using Stolzenberg.Models;

namespace Stolzenberg.Repositories.Base
{
    public interface ISourceRepository
    {
        List<Source> GetAllSources();
    }
}