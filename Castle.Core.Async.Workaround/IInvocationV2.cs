using System;
using System.Reflection;

namespace Castle.Core.Async.Workaround
{
    /// <summary>Encapsulates an invocation of a proxied method.</summary>
    public interface IInvocationV2
    {
        /// <summary>
        ///   Gets the arguments that the <see cref="P:Castle.DynamicProxy.IInvocation.Method" /> has been invoked with.
        /// </summary>
        /// <value>The arguments the method was invoked with.</value>
        object[] Arguments { get; }

        /// <summary>Gets the generic arguments of the method.</summary>
        /// <value>The generic arguments, or null if not a generic method.</value>
        Type[] GenericArguments { get; }

        /// <summary>
        ///   Gets the object on which the invocation is performed. This is different from proxy object
        ///   because most of the time this will be the proxy target object.
        /// </summary>
        /// <seealso cref="T:Castle.DynamicProxy.IChangeProxyTarget" />
        /// <value>The invocation target.</value>
        object InvocationTarget { get; }

        /// <summary>
        ///   Gets the <see cref="T:System.Reflection.MethodInfo" /> representing the method being invoked on the proxy.
        /// </summary>
        /// <value>The <see cref="T:System.Reflection.MethodInfo" /> representing the method being invoked.</value>
        MethodInfo Method { get; }

        /// <summary>
        ///   For interface proxies, this will point to the <see cref="T:System.Reflection.MethodInfo" /> on the target class.
        /// </summary>
        /// <value>The method invocation target.</value>
        MethodInfo MethodInvocationTarget { get; }

        /// <summary>
        ///   Gets the proxy object on which the intercepted method is invoked.
        /// </summary>
        /// <value>Proxy object on which the intercepted method is invoked.</value>
        object Proxy { get; }

        /// <summary>Gets or sets the return value of the method.</summary>
        /// <value>The return value of the method.</value>
        object ReturnValue { get; set; }

        /// <summary>
        ///   Gets the type of the target object for the intercepted method.
        /// </summary>
        /// <value>The type of the target object.</value>
        Type TargetType { get; }

        /// <summary>
        ///   Gets the value of the argument at the specified <paramref name="index" />.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The value of the argument at the specified <paramref name="index" />.</returns>
        object GetArgumentValue(int index);

        /// <summary>
        ///   Returns the concrete instantiation of the <see cref="P:Castle.DynamicProxy.IInvocation.Method" /> on the proxy, with any generic
        ///   parameters bound to real types.
        /// </summary>
        /// <returns>
        ///   The concrete instantiation of the <see cref="P:Castle.DynamicProxy.IInvocation.Method" /> on the proxy, or the <see cref="P:Castle.DynamicProxy.IInvocation.Method" /> if
        ///   not a generic method.
        /// </returns>
        /// <remarks>
        ///   Can be slower than calling <see cref="P:Castle.DynamicProxy.IInvocation.Method" />.
        /// </remarks>
        MethodInfo GetConcreteMethod();
        /// <summary>
        ///   Returns the concrete instantiation of <see cref="P:Castle.DynamicProxy.IInvocation.MethodInvocationTarget" />, with any
        ///   generic parameters bound to real types.
        ///   For interface proxies, this will point to the <see cref="T:System.Reflection.MethodInfo" /> on the target class.
        /// </summary>
        /// <returns>The concrete instantiation of <see cref="P:Castle.DynamicProxy.IInvocation.MethodInvocationTarget" />, or
        /// <see cref="P:Castle.DynamicProxy.IInvocation.MethodInvocationTarget" /> if not a generic method.</returns>
        /// <remarks>
        ///   In debug builds this can be slower than calling <see cref="P:Castle.DynamicProxy.IInvocation.MethodInvocationTarget" />.
        /// </remarks>
        MethodInfo GetConcreteMethodInvocationTarget();
        
        /// <summary>
        ///   Overrides the value of an argument at the given <paramref name="index" /> with the
        ///   new <paramref name="value" /> provided.
        /// </summary>
        /// <remarks>
        ///   This method accepts an <see cref="T:System.Object" />, however the value provided must be compatible
        ///   with the type of the argument defined on the method, otherwise an exception will be thrown.
        /// </remarks>
        /// <param name="index">The index of the argument to override.</param>
        /// <param name="value">The new value for the argument.</param>
        void SetArgumentValue(int index, object value);

        void InvokeOnTarget();
    }
}