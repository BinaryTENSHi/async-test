using System;
using System.Threading.Tasks;

namespace AsyncTest.Monad.Maybe
{
    public static class AsyncMaybeExamples
    {
        public static async Task Successful()
        {
            Console.WriteLine("** AsyncMaybeExamples.Successful **");

            Maybe<int> result = await (
                from two in ReturnTwoAsync()
                from five in ReturnFiveAsync()
                from fives in ReturnSquareAsync(five)
                select two + fives);

            Console.WriteLine($"HasValue: {result.HasValue}, Value: {result.Value}");
            Console.WriteLine(Environment.NewLine);
        }

        private static async Task<Maybe<int>> ReturnFiveAsync()
        {
            await Task.Delay(100).ConfigureAwait(false);
            return Maybe<int>.Just(5);
        }

        private static async Task<Maybe<int>> ReturnTwoAsync()
        {
            await Task.Delay(100).ConfigureAwait(false);
            return Maybe<int>.Just(2);
        }

        private static async Task<Maybe<int>> ReturnSquareAsync(int x)
        {
            await Task.Delay(100).ConfigureAwait(false);
            return Maybe<int>.Just(x*x);
        }
    }
}