using System;
using System.Threading.Tasks;

namespace AsyncTest.Monad
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Maybe<int> result =
                from two in ReturnTwo()
                from five in ReturnFive()
                select two + five;

            Console.WriteLine($"HasValue: {result.HasValue}, Value: {result.Value}");

            /* ASYNC */
            Task<Maybe<int>> asyncResult =
                from two in ReturnTwoAsync()
                from five in ReturnFiveAsync()
                from fives in ReturnSquareAsync(five)
                select two + fives;

            result = asyncResult.Result;
            Console.WriteLine($"HasValue: {result.HasValue}, Value: {result.Value}");

            /* RAND */
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

            Console.ReadLine();
        }

        private static Maybe<int> ReturnSquareIfGreaterThanFive(int x)
        {
            if (x < 5)
                return new Nothing<int>();

            return new Just<int>(x*x);
        }

        private static Maybe<int> SubtractOne(int x)
        {
            return new Just<int>(x - 1);
        }

        private static async Task<Maybe<int>> ReturnSquareAsync(int x)
        {
            await Task.Delay(100).ConfigureAwait(false);
            return new Just<int>(x*x);
        }

        private static async Task<Maybe<int>> ReturnFiveAsync()
        {
            await Task.Delay(1000).ConfigureAwait(false);
            return ReturnFive();
        }

        private static async Task<Maybe<int>> ReturnTwoAsync()
        {
            await Task.Delay(1000).ConfigureAwait(false);
            return ReturnTwo();
        }

        private static Maybe<int> ReturnNothing()
        {
            return new Nothing<int>();
        }

        private static Maybe<int> ReturnTwo()
        {
            return new Just<int>(2);
        }

        private static Maybe<int> ReturnFive()
        {
            return new Just<int>(5);
        }
    }

    public abstract class Maybe<T>
    {
        public abstract T Value { get; }
        public abstract bool HasValue { get; }
    }

    public static class AsyncOptionExtensions
    {
        public static async Task<Maybe<TNext>> SelectMany<T, TNext>(this Task<Maybe<T>> self,
            Func<T, Task<Maybe<TNext>>> func)
        {
            Maybe<T> res = await self.ConfigureAwait(false);
            if (!res.HasValue)
                return new Nothing<TNext>();

            return await func(res.Value).ConfigureAwait(false);
        }

        public static async Task<Maybe<TResult>> SelectMany<T, TNext, TResult>(this Task<Maybe<T>> self,
            Func<T, Task<Maybe<TNext>>> select,
            Func<T, TNext, TResult> project)
        {
            Maybe<T> res = await self.ConfigureAwait(false);
            if (!res.HasValue)
                return new Nothing<TResult>();

            Maybe<TNext> next = await select(res.Value).ConfigureAwait(false);
            if (!next.HasValue)
                return new Nothing<TResult>();

            return new Just<TResult>(project(res.Value, next.Value));
        }
    }

    public static class OptionExtensions
    {
        public static Maybe<TNext> SelectMany<T, TNext>(this Maybe<T> self, Func<T, Maybe<TNext>> func)
        {
            if (!self.HasValue)
                return new Nothing<TNext>();

            return func(self.Value);
        }

        public static Maybe<TResult> SelectMany<T, TNext, TResult>(this Maybe<T> self, Func<T, Maybe<TNext>> select,
            Func<T, TNext, TResult> project)
        {
            if (!self.HasValue)
                return new Nothing<TResult>();

            Maybe<TNext> next = select(self.Value);
            if (!next.HasValue)
                return new Nothing<TResult>();

            return new Just<TResult>(project(self.Value, next.Value));
        }
    }

    public class Just<T> : Maybe<T>
    {
        public Just(T value)
        {
            Value = value;
        }

        public override T Value { get; }

        public override bool HasValue => true;
    }

    public class Nothing<T> : Maybe<T>
    {
        public override T Value => default(T);

        public override bool HasValue => false;
    }
}