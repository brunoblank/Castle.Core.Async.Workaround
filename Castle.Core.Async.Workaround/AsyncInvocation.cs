namespace Castle.Core.Async.Workaround
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using Castle.DynamicProxy;

    public class AsyncInvocation : IInvocation, IAsyncInvocation
    {
        private readonly IEnumerator<IAsyncInterceptor> enumerator;

        private readonly IInvocation invocation;

        public AsyncInvocation(IInvocation invocation, IEnumerable<IAsyncInterceptor> interceptors)
        {
            this.invocation = invocation;
            enumerator = interceptors.GetEnumerator();
        }

        public object[] Arguments => invocation.Arguments;

        public Type[] GenericArguments => invocation.GenericArguments;

        public object InvocationTarget => invocation.InvocationTarget;

        public MethodInfo Method => invocation.Method;

        public MethodInfo MethodInvocationTarget => invocation.MethodInvocationTarget;

        public object Proxy => invocation.Proxy;

        public object ReturnValue
        {
            get => invocation.ReturnValue;
            set => invocation.ReturnValue = value;
        }

        public Type TargetType => invocation.TargetType;

        public object GetArgumentValue(int index) => invocation.GetArgumentValue(index);

        public MethodInfo GetConcreteMethod() => invocation.GetConcreteMethod();

        public MethodInfo GetConcreteMethodInvocationTarget() => invocation.GetConcreteMethodInvocationTarget();

        public void Proceed()
        {
            throw new NotSupportedException("Calling Proceed does not work");
        }

        public Task ProceedAsync()
        {
            if (enumerator.MoveNext())
            {
                return enumerator.Current.InterceptAsync(this);
            }

            return (Task)invocation.GetConcreteMethodInvocationTarget().Invoke(
                invocation.InvocationTarget,
                invocation.Arguments);
        }

        public void SetArgumentValue(int index, object value) => invocation.SetArgumentValue(index, value);
    }
}