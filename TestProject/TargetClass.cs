using System;
using System.Threading.Tasks;

namespace TestProject
{
    public class TargetClass
    {
        public virtual async Task SayHello()
        {
            await Task.Delay(500);
            Console.WriteLine("Hello!");
            await Task.Delay(500);
        }
    }
}