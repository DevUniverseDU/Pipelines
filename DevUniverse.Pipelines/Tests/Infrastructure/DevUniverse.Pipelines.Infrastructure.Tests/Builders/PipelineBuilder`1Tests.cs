using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Steps;
using DevUniverse.Pipelines.Infrastructure.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.Builders;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Builders
{
    public class PipelineBuilder1Tests
    {
        private static int Result => 1;

        private static int TargetMainResult => PipelineBuilder1Tests.Result;
        private static int TargetBranchResult => PipelineBuilder1Tests.Result * 2;

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        private static Func<Func<Task<int>>, Func<Task<int>>> ComponentForConfiguration => next => async () =>
        {
            var result = await next.Invoke();

            var temp = result - 1;

            return temp * temp;
        };

        private static Action<IPipelineBuilder<Task<int>>> ConfigurationWithoutTarget => builder => builder.Use(PipelineBuilder1Tests.ComponentForConfiguration);

        private static Action<IPipelineBuilder<Task<int>>> ConfigurationWithBranchTarget => builder =>
            builder
                .Use(PipelineBuilder1Tests.ComponentForConfiguration)
                .UseTarget(PipelineBuilder1Tests.TargetBranch);

        private static Task<int> TargetMain() => Task.FromResult(PipelineBuilder1Tests.TargetMainResult);
        private static Task<int> TargetBranch() => Task.FromResult(PipelineBuilder1Tests.TargetBranchResult);

        public static Func<bool> ConditionTrue => () => true;
        public static Func<bool> ConditionFalse => () => false;


        #region Use

        #region Use component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("component");

            var sut = this.CreateSut();

            Func<Func<Task<int>>, Func<Task<int>>> component = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(component));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            var incrementValue = 10;

            var expectedResult = PipelineBuilder1Tests.TargetMainResult + incrementValue;

            Func<Func<Task<int>>, Func<Task<int>>> component = next => async () => await next.Invoke() + incrementValue;

            var sut = this.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Use component

        #region Use handler

        [Fact]
        public void Use_Handler_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("handler");

            var sut = this.CreateSut();

            Func<Func<Task<int>>, Task<int>> handler = default;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(handler));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Handler_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder1Tests.TargetMainResult;

            Func<Func<Task<int>>, Task<int>> handler = async next => await next.Invoke();

            var sut = this.CreateSut()
                .Use(handler)
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Use handler

        #region UseStep

        #region ServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<Task<int>>)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<IPipelineStep<Task<int>>>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder1Tests.TargetMainResult * PipelineBuilder1Tests.TargetMainResult;

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<SquareStep1>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .Use<SquareStep1>()
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion ServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use((Func<IPipelineStep<Task<int>>>)null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_Factory_FactoryResultIsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("instance");

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(() => null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_Factory_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder1Tests.TargetMainResult * PipelineBuilder1Tests.TargetMainResult;

            var sut = this.CreateSut()
                .Use(() => new SquareStep1())
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #region Factory with service provider

        public static readonly Func<IServiceProvider, IPipelineStep<Task<int>>> ServiceProviderFactory = sp => sp.GetRequiredService<SquareStep1>();

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use((Func<IServiceProvider, IPipelineStep<Task<int>>>)null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<Task<int>>)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use(PipelineBuilder1Tests.ServiceProviderFactory));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_FactoryResultIsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("instance");

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection.BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<Task<int>>)null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_FactoryWithServiceProvider_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder1Tests.TargetMainResult * PipelineBuilder1Tests.TargetMainResult;

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<SquareStep1>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .Use(PipelineBuilder1Tests.ServiceProviderFactory)
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory with service provider

        #endregion UseStep

        #endregion Use

        #region UseIf

        [Fact]
        public void UseIf_Configuration_Predicate_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("predicate");

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseIf(null, PipelineBuilder1Tests.ConfigurationWithoutTarget));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("configuration");

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Action<IPipelineBuilder<Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseIf(PipelineBuilder1Tests.ConditionTrue, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder1Tests.ConditionTrue, PipelineBuilder1Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Func<IServiceProvider, IPipelineBuilder<Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder1Tests.ConditionTrue, PipelineBuilder1Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<Task<int>>)}.";

            var sut = this.CreateSut();

            Action<IPipelineBuilder<Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.UseIf(null, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_FactoryResult_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("branchBuilder");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<Task<int>>> factory = () => null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder1Tests.ConditionTrue, PipelineBuilder1Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Func<bool>, int> UseIfConfigurationConditions => new TheoryData<Func<bool>, int>()
        {
            { PipelineBuilder1Tests.ConditionTrue, (PipelineBuilder1Tests.TargetMainResult - 1) * (PipelineBuilder1Tests.TargetMainResult - 1) },
            { PipelineBuilder1Tests.ConditionFalse, PipelineBuilder1Tests.TargetMainResult }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilder1Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_AddsComponentToPipeline(Func<bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder1Tests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_Factory_AddsComponentToPipeline(Func<bool> predicate, int expectedResult)
        {
            var sut = this.CreateSut()
                .UseIf(predicate, PipelineBuilder1Tests.ConfigurationWithoutTarget, () => new PipelineBuilder<Task<int>>())
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_FactoryWithServiceProvider_AddsComponentToPipeline(Func<bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder1Tests.ConfigurationWithoutTarget, _ => new PipelineBuilder<Task<int>>())
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion UseIf

        #region UseBranchIf

        [Fact]
        public void UseBranchIf_Configuration_Predicate_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("predicate");

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseBranchIf(null, PipelineBuilder1Tests.ConfigurationWithBranchTarget));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("configuration");

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Action<IPipelineBuilder<Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseBranchIf(PipelineBuilder1Tests.ConditionTrue, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder1Tests.ConditionTrue, PipelineBuilder1Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Func<IServiceProvider, IPipelineBuilder<Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder1Tests.ConditionTrue, PipelineBuilder1Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<Task<int>>)}.";

            var sut = this.CreateSut();

            Action<IPipelineBuilder<Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.UseBranchIf(null, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_FactoryResult_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("branchBuilder");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<Task<int>>> factory = () => null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder1Tests.ConditionTrue, PipelineBuilder1Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Func<bool>, int> UseBranchIfConfigurationConditions => new TheoryData<Func<bool>, int>()
        {
            {
                PipelineBuilder1Tests.ConditionTrue,
                (PipelineBuilder1Tests.TargetBranchResult - 1) * (PipelineBuilder1Tests.TargetBranchResult - 1)
            },
            { PipelineBuilder1Tests.ConditionFalse, PipelineBuilder1Tests.TargetMainResult }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilder1Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_AddsComponentToPipeline(Func<bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder1Tests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_Factory_AddsComponentToPipeline(Func<bool> predicate, int expectedResult)
        {
            var sut = this.CreateSut()
                .UseBranchIf(predicate, PipelineBuilder1Tests.ConfigurationWithBranchTarget, () => new PipelineBuilder<Task<int>>())
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder1Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_FactoryWithServiceProvider_AddsComponentToPipeline(Func<bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder1Tests.ConfigurationWithBranchTarget, _ => new PipelineBuilder<Task<int>>())
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion UseBranchIf

        #region UseTarget

        [Fact]
        public void UseTarget_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("target");

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseTarget(null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task UseTarget_SetsTarget()
        {
            var expectedResult = PipelineBuilder1Tests.TargetMainResult;

            var sut = this.CreateSut()
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion UseTarget

        #region Build

        [Fact]
        public void Build_Target_IsNull_ThrowsException()
        {
            var expectedResultMessage =
                $"The {typeof(PipelineBuilder<Task<int>>)} does not have the target. Set the target using {nameof(IPipelineBuilder<Task<int>>.UseTarget)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var expectedResult = PipelineBuilder1Tests.TargetMainResult;

            var sut = this.CreateSut()
                .UseTarget(PipelineBuilder1Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke();

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region Copy

        public static TheoryData<bool> CopyTestData => new TheoryData<bool>()
        {
            { true },
            { false }
        };

        [Theory]
        [MemberData(nameof(CopyTestData))]
        public async Task Copy_CopiesPipelineBuilder(bool setTargetBeforeCopying)
        {
            var expectedResultSourcePipeline = PipelineBuilder1Tests.TargetMainResult * PipelineBuilder1Tests.TargetMainResult;

            var expectedResultPipelineCopy = PipelineBuilder1Tests.TargetMainResult *
                                             PipelineBuilder1Tests.TargetMainResult *
                                             PipelineBuilder1Tests.TargetMainResult *
                                             PipelineBuilder1Tests.TargetMainResult;

            var serviceProvider = PipelineBuilder1Tests.ServiceCollection
                .AddTransient<SquareStep1>()
                .BuildServiceProvider();

            var sourcePipelineBuilder = this.CreateSut(serviceProvider)
                .Use<SquareStep1>();

            if (setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder1Tests.TargetMain);
            }

            var pipelineBuilderCopy = sourcePipelineBuilder
                .Copy()
                .Use<SquareStep1>();


            if (!setTargetBeforeCopying)
            {
                sourcePipelineBuilder.UseTarget(PipelineBuilder1Tests.TargetMain);
                pipelineBuilderCopy.UseTarget(PipelineBuilder1Tests.TargetMain);
            }


            var sourcePipeline = sourcePipelineBuilder.Build();

            var actualResultSourcePipeline = await sourcePipeline.Invoke();


            var pipelineCopy = pipelineBuilderCopy.Build();

            var actualPipelineCopyResult = await pipelineCopy.Invoke();

            Assert.Equal(pipelineBuilderCopy.ServiceProvider, sourcePipelineBuilder.ServiceProvider);
            Assert.True(Object.ReferenceEquals(pipelineBuilderCopy.ServiceProvider, sourcePipelineBuilder.ServiceProvider));

            Assert.Equal(pipelineBuilderCopy.Target, sourcePipelineBuilder.Target);
            Assert.False(Object.ReferenceEquals(pipelineBuilderCopy.Target, sourcePipelineBuilder.Target));

            Assert.Equal(expectedResultSourcePipeline, actualResultSourcePipeline);
            Assert.Equal(expectedResultPipelineCopy, actualPipelineCopyResult);
        }

        #endregion Copy

        #region CreateSut

        private IPipelineBuilder<Task<int>> CreateSut(IServiceProvider serviceProvider = null) => new PipelineBuilder<Task<int>>(serviceProvider);

        #endregion CreateSut
    }

    public class SquareStep1 : IPipelineStep<Task<int>>
    {
        public async Task<int> Invoke(Func<Task<int>> next)
        {
            var nextResult = await next.Invoke();

            return nextResult * nextResult;
        }
    }
}
