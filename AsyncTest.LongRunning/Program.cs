using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncTest.LongRunning
{
    public class Program
    {
        private static Task TaskFromFactoryLongRunning()
        {
            return Task.Factory.StartNew(function: Function, creationOptions: TaskCreationOptions.LongRunning);
        }

        private static Task TaskFromRun()
        {
            return Task.Run(function: Function);
        }

        private static async Task Function()
        {
            Console.Write(".");
            await Task.Delay(50).ConfigureAwait(false);
            Console.Write(",");
        }

        public static void Main(string[] args)
        {
            Run("Task.Factory.StartNew(LongRunning)", TaskFromFactoryLongRunning);
            Run("Task.Run", TaskFromRun);

            Console.ReadLine();
        }

        private static void Run(string title, Func<Task> taskCreation)
        {
            const int count = 5000;
            Stopwatch watch = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < count; i++)
            {
                tasks.Add(taskCreation());
            }

            Task.WhenAll(tasks).Wait();

            watch.Stop();
            Console.WriteLine(string.Empty);
            Console.WriteLine("~~ {0} ~~", title);
            Console.WriteLine("{0} tasks took: {1}ms", count, watch.ElapsedMilliseconds);
        }
    }
}