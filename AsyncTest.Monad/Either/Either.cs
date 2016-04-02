namespace AsyncTest.Monad.Either
{
    public class Either<TLeft, TRight>
    {
        private Either(TLeft left)
        {
            HasLeftValue = true;
            LeftValue = left;
        }

        private Either(TRight right)
        {
            HasRightValue = true;
            RightValue = right;
        }

        public bool HasLeftValue { get; }
        public bool HasRightValue { get; }
        public TLeft LeftValue { get; }
        public TRight RightValue { get; }

        public static Either<TLeft, TRight> Left(TLeft left) => new Either<TLeft, TRight>(left);
        public static Either<TLeft, TRight> Right(TRight right) => new Either<TLeft, TRight>(right);
    }
}