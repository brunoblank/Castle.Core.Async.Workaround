namespace Castle.Core.Async.Workaround
{
    using System;
    using System.Threading.Tasks;

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