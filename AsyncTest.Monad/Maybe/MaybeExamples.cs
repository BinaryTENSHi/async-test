using System;

namespace AsyncTest.Monad.Maybe
{
    public static class MaybeExamples
    {
        public static void Successful()
        {
            Console.WriteLine("** MaybeExamples.Successful **");

            Maybe<int> result =
                from two in ReturnTwo()
                from five in ReturnFive()
                select two + five;

            Console.WriteLine($"HasValue: {result.HasValue}, Value: {result.Value}");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Failure()
        {
            Console.WriteLine("** MaybeExamples.Failure **");

            Maybe<int> result =
                from two in ReturnTwo()
                from five in ReturnNothing()
                select two + five;

            Console.WriteLine($"HasValue: {result.HasValue}, value: {result.Value}");
            Console.WriteLine(Environment.NewLine);
        }

        private static Maybe<int> ReturnNothing()
        {
            return Maybe<int>.Nothing;
        }

        private static Maybe<int> ReturnTwo()
        {
            return Maybe<int>.Just(2);
        }

        private static Maybe<int> ReturnFive()
        {
            return Maybe<int>.Just(5);
        }

        public static void Random()
        {
            Console.WriteLine("** MaybeExamples.Random **");

            Random r = new Random();

            for (int i = 0; i < 30; i++)
            {
                int num = r.Next(0, 10);
                Maybe<int> maybeResult =
                    from square in ReturnSquareIfGreaterThanFive(num)
                    from res in SubtractOne(square)
                    select res;

                Console.WriteLine($"Input: {num} => Output: {maybeResult.HasValue} : {maybeResult.Value}");
            }

            Console.WriteLine(Environment.NewLine);
        }

        private static Maybe<int> ReturnSquareIfGreaterThanFive(int x)
        {
            if (x < 5)
                return Maybe<int>.Nothing;

            return Maybe<int>.Just(x * x);
        }

        private static Maybe<int> SubtractOne(int x)
        {
            return Maybe<int>.Just(x - 1);
        }
    }
}