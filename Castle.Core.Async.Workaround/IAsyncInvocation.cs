namespace Castle.Core.Async.Workaround
{
    using System.Threading.Tasks;

    public interface IAsyncInvocation
    {
        Task ProceedAsync();
    }
}