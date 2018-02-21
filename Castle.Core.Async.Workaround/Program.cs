namespace Castle.Core.Async.Workaround
{
    using System.Threading.Tasks;

    using Castle.DynamicProxy;

    public class Program
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            var proxy = ProxyGenerator.CreateClassProxyWithTarget(
                new TargetClass(),
                new WorkaroundInterceptor(new AsyncInterceptor("AI-1"), new AsyncInterceptor("AI-2")));

            await proxy.SayHello();
        }
    }
}