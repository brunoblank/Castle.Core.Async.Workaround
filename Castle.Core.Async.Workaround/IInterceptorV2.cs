namespace Castle.Core.Async.Workaround
{
    /// <summary>
    ///   Provides the main DynamicProxy extension point that allows member interception.
    /// </summary>
    public interface IInterceptorV2
    {
        void Intercept(IInvocationV2 invocation, InvocationDelegate proceed);
    }
}