using System;
using System.Threading;
using System.Threading.Tasks;

namespace Air
{
    public static class DeadLockDemo
    {
        public static async  Task FuncAsync()
        {
            await Task.FromResult(1);
            await Task.FromResult(1).ConfigureAwait(false);
            var random = new Random();
            var delay = random.Next(2);
            await Task.Delay(delay).ConfigureAwait(false);
        }
        public static async Task TestNotFullyAsync()
        {
            await Task.Yield();
            Thread.Sleep(5000);
        }
        private static async Task DelayAsync()
        {
            await Task.Delay(1000);
            await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext:false);
        }

        public static void Test()
        {
            var delayTask = DelayAsync();
            delayTask.Wait();
        }
    }

}