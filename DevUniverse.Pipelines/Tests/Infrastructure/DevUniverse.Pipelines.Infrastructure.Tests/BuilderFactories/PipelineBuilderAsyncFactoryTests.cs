using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.Builders;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.BuilderFactories
{
    public class PipelineBuilderAsyncFactoryTests
    {
        public static IServiceProvider ServiceProvider => new ServiceCollection().BuildServiceProvider();

        #region Create

        public static TheoryData<Func<object>, Type> TestDataCreateGeneric =>
            new TheoryData<Func<object>, Type>()
            {
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<Task>(),
                    typeof(PipelineBuilderAsync<Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilderAsync<Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<Task>((object?[]?)null),
                    typeof(PipelineBuilderAsync<Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<Task>(new List<object?>() { null }),
                    typeof(PipelineBuilderAsync<Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<Task>(new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilderAsync<Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<Task>(PipelineBuilderAsyncFactoryTests.ServiceProvider),
                    typeof(PipelineBuilderAsync<Task>)
                },

                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, Task>(),
                    typeof(PipelineBuilderAsync<string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilderAsync<string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, Task>((object?[]?)null),
                    typeof(PipelineBuilderAsync<string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, Task>(new List<object?>() { null }),
                    typeof(PipelineBuilderAsync<string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, Task>(new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilderAsync<string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, Task>(PipelineBuilderAsyncFactoryTests.ServiceProvider),
                    typeof(PipelineBuilderAsync<string, Task>)
                },

                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, Task>(),
                    typeof(PipelineBuilderAsync<string, int, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilderAsync<string, int, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, Task>((object?[]?)null),
                    typeof(PipelineBuilderAsync<string, int, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, Task>(new List<object?>() { null }),
                    typeof(PipelineBuilderAsync<string, int, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, Task>(new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilderAsync<string, int, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, Task>(PipelineBuilderAsyncFactoryTests.ServiceProvider),
                    typeof(PipelineBuilderAsync<string, int, Task>)
                },

                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, Task>(),
                    typeof(PipelineBuilderAsync<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilderAsync<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, Task>((object?[]?)null),
                    typeof(PipelineBuilderAsync<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, Task>(new List<object?>() { null }),
                    typeof(PipelineBuilderAsync<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, Task>
                        (new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilderAsync<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, Task>(PipelineBuilderAsyncFactoryTests.ServiceProvider),
                    typeof(PipelineBuilderAsync<string, int, object, Task>)
                },

                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, string, Task>(),
                    typeof(PipelineBuilderAsync<string, int, object, string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, string, Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilderAsync<string, int, object, string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, string, Task>((object?[]?)null),
                    typeof(PipelineBuilderAsync<string, int, object, string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, string, Task>(new List<object?>() { null }),
                    typeof(PipelineBuilderAsync<string, int, object, string, Task>)
                },
                {
                    () =>
                        PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, string, Task>
                            (new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilderAsync<string, int, object, string, Task>)
                },
                {
                    () => PipelineBuilderAsyncFactoryTests.CreateSut().Create<string, int, object, string, Task>(PipelineBuilderAsyncFactoryTests.ServiceProvider),
                    typeof(PipelineBuilderAsync<string, int, object, string, Task>)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsyncFactoryTests.TestDataCreateGeneric))]
        public void Create_Generic_ReturnsCorrectResult(Func<object> creatingDelegate, Type expectedResultType)
        {
            var actualResult = creatingDelegate.Invoke();

            Assert.NotNull(actualResult);
            Assert.Equal(expectedResultType, actualResult.GetType());
        }

        [Fact]
        public void Create_ArgumentIsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsyncFactoryTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Create(null!));
        }

        public static TheoryData<Type, IEnumerable<object?>?> TestDataCreateNonGenericIEnumerable =>
            new TheoryData<Type, IEnumerable<object?>?>()
            {
                { typeof(PipelineBuilderAsyncTest<Task>), null },
                { typeof(PipelineBuilderAsyncTest<Task>), new List<object?>() { 5, 100M } },

                { typeof(PipelineBuilderAsync<Task>), null },
                { typeof(PipelineBuilderAsync<Task>), new List<object?>() { null } },
                { typeof(PipelineBuilderAsync<Task>), new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilderAsync<string, Task>), null },
                { typeof(PipelineBuilderAsync<string, Task>), new List<object?>() { null } },
                { typeof(PipelineBuilderAsync<string, Task>), new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilderAsync<string, int, Task>), null },
                { typeof(PipelineBuilderAsync<string, int, Task>), new List<object?>() { null } },
                { typeof(PipelineBuilderAsync<string, int, Task>), new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilderAsync<string, int, object, Task>), null },
                { typeof(PipelineBuilderAsync<string, int, object, Task>), new List<object?>() { null } },
                { typeof(PipelineBuilderAsync<string, int, object, Task>), new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilderAsync<string, int, object, string, Task>), null },
                { typeof(PipelineBuilderAsync<string, int, object, string, Task>), new List<object?>() { null } },
                { typeof(PipelineBuilderAsync<string, int, object, string, Task>), new List<object?>() { PipelineBuilderAsyncFactoryTests.ServiceProvider } }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsyncFactoryTests.TestDataCreateNonGenericIEnumerable))]
        public void Create_NonGeneric_IEnumerable_ReturnsCorrectResult(Type expectedResultType, IEnumerable<object?>? constructorArguments)
        {
            var sut = PipelineBuilderAsyncFactoryTests.CreateSut();

            var actualResult = sut.Create(expectedResultType, constructorArguments);

            Assert.NotNull(actualResult);
            Assert.Equal(expectedResultType, actualResult.GetType());
        }

        public static TheoryData<Type, IEnumerable<object?>?> TestDataCreateNonGenericArray =>
            new TheoryData<Type, IEnumerable<object?>?>()
            {
                { typeof(PipelineBuilderAsync<Task>), null },
                { typeof(PipelineBuilderAsync<Task>), new object?[] { PipelineBuilderAsyncFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilderAsync<string, Task>), null },
                { typeof(PipelineBuilderAsync<string, Task>), new object?[] { PipelineBuilderAsyncFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilderAsync<string, int, Task>), null },
                { typeof(PipelineBuilderAsync<string, int, Task>), new object?[] { PipelineBuilderAsyncFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilderAsync<string, int, object, Task>), null },
                { typeof(PipelineBuilderAsync<string, int, object, Task>), new object?[] { PipelineBuilderAsyncFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilderAsync<string, int, object, string, Task>), null },
                { typeof(PipelineBuilderAsync<string, int, object, string, Task>), new object?[] { PipelineBuilderAsyncFactoryTests.ServiceProvider } }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsyncFactoryTests.TestDataCreateNonGenericArray))]
        public void Create_NonGeneric_Array_ReturnsCorrectResult(Type expectedResultType, object?[]? constructorArguments)
        {
            var sut = PipelineBuilderAsyncFactoryTests.CreateSut();

            var actualResult = sut.Create(expectedResultType, constructorArguments);

            Assert.NotNull(actualResult);
            Assert.Equal(expectedResultType, actualResult.GetType());
        }

        #endregion Create

        #region CreateSut

        private static IPipelineBuilderAsyncFactory CreateSut() => new PipelineBuilderAsyncFactory();

        #endregion CreateSut

        private class PipelineBuilderAsyncTest<TResult> : PipelineBuilderAsync<TResult> where TResult : Task
        {
            public PipelineBuilderAsyncTest(int valueInt, decimal valueDecimal) : base(null) { }
        }
    }
}
