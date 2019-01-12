using System;
using System.Threading.Tasks;
using Castle.Core.Async.Workaround;

namespace TestProject
{
    public class AsyncInterceptor : IInterceptorV2
    {
        private readonly string _id;

        public AsyncInterceptor(string id)
        {
            _id = id;
        }

        public async Task InterceptAsync(IInvocationV2 invocation, InvocationDelegate proceed)
        {
            await Task.Delay(500);
            Console.WriteLine($"-> {_id}");

            proceed(invocation);
            await (Task) invocation.ReturnValue;

            Console.WriteLine($"- retry ({_id}) -");
            proceed(invocation);
            await (Task)invocation.ReturnValue;

            await Task.Delay(500);
            Console.WriteLine($"<- {_id}");
        }

        public void Intercept(IInvocationV2 invocation, InvocationDelegate proceed)
        {
            invocation.ReturnValue = InterceptAsync(invocation, proceed);
        }
    }
}