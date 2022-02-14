using System;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilder1
{
    public class PipelineBuilder1WithResultTests
    {
        #region Properties

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        #region Args

        private static int Arg0 => 1;

        #endregion Args

        #region Configuration

        private static Func<Func<int, int>, Func<int, int>> ComponentForConfiguration =>
            next => param0 =>
            {
                var result = next.Invoke(param0);

                var temp = result - 1;

                return temp * temp;
            };

        private static Action<IPipelineBuilder<int, int>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilder1WithResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilder<int, int>> ConfigurationWithBranchTarget => builder =>
            builder.Use(PipelineBuilder1WithResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilder1WithResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<int, int> TargetMainResult =>
            param0 => param0;

        private static Func<int, int> TargetBranchResult =>
            param0 => param0 * 2;


        private static int TargetMain(int param0) =>
            PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0);

        private static int TargetBranch(int param0) =>
            PipelineBuilder1WithResultTests.TargetBranchResult.Invoke(param0);

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder1WithResultTests.CreateSut();

            Func<Func<int, int>, Func<int, int>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public void Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult(PipelineBuilder1WithResultTests.Arg0);

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<int, int>, Func<int, int>> component =
                next => param0 => next.Invoke(param0) + incrementValue;

            var sut = PipelineBuilder1WithResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Component

        #region StepInterface

        #region WithServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder1WithResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult(PipelineBuilder1WithResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #region Factory

        private static readonly Func<IServiceProvider, IPipelineStep<int, int>> ServiceProviderFactory =
            sp => sp.GetRequiredService<SquareStep>();

        [Fact]
        public void Use_Step_ServiceProvider_Factory_FactoryResultIsNull_ThrowsException()
        {
            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<int, int>?)null!));
        }

        [Fact]
        public void Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult(PipelineBuilder1WithResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilder1WithResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion WithServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder1WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilder1WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public void Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult(PipelineBuilder1WithResultTests.Arg0);

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilder1WithResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

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
            <Func<IPipelineBuilder<int, int>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilder<int, int>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<int, bool>)null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<int, bool>)null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<int, bool>)null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<int, bool>)null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineCondition<int>>)null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineCondition<int>>)null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        () => null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineCondition<int>)null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        () => null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineCondition<int>)null!,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, int>>)null!,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, int>>)null!,
                        _ => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<int, int>>)null!,
                        _ => new PipelineBuilder<int, int>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<int, int>>)null!,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, int>>)null!,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, int>>)null!,
                        _ => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<int, int>>)null!,
                        _ => new PipelineBuilder<int, int>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<int, int>>)null!,
                        () => new PipelineBuilder<int, int>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, int>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, int>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, int>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, int>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf(PipelineBuilder1WithResultTests.ConditionAsyncTrue, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf(PipelineBuilder1WithResultTests.ConditionAsyncTrue, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf(PipelineBuilder1WithResultTests.ConditionAsyncTrue, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf(PipelineBuilder1WithResultTests.ConditionAsyncTrue, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilder<int, int>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilder1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilder<int, int>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilder<int, int>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilder<int, int>, object> delegateToCall)
        {
            var sut = PipelineBuilder1WithResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<int, bool>, Func<int, int>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<int, bool>, Func<int, int>>()
            {
                {
                    PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                    param0 =>
                    {
                        var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilder1WithResultTests.ConditionAsyncFalse,
                    param0 => PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseIfAsyncConditions))]
        public void UseIf_Predicate_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseIfAsyncConditions))]
        public void UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var sut = PipelineBuilder1WithResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget, () => new PipelineBuilder<int, int>())
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseIfAsyncConditions))]
        public void UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilder<int, int>())
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition<int>>, Func<int, int>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition<int>>, Func<int, int>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    param0 =>
                    {
                        var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    param0 => PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public void UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition<int>> conditionFactory,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var sut = PipelineBuilder1WithResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilder<int, int>()
                )
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<int, int>> TestDataUseIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<int, int>>()
            {
                {
                    true,
                    param0 =>
                    {
                        var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    param0 => PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public void UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public void UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, int>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, int>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int>>,
                Func<int, int>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineCondition<int>>,
                Func<int, int>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    param0 =>
                    {
                        var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    param0 => PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public void UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int>> conditionFactory,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilder1WithResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public void UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int>> conditionFactory,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder1WithResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, int>()
                )
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<int, bool>, Func<int, int>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<int, bool>, Func<int, int>>()
            {
                {
                    PipelineBuilder1WithResultTests.ConditionAsyncTrue,
                    param0 =>
                    {
                        var targetBranchResult = PipelineBuilder1WithResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilder1WithResultTests.ConditionAsyncFalse,
                    param0 => PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public void UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public void UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var sut = PipelineBuilder1WithResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<int, int>()
                )
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseBranchIfAsyncConditions))]
        public void UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<int, bool> predicate,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int>()
                )
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition<int>>, Func<int, int>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition<int>>, Func<int, int>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    param0 =>
                    {
                        var targetBranchResult = PipelineBuilder1WithResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    param0 => PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public void UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition<int>> conditionFactory,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var sut = PipelineBuilder1WithResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<int, int>()
                )
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<int, int>> TestDataUseBranchIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<int, int>>()
            {
                {
                    true,
                    param0 =>
                    {
                        var targetBranchResult = PipelineBuilder1WithResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    param0 => PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public void UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public void UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int>>,
                Func<int, int>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int>>,
                Func<int, int>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    param0 =>
                    {
                        var targetBranchResult = PipelineBuilder1WithResultTests.TargetBranchResult.Invoke(param0);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    param0 => PipelineBuilder1WithResultTests.TargetMainResult.Invoke(param0)
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public void UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int>> conditionFactory,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public void UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int>> conditionFactory,
            Func<int, int> expectedResultDelegate
        )
        {
            var expectedResult = expectedResultDelegate.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder1WithResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int>(serviceProvider)
                )
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

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
            var sut = PipelineBuilder1WithResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public void UseTarget_SetsTarget()
        {
            var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult(PipelineBuilder1WithResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder1WithResultTests.CreateSut()
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

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
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataCopyTargetSetOrder))]
        public void Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult(PipelineBuilder1WithResultTests.Arg0);

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder1WithResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder1WithResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilder1WithResultTests.TargetMain);
            }

            var sourcePipeline = sourcePipelineBuilder.Build();

            var actualResultSourcePipeline = sourcePipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            var pipelineCopy = pipelineBuilderCopy.Build();

            var actualResultPipelineCopy = pipelineCopy.Invoke(PipelineBuilder1WithResultTests.Arg0);

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
            var sut = PipelineBuilder1WithResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Build_BuildsPipeline()
        {
            var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult(PipelineBuilder1WithResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder1WithResultTests.CreateSut()
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = PipelineBuilder1WithResultTests.TargetMainResult(PipelineBuilder1WithResultTests.Arg0);

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder1WithResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilder1WithResultTests.TargetMain);

            var actualResult = pipeline.Invoke(PipelineBuilder1WithResultTests.Arg0);

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
        [MemberData(nameof(PipelineBuilder1WithResultTests.TestDataMultiple))]
        public void Multiple_AddsComponentsToPipeline(int arg0, int expectedResult)
        {
            var serviceProvider = PipelineBuilder1WithResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder1WithResultTests.CreateSut(serviceProvider)
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
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilder1WithResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder1WithResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = pipeline.Invoke(arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilder<int, int> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilder<int, int>(serviceProvider);

        #endregion CreateSut

        #endregion Methods

        #region Pipeline steps

        private class SquareStep : IPipelineStep<int, int>
        {
            public int Invoke(int param0, Func<int, int> next)
            {
                var nextResult = next.Invoke(param0);

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
