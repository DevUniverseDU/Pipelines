using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilderAsync1
{
    public class PipelineBuilderAsync1WithResultTests
    {
        #region Properties

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        #region Args

        private static int Arg0 => 1;

        #endregion Args

        #region Configuration

        private static Func<Func<int, Task<int>>, Func<int, Task<int>>> ComponentForConfiguration =>
            next => async param0 =>
            {
                var result = await next.Invoke(param0);

                var temp = result - 1;

                return temp * temp;
            };

        private static Action<IPipelineBuilderAsync<int, Task<int>>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilderAsync1WithResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilderAsync<int, Task<int>>> ConfigurationWithBranchTarget => builder =>
            builder.Use(PipelineBuilderAsync1WithResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<int, Task<int>> TargetMainResult =>
            param0 => Task.FromResult(param0);

        private static Func<int, Task<int>> TargetBranchResult =>
            param0 => Task.FromResult(param0 * 2);


        private static Task<int> TargetMain(int param0) =>
            PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0);

        private static Task<int> TargetBranch(int param0) =>
            PipelineBuilderAsync1WithResultTests.TargetBranchResult.Invoke(param0);

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync1WithResultTests.CreateSut();

            Func<Func<int, Task<int>>, Func<int, Task<int>>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult(PipelineBuilderAsync1WithResultTests.Arg0);

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<int, Task<int>>, Func<int, Task<int>>> component =
                next => async param0 => await next.Invoke(param0) + incrementValue;

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Component

        #region StepInterface

        #region WithServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync1WithResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult(PipelineBuilderAsync1WithResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #region Factory

        private static readonly Func<IServiceProvider, IPipelineStep<int, Task<int>>> ServiceProviderFactory =
            sp => sp.GetRequiredService<SquareStep>();

        [Fact]
        public void Use_Step_ServiceProvider_Factory_FactoryResultIsNull_ThrowsException()
        {
            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<int, Task<int>>?)null!));
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult(PipelineBuilderAsync1WithResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilderAsync1WithResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion WithServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync1WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync1WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public async Task Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult(PipelineBuilderAsync1WithResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion StepInterface

        #endregion Use

        #region Conditions

        private static Func<int, Task<bool>> ConditionAsyncTrue => _ => Task.FromResult(true);
        private static Func<int, Task<bool>> ConditionAsyncFalse => _ => Task.FromResult(false);

        #region Params check

        public static TheoryData
            <Func<IPipelineBuilderAsync<int, Task<int>>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilderAsync<int, Task<int>>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<int, Task<bool>>)null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<int, Task<bool>>)null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<int, Task<bool>>)null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<int, Task<bool>>)null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineConditionAsync<int>>)null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineConditionAsync<int>>)null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        () => null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineConditionAsync<int>)null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        () => null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineConditionAsync<int>)null!,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<int, Task<int>>>)null!,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<int, Task<int>>>)null!,
                        _ => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilderAsync<int, Task<int>>>)null!,
                        _ => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilderAsync<int, Task<int>>>)null!,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<int, Task<int>>>)null!,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<int, Task<int>>>)null!,
                        _ => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilderAsync<int, Task<int>>>)null!,
                        _ => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilderAsync<int, Task<int>>>)null!,
                        () => new PipelineBuilderAsync<int, Task<int>>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<int, Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<int, Task<int>>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<int, Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<int, Task<int>>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf(PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf(PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf
                        (PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf
                        (PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilderAsync<int, Task<int>>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilderAsync<int, Task<int>>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilderAsync<int, Task<int>>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilderAsync<int, Task<int>>, object> delegateToCall)
        {
            var sut = PipelineBuilderAsync1WithResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<int, Task<bool>>, Func<int, Task<int>>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<int, Task<bool>>, Func<int, Task<int>>>()
            {
                {
                    PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                    async param0 =>
                    {
                        var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilderAsync1WithResultTests.ConditionAsyncFalse,
                    param0 => PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_AddsComponentToPipeline
        (
            Func<int, Task<bool>> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<int, Task<bool>> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget, () => new PipelineBuilderAsync<int, Task<int>>())
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<int, Task<bool>> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilderAsync<int, Task<int>>())
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineConditionAsync<int>>, Func<int, Task<int>>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineConditionAsync<int>>, Func<int, Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async param0 =>
                    {
                        var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    param0 => PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public async Task UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineConditionAsync<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilderAsync<int, Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<int, Task<int>>> TestDataUseIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<int, Task<int>>>()
            {
                {
                    true,
                    async param0 =>
                    {
                        var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    param0 => PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<int>>,
                Func<int, Task<int>>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineConditionAsync<int>>,
                Func<int, Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async param0 =>
                    {
                        var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    param0 => PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<int, Task<bool>>, Func<int, Task<int>>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<int, Task<bool>>, Func<int, Task<int>>>()
            {
                {
                    PipelineBuilderAsync1WithResultTests.ConditionAsyncTrue,
                    async param0 =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync1WithResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilderAsync1WithResultTests.ConditionAsyncFalse,
                    param0 => PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<int, Task<bool>> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<int, Task<bool>> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilderAsync<int, Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<int, Task<bool>> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineConditionAsync<int>>, Func<int, Task<int>>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineConditionAsync<int>>, Func<int, Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async param0 =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync1WithResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    param0 => PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public async Task UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineConditionAsync<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilderAsync<int, Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<int, Task<int>>> TestDataUseBranchIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<int, Task<int>>>()
            {
                {
                    true,
                    async param0 =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync1WithResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    param0 => PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<int>>,
                Func<int, Task<int>>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<int>>,
                Func<int, Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async param0 =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync1WithResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    param0 => PipelineBuilderAsync1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilderAsync1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<int, Task<int>>(serviceProvider)
                )
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseBranchIf

        #endregion Conditions

        #region UseTarget

        [Fact]
        public void UseTarget_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync1WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public async Task UseTarget_SetsTarget()
        {
            var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult(PipelineBuilderAsync1WithResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut()
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion UseTarget

        #region Copy

        public static TheoryData<bool> TestDataCopyTargetSetOrder => new TheoryData<bool>()
        {
            { true },
            { false }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataCopyTargetSetOrder))]
        public async Task Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult(PipelineBuilderAsync1WithResultTests.Arg0);

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);
            }

            var sourcePipeline = sourcePipelineBuilder.Build();

            var actualResultSourcePipeline = await sourcePipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            var pipelineCopy = pipelineBuilderCopy.Build();

            var actualResultPipelineCopy = await pipelineCopy.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(pipelineBuilderCopy.ServiceProvider, sourcePipelineBuilder.ServiceProvider);
            Assert.True(Object.ReferenceEquals(pipelineBuilderCopy.ServiceProvider, sourcePipelineBuilder.ServiceProvider));

            Assert.Equal(pipelineBuilderCopy.Target, sourcePipelineBuilder.Target);
            Assert.False(Object.ReferenceEquals(pipelineBuilderCopy.Target, sourcePipelineBuilder.Target));

            Assert.Equal(expectedResultSourcePipeline, actualResultSourcePipeline);
            Assert.Equal(expectedResultPipelineCopy, actualResultPipelineCopy);
        }

        #endregion Copy

        #region Build

        [Fact]
        public void Build_Target_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync1WithResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult(PipelineBuilderAsync1WithResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut()
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync1WithResultTests.TargetMainResult(PipelineBuilderAsync1WithResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilderAsync1WithResultTests.TargetMain);

            var actualResult = await pipeline.Invoke(PipelineBuilderAsync1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region Multiple steps

        public static TheoryData<int, int> TestDataMultiple => new TheoryData<int, int>()
        {
            { 5, 1679616 },
            { -5, 4096 }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync1WithResultTests.TestDataMultiple))]
        public async Task Multiple_AddsComponentsToPipeline(int arg0, int expectedResult)
        {
            var serviceProvider = PipelineBuilderAsync1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync1WithResultTests.CreateSut(serviceProvider)
                .Use(next => param0 => next.Invoke(param0 + 1))
                .UseIf
                (
                    param0 => Task.FromResult(param0 > 0),
                    builder => builder.Use(() => new SquareStep())
                )
                .UseIf<PipelineConditionTrue>(null, builder => builder.Use(() => new SquareStep()))
                .UseBranchIf
                (
                    param0 => Task.FromResult(param0 < 0),
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilderAsync1WithResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilderAsync1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilderAsync<int, Task<int>> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilderAsync<int, Task<int>>(serviceProvider);

        #endregion CreateSut

        #endregion Methods

        #region Pipeline steps

        private class SquareStep : IPipelineStep<int, Task<int>>
        {
            public async Task<int> Invoke(int param0, Func<int, Task<int>> next)
            {
                var nextResult = await next.Invoke(param0);

                return nextResult * nextResult;
            }
        }

        #endregion Pipeline steps

        #region Pipeline condition

        private class PipelineConditionTrue : IPipelineConditionAsync<int>
        {
            public Task<bool> InvokeAsync(int param0) => Task.FromResult(true);
        }

        private class PipelineConditionFalse : IPipelineConditionAsync<int>
        {
            public Task<bool> InvokeAsync(int param0) => Task.FromResult(false);
        }

        #endregion Pipeline condition
    }
}
