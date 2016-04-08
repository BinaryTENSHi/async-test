using System.Collections.Generic;

namespace AsyncTest.Monad.Maybe
{
    public class Maybe<T>
    {
        private Maybe(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }

        public T Value { get; }
        public bool HasValue { get; }

        public static Maybe<T> Nothing => new Maybe<T>(default(T), false);
        public static Maybe<T> Just(T value) => new Maybe<T>(value, true);

        protected bool Equals(Maybe<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value) && HasValue == other.HasValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Maybe<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Value) * 397) ^ HasValue.GetHashCode();
            }
        }
    }
}