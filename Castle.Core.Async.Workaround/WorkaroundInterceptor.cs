namespace Castle.Core.Async.Workaround
{
    using Castle.DynamicProxy;

    public class WorkaroundInterceptor : IInterceptor
    {
        private readonly IAsyncInterceptor[] interceptors;

        public WorkaroundInterceptor(params IAsyncInterceptor[] interceptors)
        {
            this.interceptors = interceptors;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = new AsyncInvocation(invocation, interceptors).ProceedAsync();
        }
    }
}