using System;
using System.Collections.Generic;
using System.Linq;

namespace Castle.Core.Async.Workaround
{
    public class InterceptorBuilder
    {
        private readonly IList<Func<InvocationDelegate, InvocationDelegate>> _components = new List<Func<InvocationDelegate, InvocationDelegate>>();

        public InterceptorBuilder Use(Action<IInvocationV2, Action> interceptor)
        {
            return Use(proceed => invocation => interceptor(invocation, () => proceed(invocation)));
        }

        public InterceptorBuilder Use(Func<InvocationDelegate, InvocationDelegate> interceptor)
        {
            _components.Add(interceptor);
            return this;
        }

        public InterceptorBuilder Use(IInterceptorV2 interceptor)
        {
            return Use(proceed => invocation => interceptor.Intercept(invocation, proceed));
        }

        public InvocationDelegate Build()
        {
            InvocationDelegate app = invocation => invocation.InvokeOnTarget();

            foreach (var component in _components.Reverse())
                app = component(app);

            return app;
        }
    }
}