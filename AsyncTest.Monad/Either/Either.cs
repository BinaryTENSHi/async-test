using System.Collections.Generic;

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

        protected bool Equals(Either<TLeft, TRight> other)
        {
            return HasLeftValue == other.HasLeftValue
                   && HasRightValue == other.HasRightValue
                   && EqualityComparer<TLeft>.Default.Equals(LeftValue, other.LeftValue)
                   && EqualityComparer<TRight>.Default.Equals(RightValue, other.RightValue);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Either<TLeft, TRight>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = HasLeftValue.GetHashCode();
                hashCode = (hashCode * 397) ^ HasRightValue.GetHashCode();
                hashCode = (hashCode * 397) ^ EqualityComparer<TLeft>.Default.GetHashCode(LeftValue);
                hashCode = (hashCode * 397) ^ EqualityComparer<TRight>.Default.GetHashCode(RightValue);
                return hashCode;
            }
        }
    }
}