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

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders.PipelineBuilderAsync3
{
    public class PipelineBuilderAsync3WithoutResultTests
    {
        #region Properties

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        #region Args

        private static Arg Arg0 => new Arg() { Property = 3 };
        private static int Arg1 => 30;
        private static int Arg2 => 300;

        #endregion Args

        #region Configuration

        private static Func<Func<Arg, int, int, Task>, Func<Arg, int, int, Task>> ComponentForConfiguration =>
            next => async (param0, param1, param2) =>
            {
                await next.Invoke(param0, param1, param2);

                param0.Property -= 1;
                param0.Property *= param0.Property;
            };

        private static Action<IPipelineBuilderAsync<Arg, int, int, Task>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilderAsync3WithoutResultTests.ComponentForConfiguration);

        private static Action<IPipelineBuilderAsync<Arg, int, int, Task>> ConfigurationWithBranchTarget => builder =>
            builder
                .Use(PipelineBuilderAsync3WithoutResultTests.ComponentForConfiguration)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetBranch);

        #endregion Configuration

        #region Target

        private static Func<Arg, int, int, Task<Arg>> TargetMainResult =>
            (param0, param1, param2) => Task.Run
            (
                () =>
                {
                    param0.Property = param2 - param1 - param0.Property;

                    return param0;
                }
            );

        private static Func<Arg, int, int, Task<Arg>> TargetBranchResult =>
            (param0, param1, param2) => Task.Run
            (
                () =>
                {
                    param0.Property = param2 - param1 + param0.Property;
                    param0.Property *= 2;

                    return param0;
                }
            );


        private static Task TargetMain(Arg param0, int param1, int param2) =>
            PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2);

        private static Task TargetBranch(Arg param0, int param1, int param2) =>
            PipelineBuilderAsync3WithoutResultTests.TargetBranchResult.Invoke(param0, param1, param2);

        #endregion Target

        #endregion Properties

        #region Methods

        #region Use

        #region Component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut();

            Func<Func<Arg, int, int, Task>, Func<Arg, int, int, Task>>? component = null;

            Assert.Throws<ArgumentNullException>(() => sut.Use(component!));
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            const int incrementValue = 10;

            var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var expectedResult = targetMainResult + incrementValue;

            Func<Func<Arg, int, int, Task>, Func<Arg, int, int, Task>> component =
                next => async (param0, param1, param2) =>
                {
                    await next.Invoke(param0, param1, param2);
                    param0.Property += incrementValue;
                };

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
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
            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<SquareStep>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #region Factory

        private static readonly Func<IServiceProvider, IPipelineStep<Arg, int, int, Task>> ServiceProviderFactory =
            sp => sp.GetRequiredService<SquareStep>();

        [Fact]
        public void Use_Step_ServiceProvider_Factory_FactoryResultIsNull_ThrowsException()
        {
            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection.BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<Arg, int, int, Task>?)null!));
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var expectedResult = targetMainResult * targetMainResult;

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .Use(PipelineBuilderAsync3WithoutResultTests.ServiceProviderFactory)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
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
            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use((Func<SquareStep>?)null!));
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Use(() => null!));
        }

        [Fact]
        public async Task Use_Step_Factory_AddsComponentToPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var expectedResult = targetMainResult * targetMainResult;

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut()
                .Use(() => new SquareStep())
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #endregion StepInterface

        #endregion Use

        #region Conditions

        private static Func<Arg, int, int, Task<bool>> ConditionAsyncTrue => (_, _, _) => Task.FromResult(true);
        private static Func<Arg, int, int, Task<bool>> ConditionAsyncFalse => (_, _, _) => Task.FromResult(false);

        #region Params check

        public static TheoryData
            <Func<IPipelineBuilderAsync<Arg, int, int, Task>, object>, bool> TestDataConditionsParamNullCheck =>
            new TheoryData<Func<IPipelineBuilderAsync<Arg, int, int, Task>, object>, bool>()
            {
                #region Predicate

                {
                    sut => sut.UseIf
                    (
                        (Func<Arg, int, int, Task<bool>>)null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        (Func<Arg, int, int, Task<bool>>)null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<Arg, int, int, Task<bool>>)null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        (Func<Arg, int, int, Task<bool>>)null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                #endregion Predicate

                #region Condition factory

                {
                    sut => sut.UseIf
                    (
                        (Func<IPipelineConditionAsync<Arg, int, int>>)null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        (Func<IPipelineConditionAsync<Arg, int, int>>)null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                #endregion Condition factory

                #region Condition factory result

                {
                    sut => sut.UseIf
                    (
                        () => null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        _ => (IPipelineConditionAsync<Arg, int, int>)null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        () => null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        _ => (IPipelineConditionAsync<Arg, int, int>)null!,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                #endregion Condition factory result

                #region Branch builder configuration

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Arg, int, int, Task>>)null!,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Arg, int, int, Task>>)null!,
                        _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilderAsync<Arg, int, int, Task>>)null!,
                        _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilderAsync<Arg, int, int, Task>>)null!,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Arg, int, int, Task>>)null!,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                        (Action<IPipelineBuilderAsync<Arg, int, int, Task>>)null!,
                        _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                {
                    sut => sut.UseBranchIf<PipelineConditionTrue>
                    (
                        null,
                        (Action<IPipelineBuilderAsync<Arg, int, int, Task>>)null!,
                        _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        (Action<IPipelineBuilderAsync<Arg, int, int, Task>>)null!,
                        () => new PipelineBuilderAsync<Arg, int, int, Task>()
                    ),
                    true
                },

                #endregion Branch builder configuration

                #region Branch builder factory

                {
                    sut => sut.UseIf
                    (
                        PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Arg, int, int, Task>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Arg, int, int, Task>>?)null!
                    ),
                    false
                },

                {
                    sut => sut.UseBranchIf
                    (
                        PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Arg, int, int, Task>>?)null!
                    ),
                    false
                },
                {
                    sut => sut.UseBranchIf
                    (
                        () => new PipelineConditionTrue(),
                        PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                        (Func<IPipelineBuilderAsync<Arg, int, int, Task>>?)null!
                    ),
                    false
                },

                #endregion Branch builder factory

                #region Branch builder factory result

                {
                    sut => sut.UseIf
                        (PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseIf
                        (PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                },

                {
                    sut => sut.UseBranchIf
                        (PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget, () => null!),
                    false
                },
                {
                    sut => sut.UseBranchIf
                        (PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget, _ => null!),
                    false
                }

                #endregion Branch builder factory result
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataConditionsParamNullCheck))]
        public void Condition_ParamNullCheck_ThrowsException
        (
            Func<IPipelineBuilderAsync<Arg, int, int, Task>, object> delegateToCall,
            bool addPipelines
        )
        {
            var serviceCollection = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>();

            if (addPipelines)
            {
                serviceCollection = serviceCollection.AddPipelines();
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider);

            Assert.Throws<ArgumentNullException>(() => delegateToCall.Invoke(sut));
        }

        #endregion Params check

        #region ServiceProvider check

        public static TheoryData<Func<IPipelineBuilderAsync<Arg, int, int, Task>, object>> TestDataConditionsServiceProviderNotSetCheck =>
            new TheoryData<Func<IPipelineBuilderAsync<Arg, int, int, Task>, object>>()
            {
                sut => sut.UseIf
                (
                    PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                ),
                sut => sut.UseIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                ),

                sut => sut.UseBranchIf
                (
                    PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                ),
                sut => sut.UseBranchIf
                (
                    _ => new PipelineConditionTrue(),
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                )
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataConditionsServiceProviderNotSetCheck))]
        public void Condition_ServiceProviderNotSetCheck_ThrowsException(Func<IPipelineBuilderAsync<Arg, int, int, Task>, object> delegateToCall)
        {
            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut();

            var expectedResultMessage = $"The service provider is not set for {sut.GetType()}.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => delegateToCall.Invoke(sut));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        #endregion ServiceProvider check

        #region UseIf

        #region Predicate

        public static TheoryData<Func<Arg, int, int, Task<bool>>, Func<Arg, int, int, Task<Arg>>> TestDataUseIfAsyncConditions =>
            new TheoryData<Func<Arg, int, int, Task<bool>>, Func<Arg, int, int, Task<Arg>>>()
            {
                {
                    PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                    async (param0, param1, param2) =>
                    {
                        var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    PipelineBuilderAsync3WithoutResultTests.ConditionAsyncFalse,
                    (param0, param1, param2) => PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_AddsComponentToPipeline
        (
            Func<Arg, int, int, Task<bool>> predicate,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<Arg, int, int, Task<bool>> predicate,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut()
                .UseIf(predicate, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget, () => new PipelineBuilderAsync<Arg, int, int, Task>())
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseIfAsyncConditions))]
        public async Task UseIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<Arg, int, int, Task<bool>> predicate,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget, _ => new PipelineBuilderAsync<Arg, int, int, Task>())
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineConditionAsync<Arg, int, int>>, Func<Arg, int, int, Task<Arg>>> TestDataUseIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineConditionAsync<Arg, int, int>>, Func<Arg, int, int, Task<Arg>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async (param0, param1, param2) =>
                    {
                        var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    (param0, param1, param2) => PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseIfInterfaceConditionsFactories))]
        public async Task UseIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineConditionAsync<Arg, int, int>> conditionFactory,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut()
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                    () => new PipelineBuilderAsync<Arg, int, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<Arg, int, int, Task<Arg>>> TestDataUseIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<Arg, int, int, Task<Arg>>>()
            {
                {
                    true,
                    async (param0, param1, param2) =>
                    {
                        var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    false,
                    (param0, param1, param2) => PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>(null, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget);
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>(null, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget);
            }

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseIfInterfaceConditionsWithoutFactory))]
        public async Task UseIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            if (condition)
            {
                sut.UseIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                );
            }
            else
            {
                sut.UseIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                );
            }

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<Arg, int, int>>,
                Func<Arg, int, int, Task<Arg>>
            >
            TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData<
                Func<IServiceProvider, IPipelineConditionAsync<Arg, int, int>>,
                Func<Arg, int, int, Task<Arg>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async (param0, param1, param2) =>
                    {
                        var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2);

                        return (targetMainResult - 1) * (targetMainResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    (param0, param1, param2) => PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2)
                }
            };


        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<Arg, int, int>> conditionFactory,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseIf(conditionFactory, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<Arg, int, int>> conditionFactory,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseIf
                (
                    conditionFactory,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithoutTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories with service provider for condition and branch pipeline builder

        #endregion Interface

        #endregion UseIf

        #region UseBranchIf

        #region Predicate

        public static TheoryData<Func<Arg, int, int, Task<bool>>, Func<Arg, int, int, Task<Arg>>> TestDataUseBranchIfAsyncConditions =>
            new TheoryData<Func<Arg, int, int, Task<bool>>, Func<Arg, int, int, Task<Arg>>>()
            {
                {
                    PipelineBuilderAsync3WithoutResultTests.ConditionAsyncTrue,
                    async (param0, param1, param2) =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync3WithoutResultTests.TargetBranchResult.Invoke(param0, param1, param2);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    PipelineBuilderAsync3WithoutResultTests.ConditionAsyncFalse,
                    (param0, param1, param2) => PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_AddsComponentToPipeline
        (
            Func<Arg, int, int, Task<bool>> predicate,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddPipelines()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_Factory_AddsComponentToPipeline
        (
            Func<Arg, int, int, Task<bool>> predicate,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut()
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilderAsync<Arg, int, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseBranchIfAsyncConditions))]
        public async Task UseBranchIf_Predicate_FactoryWithServiceProvider_AddsComponentToPipeline
        (
            Func<Arg, int, int, Task<bool>> predicate,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    predicate,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Predicate

        #region Interface

        #region Factories for condition and branch builder

        public static TheoryData<Func<IPipelineConditionAsync<Arg, int, int>>, Func<Arg, int, int, Task<Arg>>> TestDataUseBranchIfInterfaceConditionsFactories =>
            new TheoryData<Func<IPipelineConditionAsync<Arg, int, int>>, Func<Arg, int, int, Task<Arg>>>()
            {
                {
                    () => new PipelineConditionTrue(),
                    async (param0, param1, param2) =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync3WithoutResultTests.TargetBranchResult.Invoke(param0, param1, param2);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    () => new PipelineConditionFalse(),
                    (param0, param1, param2) => PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2)
                }
            };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseBranchIfInterfaceConditionsFactories))]
        public async Task UseBranchIf_Interface_Factories_AddsComponentToPipeline
        (
            Func<IPipelineConditionAsync<Arg, int, int>> conditionFactory,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut()
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    () => new PipelineBuilderAsync<Arg, int, int, Task>()
                )
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factories for condition and branch builder


        public static TheoryData<bool, Func<Arg, int, int, Task<Arg>>> TestDataUseBranchIfInterfaceConditionsWithoutFactory =>
            new TheoryData<bool, Func<Arg, int, int, Task<Arg>>>()
            {
                {
                    true,
                    async (param0, param1, param2) =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync3WithoutResultTests.TargetBranchResult.Invoke(param0, param1, param2);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    false,
                    (param0, param1, param2) => PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2)
                }
            };

        #region No factories

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_NoFactories_AddsComponentToPipeline
        (
            bool condition,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>(null, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget);
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>(null, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget);
            }

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion No factories

        #region Factory with service provider for branch builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseBranchIfInterfaceConditionsWithoutFactory))]
        public async Task UseBranchIf_Interface_FactoryForBranchBuilder_WithServiceProvider_AddsComponentToPipeline
        (
            bool condition,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            if (condition)
            {
                sut.UseBranchIf<PipelineConditionTrue>
                (
                    null,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                );
            }
            else
            {
                sut.UseBranchIf<PipelineConditionFalse>
                (
                    null,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>()
                );
            }

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for branch builder


        public static TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<Arg, int, int>>,
                Func<Arg, int, int, Task<Arg>>
            >
            TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider =>
            new TheoryData
            <
                Func<IServiceProvider, IPipelineConditionAsync<Arg, int, int>>,
                Func<Arg, int, int, Task<Arg>>
            >()
            {
                {
                    sp => sp.GetRequiredService<PipelineConditionTrue>(),
                    async (param0, param1, param2) =>
                    {
                        var targetBranchResult = await PipelineBuilderAsync3WithoutResultTests.TargetBranchResult.Invoke(param0, param1, param2);

                        return (targetBranchResult - 1) * (targetBranchResult - 1);
                    }
                },
                {
                    sp => sp.GetRequiredService<PipelineConditionFalse>(),
                    (param0, param1, param2) => PipelineBuilderAsync3WithoutResultTests.TargetMainResult.Invoke(param0, param1, param2)
                }
            };

        #region Factory with service provider for condition

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_FactoryForCondition_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<Arg, int, int>> conditionFactory,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseBranchIf(conditionFactory, PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider for condition

        #region Factories with service provider for condition and branch pipeline builder

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataUseBranchIfInterfaceConditionsFactoriesWithServiceProvider))]
        public async Task UseBranchIf_Interface_Factories_WithServiceProvider_AddsComponentToPipeline
        (
            Func<IServiceProvider, IPipelineConditionAsync<Arg, int, int>> conditionFactory,
            Func<Arg, int, int, Task<Arg>> expectedResultDelegate
        )
        {
            var expectedResult = await expectedResultDelegate.Invoke
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .UseBranchIf
                (
                    conditionFactory,
                    PipelineBuilderAsync3WithoutResultTests.ConfigurationWithBranchTarget,
                    _ => new PipelineBuilderAsync<Arg, int, int, Task>(serviceProvider)
                )
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
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
            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null!));
        }

        [Fact]
        public async Task UseTarget_SetsTarget()
        {
            var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut()
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
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
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataCopyTargetSetOrder))]
        public async Task Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var expectedResultSourcePipeline = targetMainResult * targetMainResult * targetMainResult * targetMainResult;
            var expectedResultPipelineCopy = expectedResultSourcePipeline * expectedResultSourcePipeline;

            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .Use<SquareStep>()
                .Use<SquareStep>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep>();

            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);
            }

            var sourcePipeline = sourcePipelineBuilder.Build();

            var sourcePipelineArg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await sourcePipeline.Invoke
            (
                sourcePipelineArg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResultSourcePipeline = sourcePipelineArg0;

            var pipelineCopy = pipelineBuilderCopy.Build();

            var pipelineCopyArg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipelineCopy.Invoke
            (
                pipelineCopyArg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
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
            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut();

            var expectedResultMessage = $"The {sut.GetType()} does not have the target.";

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut()
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task Build_WithTarget_BuildsPipeline()
        {
            var targetMainResult = await PipelineBuilderAsync3WithoutResultTests.TargetMainResult
            (
                PipelineBuilderAsync3WithoutResultTests.Arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var expectedResult = targetMainResult;

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut();

            var pipeline = sut.Build(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var arg0 = PipelineBuilderAsync3WithoutResultTests.Arg0;

            await pipeline.Invoke
            (
                arg0,
                PipelineBuilderAsync3WithoutResultTests.Arg1,
                PipelineBuilderAsync3WithoutResultTests.Arg2
            );

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region Multiple steps

        public static TheoryData<Arg, int, int, int> TestDataMultiple => new TheoryData<Arg, int, int, int>()
        {
            { new Arg() { Property = 5 }, 3, 7, 256 },
            { new Arg() { Property = -5 }, 3, 7, 4096 },
            { new Arg() { Property = 5 }, -3, 7, 256 },
            { new Arg() { Property = -5 }, -3, 7, 38416 },
            { new Arg() { Property = 5 }, 3, -7, 65536 },
            { new Arg() { Property = -5 }, 3, -7, 1296 },
            { new Arg() { Property = 5 }, -3, -7, 10000 },
            { new Arg() { Property = -5 }, -3, -7, 65536 }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilderAsync3WithoutResultTests.TestDataMultiple))]
        public async Task Multiple_AddsComponentsToPipeline(Arg arg0, int arg1, int arg2, int expectedResult)
        {
            var serviceProvider = PipelineBuilderAsync3WithoutResultTests.ServiceCollection
                .AddPipelines()
                .AddTransient<PipelineConditionTrue>()
                .AddTransient<PipelineConditionFalse>()
                .AddTransient<SquareStep>()
                .BuildServiceProvider();

            var sut = PipelineBuilderAsync3WithoutResultTests.CreateSut(serviceProvider)
                .Use
                (
                    next => (param0, param1, param2) =>
                    {
                        param0.Property += 1;

                        return next.Invoke(param0, param1 + 1, param2 + 1);
                    }
                )
                .UseIf
                (
                    (param0, param1, param2) => Task.FromResult(param0.Property > 0 && param1 > 0 && param2 > 0),
                    builder => builder.Use(() => new SquareStep())
                )
                .UseIf<PipelineConditionTrue>(null, builder => builder.Use(() => new SquareStep()))
                .UseBranchIf
                (
                    (param0, param1, param2) => Task.FromResult(param0.Property < 0 && param1 < 0 && param2 < 0),
                    builder => builder.Use(() => new SquareStep()).UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetBranch)
                )
                .Use<SquareStep>()
                .UseTarget(PipelineBuilderAsync3WithoutResultTests.TargetMain);

            var pipeline = sut.Build();

            await pipeline.Invoke(arg0, arg1, arg2);

            var actualResult = arg0;

            Assert.Equal(expectedResult, actualResult.Property);
        }

        #endregion Multiple steps

        #region CreateSut

        private static IPipelineBuilderAsync<Arg, int, int, Task> CreateSut(IServiceProvider? serviceProvider = null) =>
            new PipelineBuilderAsync<Arg, int, int, Task>(serviceProvider);

        #endregion CreateSut

        #endregion Methods

        #region Pipeline steps

        private class SquareStep : IPipelineStep<Arg, int, int, Task>
        {
            public async Task Invoke(Arg param0, int param1, int param2, Func<Arg, int, int, Task> next)
            {
                await next.Invoke(param0, param1, param2);

                param0.Property *= param0.Property;
            }
        }

        #endregion Pipeline steps

        #region Pipeline condition

        private class PipelineConditionTrue : IPipelineConditionAsync<Arg, int, int>
        {
            public Task<bool> InvokeAsync(Arg param0, int param1, int param2) => Task.FromResult(true);
        }

        private class PipelineConditionFalse : IPipelineConditionAsync<Arg, int, int>
        {
            public Task<bool> InvokeAsync(Arg param0, int param1, int param2) => Task.FromResult(false);
        }

        #endregion Pipeline condition
    }
}
