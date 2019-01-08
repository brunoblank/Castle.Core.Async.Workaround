using Castle.DynamicProxy;

namespace Castle.Core.Async.Workaround
{
    public class WorkaroundInterceptor : IInterceptor
    {
        private readonly IInterceptor[] _interceptors;

        public WorkaroundInterceptor(params IInterceptor[] interceptors)
        {
            _interceptors = interceptors;
        }

        public void Intercept(IInvocation invocation)
        {
            new WorkaroundInvocation(invocation, _interceptors, 0).Invoke();
        }
    }
}