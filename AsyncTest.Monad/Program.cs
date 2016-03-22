using System;
using System.Threading.Tasks;

namespace AsyncTest.Monad
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Option<int> result =
                from two in ReturnTwo()
                from five in ReturnFive()
                select two + five;

            Console.WriteLine($"HasValue: {result.HasValue}, Value: {result.Value}");

            /* ASYNC */
            Task<Option<int>> asyncResult =
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
                Option<int> optionResult =
                    from square in ReturnSquareIfGreaterThanFive(num)
                    from res in SubtractOne(square)
                    select res;

                Console.WriteLine($"Input: {num} => Output: {optionResult.HasValue} : {optionResult.Value}");
            }

            Console.ReadLine();
        }

        private static Option<int> ReturnSquareIfGreaterThanFive(int x)
        {
            if (x < 5)
                return new Nothing<int>();

            return new Just<int>(x*x);
        }

        private static Option<int> SubtractOne(int x)
        {
            return new Just<int>(x - 1);
        }

        private static async Task<Option<int>> ReturnSquareAsync(int x)
        {
            await Task.Delay(100).ConfigureAwait(false);
            return new Just<int>(x*x);
        }

        private static async Task<Option<int>> ReturnFiveAsync()
        {
            await Task.Delay(1000).ConfigureAwait(false);
            return ReturnFive();
        }

        private static async Task<Option<int>> ReturnTwoAsync()
        {
            await Task.Delay(1000).ConfigureAwait(false);
            return ReturnTwo();
        }

        private static Option<int> ReturnNothing()
        {
            return new Nothing<int>();
        }

        private static Option<int> ReturnTwo()
        {
            return new Just<int>(2);
        }

        private static Option<int> ReturnFive()
        {
            return new Just<int>(5);
        }
    }

    public abstract class Option<T>
    {
        public abstract T Value { get; }
        public abstract bool HasValue { get; }
    }

    public static class AsyncOptionExtensions
    {
        public static async Task<Option<TNext>> SelectMany<T, TNext>(this Task<Option<T>> self,
            Func<T, Task<Option<TNext>>> func)
        {
            Option<T> res = await self.ConfigureAwait(false);
            if (!res.HasValue)
                return new Nothing<TNext>();

            return await func(res.Value).ConfigureAwait(false);
        }

        public static async Task<Option<TResult>> SelectMany<T, TNext, TResult>(this Task<Option<T>> self,
            Func<T, Task<Option<TNext>>> select,
            Func<T, TNext, TResult> project)
        {
            Option<T> res = await self.ConfigureAwait(false);
            if (!res.HasValue)
                return new Nothing<TResult>();

            Option<TNext> next = await select(res.Value).ConfigureAwait(false);
            if (!next.HasValue)
                return new Nothing<TResult>();

            return new Just<TResult>(project(res.Value, next.Value));
        }
    }

    public static class OptionExtensions
    {
        public static Option<TNext> SelectMany<T, TNext>(this Option<T> self, Func<T, Option<TNext>> func)
        {
            if (!self.HasValue)
                return new Nothing<TNext>();

            return func(self.Value);
        }

        public static Option<TResult> SelectMany<T, TNext, TResult>(this Option<T> self, Func<T, Option<TNext>> select,
            Func<T, TNext, TResult> project)
        {
            if (!self.HasValue)
                return new Nothing<TResult>();

            Option<TNext> next = select(self.Value);
            if (!next.HasValue)
                return new Nothing<TResult>();

            return new Just<TResult>(project(self.Value, next.Value));
        }
    }

    public class Just<T> : Option<T>
    {
        public Just(T value)
        {
            Value = value;
        }

        public override T Value { get; }

        public override bool HasValue => true;
    }

    public class Nothing<T> : Option<T>
    {
        public override T Value => default(T);

        public override bool HasValue => false;
    }
}