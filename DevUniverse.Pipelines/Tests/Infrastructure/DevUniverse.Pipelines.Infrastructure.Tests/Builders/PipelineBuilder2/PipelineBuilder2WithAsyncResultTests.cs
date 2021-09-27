using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilder2
{
    public class PipelineBuilder2WithAsyncResultTests
    {
        #region Properties

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        #region Args

        private static int Arg0 => 2;
        private static int Arg1 => 20;

        #endregion Args

        #region Configuration

        private static Func<Func<int, int, Task<int>>, Func<int, int, Task<int>>> ComponentForConfiguration =>
            next => async (param0, param1) =>
            {
                var result = await next.Invoke(param0, param1);

                var temp = result - 1;

                return temp * temp;
            };

        private static Action<IPipelineBuilder<int, int, Task<int>>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilder2WithAsyncResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilder<int, int, Task<int>>> ConfigurationWithBranchTarget => builder =>
            builder.Use(PipelineBuilder2WithAsyncResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<int, int, Task<int>> TargetMainResult =>
            (param0, param1) => Task.FromResult(param1 - param0);

        private static Func<int, int, Task<int>> TargetBranchResult =>
            (param0, param1) => Task.FromResult((param1 + param0) * 2);


        private static Task<int> TargetMain(int param0, int param1) =>
            PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1);

        private static Task<int> TargetBranch(int param0, int param1) =>
            PipelineBuilder2WithAsyncResultTests.TargetBranchResult.Invoke(param0, param1);

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut();

            Func<Func<int, int, Task<int>>, Func<int, int, Task<int>>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<int, int, Task<int>>, Func<int, int, Task<int>>> component =
                next => async (param0, param1) => await next.Invoke(param0, param1) + incrementValue;

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Component

        #region StepInterface

        #region WithServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #region Factory

        private static readonly Func<IServiceProvider, IPipelineStep<int, int, Task<int>>> ServiceProviderFactory =
            sp => sp.GetRequiredService<SquareStep>();

        [Fact]
        public void Use_Step_ServiceProvider_Factory_FactoryResultIsNull_ThrowsException()
        {
            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<int, int, Task<int>>?)null!));
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilder2WithAsyncResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion WithServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public async Task Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion StepInterface

        #endregion Use

        #region Conditions

        private static Func<int, int, bool> ConditionAsyncTrue => (_, _) => true;
        private static Func<int, int, bool> ConditionAsyncFalse => (_, _) => false;

        #region Params check

        public static TheoryData
            <Func<IPipelineBuilder<int, int, Task<int>>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilder<int, int, Task<int>>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<int, int, bool>)null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<int, int, bool>)null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<int, int, bool>)null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<int, int, bool>)null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineCondition<int, int>>)null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineCondition<int, int>>)null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        () => null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineCondition<int, int>)null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        () => null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineCondition<int, int>)null!,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, int, Task<int>>>)null!,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, int, Task<int>>>)null!,
                        _ => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<int, int, Task<int>>>)null!,
                        _ => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<int, int, Task<int>>>)null!,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, int, Task<int>>>)null!,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilder<int, int, Task<int>>>)null!,
                        _ => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilder<int, int, Task<int>>>)null!,
                        _ => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilder<int, int, Task<int>>>)null!,
                        () => new PipelineBuilder<int, int, Task<int>>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, int, Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, int, Task<int>>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, int, Task<int>>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilder<int, int, Task<int>>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf(PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf(PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf
                        (PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf
                        (PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilder<int, int, Task<int>>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilder<int, int, Task<int>>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilder<int, int, Task<int>>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilder<int, int, Task<int>>, object> delegateToCall)
        {
            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<int, int, bool>, Func<int, int, Task<int>>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<int, int, bool>, Func<int, int, Task<int>>>()
            {
                {
                    PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                    async (param0, param1) =>
                    {
                        var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilder2WithAsyncResultTests.ConditionAsyncFalse,
                    (param0, param1) => PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_AddsComponentToPipeline
        (
            Func<int, int, bool> predicate,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<int, int, bool> predicate,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget, () => new PipelineBuilder<int, int, Task<int>>())
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<int, int, bool> predicate,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilder<int, int, Task<int>>())
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition<int, int>>, Func<int, int, Task<int>>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition<int, int>>, Func<int, int, Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async (param0, param1) =>
                    {
                        var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    (param0, param1) => PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public async Task UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition<int, int>> conditionFactory,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilder<int, int, Task<int>>()
                )
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<int, int, Task<int>>> TestDataUseIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<int, int, Task<int>>>()
            {
                {
                    true,
                    async (param0, param1) =>
                    {
                        var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    (param0, param1) => PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int, int>>,
                Func<int, int, Task<int>>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineCondition<int, int>>,
                Func<int, int, Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async (param0, param1) =>
                    {
                        var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    (param0, param1) => PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int, int>> conditionFactory,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int, int>> conditionFactory,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                )
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<int, int, bool>, Func<int, int, Task<int>>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<int, int, bool>, Func<int, int, Task<int>>>()
            {
                {
                    PipelineBuilder2WithAsyncResultTests.ConditionAsyncTrue,
                    async (param0, param1) =>
                    {
                        var targetBranchResult = await PipelineBuilder2WithAsyncResultTests.TargetBranchResult.Invoke(param0, param1);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilder2WithAsyncResultTests.ConditionAsyncFalse,
                    (param0, param1) => PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<int, int, bool> predicate,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<int, int, bool> predicate,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<int, int, Task<int>>()
                )
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<int, int, bool> predicate,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                )
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineCondition<int, int>>, Func<int, int, Task<int>>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineCondition<int, int>>, Func<int, int, Task<int>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async (param0, param1) =>
                    {
                        var targetBranchResult = await PipelineBuilder2WithAsyncResultTests.TargetBranchResult.Invoke(param0, param1);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    (param0, param1) => PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public async Task UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineCondition<int, int>> conditionFactory,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilder<int, int, Task<int>>()
                )
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<int, int, Task<int>>> TestDataUseBranchIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<int, int, Task<int>>>()
            {
                {
                    true,
                    async (param0, param1) =>
                    {
                        var targetBranchResult = await PipelineBuilder2WithAsyncResultTests.TargetBranchResult.Invoke(param0, param1);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    (param0, param1) => PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>()
                );
            }

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int, int>>,
                Func<int, int, Task<int>>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineCondition<int, int>>,
                Func<int, int, Task<int>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async (param0, param1) =>
                    {
                        var targetBranchResult = await PipelineBuilder2WithAsyncResultTests.TargetBranchResult.Invoke(param0, param1);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    (param0, param1) => PipelineBuilder2WithAsyncResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int, int>> conditionFactory,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineCondition<int, int>> conditionFactory,
            Func<int, int, Task<int>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilder2WithAsyncResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilder<int, int, Task<int>>(serviceProvider)
                )
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

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
            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public async Task UseTarget_SetsTarget()
        {
            var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut()
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

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
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataCopyTargetSetOrder))]
        public async Task Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);
            }

            var sourcePipeline = sourcePipelineBuilder.Build();

            var actualResultSourcePipeline = await sourcePipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var pipelineCopy = pipelineBuilderCopy.Build();

            var actualResultPipelineCopy = await pipelineCopy.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

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
            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut()
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilder2WithAsyncResultTests.TargetMainResult
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var actualResult = await pipeline.Invoke
            (
                PipelineBuilder2WithAsyncResultTests.Arg0,
                PipelineBuilder2WithAsyncResultTests.Arg1
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region Multiple steps

        public static TheoryData<int, int, int> TestDataMultiple => new TheoryData<int, int, int>()
        {
            { 5, 3, 256 },
            { -5, 3, 4096 },
            { 5, -3, 4096 },
            { -5, -3, 20736 }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilder2WithAsyncResultTests.TestDataMultiple))]
        public async Task Multiple_AddsComponentsToPipeline(int arg0, int arg1, int expectedResult)
        {
            var serviceProvider = PipelineBuilder2WithAsyncResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilder2WithAsyncResultTests.CreateSut(serviceProvider)
                .Use(next => (param0, param1) => next.Invoke(param0 + 1, param1 + 1))
                .UseIf
                (
                    (param0, param1) => param0 > 0 && param1 > 0,
                    builder => builder.Use(() => new SquareStep())
                )
                .UseIf<PipelineConditionTrue>(null, builder => builder.Use(() => new SquareStep()))
                .UseBranchIf
                (
                    (param0, param1) => param0 < 0 && param1 < 0,
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilder2WithAsyncResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilder2WithAsyncResultTests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(arg0, arg1);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilder<int, int, Task<int>> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilder<int, int, Task<int>>(serviceProvider);

        #endregion CreateSut

        #endregion Methods

        #region Pipeline steps

        private class SquareStep : IPipelineStep<int, int, Task<int>>
        {
            public async Task<int> Invoke(int param0, int param1, Func<int, int, Task<int>> next)
            {
                var nextResult = await next.Invoke(param0, param1);

                return nextResult * nextResult;
            }
        }

        #endregion Pipeline steps

        #region Pipeline condition

        private class PipelineConditionTrue : IPipelineCondition<int, int>
        {
            public bool Invoke(int param0, int param1) => true;
        }

        private class PipelineConditionFalse : IPipelineCondition<int, int>
        {
            public bool Invoke(int param0, int param1) => false;
        }

        #endregion Pipeline condition
    }
}
