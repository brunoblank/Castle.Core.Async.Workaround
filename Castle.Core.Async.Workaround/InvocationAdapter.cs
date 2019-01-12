using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Castle.Core.Async.Workaround
{
    internal class InvocationAdapter : IInvocationV2
    {
        private static readonly MethodInfo InvokeOnTargetMethodInfo = typeof(AbstractInvocation)
            .GetMethod("InvokeMethodOnTarget", BindingFlags.Instance | BindingFlags.NonPublic);

        private delegate void InvokeOnTargetDelegate();
        private readonly IInvocation _invocation;

        public InvocationAdapter(IInvocation invocation)
        {
            _invocation = invocation;
        }

        public object[] Arguments => _invocation.Arguments;

        public Type[] GenericArguments => _invocation.GenericArguments;

        public object InvocationTarget => _invocation.InvocationTarget;

        public MethodInfo Method => _invocation.Method;

        public MethodInfo MethodInvocationTarget => _invocation.MethodInvocationTarget;

        public object Proxy => _invocation.Proxy;

        public object ReturnValue
        {
            get => _invocation.ReturnValue;
            set => _invocation.ReturnValue = value;
        }

        public Type TargetType => _invocation.TargetType;

        public object GetArgumentValue(int index)
        {
            return _invocation.GetArgumentValue(index);
        }

        public MethodInfo GetConcreteMethod()
        {
            return _invocation.GetConcreteMethod();
        }

        public MethodInfo GetConcreteMethodInvocationTarget()
        {
            return _invocation.GetConcreteMethodInvocationTarget();
        }

        public void SetArgumentValue(int index, object value)
        {
            _invocation.SetArgumentValue(index, value);
        }

        public void InvokeOnTarget()
        {
            //Workaround to call the private "InvokeOnTarget" on the AbstractInvocation class
            var invokeDelegate = (InvokeOnTargetDelegate) InvokeOnTargetMethodInfo
                .CreateDelegate(typeof(InvokeOnTargetDelegate), _invocation);

            invokeDelegate();
        }
    }
}