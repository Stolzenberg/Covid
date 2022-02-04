using System.Collections.Generic;
using Stolzenberg.Models;

namespace Stolzenberg.Services.Base
{
    public interface ISourceService
    {
        List<Source> GetAllSources();
    }
}