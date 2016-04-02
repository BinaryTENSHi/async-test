﻿using System;
using System.Threading.Tasks;

namespace AsyncTest.Monad.Either
{
    public static class EitherExtensions
    {
        public static Either<TLeft, TNRight> SelectMany<TLeft, TRight, TNRight>(
            this Either<TLeft, TRight> self,
            Func<TRight, Either<TLeft, TNRight>> func)
        {
            if (!self.HasLeftValue)
                return Either<TLeft, TNRight>.Left(self.LeftValue);

            return func(self.RightValue);
        }

        public static Either<TLeft, TNRight> SelectMany<TLeft, TRight, TNRight>(
            this Either<TLeft, TRight> self,
            Func<TRight, Either<TLeft, TNRight>> select,
            Func<TRight, TNRight, TNRight> project)
        {
            if (self.HasLeftValue)
                return Either<TLeft, TNRight>.Left(self.LeftValue);

            Either<TLeft, TNRight> next = select(self.RightValue);
            if (next.HasLeftValue)
                return next;

            return Either<TLeft, TNRight>.Right(project(self.RightValue, next.RightValue));
        }

        public static async Task<Either<TLeft, TNRight>> SelectMany<TLeft, TRight, TNRight>(
            this Task<Either<TLeft, TRight>> self,
            Func<TRight, Task<Either<TLeft, TNRight>>> select,
            Func<TRight, TNRight, TNRight> project)
        {
            Either<TLeft, TRight> result = await self.ConfigureAwait(false);
            if (result.HasLeftValue)
                return Either<TLeft, TNRight>.Left(result.LeftValue);

            Either<TLeft, TNRight> next = await select(result.RightValue).ConfigureAwait(false);
            if (next.HasLeftValue)
                return next;

            return Either<TLeft, TNRight>.Right(project(result.RightValue, next.RightValue));
        }
    }
}