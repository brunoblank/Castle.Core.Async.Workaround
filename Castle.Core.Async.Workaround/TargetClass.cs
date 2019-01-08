using System;
using System.Threading.Tasks;

namespace Castle.Core.Async.Workaround
{
    public class TargetClass
    {
        public virtual async Task SayHello()
        {
            await Task.Delay(1000);
            Console.WriteLine("Hello!");
            await Task.Delay(1000);
        }
    }
}