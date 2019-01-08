using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.DynamicProxy;

namespace Castle.Core.Async.Workaround
{
    public class WorkaroundInvocation : IInvocation
    {
        private readonly int _index;
        private readonly IReadOnlyList<IInterceptor> _interceptors;
        private readonly IInvocation _invocation;

        public WorkaroundInvocation(IInvocation invocation, IReadOnlyList<IInterceptor> interceptors, int index)
        {
            _invocation = invocation;
            _interceptors = interceptors;
            _index = index;
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

        public void Proceed()
        {
            new WorkaroundInvocation(_invocation, _interceptors, _index + 1).Invoke();
        }

        public void SetArgumentValue(int index, object value)
        {
            _invocation.SetArgumentValue(index, value);
        }

        public void Invoke()
        {
            if (_index < _interceptors.Count)
                _interceptors[_index].Intercept(this);
            else if (ReferenceEquals(_invocation.InvocationTarget, _invocation.Proxy))
                throw new InvalidOperationException(
                    "This is a DynamicProxy2 (" + nameof(WorkaroundInvocation)+ ") error: invocation.Proceed() " +
                    "is not expected to be called on the last interceptor. \r\n" +
                    " Method: " + Method + "\r\n Interceptor: " +
                    _interceptors[_interceptors.Count - 1].GetType().FullName);
            else
                _invocation.ReturnValue = _invocation.GetConcreteMethodInvocationTarget().Invoke(
                    _invocation.InvocationTarget,
                    _invocation.Arguments);
        }
    }
}