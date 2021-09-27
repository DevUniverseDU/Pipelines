using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilderAsync0
{
    public class PipelineBuilderAsync0WithResultTests
    {
        #region Properties

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        #region Configuration

        private static Func<Func<Task<int>>, Func<Task<int>>> ComponentForConfiguration =>
            next => async () =>
            {
                var result = await next.Invoke();

                var temp = result - 1;

                return temp * temp;
            };

        private static Action<IPipelineBuilderAsync<Task<int>>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilderAsync0WithResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilderAsync<Task<int>>> ConfigurationWithBranchTarget => builder =>
            builder.Use(PipelineBuilderAsync0WithResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<Task<int>> TargetMainResult =>
            () => Task.FromResult(5);

        private static Func<Task<int>> TargetBranchResult =>
            () => Task.FromResult(3);


        private static Task<int> TargetMain() =>
            PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke();

        private static Task<int> TargetBranch() =>
            PipelineBuilderAsync0WithResultTests.TargetBranchResult.Invoke();

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync0WithResultTests.CreateSut();

            Func<Func<Task<int>>, Func<Task<int>>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<Task<int>>, Func<Task<int>>> component =
                next => async () => await next.Invoke() + incrementValue;

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Component

        #region StepInterface

        #region WithServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync0WithResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #region Factory

        private static readonly Func<IServiceProvider, IPipelineStep<Task<int>>> ServiceProviderFactory =
            sp => sp.GetRequiredService<SquareStep>();

        [Fact]
        public void Use_Step_ServiceProvider_Factory_FactoryResultIsNull_ThrowsException()
        {
            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<Task<int>>?)null!));
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilderAsync0WithResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion WithServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync0WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync0WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public async Task Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion StepInterface

        #endregion Use

        #region Conditions

        private static Func<Task<bool>> ConditionAsyncTrue => () => Task.FromResult(true);
        private static Func<Task<bool>> ConditionAsyncFalse => () => Task.FromResult(false);

        #region Params check

        public static TheoryData
            <Func<IPipelineBuilderAsync<Task<int>>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilderAsync<Task<int>>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<Task<bool>>)null!,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<Task<bool>>)null!,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<Task<bool>>)null!,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<Task<bool>>)null!,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineConditionAsync>)null!,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineConditionAsync>)null!,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineConditionAsync>)(() => null!),
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineConditionAsync)null!,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineConditionAsync>)(() => null!),
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineConditionAsync)null!,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Task<int>>>)null!,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Task<int>>>)null!,
                        _ => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilderAsync<Task<int>>>)null!,
                        _ => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilderAsync<Task<int>>>)null!,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Task<int>>>)null!,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Task<int>>>)null!,
                        _ => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilderAsync<Task<int>>>)null!,
                        _ => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilderAsync<Task<int>>>)null!,
                        () => new PipelineBuilderAsync<Task<int>>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Task<int>>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Task<int>>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf(PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf(PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf
                        (PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf
                        (PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilderAsync<Task<int>>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilderAsync<Task<int>>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilderAsync<Task<int>>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilderAsync<Task<int>>, object> delegateToCall)
        {
            var sut = PipelineBuilderAsync0WithResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<Task<bool>>, Func<Task<int>>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<Task<bool>>, Func<Task<int>>>()
            {
                {
                    PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                    async () =>
                    {
                        var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilderAsync0WithResultTests.ConditionAsyncFalse,
                    () => PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_AddsComponentToPipeline
        (
            Func<Task<bool>> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<Task<bool>> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget, () => new PipelineBuilderAsync<Task<int>>())
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<Task<bool>> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilderAsync<Task<int>>())
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineConditionAsync>, Func<Task<int>>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineConditionAsync>, Func<Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async () =>
                    {
                        var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    () => PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public async Task UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineConditionAsync> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilderAsync<Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<Task<int>>> TestDataUseIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<Task<int>>>()
            {
                {
                    true,
                    async () =>
                    {
                        var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    () => PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke()
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync>,
                Func<Task<int>>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineConditionAsync>,
                Func<Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async () =>
                    {
                        var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    () => PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke()
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<Task<bool>>, Func<Task<int>>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<Task<bool>>, Func<Task<int>>>()
            {
                {
                    PipelineBuilderAsync0WithResultTests.ConditionAsyncTrue,
                    async () =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync0WithResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilderAsync0WithResultTests.ConditionAsyncFalse,
                    () => PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<Task<bool>> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<Task<bool>> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilderAsync<Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<Task<bool>> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineConditionAsync>, Func<Task<int>>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineConditionAsync>, Func<Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async () =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync0WithResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    () => PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public async Task UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineConditionAsync> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilderAsync<Task<int>>()
                )
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<Task<int>>> TestDataUseBranchIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<Task<int>>>()
            {
                {
                    true,
                    async () =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync0WithResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    () => PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke()
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync>,
                Func<Task<int>>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync>,
                Func<Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async () =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync0WithResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    () => PipelineBuilderAsync0WithResultTests.TargetMainResult.Invoke()
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilderAsync0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Task<int>>(serviceProvider)
                )
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

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
            var sut = PipelineBuilderAsync0WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public async Task UseTarget_SetsTarget()
        {
            var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut()
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

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
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataCopyTargetSetOrder))]
        public async Task Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult();

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);
            }

            var sourcePipeline = sourcePipelineBuilder.Build();

            var actualResultSourcePipeline = await sourcePipeline.Invoke();

            var pipelineCopy = pipelineBuilderCopy.Build();

            var actualResultPipelineCopy = await pipelineCopy.Invoke();

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
            var sut = PipelineBuilderAsync0WithResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut()
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilderAsync0WithResultTests.TargetMain);

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region Multiple steps

        public static TheoryData<int> TestDataMultiple => new TheoryData<int>()
        {
            { 390626 }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync0WithResultTests.TestDataMultiple))]
        public async Task Multiple_AddsComponentsToPipeline(int expectedResult)
        {
            var serviceProvider = PipelineBuilderAsync0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync0WithResultTests.CreateSut(serviceProvider)
                .Use(next => async () => await next.Invoke() + 1)
                .UseIf
                (
                    () => Task.FromResult(true),
                    builder => builder.Use(() => new SquareStep())
                )
                .UseIf<PipelineConditionTrue>(null, builder => builder.Use(() => new SquareStep()))
                .UseBranchIf
                (
                    () => Task.FromResult(false),
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilderAsync0WithResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilderAsync0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilderAsync<Task<int>> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilderAsync<Task<int>>(serviceProvider);

        #endregion CreateSut

        #endregion Methods

        #region Pipeline steps

        private class SquareStep : IPipelineStep<Task<int>>
        {
            public async Task<int> Invoke(Func<Task<int>> next)
            {
                var nextResult = await next.Invoke();

                return nextResult * nextResult;
            }
        }

        #endregion Pipeline steps

        #region Pipeline condition

        private class PipelineConditionTrue : IPipelineConditionAsync
        {
            public Task<bool> InvokeAsync() => Task.FromResult(true);
        }

        private class PipelineConditionFalse : IPipelineConditionAsync
        {
            public Task<bool> InvokeAsync() => Task.FromResult(false);
        }

        #endregion Pipeline condition
    }
}
