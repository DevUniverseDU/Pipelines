using System;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilder0
{
    public class PipelineBuilder0WithResultTests
    {
        #region Properties

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        #region Configuration

        private static Func<Func<int>, Func<int>> ComponentForConfiguration =>
            next => () =>
            {
                var result = next.Invoke();

                var temp = result - 1;

                return temp * temp;
            };

        private static Action<IPipelineBuilder<int>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilder0WithResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilder<int>> ConfigurationWithBranchTarget => builder =>
            builder.Use(PipelineBuilder0WithResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilder0WithResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<int> TargetMainResult =>
            () => 5;

        private static Func<int> TargetBranchResult =>
            () => 3;


        private static int TargetMain() =>
            PipelineBuilder0WithResultTests.TargetMainResult.Invoke();

        private static int TargetBranch() =>
            PipelineBuilder0WithResultTests.TargetBranchResult.Invoke();

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder0WithResultTests.CreateSut();

            Func<Func<int>, Func<int>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public void Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<int>, Func<int>> component =
                next => () => next.Invoke() + incrementValue;

            var sut = PipelineBuilder0WithResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Component

        #region StepInterface

        #region WithServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder0WithResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #region Factory

        private static readonly Func<IServiceProvider, IPipelineStep<int>> ServiceProviderFactory =
            sp => sp.GetRequiredService<SquareStep>();

        [Fact]
        public void Use_Step_ServiceProvider_Factory_FactoryResultIsNull_ThrowsException()
        {
            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<int>?)null!));
        }

        [Fact]
        public void Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilder0WithResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion WithServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder0WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilder0WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public void Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilder0WithResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

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
            <Func<IPipelineBuilder<int>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilder<int>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<bool>)null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<bool>)null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<bool>)null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<bool>)null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineCondition>)null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineCondition>)null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        () => null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineCondition)null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        () => null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineCondition)null!,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int>>)null!,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int>>)null!,
                        _ => new PipelineBuilder<int>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<int>>)null!,
                        _ => new PipelineBuilder<int>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<int>>)null!,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int>>)null!,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int>>)null!,
                        _ => new PipelineBuilder<int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<int>>)null!,
                        _ => new PipelineBuilder<int>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<int>>)null!,
                        () => new PipelineBuilder<int>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf(PipelineBuilder0WithResultTests.ConditionAsyncTrue, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf(PipelineBuilder0WithResultTests.ConditionAsyncTrue, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf(PipelineBuilder0WithResultTests.ConditionAsyncTrue, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf(PipelineBuilder0WithResultTests.ConditionAsyncTrue, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilder<int>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilder0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilder<int>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilder<int>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilder<int>, object> delegateToCall)
        {
            var sut = PipelineBuilder0WithResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<bool>, Func<int>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<bool>, Func<int>>()
            {
                {
                    PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                    () =>
                    {
                        var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilder0WithResultTests.ConditionAsyncFalse,
                    () => PipelineBuilder0WithResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseIfAsyncConditions))]
        public void UseIf_Predicate_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseIfAsyncConditions))]
        public void UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var sut = PipelineBuilder0WithResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget, () => new PipelineBuilder<int>())
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseIfAsyncConditions))]
        public void UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilder<int>())
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition>, Func<int>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition>, Func<int>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    () =>
                    {
                        var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    () => PipelineBuilder0WithResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public void UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition> conditionFactory,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var sut = PipelineBuilder0WithResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilder<int>()
                )
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<int>> TestDataUseIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<int>>()
            {
                {
                    true,
                    () =>
                    {
                        var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    () => PipelineBuilder0WithResultTests.TargetMainResult.Invoke()
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public void UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public void UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition>,
                Func<int>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineCondition>,
                Func<int>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    () =>
                    {
                        var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult.Invoke();

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    () => PipelineBuilder0WithResultTests.TargetMainResult.Invoke()
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public void UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition> conditionFactory,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilder0WithResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public void UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition> conditionFactory,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder0WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int>()
                )
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<bool>, Func<int>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<bool>, Func<int>>()
            {
                {
                    PipelineBuilder0WithResultTests.ConditionAsyncTrue,
                    () =>
                    {
                        var targetBranchResult = PipelineBuilder0WithResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilder0WithResultTests.ConditionAsyncFalse,
                    () => PipelineBuilder0WithResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public void UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public void UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var sut = PipelineBuilder0WithResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<int>()
                )
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public void UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<bool> predicate,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int>()
                )
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition>, Func<int>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition>, Func<int>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    () =>
                    {
                        var targetBranchResult = PipelineBuilder0WithResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    () => PipelineBuilder0WithResultTests.TargetMainResult.Invoke()
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public void UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition> conditionFactory,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var sut = PipelineBuilder0WithResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<int>()
                )
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<int>> TestDataUseBranchIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<int>>()
            {
                {
                    true,
                    () =>
                    {
                        var targetBranchResult = PipelineBuilder0WithResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    () => PipelineBuilder0WithResultTests.TargetMainResult.Invoke()
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public void UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public void UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition>,
                Func<int>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineCondition>,
                Func<int>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    () =>
                    {
                        var targetBranchResult = PipelineBuilder0WithResultTests.TargetBranchResult.Invoke();

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    () => PipelineBuilder0WithResultTests.TargetMainResult.Invoke()
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public void UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition> conditionFactory,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public void UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition> conditionFactory,
            Func<int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke();

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder0WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int>(serviceProvider)
                )
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

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
            var sut = PipelineBuilder0WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public void UseTarget_SetsTarget()
        {
            var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder0WithResultTests.CreateSut()
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

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
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataCopyTargetSetOrder))]
        public void Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult();

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder0WithResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder0WithResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilder0WithResultTests.TargetMain);
            }

            var sourcePipeline = sourcePipelineBuilder.Build();

            var actualResultSourcePipeline = sourcePipeline.Invoke();

            var pipelineCopy = pipelineBuilderCopy.Build();

            var actualResultPipelineCopy = pipelineCopy.Invoke();

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
            var sut = PipelineBuilder0WithResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Build_BuildsPipeline()
        {
            var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder0WithResultTests.CreateSut()
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = PipelineBuilder0WithResultTests.TargetMainResult();

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder0WithResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilder0WithResultTests.TargetMain);

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region Multiple steps

        public static TheoryData<int> TestDataMultiple => new TheoryData<int>()
        {
            { 390626 }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilder0WithResultTests.TestDataMultiple))]
        public void Multiple_AddsComponentsToPipeline(int expectedResult)
        {
            var serviceProvider = PipelineBuilder0WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder0WithResultTests.CreateSut(serviceProvider)
                .Use(next => () => next.Invoke() + 1)
                .UseIf
                (
                    () => true,
                    builder => builder.Use(() => new SquareStep())
                )
                .UseIf<PipelineConditionTrue>(null, builder => builder.Use(() => new SquareStep()))
                .UseBranchIf
                (
                    () => false,
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilder0WithResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder0WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilder<int> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilder<int>(serviceProvider);

        #endregion CreateSut

        #endregion Methods

        #region Pipeline steps

        private class SquareStep : IPipelineStep<int>
        {
            public int Invoke(Func<int> next)
            {
                var nextResult = next.Invoke();

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
