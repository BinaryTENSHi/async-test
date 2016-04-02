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
    }
}