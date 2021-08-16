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
    public class PipelineBuilder5Tests
    {
        private static int Arg0 => 4;
        private static int Arg1 => 40;
        private static int Arg2 => 400;
        private static int Arg3 => 4000;

        private static int TargetMainResult =>
            PipelineBuilder5Tests.Arg3 + PipelineBuilder5Tests.Arg2 - PipelineBuilder5Tests.Arg1 - PipelineBuilder5Tests.Arg0;

        private static int TargetBranchResult =>
            (PipelineBuilder5Tests.Arg3 - PipelineBuilder5Tests.Arg2 - PipelineBuilder5Tests.Arg1 + PipelineBuilder5Tests.Arg0) * 2;

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        private static Func<Func<int, int, int, int, Task<int>>, Func<int, int, int, int, Task<int>>> ComponentForConfiguration => next => async (arg0, arg1, arg2, arg3) =>
        {
            var result = await next.Invoke(arg0, arg1, arg2, arg3);

            var temp = result - 1;

            return temp * temp;
        };

        private static Action<IPipelineBuilder<int, int, int, int, Task<int>>> ConfigurationWithoutTarget =>
            builder => builder.Use(PipelineBuilder5Tests.ComponentForConfiguration);

        private static Action<IPipelineBuilder<int, int, int, int, Task<int>>> ConfigurationWithBranchTarget => builder =>
            builder
                .Use(PipelineBuilder5Tests.ComponentForConfiguration)
                .UseTarget(PipelineBuilder5Tests.TargetBranch);

        private static Task<int> TargetMain(int arg0, int arg1, int arg2, int arg3) => Task.FromResult(PipelineBuilder5Tests.TargetMainResult);
        private static Task<int> TargetBranch(int arg0, int arg1, int arg2, int arg3) => Task.FromResult(PipelineBuilder5Tests.TargetBranchResult);

        public static Func<int, int, int, int, bool> ConditionTrue => (_, _, _, _) => true;
        public static Func<int, int, int, int, bool> ConditionFalse => (_, _, _, _) => false;

        #region Use

        #region Use component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("component");

            var sut = this.CreateSut();

            Func<Func<int, int, int, int, Task<int>>, Func<int, int, int, int, Task<int>>> component = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(component));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            var incrementValue = 10;

            var expectedResult = PipelineBuilder5Tests.TargetMainResult + incrementValue;

            Func<Func<int, int, int, int, Task<int>>, Func<int, int, int, int, Task<int>>> component = next => async (arg0, arg1, arg2, arg3) =>
                await next.Invoke(arg0, arg1, arg2, arg3) + incrementValue;

            var sut = this.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Use component

        #region Use handler

        [Fact]
        public void Use_Handler_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("handler");

            var sut = this.CreateSut();

            Func<int, int, int, int, Func<int, int, int, int, Task<int>>, Task<int>> handler = default;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(handler));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Handler_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder5Tests.TargetMainResult;

            Func<int, int, int, int, Func<int, int, int, int, Task<int>>, Task<int>> handler = async
                (arg0, arg1, arg2, arg3, next) => await next.Invoke(arg0, arg1, arg2, arg3);

            var sut = this.CreateSut()
                .Use(handler)
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Use handler

        #region UseStep

        #region ServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<int, int, int, int, Task<int>>)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<IPipelineStep<int, int, int, int, Task<int>>>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder5Tests.TargetMainResult * PipelineBuilder5Tests.TargetMainResult;

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<SquareStep5>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .Use<SquareStep5>()
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion ServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use((Func<IPipelineStep<int, int, int, int, Task<int>>>)null));

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
            var expectedResult = PipelineBuilder5Tests.TargetMainResult * PipelineBuilder5Tests.TargetMainResult;

            var sut = this.CreateSut()
                .Use(() => new SquareStep5())
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #region Factory with service provider

        public static readonly Func<IServiceProvider, IPipelineStep<int, int, int, int, Task<int>>> ServiceProviderFactory = sp => sp.GetRequiredService<SquareStep5>();

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use((Func<IServiceProvider, IPipelineStep<int, int, int, int, Task<int>>>)null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<int, int, int, int, Task<int>>)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use(PipelineBuilder5Tests.ServiceProviderFactory));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_FactoryResultIsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("instance");

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection.BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<int, int, int, int, Task<int>>)null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_FactoryWithServiceProvider_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder5Tests.TargetMainResult * PipelineBuilder5Tests.TargetMainResult;

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<SquareStep5>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .Use(PipelineBuilder5Tests.ServiceProviderFactory)
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

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

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseIf(null, PipelineBuilder5Tests.ConfigurationWithoutTarget));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("configuration");

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Action<IPipelineBuilder<int, int, int, int, Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseIf(PipelineBuilder5Tests.ConditionTrue, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<int, int, int, int, Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder5Tests.ConditionTrue, PipelineBuilder5Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Func<IServiceProvider, IPipelineBuilder<int, int, int, int, Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder5Tests.ConditionTrue, PipelineBuilder5Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<int, int, int, int, Task<int>>)}.";

            var sut = this.CreateSut();

            Action<IPipelineBuilder<int, int, int, int, Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.UseIf(null, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_FactoryResult_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("branchBuilder");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<int, int, int, int, Task<int>>> factory = () => null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder5Tests.ConditionTrue, PipelineBuilder5Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Func<int, int, int, int, bool>, int> UseIfConfigurationConditions => new TheoryData<Func<int, int, int, int, bool>, int>()
        {
            { PipelineBuilder5Tests.ConditionTrue, (PipelineBuilder5Tests.TargetMainResult - 1) * (PipelineBuilder5Tests.TargetMainResult - 1) },
            { PipelineBuilder5Tests.ConditionFalse, PipelineBuilder5Tests.TargetMainResult }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilder5Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_AddsComponentToPipeline(Func<int, int, int, int, bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder5Tests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder5Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_Factory_AddsComponentToPipeline(Func<int, int, int, int, bool> predicate, int expectedResult)
        {
            var sut = this.CreateSut()
                .UseIf(predicate, PipelineBuilder5Tests.ConfigurationWithoutTarget, () => new PipelineBuilder<int, int, int, int, Task<int>>())
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder5Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_FactoryWithServiceProvider_AddsComponentToPipeline(Func<int, int, int, int, bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder5Tests.ConfigurationWithoutTarget, _ => new PipelineBuilder<int, int, int, int, Task<int>>())
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion UseIf

        #region UseBranchIf

        [Fact]
        public void UseBranchIf_Configuration_Predicate_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("predicate");

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseBranchIf(null, PipelineBuilder5Tests.ConfigurationWithBranchTarget));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("configuration");

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Action<IPipelineBuilder<int, int, int, int, Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseBranchIf(PipelineBuilder5Tests.ConditionTrue, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<int, int, int, int, Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder5Tests.ConditionTrue, PipelineBuilder5Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Func<IServiceProvider, IPipelineBuilder<int, int, int, int, Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder5Tests.ConditionTrue, PipelineBuilder5Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<int, int, int, int, Task<int>>)}.";

            var sut = this.CreateSut();

            Action<IPipelineBuilder<int, int, int, int, Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.UseBranchIf(null, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_FactoryResult_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("branchBuilder");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<int, int, int, int, Task<int>>> factory = () => null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder5Tests.ConditionTrue, PipelineBuilder5Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Func<int, int, int, int, bool>, int> UseBranchIfConfigurationConditions => new TheoryData<Func<int, int, int, int, bool>, int>()
        {
            { PipelineBuilder5Tests.ConditionTrue, (PipelineBuilder5Tests.TargetBranchResult - 1) * (PipelineBuilder5Tests.TargetBranchResult - 1) },
            { PipelineBuilder5Tests.ConditionFalse, PipelineBuilder5Tests.TargetMainResult }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilder5Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_AddsComponentToPipeline(Func<int, int, int, int, bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder5Tests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder5Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_Factory_AddsComponentToPipeline(Func<int, int, int, int, bool> predicate, int expectedResult)
        {
            var sut = this.CreateSut()
                .UseBranchIf(predicate, PipelineBuilder5Tests.ConfigurationWithBranchTarget, () => new PipelineBuilder<int, int, int, int, Task<int>>())
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder5Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_FactoryWithServiceProvider_AddsComponentToPipeline(Func<int, int, int, int, bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder5Tests.ServiceCollection
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder5Tests.ConfigurationWithBranchTarget, _ => new PipelineBuilder<int, int, int, int, Task<int>>())
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

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
            var expectedResult = PipelineBuilder5Tests.TargetMainResult;

            var sut = this.CreateSut()
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion UseTarget

        #region Build

        [Fact]
        public void Build_Target_IsNull_ThrowsException()
        {
            var expectedResultMessage =
                $"The {typeof(PipelineBuilder<int, int, int, int, Task<int>>)} does not have the target. Set the target using {nameof(IPipelineBuilder<int, int, int, int, Task<int>>.UseTarget)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var expectedResult = PipelineBuilder5Tests.TargetMainResult;

            var sut = this.CreateSut()
                .UseTarget(PipelineBuilder5Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder5Tests.Arg0, PipelineBuilder5Tests.Arg1, PipelineBuilder5Tests.Arg2, PipelineBuilder5Tests.Arg3);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region CreateSut

        private IPipelineBuilder<int, int, int, int, Task<int>> CreateSut(IServiceProvider serviceProvider = null) =>
            new PipelineBuilder<int, int, int, int, Task<int>>(serviceProvider);

        #endregion CreateSut
    }

    public class SquareStep5 : IPipelineStep<int, int, int, int, Task<int>>
    {
        public async Task<int> Invoke(int arg0, int arg1, int arg2, int arg3, Func<int, int, int, int, Task<int>> next)
        {
            var nextResult = await next.Invoke(arg0, arg1, arg2, arg3);

            return nextResult * nextResult;
        }
    }
}
