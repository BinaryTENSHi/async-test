using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncTest.Pump;
using NUnit.Framework;

namespace AsyncTest.Test.Pump
{
    public class AsyncPumpTest
    {
        [Test]
        public async Task ShouldPump()
        {
            AsyncPump pump = new AsyncPump(1);

            Stopwatch stopwatch = Stopwatch.StartNew();

            pump.Enqueue(new DelayQueueitem(TimeSpan.FromSeconds(15)));
            pump.Enqueue(new DelayQueueitem(TimeSpan.FromSeconds(15)));
            pump.Enqueue(new DelayQueueitem(TimeSpan.FromSeconds(15)));

            pump.Start();

            await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
            await pump.StopAsync(TimeSpan.FromSeconds(2)).ConfigureAwait(false);

            stopwatch.Stop();
            Console.WriteLine($"Took {stopwatch.Elapsed.TotalSeconds} seconds");

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.EqualTo(4).Within(0.2));
        }

        [Test]
        public async Task ShouldExecuteAllItems()
        {
            AsyncPump pump = new AsyncPump(1);

            Stopwatch stopwatch = Stopwatch.StartNew();
            List<object> resultList = new List<object>();
            Func<Task> func =
                () => Task.Run(
                    () => resultList.Add(new object()));

            pump.Enqueue(new FuncQueueItem(func));
            pump.Enqueue(new FuncQueueItem(func));
            pump.Enqueue(new FuncQueueItem(func));

            pump.Start();

            await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
            await pump.StopAsync(TimeSpan.FromSeconds(2)).ConfigureAwait(false);

            stopwatch.Stop();
            Console.WriteLine($"Took {stopwatch.Elapsed.TotalSeconds} seconds");

            Assert.That(resultList.Count, Is.EqualTo(3));
        }
    }
}