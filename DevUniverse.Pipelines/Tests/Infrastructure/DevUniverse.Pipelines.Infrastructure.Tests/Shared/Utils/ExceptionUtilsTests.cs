using System;

using DevUniverse.Pipelines.Infrastructure.Shared;
using DevUniverse.Pipelines.Infrastructure.Shared.Utils;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Shared
{
    public class ExceptionUtilsTests
    {
        #region CheckIfNull

        public static TheoryData<object?, bool> TestDataCheckIfNull => new TheoryData<object?, bool>()
        {
            { new object(), false },
            { null, true },
            { "", false },
            { (string?)null, true },
            { 5, false },
            { (int?)null, true }
        };

        [Theory]
        [MemberData(nameof(ExceptionUtilsTests.TestDataCheckIfNull))]
        public void CheckIfNull_ReturnsCorrectResult(object? target, bool expectedResult)
        {
            var actualResult = ExceptionUtils.CheckIfNull(target);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CheckIfNull

        #region Process

        public static TheoryData<object?, Func<object?, bool>?, Func<Exception>?> TestDataProcessException => new TheoryData<object?, Func<object?, bool>?, Func<Exception>?>()
        {
            { new object(), null, null },
            { new object(), param => param == null, null }
        };

        [Theory]
        [MemberData(nameof(ExceptionUtilsTests.TestDataProcessException))]
        public void Process_ArgumentIsNull_ThrowsException(object? arg, Func<object?, bool>? predicate, Func<Exception>? exceptionFactory) =>
            Assert.Throws<ArgumentNullException>(() => ExceptionUtils.Process(arg, predicate!, exceptionFactory!));

        public static TheoryData<object?, Func<object?, bool>, Func<Exception>, bool> TestDataProcess => new TheoryData<object?, Func<object?, bool>, Func<Exception>, bool>()
        {
            { new object(), ExceptionUtils.CheckIfNull, () => new ArgumentNullException(), false },
            { null, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(), true },
            { "", ExceptionUtils.CheckIfNull, () => new ArgumentNullException(), false },
            { (string?)null, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(), true },
            { 5, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(), false },
            { (int?)null, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(), true }
        };

        [Theory]
        [MemberData(nameof(ExceptionUtilsTests.TestDataProcess))]
        public void Process_ExecutesCorrectly(object? arg, Func<object?, bool> predicate, Func<Exception> exceptionFactory, bool shouldThrow)
        {
            Action processDelegate = () => ExceptionUtils.Process(arg, predicate, exceptionFactory);

            if (shouldThrow)
            {
                Assert.Throws<ArgumentNullException>(processDelegate);
            }
            else
            {
                processDelegate.Invoke();
            }
        }

        #endregion Process
    }
}
