using System;
using AsyncTest.Monad.Maybe;

namespace AsyncTest.Monad
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MaybeExamples.Successful();
            MaybeExamples.Failure();
            MaybeExamples.Random();

            AsyncMaybeExamples.Successful().Wait();

            Console.WriteLine("Done. Press ENTER to quit.");
            Console.ReadLine();
        }
    }
}