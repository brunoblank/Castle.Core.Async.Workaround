using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Castle.Core.Async.Workaround
{
    public class AsyncInterceptor : IInterceptor
    {
        private readonly string _id;

        public AsyncInterceptor(string id)
        {
            _id = id;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = InterceptAsync(invocation);
        }

        public async Task InterceptAsync(IInvocation invocation)
        {
            await Task.Delay(500);
            Console.WriteLine($"-> {_id}");

            invocation.Proceed();
            await (Task) invocation.ReturnValue;

            await Task.Delay(500);
            Console.WriteLine($"<- {_id}");
        }
    }
}