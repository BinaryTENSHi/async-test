using System;
using System.Threading.Tasks;

namespace AsyncTest.Monad.Either
{
    public static class EitherAsyncExamples
    {
        public static async Task Successful()
        {
            Console.WriteLine("** EitherAsyncExamples.Successful **");

            Either<string, int> result = await (
                from two in ReturnTwoAsync()
                from five in ReturnFiveAsync()
                select two + five);

            Console.WriteLine($"HasLeftValue: {result.HasLeftValue}, LeftValue: {result.LeftValue}");
            Console.WriteLine($"HasRightValue: {result.HasRightValue}, RightValue: {result.RightValue}");
            Console.WriteLine(Environment.NewLine);
        }

        public static async Task Failure()
        {
            Console.WriteLine("** EitherAsyncExamples.Failure **");

            Either<string, int> result = await (
                from two in ReturnTwoAsync()
                from five in ReturnFailureAsync("From five")
                select two + five);

            Console.WriteLine($"HasLeftValue: {result.HasLeftValue}, LeftValue: {result.LeftValue}");
            Console.WriteLine($"HasRightValue: {result.HasRightValue}, RightValue: {result.RightValue}");
            Console.WriteLine(Environment.NewLine);
        }

        private static async Task<Either<string, int>> ReturnTwoAsync()
        {
            await Task.Delay(100).ConfigureAwait(false);
            return Either<string, int>.Right(2);
        }

        private static async Task<Either<string, int>> ReturnFiveAsync()
        {
            await Task.Delay(100).ConfigureAwait(false);
            return Either<string, int>.Right(5);
        }

        private static async Task<Either<string, int>> ReturnFailureAsync(string content)
        {
            await Task.Delay(100).ConfigureAwait(false);
            return Either<string, int>.Left(content);
        }
    }
}