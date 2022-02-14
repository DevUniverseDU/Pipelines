using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilder0
{
    public class PipelineBuilder0WithAsyncResultTests
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

        private static Action<IPipelineBuilder<Task<int>>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilder0WithAsyncResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilder<Task<int>>> ConfigurationWithBranchTarget => builder =>
            builder.Use(PipelineBuilder0WithAsyncResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<Task<int>> TargetMainResult =>
            () => Task.FromResult(5);

        private static Func<Task<int>> TargetBranchResult =>
            () => Task.FromResult(3);


        private static Task<int> TargetMain() =>
            PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke();

        private static Task<int> TargetBranch() =>
            PipelineBuilder0WithAsyncResultTests.TargetBranchResult.Invoke();

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut();

            Func<Func<Task<int>>, Func<Task<int>>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult();

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<Task<int>>, Func<Task<int>>> component =
                next => async () => await next.Invoke() + incrementValue;

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

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
            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

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
            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<Task<int>>?)null!));
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilder0WithAsyncResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

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
            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public async Task Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion StepInterface

        #endregion Use

        #region Conditions

        private static Func<bool> ConditionAsyncTrue => () => true;
        private static Func<bool> ConditionAsyncFalse => () => false;

        #region Params check

        public static TheoryData
            <Func<IPipelineBuilder<Task<int>>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilder<Task<int>>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<bool>)null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<bool>)null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<bool>)null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<bool>)null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineCondition>)null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineCondition>)null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        () => null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineCondition)null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        () => null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineCondition)null!,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<Task<int>>>)null!,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<Task<int>>>)null!,
                        _ => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<Task<int>>>)null!,
                        _ => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<Task<int>>>)null!,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<Task<int>>>)null!,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<Task<int>>>)null!,
                        _ => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<Task<int>>>)null!,
                        _ => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<Task<int>>>)null!,
                        () => new PipelineBuilder<Task<int>>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<Task<int>>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<Task<int>>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf(PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf(PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf
                        (PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf
                        (PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilder<Task<int>>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilder<Task<int>>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilder<Task<int>>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<Task<int>>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<Task<int>>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<Task<int>>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<Task<int>>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilder<Task<int>>, object> delegateToCall)
        {
            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<bool>, Func<Task<int>>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<bool>, Func<Task<int>>>()
            {
                {
                    PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                    async () =>
                    {
                        var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilder0WithAsyncResultTests.ConditionAsyncFalse,
                    () => PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget, () => new PipelineBuilder<Task<int>>())
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilder<Task<int>>())
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition>, Func<Task<int>>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition>, Func<Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async () =>
                    {
                        var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    () => PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public async Task UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilder<Task<int>>()
                )
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

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
                        var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    () => PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke()
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<Task<int>>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition>,
                Func<Task<int>>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineCondition>,
                Func<Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async () =>
                    {
                        var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    () => PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke()
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<Task<int>>()
                )
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<bool>, Func<Task<int>>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<bool>, Func<Task<int>>>()
            {
                {
                    PipelineBuilder0WithAsyncResultTests.ConditionAsyncTrue,
                    async () =>
                    {
                        var targetBranchResult = await PipelineBuilder0WithAsyncResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilder0WithAsyncResultTests.ConditionAsyncFalse,
                    () => PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<Task<int>>()
                )
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<Task<int>>()
                )
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition>, Func<Task<int>>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition>, Func<Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async () =>
                    {
                        var targetBranchResult = await PipelineBuilder0WithAsyncResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    () => PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public async Task UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<Task<int>>()
                )
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

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
                        var targetBranchResult = await PipelineBuilder0WithAsyncResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    () => PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke()
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<Task<int>>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition>,
                Func<Task<int>>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineCondition>,
                Func<Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async () =>
                    {
                        var targetBranchResult = await PipelineBuilder0WithAsyncResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    () => PipelineBuilder0WithAsyncResultTests.TargetMainResult.Invoke()
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition> conditionFactory,
            Func<Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder0WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<Task<int>>(serviceProvider)
                )
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

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
            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public async Task UseTarget_SetsTarget()
        {
            var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut()
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

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
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataCopyTargetSetOrder))]
        public async Task Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult();

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);
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
            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut()
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilder0WithAsyncResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilder0WithAsyncResultTests.TargetMain);

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
        [MemberData(nameof(PipelineBuilder0WithAsyncResultTests.TestDataMultiple))]
        public async Task Multiple_AddsComponentsToPipeline(int expectedResult)
        {
            var serviceProvider = PipelineBuilder0WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithAsyncResultTests.CreateSut(serviceProvider)
                .Use(next => async () => await next.Invoke() + 1)
                .UseIf
                (
                    () => true,
                    builder => builder.Use(() => new SquareStep())
                )
                .UseIf<PipelineConditionTrue>(null, builder => builder.Use(() => new SquareStep()))
                .UseBranchIf
                (
                    () => false,
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilder0WithAsyncResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder0WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilder<Task<int>> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilder<Task<int>>(serviceProvider);

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

        private class PipelineConditionTrue : IPipelineCondition
        {
            public bool Invoke() => true;
        }

        private class PipelineConditionFalse : IPipelineCondition
        {
            public bool Invoke() => false;
        }

        #endregion Pipeline condition
    }
}
