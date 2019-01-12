using System;
using System.Threading.Tasks;
using Castle.Core.Async.Workaround;
using Castle.DynamicProxy;

namespace TestProject
{
    public class Program
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public static async Task Main(string[] args)
        {
            await ProxyGenerator
                .CreateClassProxyWithTarget(new TargetClass(), new AsyncInterceptor("AI-1"), new AsyncInterceptor("AI-2")
                ).SayHello();

            Console.WriteLine(" -------------------------------- ");
            await ProxyGenerator.CreateInterfaceProxyWithoutTarget<ITargetInterface>(new AsyncInterceptor("AI-1")).SayHello();
        }
    }
}