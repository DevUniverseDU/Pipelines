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
    public class PipelineBuilderFactoryTests
    {
        public static IServiceProvider ServiceProvider => new ServiceCollection().BuildServiceProvider();

        #region Create

        public static TheoryData<Func<object>, Type> TestDataCreateGeneric =>
            new TheoryData<Func<object>, Type>()
            {
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<Task>(),
                    typeof(PipelineBuilder<Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilder<Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<Task>((object?[]?)null),
                    typeof(PipelineBuilder<Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<Task>(new List<object?>() { null }),
                    typeof(PipelineBuilder<Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<Task>(new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilder<Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<Task>(PipelineBuilderFactoryTests.ServiceProvider),
                    typeof(PipelineBuilder<Task>)
                },

                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, Task>(),
                    typeof(PipelineBuilder<string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilder<string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, Task>((object?[]?)null),
                    typeof(PipelineBuilder<string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, Task>(new List<object?>() { null }),
                    typeof(PipelineBuilder<string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, Task>(new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilder<string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, Task>(PipelineBuilderFactoryTests.ServiceProvider),
                    typeof(PipelineBuilder<string, Task>)
                },

                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, Task>(),
                    typeof(PipelineBuilder<string, int, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilder<string, int, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, Task>((object?[]?)null),
                    typeof(PipelineBuilder<string, int, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, Task>(new List<object?>() { null }),
                    typeof(PipelineBuilder<string, int, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, Task>(new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilder<string, int, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, Task>(PipelineBuilderFactoryTests.ServiceProvider),
                    typeof(PipelineBuilder<string, int, Task>)
                },

                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, Task>(),
                    typeof(PipelineBuilder<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilder<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, Task>((object?[]?)null),
                    typeof(PipelineBuilder<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, Task>(new List<object?>() { null }),
                    typeof(PipelineBuilder<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, Task>(new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilder<string, int, object, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, Task>(PipelineBuilderFactoryTests.ServiceProvider),
                    typeof(PipelineBuilder<string, int, object, Task>)
                },

                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, string, Task>(),
                    typeof(PipelineBuilder<string, int, object, string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, string, Task>((IEnumerable<object?>?)null),
                    typeof(PipelineBuilder<string, int, object, string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, string, Task>((object?[]?)null),
                    typeof(PipelineBuilder<string, int, object, string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, string, Task>(new List<object?>() { null }),
                    typeof(PipelineBuilder<string, int, object, string, Task>)
                },
                {
                    () =>
                        PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, string, Task>(new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider }),
                    typeof(PipelineBuilder<string, int, object, string, Task>)
                },
                {
                    () => PipelineBuilderFactoryTests.CreateSut().Create<string, int, object, string, Task>(PipelineBuilderFactoryTests.ServiceProvider),
                    typeof(PipelineBuilder<string, int, object, string, Task>)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderFactoryTests.TestDataCreateGeneric))]
        public void Create_Generic_ReturnsCorrectResult(Func<object> creatingDelegate, Type expectedResultType)
        {
            var actualResult = creatingDelegate.Invoke();

            Assert.NotNull(actualResult);
            Assert.Equal(expectedResultType, actualResult.GetType());
        }


        [Fact]
        public void Create_ArgumentIsNull_ThrowsException()
        {
            var sut = PipelineBuilderFactoryTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Create(null!));
        }

        public static TheoryData<Type, IEnumerable<object?>?> TestDataCreateNonGenericIEnumerable =>
            new TheoryData<Type, IEnumerable<object?>?>()
            {
                { typeof(PipelineBuilderTest<Task>), null },
                { typeof(PipelineBuilderTest<Task>), new List<object?>() { 5, 100M } },

                { typeof(PipelineBuilder<Task>), null },
                { typeof(PipelineBuilder<Task>), new List<object?>() { null } },
                { typeof(PipelineBuilder<Task>), new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilder<string, Task>), null },
                { typeof(PipelineBuilder<string, Task>), new List<object?>() { null } },
                { typeof(PipelineBuilder<string, Task>), new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilder<string, int, Task>), null },
                { typeof(PipelineBuilder<string, int, Task>), new List<object?>() { null } },
                { typeof(PipelineBuilder<string, int, Task>), new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilder<string, int, object, Task>), null },
                { typeof(PipelineBuilder<string, int, object, Task>), new List<object?>() { null } },
                { typeof(PipelineBuilder<string, int, object, Task>), new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilder<string, int, object, string, Task>), null },
                { typeof(PipelineBuilder<string, int, object, string, Task>), new List<object?>() { null } },
                { typeof(PipelineBuilder<string, int, object, string, Task>), new List<object?>() { PipelineBuilderFactoryTests.ServiceProvider } }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderFactoryTests.TestDataCreateNonGenericIEnumerable))]
        public void Create_NonGeneric_IEnumerable_ReturnsCorrectResult(Type expectedResultType, IEnumerable<object?>? constructorArguments)
        {
            var sut = PipelineBuilderFactoryTests.CreateSut();

            var actualResult = sut.Create(expectedResultType, constructorArguments);

            Assert.NotNull(actualResult);
            Assert.Equal(expectedResultType, actualResult.GetType());
        }

        public static TheoryData<Type, IEnumerable<object?>?> TestDataCreateNonGenericArray =>
            new TheoryData<Type, IEnumerable<object?>?>()
            {
                { typeof(PipelineBuilder<Task>), null },
                { typeof(PipelineBuilder<Task>), new object?[] { PipelineBuilderFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilder<string, Task>), null },
                { typeof(PipelineBuilder<string, Task>), new object?[] { PipelineBuilderFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilder<string, int, Task>), null },
                { typeof(PipelineBuilder<string, int, Task>), new object?[] { PipelineBuilderFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilder<string, int, object, Task>), null },
                { typeof(PipelineBuilder<string, int, object, Task>), new object?[] { PipelineBuilderFactoryTests.ServiceProvider } },

                { typeof(PipelineBuilder<string, int, object, string, Task>), null },
                { typeof(PipelineBuilder<string, int, object, string, Task>), new object?[] { PipelineBuilderFactoryTests.ServiceProvider } }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderFactoryTests.TestDataCreateNonGenericArray))]
        public void Create_NonGeneric_Array_ReturnsCorrectResult(Type expectedResultType, object?[]? constructorArguments)
        {
            var sut = PipelineBuilderFactoryTests.CreateSut();

            var actualResult = sut.Create(expectedResultType, constructorArguments);

            Assert.NotNull(actualResult);
            Assert.Equal(expectedResultType, actualResult.GetType());
        }

        #endregion Create

        #region CreateSut

        private static IPipelineBuilderFactory CreateSut() => new PipelineBuilderFactory();

        #endregion CreateSut

        private class PipelineBuilderTest<TResult> : PipelineBuilder<TResult>
        {
            public PipelineBuilderTest(int valueInt, decimal valueDecimal) : base(null) { }
        }
    }
}
