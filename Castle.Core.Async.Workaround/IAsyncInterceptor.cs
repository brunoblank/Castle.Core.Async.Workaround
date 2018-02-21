namespace Castle.Core.Async.Workaround
{
    using System.Threading.Tasks;

    public interface IAsyncInterceptor
    {
        Task InterceptAsync(IAsyncInvocation invocation);
    }
}