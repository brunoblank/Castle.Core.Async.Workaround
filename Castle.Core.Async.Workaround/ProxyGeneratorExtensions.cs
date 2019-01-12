using Castle.DynamicProxy;

namespace Castle.Core.Async.Workaround
{
    public static class ProxyGeneratorExtensions
    {
        public static T CreateClassProxyWithTarget<T>(this ProxyGenerator proxyGenerator, T target, params IInterceptorV2[] interceptors) where T:class
        {
            var interceptorBuilder = new InterceptorBuilder();
            foreach (var interceptor in interceptors)
                interceptorBuilder.Use(interceptor);

            return proxyGenerator.CreateClassProxyWithTarget(target, new InterceptorAdapter(interceptorBuilder.Build()));
        }

        public static T CreateInterfaceProxyWithoutTarget<T>(this ProxyGenerator proxyGenerator, params IInterceptorV2[] interceptors) where T : class
        {
            var interceptorBuilder = new InterceptorBuilder();
            foreach (var interceptor in interceptors)
                interceptorBuilder.Use(interceptor);

            return proxyGenerator.CreateInterfaceProxyWithoutTarget<T>(new InterceptorAdapter(interceptorBuilder.Build()));
        }
    }
}