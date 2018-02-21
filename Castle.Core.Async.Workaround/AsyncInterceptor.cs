namespace Castle.Core.Async.Workaround
{
    using System;
    using System.Threading.Tasks;

    public class AsyncInterceptor : IAsyncInterceptor
    {
        private readonly string id;

        public AsyncInterceptor(string id)
        {
            this.id = id;
        }

        public async Task InterceptAsync(IAsyncInvocation invocation)
        {
            await Task.Delay(500);
            Console.WriteLine($"-> {id}");
            await invocation.ProceedAsync();
            await Task.Delay(500);
            Console.WriteLine($"<- {id}");
        }
    }
}