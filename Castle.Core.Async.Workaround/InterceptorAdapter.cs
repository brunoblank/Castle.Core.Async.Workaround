using Castle.DynamicProxy;

namespace Castle.Core.Async.Workaround
{
    internal class InterceptorAdapter : IInterceptor
    {
        private readonly InvocationDelegate _invocationDelegate;

        public InterceptorAdapter(InvocationDelegate invocationDelegate)
        {
            _invocationDelegate = invocationDelegate;
        }

        public void Intercept(IInvocation invocation)
        {
            _invocationDelegate(new InvocationAdapter(invocation));
        }
    }
}