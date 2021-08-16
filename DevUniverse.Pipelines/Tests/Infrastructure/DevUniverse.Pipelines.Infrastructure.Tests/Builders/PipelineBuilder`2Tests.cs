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
    public class PipelineBuilder2Tests
    {
        private static int Arg0 => 2;

        private static int TargetMainResult => PipelineBuilder2Tests.Arg0;
        private static int TargetBranchResult => PipelineBuilder2Tests.Arg0 * 2;

        private static IServiceCollection ServiceCollection => new ServiceCollection();

        private static Func<Func<int, Task<int>>, Func<int, Task<int>>> ComponentForConfiguration => next => async arg0 =>
        {
            var result = await next.Invoke(arg0);

            var temp = result - 1;

            return temp * temp;
        };

        private static Action<IPipelineBuilder<int, Task<int>>> ConfigurationWithoutTarget => builder => builder.Use(PipelineBuilder2Tests.ComponentForConfiguration);

        private static Action<IPipelineBuilder<int, Task<int>>> ConfigurationWithBranchTarget => builder =>
            builder
                .Use(PipelineBuilder2Tests.ComponentForConfiguration)
                .UseTarget(PipelineBuilder2Tests.TargetBranch);

        private static Task<int> TargetMain(int arg0) => Task.FromResult(PipelineBuilder2Tests.TargetMainResult);
        private static Task<int> TargetBranch(int arg0) => Task.FromResult(PipelineBuilder2Tests.TargetBranchResult);

        public static Func<int, bool> ConditionTrue => _ => true;
        public static Func<int, bool> ConditionFalse => _ => false;


        #region Use

        #region Use component

        [Fact]
        public void Use_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("component");

            var sut = this.CreateSut();

            Func<Func<int, Task<int>>, Func<int, Task<int>>> component = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(component));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Component_AddsComponentToPipeline()
        {
            var incrementValue = 10;

            var expectedResult = PipelineBuilder2Tests.TargetMainResult + incrementValue;

            Func<Func<int, Task<int>>, Func<int, Task<int>>> component = next => async arg0 => await next.Invoke(arg0) + incrementValue;

            var sut = this.CreateSut()
                .Use(component)
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Use component

        #region Use handler

        [Fact]
        public void Use_Handler_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("handler");

            var sut = this.CreateSut();

            Func<int, Func<int, Task<int>>, Task<int>> handler = default;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(handler));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Handler_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder2Tests.TargetMainResult;

            Func<int, Func<int, Task<int>>, Task<int>> handler = async (arg0, next) => await next.Invoke(arg0);

            var sut = this.CreateSut()
                .Use(handler)
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Use handler

        #region UseStep

        #region ServiceProvider

        [Fact]
        public void Use_Step_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<int, Task<int>>)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use<IPipelineStep<int, Task<int>>>());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_ServiceProvider_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder2Tests.TargetMainResult * PipelineBuilder2Tests.TargetMainResult;

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<SquareStep2>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .Use<SquareStep2>()
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion ServiceProvider

        #region Factory

        [Fact]
        public void Use_Step_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use((Func<IPipelineStep<int, Task<int>>>)null));

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
            var expectedResult = PipelineBuilder2Tests.TargetMainResult * PipelineBuilder2Tests.TargetMainResult;

            var sut = this.CreateSut()
                .Use(() => new SquareStep2())
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Factory

        #region Factory with service provider

        public static readonly Func<IServiceProvider, IPipelineStep<int, Task<int>>> ServiceProviderFactory = sp => sp.GetRequiredService<SquareStep2>();

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use((Func<IServiceProvider, IPipelineStep<int, Task<int>>>)null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<int, Task<int>>)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Use(PipelineBuilder2Tests.ServiceProviderFactory));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void Use_Step_FactoryWithServiceProvider_FactoryResultIsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("instance");

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection.BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.Use(_ => (IPipelineStep<int, Task<int>>)null));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Use_Step_FactoryWithServiceProvider_AddsComponentToPipeline()
        {
            var expectedResult = PipelineBuilder2Tests.TargetMainResult * PipelineBuilder2Tests.TargetMainResult;

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<SquareStep2>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .Use(PipelineBuilder2Tests.ServiceProviderFactory)
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

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

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseIf(null, PipelineBuilder2Tests.ConfigurationWithoutTarget));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("configuration");

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Action<IPipelineBuilder<int, Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseIf(PipelineBuilder2Tests.ConditionTrue, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<int, Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder2Tests.ConditionTrue, PipelineBuilder2Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Func<IServiceProvider, IPipelineBuilder<int, Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder2Tests.ConditionTrue, PipelineBuilder2Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<int, Task<int>>)}.";

            var sut = this.CreateSut();

            Action<IPipelineBuilder<int, Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.UseIf(null, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseIf_Configuration_FactoryResult_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("branchBuilder");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<int, Task<int>>> factory = () => null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseIf(PipelineBuilder2Tests.ConditionTrue, PipelineBuilder2Tests.ConfigurationWithoutTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Func<int, bool>, int> UseIfConfigurationConditions => new TheoryData<Func<int, bool>, int>()
        {
            { PipelineBuilder2Tests.ConditionTrue, (PipelineBuilder2Tests.TargetMainResult - 1) * (PipelineBuilder2Tests.TargetMainResult - 1) },
            { PipelineBuilder2Tests.ConditionFalse, PipelineBuilder2Tests.TargetMainResult }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilder2Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_AddsComponentToPipeline(Func<int, bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder2Tests.ConfigurationWithoutTarget)
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder2Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_Factory_AddsComponentToPipeline(Func<int, bool> predicate, int expectedResult)
        {
            var sut = this.CreateSut()
                .UseIf(predicate, PipelineBuilder2Tests.ConfigurationWithoutTarget, () => new PipelineBuilder<int, Task<int>>())
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder2Tests.UseIfConfigurationConditions))]
        public async Task UseIf_Configuration_FactoryWithServiceProvider_AddsComponentToPipeline(Func<int, bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseIf(predicate, PipelineBuilder2Tests.ConfigurationWithoutTarget, _ => new PipelineBuilder<int, Task<int>>())
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion UseIf

        #region UseBranchIf

        [Fact]
        public void UseBranchIf_Configuration_Predicate_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("predicate");

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseBranchIf(null, PipelineBuilder2Tests.ConfigurationWithBranchTarget));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_Component_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("configuration");

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Action<IPipelineBuilder<int, Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.UseBranchIf(PipelineBuilder2Tests.ConditionTrue, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_Factory_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<int, Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder2Tests.ConditionTrue, PipelineBuilder2Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_FactoryWithServiceProvider_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("factory");

            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider);

            Func<IServiceProvider, IPipelineBuilder<int, Task<int>>> factory = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder2Tests.ConditionTrue, PipelineBuilder2Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_ServiceProvider_IsNotSet_ThrowsException()
        {
            var expectedResultMessage =
                $"The service provider is not set for pipeline builder {typeof(PipelineBuilder<int, Task<int>>)}.";

            var sut = this.CreateSut();

            Action<IPipelineBuilder<int, Task<int>>> configuration = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.UseBranchIf(null, configuration));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void UseBranchIf_Configuration_FactoryResult_IsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("branchBuilder");

            var sut = this.CreateSut();

            Func<IPipelineBuilder<int, Task<int>>> factory = () => null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>
            (
                () =>
                    sut.UseBranchIf(PipelineBuilder2Tests.ConditionTrue, PipelineBuilder2Tests.ConfigurationWithBranchTarget, factory)
            );

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Func<int, bool>, int> UseBranchIfConfigurationConditions => new TheoryData<Func<int, bool>, int>()
        {
            { PipelineBuilder2Tests.ConditionTrue, (PipelineBuilder2Tests.TargetBranchResult - 1) * (PipelineBuilder2Tests.TargetBranchResult - 1) },
            { PipelineBuilder2Tests.ConditionFalse, PipelineBuilder2Tests.TargetMainResult }
        };

        [Theory]
        [MemberData(nameof(PipelineBuilder2Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_AddsComponentToPipeline(Func<int, bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .AddTransient<IPipelineBuilderFactory, PipelineBuilderFactory>()
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder2Tests.ConfigurationWithBranchTarget)
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder2Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_Factory_AddsComponentToPipeline(Func<int, bool> predicate, int expectedResult)
        {
            var sut = this.CreateSut()
                .UseBranchIf(predicate, PipelineBuilder2Tests.ConfigurationWithBranchTarget, () => new PipelineBuilder<int, Task<int>>())
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(PipelineBuilder2Tests.UseBranchIfConfigurationConditions))]
        public async Task UseBranchIf_Configuration_FactoryWithServiceProvider_AddsComponentToPipeline(Func<int, bool> predicate, int expectedResult)
        {
            var serviceProvider = PipelineBuilder2Tests.ServiceCollection
                .BuildServiceProvider();

            var sut = this.CreateSut(serviceProvider)
                .UseBranchIf(predicate, PipelineBuilder2Tests.ConfigurationWithBranchTarget, _ => new PipelineBuilder<int, Task<int>>())
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

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
            var expectedResult = PipelineBuilder2Tests.TargetMainResult;

            var sut = this.CreateSut()
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion UseTarget

        #region Build

        [Fact]
        public void Build_Target_IsNull_ThrowsException()
        {
            var expectedResultMessage =
                $"The {typeof(PipelineBuilder<int, Task<int>>)} does not have the target. Set the target using {nameof(IPipelineBuilder<int, Task<int>>.UseTarget)}.";

            var sut = this.CreateSut();

            var actualResult = Assert.Throws<InvalidOperationException>(() => sut.Build());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public async Task Build_BuildsPipeline()
        {
            var expectedResult = PipelineBuilder2Tests.TargetMainResult;

            var sut = this.CreateSut()
                .UseTarget(PipelineBuilder2Tests.TargetMain);

            var pipeline = sut.Build();

            var actualResult = await pipeline.Invoke(PipelineBuilder2Tests.Arg0);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion Build

        #region CreateSut

        private IPipelineBuilder<int, Task<int>> CreateSut(IServiceProvider serviceProvider = null) => new PipelineBuilder<int, Task<int>>(serviceProvider);

        #endregion CreateSut
    }

    public class SquareStep2 : IPipelineStep<int, Task<int>>
    {
        public async Task<int> Invoke(int arg0, Func<int, Task<int>> next)
        {
            var nextResult = await next.Invoke(arg0);

            return nextResult * nextResult;
        }
    }
}
