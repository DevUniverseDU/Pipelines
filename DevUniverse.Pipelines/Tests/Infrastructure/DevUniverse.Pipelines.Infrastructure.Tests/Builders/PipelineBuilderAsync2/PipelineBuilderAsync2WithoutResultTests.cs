using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;
using DevUniverse.Pipelines.Infrastructure.Tests.Builders.Shared;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilderAsync2
{
    public class PipelineBuilderAsync2WithoutResultTests
    {
        #region Properties

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        #region Args

        private static Arg Arg0 => new Arg() { Property = 2 };
        private static int Arg1 => 20;

        #endregion Args

        #region Configuration

        private static Func<Func<Arg, int, Task>, Func<Arg, int, Task>> ComponentForConfiguration =>
            next => async (param0, param1) =>
            {
                await next.Invoke(param0, param1);

                param0.Property -= 1;
                param0.Property *= param0.Property;
            };

        private static Action<IPipelineBuilderAsync<Arg, int, Task>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilderAsync2WithoutResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilderAsync<Arg, int, Task>> ConfigurationWithBranchTarget => builder =>
            builder
                .Use(PipelineBuilderAsync2WithoutResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<Arg, int, Task<Arg>> TargetMainResult =>
            (param0, param1) => Task.Run
            (
                () =>
                {
                    param0.Property = param1 - param0.Property;

                    return param0;
                }
            );

        private static Func<Arg, int, Task<Arg>> TargetBranchResult =>
            (param0, param1) => Task.Run
            (
                () =>
                {
                    param0.Property = param1 + param0.Property;
                    param0.Property *= 2;

                    return param0;
                }
            );


        private static Task TargetMain(Arg param0, int param1) =>
            PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1);

        private static Task TargetBranch(Arg param0, int param1) =>
            PipelineBuilderAsync2WithoutResultTests.TargetBranchResult.Invoke(param0, param1);

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut();

            Func<Func<Arg, int, Task>, Func<Arg, int, Task>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<Arg, int, Task>, Func<Arg, int, Task>> component =
                next => async (param0, param1) =>
                {
                    await next.Invoke(param0, param1);
                    param0.Property += incrementValue;
                };

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Component

        #region StepInterface

        #region WithServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #region Factory

        private static readonly Func<IServiceProvider, IPipelineStep<Arg, int, Task>> ServiceProviderFactory =
            sp => sp.GetRequiredService<SquareStep>();

        [Fact]
        public void Use_Step_ServiceProvider_Factory_FactoryResultIsNull_ThrowsException()
        {
            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<Arg, int, Task>?)null!));
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilderAsync2WithoutResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion WithServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public async Task Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion StepInterface

        #endregion Use

        #region Conditions

        private static Func<Arg, int, Task<bool>> ConditionAsyncTrue => (_, _) => Task.FromResult(true);
        private static Func<Arg, int, Task<bool>> ConditionAsyncFalse => (_, _) => Task.FromResult(false);

        #region Params check

        public static TheoryData
            <Func<IPipelineBuilderAsync<Arg, int, Task>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilderAsync<Arg, int, Task>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<Arg, int, Task<bool>>)null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<Arg, int, Task<bool>>)null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<Arg, int, Task<bool>>)null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<Arg, int, Task<bool>>)null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineConditionAsync<Arg, int>>)null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineConditionAsync<Arg, int>>)null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        () => null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineConditionAsync<Arg, int>)null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        () => null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineConditionAsync<Arg, int>)null!,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Arg, int, Task>>)null!,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Arg, int, Task>>)null!,
                        _ => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilderAsync<Arg, int, Task>>)null!,
                        _ => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilderAsync<Arg, int, Task>>)null!,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Arg, int, Task>>)null!,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Arg, int, Task>>)null!,
                        _ => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilderAsync<Arg, int, Task>>)null!,
                        _ => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilderAsync<Arg, int, Task>>)null!,
                        () => new PipelineBuilderAsync<Arg, int, Task>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Arg, int, Task>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Arg, int, Task>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Arg, int, Task>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Arg, int, Task>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf
                        (PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf
                        (PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf
                        (PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf
                        (PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilderAsync<Arg, int, Task>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilderAsync<Arg, int, Task>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilderAsync<Arg, int, Task>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilderAsync<Arg, int, Task>, object> delegateToCall)
        {
            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<Arg, int, Task<bool>>, Func<Arg, int, Task<Arg>>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<Arg, int, Task<bool>>, Func<Arg, int, Task<Arg>>>()
            {
                {
                    PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                    async (param0, param1) =>
                    {
                        var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilderAsync2WithoutResultTests.ConditionAsyncFalse,
                    (param0, param1) => PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_AddsComponentToPipeline
        (
            Func<Arg, int, Task<bool>> predicate,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<Arg, int, Task<bool>> predicate,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget, () => new PipelineBuilderAsync<Arg, int, Task>())
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<Arg, int, Task<bool>> predicate,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilderAsync<Arg, int, Task>())
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineConditionAsync<Arg, int>>, Func<Arg, int, Task<Arg>>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineConditionAsync<Arg, int>>, Func<Arg, int, Task<Arg>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async (param0, param1) =>
                    {
                        var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    (param0, param1) => PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public async Task UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineConditionAsync<Arg, int>> conditionFactory,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilderAsync<Arg, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<Arg, int, Task<Arg>>> TestDataUseIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<Arg, int, Task<Arg>>>()
            {
                {
                    true,
                    async (param0, param1) =>
                    {
                        var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    (param0, param1) => PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                );
            }

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<Arg, int>>,
                Func<Arg, int, Task<Arg>>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineConditionAsync<Arg, int>>,
                Func<Arg, int, Task<Arg>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async (param0, param1) =>
                    {
                        var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    (param0, param1) => PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<Arg, int>> conditionFactory,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<Arg, int>> conditionFactory,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<Arg, int, Task<bool>>, Func<Arg, int, Task<Arg>>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<Arg, int, Task<bool>>, Func<Arg, int, Task<Arg>>>()
            {
                {
                    PipelineBuilderAsync2WithoutResultTests.ConditionAsyncTrue,
                    async (param0, param1) =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync2WithoutResultTests.TargetBranchResult.Invoke(param0, param1);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilderAsync2WithoutResultTests.ConditionAsyncFalse,
                    (param0, param1) => PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<Arg, int, Task<bool>> predicate,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<Arg, int, Task<bool>> predicate,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilderAsync<Arg, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<Arg, int, Task<bool>> predicate,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineConditionAsync<Arg, int>>, Func<Arg, int, Task<Arg>>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineConditionAsync<Arg, int>>, Func<Arg, int, Task<Arg>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async (param0, param1) =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync2WithoutResultTests.TargetBranchResult.Invoke(param0, param1);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    (param0, param1) => PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public async Task UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineConditionAsync<Arg, int>> conditionFactory,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilderAsync<Arg, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<Arg, int, Task<Arg>>> TestDataUseBranchIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<Arg, int, Task<Arg>>>()
            {
                {
                    true,
                    async (param0, param1) =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync2WithoutResultTests.TargetBranchResult.Invoke(param0, param1);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    (param0, param1) => PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>()
                );
            }

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<Arg, int>>,
                Func<Arg, int, Task<Arg>>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<Arg, int>>,
                Func<Arg, int, Task<Arg>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async (param0, param1) =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync2WithoutResultTests.TargetBranchResult.Invoke(param0, param1);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    (param0, param1) => PipelineBuilderAsync2WithoutResultTests.TargetMainResult.Invoke(param0, param1)
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<Arg, int>> conditionFactory,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<Arg, int>> conditionFactory,
            Func<Arg, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilderAsync2WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, Task>(serviceProvider)
                )
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

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
            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public async Task UseTarget_SetsTarget()
        {
            var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut()
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

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
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataCopyTargetSetOrder))]
        public async Task Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);
            }

            var sourcePipeline = sourcePipelineBuilder.Build();

            var sourcePipelineArg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await sourcePipeline.Invoke
            (
                sourcePipelineArg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResultSourcePipeline = sourcePipelineArg0;

            var pipelineCopy = pipelineBuilderCopy.Build();

            var pipelineCopyArg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipelineCopy.Invoke
            (
                pipelineCopyArg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResultPipelineCopy = pipelineCopyArg0;

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
            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut()
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync2WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync2WithoutResultTests.Arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var arg0 = PipelineBuilderAsync2WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync2WithoutResultTests.Arg1
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region Multiple steps

        public static TheoryData<Arg, int, int> TestDataMultiple => new TheoryData<Arg, int, int>()
        {
            { new Arg() { Property = 5 }, 3, 256 },
            { new Arg() { Property = -5 }, 3, 4096 },
            { new Arg() { Property = 5 }, -3, 4096 },
            { new Arg() { Property = -5 }, -3, 20736 }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync2WithoutResultTests.TestDataMultiple))]
        public async Task Multiple_AddsComponentsToPipeline(Arg arg0, int arg1, int expectedResult)
        {
            var serviceProvider = PipelineBuilderAsync2WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync2WithoutResultTests.CreateSut(serviceProvider)
                .Use
                (
                    next => (param0, param1) =>
                    {
                        param0.Property += 1;

                        return next.Invoke(param0, param1 + 1);
                    }
                )
                .UseIf
                (
                    (param0, param1) => Task.FromResult(param0.Property > 0 && param1 > 0),
                    builder => builder.Use(() => new SquareStep())
                )
                .UseIf<PipelineConditionTrue>(null, builder => builder.Use(() => new SquareStep()))
                .UseBranchIf
                (
                    (param0, param1) => Task.FromResult(param0.Property < 0 && param1 < 0),
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilderAsync2WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            await pipeline.Invoke(arg0, arg1);

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult.Property);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilderAsync<Arg, int, Task> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilderAsync<Arg, int, Task>(serviceProvider);

        #endregion CreateSut

        #endregion Methods

        #region Pipeline steps

        private class SquareStep : IPipelineStep<Arg, int, Task>
        {
            public async Task Invoke(Arg param0, int param1, Func<Arg, int, Task> next)
            {
                await next.Invoke(param0, param1);

                param0.Property *= param0.Property;
            }
        }

        #endregion Pipeline steps

        #region Pipeline condition

        private class PipelineConditionTrue : IPipelineConditionAsync<Arg, int>
        {
            public Task<bool> InvokeAsync(Arg param0, int param1) => Task.FromResult(true);
        }

        private class PipelineConditionFalse : IPipelineConditionAsync<Arg, int>
        {
            public Task<bool> InvokeAsync(Arg param0, int param1) => Task.FromResult(false);
        }

        #endregion Pipeline condition
    }
}
