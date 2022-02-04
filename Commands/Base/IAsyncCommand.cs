using System.Threading.Tasks;

namespace Stolzenberg.Commands.Base
{
    public interface IAsyncCommand<T>
    {
        Task<T> Execute();
    }
}