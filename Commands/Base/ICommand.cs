namespace Stolzenberg.Commands.Base
{
    public interface ICommand<T>
    {
        T Execute();
    }
}