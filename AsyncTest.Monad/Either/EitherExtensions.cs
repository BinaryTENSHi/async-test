using System;

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
                return Either<TLeft, TNRight>.Left(next.LeftValue);

            return Either<TLeft, TNRight>.Right(project(self.RightValue, next.RightValue));
        }
    }
}