using System;

namespace AsyncTest.Monad.Either
{
    public static class EitherExamples
    {
        public static void Successful()
        {
            Console.WriteLine("** EitherExamples.Successful **");

            Either<string, int> result =
                from two in ReturnTwo()
                from five in ReturnFive()
                select two + five;

            Console.WriteLine($"HasLeftValue: {result.HasLeftValue}, LeftValue: {result.LeftValue}");
            Console.WriteLine($"HasRightValue: {result.HasRightValue}, RightValue: {result.RightValue}");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Failure()
        {
            Console.WriteLine("** EitherExamples.Failure **");

            Either<string, int> result =
                from two in ReturnTwo()
                from five in ReturnFailure("From five")
                select two + five;

            Console.WriteLine($"HasLeftValue: {result.HasLeftValue}, LeftValue: {result.LeftValue}");
            Console.WriteLine($"HasRightValue: {result.HasRightValue}, RightValue: {result.RightValue}");
            Console.WriteLine(Environment.NewLine);
        }

        public static void MultipleFailures()
        {
            Console.WriteLine("** EitherExamples.Failure **");

            Either<string, int> result =
                from two in ReturnFailure("From two")
                from five in ReturnFailure("From five")
                select two + five;

            Console.WriteLine($"HasLeftValue: {result.HasLeftValue}, LeftValue: {result.LeftValue}");
            Console.WriteLine($"HasRightValue: {result.HasRightValue}, RightValue: {result.RightValue}");
            Console.WriteLine(Environment.NewLine);
        }

        private static Either<string, int> ReturnTwo()
        {
            return Either<string, int>.Right(2);
        }

        private static Either<string, int> ReturnFive()
        {
            return Either<string, int>.Right(5);
        }

        private static Either<string, int> ReturnFailure(string content)
        {
            return Either<string, int>.Left(content);
        }
    }
}