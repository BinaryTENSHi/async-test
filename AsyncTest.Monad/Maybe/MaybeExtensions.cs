using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncTest.Monad.Maybe
{
    public static class MaybeExtensions
    {
        public static Maybe<TNext> Select<T, TNext>(
            this Maybe<T> self,
            Func<T, Maybe<TNext>> func)
        {
            if (!self.HasValue)
                return Maybe<TNext>.Nothing;

            return func(self.Value);
        }

        public static Maybe<TNext> SelectMany<T, TNext>(
            this Maybe<T> self,
            Func<T, Maybe<TNext>> func)
        {
            if (!self.HasValue)
                return Maybe<TNext>.Nothing;

            return func(self.Value);
        }

        public static Maybe<TResult> SelectMany<T, TNext, TResult>(
            this Maybe<T> self,
            Func<T, Maybe<TNext>> select,
            Func<T, TNext, TResult> project)
        {
            if (!self.HasValue)
                return Maybe<TResult>.Nothing;

            Maybe<TNext> next = select(self.Value);
            if (!next.HasValue)
                return Maybe<TResult>.Nothing;

            return Maybe<TResult>.Just(project(self.Value, next.Value));
        }

        public static async Task<Maybe<TNext>> SelectMany<T, TNext>(
            this Task<Maybe<T>> self,
            Func<T, Task<Maybe<TNext>>> func)
        {
            Maybe<T> res = await self.ConfigureAwait(false);
            if (!res.HasValue)
                return Maybe<TNext>.Nothing;

            return await func(res.Value).ConfigureAwait(false);
        }

        public static async Task<Maybe<TResult>> SelectMany<T, TNext, TResult>(
            this Task<Maybe<T>> self,
            Func<T, Task<Maybe<TNext>>> select,
            Func<T, TNext, TResult> project)
        {
            Maybe<T> res = await self.ConfigureAwait(false);
            if (!res.HasValue)
                return Maybe<TResult>.Nothing;

            Maybe<TNext> next = await select(res.Value).ConfigureAwait(false);
            if (!next.HasValue)
                return Maybe<TResult>.Nothing;

            return Maybe<TResult>.Just(project(res.Value, next.Value));
        }

        public static Maybe<T> Fold<T>(this IEnumerable<Maybe<T>> enumerable, Func<T, T, T> func)
            => Fold(enumerable, default(T), func);

        public static Maybe<T> Fold<T>(
            this IEnumerable<Maybe<T>> enumerable,
            T startValue,
            Func<T, T, T> func)
        {
            T acc = startValue;
            foreach (Maybe<T> maybe in enumerable)
            {
                if (!maybe.HasValue)
                    return Maybe<T>.Nothing;

                acc = func(acc, maybe.Value);
            }

            return Maybe<T>.Just(acc);
        }

        public static IEnumerable<Maybe<TNext>> Map<T, TNext>(
            this IEnumerable<Maybe<T>> self,
            Func<T, TNext> func)
        {
            return self.Select(maybe => !maybe.HasValue
                ? Maybe<TNext>.Nothing
                : Maybe<TNext>.Just(func(maybe.Value)));
        }
    }
}