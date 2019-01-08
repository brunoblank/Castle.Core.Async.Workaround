using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Castle.Core.Async.Workaround
{
    public class Program
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public static async Task Main(string[] args)
        {
            var proxy = ProxyGenerator.CreateClassProxyWithTarget(
                new TargetClass(),
                new WorkaroundInterceptor(new AsyncInterceptor("AI-1"), new AsyncInterceptor("AI-2")));

            await proxy.SayHello();
        }
    }
}