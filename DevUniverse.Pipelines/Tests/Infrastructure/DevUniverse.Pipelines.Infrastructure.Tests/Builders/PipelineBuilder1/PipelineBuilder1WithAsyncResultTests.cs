using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilder1
{
    public class PipelineBuilder1WithAsyncResultTests
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

        private static Action<IPipelineBuilder<int, Task<int>>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilder1WithAsyncResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilder<int, Task<int>>> ConfigurationWithBranchTarget => builder =>
            builder.Use(PipelineBuilder1WithAsyncResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<int, Task<int>> TargetMainResult =>
            param0 => Task.FromResult(param0);

        private static Func<int, Task<int>> TargetBranchResult =>
            param0 => Task.FromResult(param0 * 2);


        private static Task<int> TargetMain(int param0) =>
            PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0);

        private static Task<int> TargetBranch(int param0) =>
            PipelineBuilder1WithAsyncResultTests.TargetBranchResult.Invoke(param0);

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut();

            Func<Func<int, Task<int>>, Func<int, Task<int>>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult(PipelineBuilder1WithAsyncResultTests.Arg0);

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<int, Task<int>>, Func<int, Task<int>>> component =
                next => async param0 => await next.Invoke(param0) + incrementValue;

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Component

        #region StepInterface

        #region WithServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult(PipelineBuilder1WithAsyncResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #region Factory

        private static readonly Func<IServiceProvider, IPipelineStep<int, Task<int>>> ServiceProviderFactory =
            sp => sp.GetRequiredService<SquareStep>();

        [Fact]
        public void Use_Step_ServiceProvider_Factory_FactoryResultIsNull_ThrowsException()
        {
            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<int, Task<int>>?)null!));
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult(PipelineBuilder1WithAsyncResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilder1WithAsyncResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion WithServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public async Task Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult(PipelineBuilder1WithAsyncResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion StepInterface

        #endregion Use

        #region Conditions

        private static Func<int, bool> ConditionAsyncTrue => _ => true;
        private static Func<int, bool> ConditionAsyncFalse => _ => false;

        #region Params check

        public static TheoryData
            <Func<IPipelineBuilder<int, Task<int>>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilder<int, Task<int>>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<int, bool>)null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<int, bool>)null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<int, bool>)null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<int, bool>)null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineCondition<int>>)null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineCondition<int>>)null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        () => null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineCondition<int>)null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        () => null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineCondition<int>)null!,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, Task<int>>>)null!,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, Task<int>>>)null!,
                        _ => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<int, Task<int>>>)null!,
                        _ => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<int, Task<int>>>)null!,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, Task<int>>>)null!,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, Task<int>>>)null!,
                        _ => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<int, Task<int>>>)null!,
                        _ => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<int, Task<int>>>)null!,
                        () => new PipelineBuilder<int, Task<int>>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, Task<int>>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, Task<int>>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf(PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf(PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf
                        (PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf
                        (PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilder<int, Task<int>>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilder<int, Task<int>>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilder<int, Task<int>>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilder<int, Task<int>>, object> delegateToCall)
        {
            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<int, bool>, Func<int, Task<int>>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<int, bool>, Func<int, Task<int>>>()
            {
                {
                    PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                    async param0 =>
                    {
                        var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilder1WithAsyncResultTests.ConditionAsyncFalse,
                    param0 => PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget, () => new PipelineBuilder<int, Task<int>>())
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilder<int, Task<int>>())
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition<int>>, Func<int, Task<int>>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition<int>>, Func<int, Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async param0 =>
                    {
                        var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    param0 => PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public async Task UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilder<int, Task<int>>()
                )
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

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
                        var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    param0 => PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int>>,
                Func<int, Task<int>>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineCondition<int>>,
                Func<int, Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async param0 =>
                    {
                        var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    param0 => PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0)
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                )
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<int, bool>, Func<int, Task<int>>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<int, bool>, Func<int, Task<int>>>()
            {
                {
                    PipelineBuilder1WithAsyncResultTests.ConditionAsyncTrue,
                    async param0 =>
                    {
                        var targetBranchResult = await PipelineBuilder1WithAsyncResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilder1WithAsyncResultTests.ConditionAsyncFalse,
                    param0 => PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<int, Task<int>>()
                )
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                )
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition<int>>, Func<int, Task<int>>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition<int>>, Func<int, Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async param0 =>
                    {
                        var targetBranchResult = await PipelineBuilder1WithAsyncResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    param0 => PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public async Task UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<int, Task<int>>()
                )
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

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
                        var targetBranchResult = await PipelineBuilder1WithAsyncResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    param0 => PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int>>,
                Func<int, Task<int>>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int>>,
                Func<int, Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async param0 =>
                    {
                        var targetBranchResult = await PipelineBuilder1WithAsyncResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    param0 => PipelineBuilder1WithAsyncResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int>> conditionFactory,
            Func<int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder1WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, Task<int>>(serviceProvider)
                )
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

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
            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public async Task UseTarget_SetsTarget()
        {
            var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult(PipelineBuilder1WithAsyncResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut()
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

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
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataCopyTargetSetOrder))]
        public async Task Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult(PipelineBuilder1WithAsyncResultTests.Arg0);

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);
            }

            var sourcePipeline = sourcePipelineBuilder.Build();

            var actualResultSourcePipeline = await sourcePipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            var pipelineCopy = pipelineBuilderCopy.Build();

            var actualResultPipelineCopy = await pipelineCopy.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

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
            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult(PipelineBuilder1WithAsyncResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut()
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilder1WithAsyncResultTests.TargetMainResult(PipelineBuilder1WithAsyncResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var actualResult = await pipeline.Invoke(PipelineBuilder1WithAsyncResultTests.Arg0);

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
        [MemberData(nameof(PipelineBuilder1WithAsyncResultTests.TestDataMultiple))]
        public async Task Multiple_AddsComponentsToPipeline(int arg0, int expectedResult)
        {
            var serviceProvider = PipelineBuilder1WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithAsyncResultTests.CreateSut(serviceProvider)
                .Use(next => param0 => next.Invoke(param0 + 1))
                .UseIf
                (
                    param0 => param0 > 0,
                    builder => builder.Use(() => new SquareStep())
                )
                .UseIf<PipelineConditionTrue>(null, builder => builder.Use(() => new SquareStep()))
                .UseBranchIf
                (
                    param0 => param0 < 0,
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilder1WithAsyncResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder1WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilder<int, Task<int>> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilder<int, Task<int>>(serviceProvider);

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

        private class PipelineConditionTrue : IPipelineCondition<int>
        {
            public bool Invoke(int param0) => true;
        }

        private class PipelineConditionFalse : IPipelineCondition<int>
        {
            public bool Invoke(int param0) => false;
        }

        #endregion Pipeline condition
    }
}
