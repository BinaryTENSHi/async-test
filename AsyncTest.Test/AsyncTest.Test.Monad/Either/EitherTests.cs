﻿using System;
using System.Collections.Generic;
using System.Linq;
using AsyncTest.Monad.Either;
using NUnit.Framework;

namespace AsyncTest.Test.Monad.Either
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class EitherTests
    {
        private const string ErrorMessage = "Error";

        [Test]
        public void Monad_Identity()
        {
            // arrange
            Either<string, int> rightValue = Either<string, int>.Right(32);
            Either<string, int> leftValue = Either<string, int>.Left(ErrorMessage);

            // act
            Func<Either<string, int>, Either<string, int>> identityFunc =
                either => from value in either
                    select Either<string, int>.Right(value);

            Either<string, int> resultRightValue = identityFunc(rightValue);
            Either<string, int> resultLeftValue = identityFunc(leftValue);

            // assert
            Assert.That(resultRightValue, Is.EqualTo(rightValue));
            Assert.That(resultLeftValue, Is.EqualTo(leftValue));
        }

        [Test]
        [TestCase(null, 22)]
        [TestCase(1, 2, null, 8)]
        [TestCase(1, 1, 1, null, 1, 1, 1, 1, 1, 1, 1, 1, null, 1, 1, 1)]
        public void Fold_Addition_WhenInvalidNumbers_ThenReturnErrorMessage(params int?[] values)
        {
            // arrange
            IEnumerable<Either<string, int>> monads = values.Select(
                x => x.HasValue ? Either<string, int>.Right(x.Value) : Either<string, int>.Left(ErrorMessage));

            // act
            Either<string, int> result = monads.Fold((acc, current) => acc + current);

            // assert
            Assert.That(result.HasRightValue, Is.False);
            Assert.That(result.HasLeftValue, Is.True);
            Assert.That(result.LeftValue, Is.EqualTo(ErrorMessage));
        }

        [Test]
        [TestCase(42, 20, 22)]
        [TestCase(15, 1, 2, 4, 8)]
        [TestCase(16, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)]
        public void Fold_Addition_WhenValidNumbers_ThenSuccessfullyAdded(int expectedValue, params int?[] values)
        {
            // arrange
            IEnumerable<Either<string, int>> monads = values.Select(
                x => x.HasValue ? Either<string, int>.Right(x.Value) : Either<string, int>.Left(ErrorMessage));

            // act
            Either<string, int> result = monads.Fold((acc, current) => acc + current);

            // assert
            Assert.That(result.HasLeftValue, Is.False);
            Assert.That(result.HasRightValue, Is.True);
            Assert.That(result.RightValue, Is.EqualTo(expectedValue));
        }
    }
}