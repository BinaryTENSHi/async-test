using System.Collections.Generic;
using System.Linq;
using AsyncTest.Monad.Maybe;
using NUnit.Framework;

namespace AsyncTest.Test.Monad.Maybe
{
    [TestFixture]
    public class MaybeTests
    {
        private static IEnumerable<TestCaseData> PointsSource()
        {
            yield return new TestCaseData(
                new Point(10, 5),
                new[] { new Point(0, 5), new Point(3, 0), new Point(7, 0) });

            yield return new TestCaseData(
                new Point(4, 5),
                new[] { new Point(0, 6), new Point(7, -1), new Point(-3, 0) });
        }

        private static IEnumerable<TestCaseData> PointsSourceWithNull()
        {
            yield return new TestCaseData(
                new Point?[] { new Point(0, 5), null, new Point(7, 0) });

            yield return new TestCaseData(
                new Point?[] { null, new Point(7, -1), null });
        }

        public struct Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }

        [Test]
        [TestCase(null, 22)]
        [TestCase(1, 2, null, 8)]
        [TestCase(1, 1, 1, null, 1, 1, 1, 1, 1, 1, 1, 1, null, 1, 1, 1)]
        public void Fold_Addition_WhenInvalidNumbers_ThenReturnNothing(params int?[] values)
        {
            // arrange
            IEnumerable<Maybe<int>> monads = values.Select(x =>
                x.HasValue ? Maybe<int>.Just(x.Value) : Maybe<int>.Nothing);

            // act
            Maybe<int> result = monads.Fold((acc, current) => acc + current);

            // assert
            Assert.That(result.HasValue, Is.False);
            Assert.That(result.Value, Is.EqualTo(default(int)));
        }

        [Test]
        [TestCase(2, 2)]
        [TestCase(42, 20, 22)]
        [TestCase(15, 1, 2, 4, 8)]
        [TestCase(16, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)]
        public void Fold_Addition_WhenValidNumbers_ThenSuccessfullyAdded(int expectedResult, params int[] values)
        {
            // arrange
            IEnumerable<Maybe<int>> monads = values.Select(Maybe<int>.Just);

            // act
            Maybe<int> result = monads.Fold((acc, current) => acc + current);

            // assert
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCaseSource(nameof(PointsSourceWithNull))]
        public void Fold_PointAddition_WhenInvalidPoints_ThenReturnNothing(params Point?[] values)
        {
            // arrange
            IEnumerable<Maybe<Point>> monads = values.Select(x =>
                x.HasValue ? Maybe<Point>.Just(x.Value) : Maybe<Point>.Nothing);

            // act
            Maybe<Point> result = monads.Fold((acc, current) => new Point(acc.X + current.X, acc.Y + current.Y));

            // assert
            Assert.That(result.HasValue, Is.False);
            Assert.That(result.Value, Is.EqualTo(default(Point)));
        }

        [Test]
        [TestCaseSource(nameof(PointsSource))]
        public void Fold_PointAddition_WhenValidPoints_ThenSuccessfullyAdded(
            Point expectedResult,
            params Point[] values)
        {
            // arrange
            IEnumerable<Maybe<Point>> monads = values.Select(Maybe<Point>.Just);

            // act
            Maybe<Point> result = monads.Fold((acc, current) => new Point(acc.X + current.X, acc.Y + current.Y));

            // assert
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("Hello World", "Hello", " ", "World")]
        [TestCase("Null is a valid string", "Null ", null, "is a ", null, "valid string")]
        public void Fold_StringConcat_WhenValidStrings_ThenSuccessfullyConcatted(
            string expectedResult,
            params string[] values)
        {
            // arrange
            IEnumerable<Maybe<string>> monads = values.Select(Maybe<string>.Just);

            // act
            Maybe<string> result = monads.Fold((acc, current) => acc + current);

            // assert
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.EqualTo(expectedResult));
        }
    }
}