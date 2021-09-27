<!-- omit in toc -->
# Usage examples

<!-- omit in toc -->
## Table of contents

- [Overview](#overview)
- [Create pipeline builders](#create-pipeline-builders)
  - [`Constructor`](#constructor)
  - [`Create`](#create)
  - [`Copy`](#copy)
- [Add steps](#add-steps)
  - [`Use` component](#use-component)
  - [`Use` interface implementation](#use-interface-implementation)
- [Add steps and execute conditionally](#add-steps-and-execute-conditionally)
  - [`UseIf`](#useif)
  - [`UseBranchIf`](#usebranchif)
  - [Condition interfaces](#condition-interfaces)
  - [Custom factories for pipeline conditions](#custom-factories-for-pipeline-conditions)
  - [Custom factories for branch pipeline builders](#custom-factories-for-branch-pipeline-builders)
  - [Async conditions](#async-conditions)
- [Set the pipeline target](#set-the-pipeline-target)
  - [`UseTarget`](#usetarget)
- [Building the pipeline](#building-the-pipeline)
  - [`Build`](#build)

<br/>

## Overview

`IPipelineBuilder` interfaces allow creating pipelines with different parameters and return result. Every pipeline is configured by adding steps. Steps are executed in the same order they were added using pipeline builders. It is possible to execute steps conditionally - see [Add steps and execute conditionally](#add-steps-and-execute-conditionally).

`IPipelineBuilderAsync` interfaces have same functionality as `IPipelineBuilder` ones, but force creating the pipelines which return `Task`-based results (asynchronous). This allows using asynchronous conditions for steps execution - see [Async conditions](#async-conditions).

<br />

## Create pipeline builders

### `Constructor`

Any pipeline builder can be created using the constructor:

```cs
var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>();

var pipelineBuilderAsync = new PipelineBuilderAsync<TestArg, CancellationToken, Task<TestResult>>();
```

It is possible to pass `IServiceProvider` instance as a constructor argument:

```cs
var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

var pipelineBuilderAsync = new PipelineBuilderAsync<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);
```

<br/>

### `Create`

`IPipelineBuilderFactory` and `IPipelineBuilderAsyncFactory` can be used to create instances of pipeline builders:

```cs
var pipelineBuilderFactory = new PipelineBuilderFactory();

// using generic version of `Create` method
var pipelineBuilder = pipelineBuilderFactory.Create<TestArg, CancellationToken, Task<TestResult>>();

// using non-generic version of the `Create` method with type variable:
var anotherPipelineBuilder = pipelineBuilderFactory.Create(typeof(PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>));


// async

var pipelineBuilderAsyncFactory = new PipelineBuilderAsyncFactory();

// using generic version of `Create` method
var pipelineBuilderAsync = pipelineBuilderAsyncFactory.Create<TestArg, CancellationToken, Task<TestResult>>();

// using non-generic version of the `Create` method with type variable:
var anotherPipelineBuilderAsync = pipelineBuilderAsyncFactory.Create(typeof(PipelineBuilderAsync<TestArg, CancellationToken, Task<TestResult>>));
```

<br/>

`Create` methods accept `constructorArg` which are used to create pipeline builder.

It is possible to create pipeline builders with `IServiceProvider` by passing the instance to `Create` methods:

```cs
var pipelineBuilderFactory = new PipelineBuilderFactory();

// using generic version of `Create` method
var pipelineBuilder = pipelineBuilderFactory.Create<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

// using non-generic version of the `Create` method with type variable:
var anotherPipelineBuilder = pipelineBuilderFactory.Create(typeof(PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>), serviceProvider);


// async

var pipelineBuilderAsyncFactory = new PipelineBuilderAsyncFactory();

// using generic version of `Create` method
var pipelineBuilderAsync = pipelineBuilderAsyncFactory.Create<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

// using non-generic version of the `Create` method with type variable:
var anotherPipelineBuilderAsync = pipelineBuilderAsyncFactory.Create(typeof(PipelineBuilderAsync<TestArg, CancellationToken, Task<TestResult>>), serviceProvider);
 ```

<br/>

### `Copy`

`Copy` creates a new pipeline builder instance using the configuration (components/steps, target, service provider) of the source pipeline builder.

It is useful when pipeline builders are very similar, so there is no need to create a new pipeline builder from scratch. `Copy` returns a new pipeline builder instance and it can be configured right after calling this method:

```cs
var serviceProvider = new ServiceCollection()
    .AddTransient<TestStep>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder
    .Use<TestStep>()
    .UseTarget(async (param, cancellationToken) => await Task.FromResult(new TestResult()));

var pipelineBuilderCopy = pipelineBuilder
    .Copy() // returns new pipeline builder
    .Use    // adds new component/step to copied pipeline builder without affecting the source one
    (
        next => async (param, cancellationToken) =>
        {
            // do some stuff

            var nextStepResult = await next.Invoke(param, cancellationToken);

            // do some stuff

            return nextStepResult;
        }
    );
 ```

<br/>

## Add steps

Every pipeline executes steps in the order these steps were added to the pipeline.

<br/>

### `Use` component

A component is the delegate which accepts one parameter (which is the next step of the pipeline) and returns the delegate containing the current step logic.

`Use` method can be used to add pipeline step using the component:

```cs
pipelineBuilder.Use
(
    next => async (param, cancellationToken) =>
    {
        // do some stuff

        var nextStepResult = await next.Invoke(param, cancellationToken);

        // do some stuff

        return nextStepResult;
    }
);
```

<br/>

### `Use` interface implementation

When pipeline steps require some external dependencies the new types implementing `IPipelineStep` interfaces should be created (internally this method uses `Use` passing the component, which is created from the interface method):


```cs
public class TestStep : IPipelineStep<TestArg, CancellationToken, Task<TestResult>>
{
    // use constructor to inject needed dependencies

    public async Task<TestResult> Invoke(TestArg param, CancellationToken cancellationToken, Func<TestArg, CancellationToken, Task<TestResult>> next)
    {
        // do some stuff

        var nextStepResult = await next.Invoke(param, cancellationToken);

        // do some stuff

        return nextStepResult;
    }
}
```

<br/>

There are several `Use` method overloads to add pipeline step using interface implementation:

`Use` method with generic type parameter can be used if step instance can be provided by `IServiceProvider`:

```cs
var serviceProvider = new ServiceCollection()
    .AddTransient<TestStep>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder.Use<TestStep>();
```

<br/>

`Use` method with custom factory parameter. The factory provides instances of the pipeline step:

```cs
var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>();

pipelineBuilder.Use(() => new TestStep());
```

<br/>

`Use` method with custom factory accepting `IServiceProvider`. The factory provides instances of the pipeline step:

```cs
var serviceProvider = new ServiceCollection()
    .AddTransient<TestStep>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder.Use(sp => sp.GetRequiredService<TestStep>());
```

<br/>

## Add steps and execute conditionally

It is possible to add steps which are executed only when some condition is met.

<br/>

### `UseIf`

`UseIf` is used to create new branches of the pipeline with conditional steps execution and execute these steps only when condition is met. The branch is rejoined to the main pipeline.

If condition is met the branch steps are executed and then next step of the main pipeline is executed.  

In case the condition is NOT met these steps are skipped and next step in the main pipeline is executed. 

<br/>

Branch pipeline can be configured using the configuration delegate.

<br />

Example:

```cs
var serviceProvider = new ServiceCollection()
    .AddPipelines()
    .AddTransient<TestStep>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder
    .UseIf
    (
        (param, cancellationToken) => param.Property > 10,
        branchBuilderConfiguration =>
            branchBuilderConfiguration
                .Use<TestStep>()
                .Use
                (
                    next => async (param, cancellationToken) =>
                    {
                        // do some stuff

                        var nextStepResult = await next.Invoke(param, cancellationToken);

                        // do some stuff

                        return nextStepResult;
                    }
                )
    );
```

<br/>

### `UseBranchIf`

`UseBranchIf` is used to create new branches of the pipeline with conditional steps execution and execute these steps only when condition is met. The branch is NOT rejoined to the main pipeline. Every branch should have own `Target` set;

If condition is met the branch steps are executed. This branch is not rejoined to the main pipeline, so steps in main pipeline won't be executed. 

In case the condition is not met the step is skipped and next step in pipeline is executed. The `IServiceProvider` and `IPipelineBuilderFactory` are used to get instance of branch pipeline builder.

<br/>

Branch pipeline can be configured using the configuration delegate.

The `Target` should be set for branch pipeline.

<br />

Example:

```cs
var serviceProvider = new ServiceCollection()
    .AddPipelines()
    .AddTransient<TestStep>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder
    .UseBranchIf
    (
        (param, cancellationToken) => param.Property > 10,
        branchBuilderConfiguration =>
            branchBuilderConfiguration
                .Use<TestStep>()
                .Use
                (
                    next => async (param, cancellationToken) =>
                    {
                        // do some stuff

                        var nextStepResult = await next.Invoke(param, cancellationToken);

                        // do some stuff

                        return nextStepResult;
                    }
                )
                .UseTarget(async (param, cancellationToken) => await Task.FromResult(new TestResult())) // target
    );
```

<br/>

### Condition interfaces

If condition has some external dependencies the implementations of `IPipelineCondition` interfaces should be used:

```cs
var serviceProvider = new ServiceCollection()
    .AddPipelines()
    .AddTransient<TestStep>()
    .AddTransient<TestCondition>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder
    .UseIf<TestCondition>
    (
        null, // condition factory should be null if condition is got from service provider
        branchBuilderConfiguration =>
            branchBuilderConfiguration.Use<TestStep>()
    );
```

<br />

and the condition implementation: 

```cs
public class TestCondition : IPipelineCondition<TestArg, CancellationToken>
{
    // use constructor to inject needed dependencies

    public bool Invoke(TestArg input, CancellationToken cancellationToken)
    {
        // do some stuff

        return result;
    }
}
```

<br />

### Custom factories for pipeline conditions

It is possible to pass custom condition factory which provides the condition instance.

<br/>

Using factory without `IServiceProvider`:

```cs
pipelineBuilder
    .UseIf
    (
        () => new TestCondition(),
        builder =>
            builder.Use
            (
                next => async (param, cancellationToken) =>
                {
                    // do some stuff

                    var nextStepResult = await next.Invoke(param, cancellationToken);

                    // do some stuff

                    return nextStepResult;
                }
            ),
        () => new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>()
    );
```

<br/>

Using factory with `IServiceProvider` :

```cs
var serviceProvider = new ServiceCollection()
    .AddTransient<TestCondition>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder
    .UseIf
    (
        sp => sp.GetRequiredService<TestCondition>(),
        builder =>
            builder.Use
            (
                next => async (param, cancellationToken) =>
                {
                    // do some stuff

                    var nextStepResult = await next.Invoke(param, cancellationToken);

                    // do some stuff

                    return nextStepResult;
                }
            ),
        sp => new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>()
    );
```

<br />

### Custom factories for branch pipeline builders

It is possible to pass custom branch builder factory which provides the branch pipeline builder instance.

<br/>

Using factory without `IServiceProvider`:

```cs
pipelineBuilder
    .UseIf
    (
        (param, cancellationToken) => param.Property > 10,
        builder =>
            builder.Use
            (
                next => async (param, cancellationToken) =>
                {
                    // do some stuff

                    var nextStepResult = await next.Invoke(param, cancellationToken);

                    // do some stuff

                    return nextStepResult;
                }
            ),
        () => new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>()
    );
```

<br/>

Using factory with `IServiceProvider`:

```cs
var serviceProvider = new ServiceCollection()
    .AddTransient<IPipelineBuilder<TestArg, CancellationToken, Task<TestResult>>, PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder
    .UseBranchIf
    (
        (param, cancellationToken) => param.Property > 10,
        builder =>
            builder.Use
            (
                next => async (param, cancellationToken) =>
                {
                    // do some stuff

                    var nextStepResult = await next.Invoke(param, cancellationToken);

                    // do some stuff

                    return nextStepResult;
                }
            ).UseTarget((param, cancellationToken) => Task.FromResult(new TestResult())),
        sp => sp.GetRequiredService<IPipelineBuilder<TestArg, CancellationToken, Task<TestResult>>>()
    );
```

<br/>

Using factory without `IServiceProvider` - if interface is used to add pipeline step the `IServiceProvider` should be set for branch pipeline builder. Factory is used only for providing instances of branch pipeline builders:

```cs
var serviceProvider = new ServiceCollection()
    .AddTransient<TestStep>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>();

pipelineBuilder
    .UseIf
    (
        (param, cancellationToken) => param.Property > 10,
        branchBuilderConfiguration =>
            branchBuilderConfiguration.Use<TestStep>(), // service provider is needed to get step instance
        () => new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>
            (serviceProvider) // service provider should be passed to the branch pipeline builder
    );
```

<br />

### Async conditions

When using `IPipelineBuilderAsync` interfaces the async condition should be used - there are `IPipelineConditionAsync` interfaces:

```cs
public class TestConditionAsync : IPipelineConditionAsync<TestArg, CancellationToken>
{
    // use constructor to inject needed dependencies

    public async Task<bool> InvokeAsync(TestArg input, CancellationToken cancellationToken)
    {
        // do some stuff

        return result;
    }
}
```

<br />

and usage:

```cs
var serviceProvider = new ServiceCollection()
    .AddPipelines()
    .AddTransient<TestStep>()
    .AddTransient<TestConditionAsync>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilderAsync<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder
    .UseIf<TestConditionAsync>
    (
        null,
        branchBuilderConfiguration =>
            branchBuilderConfiguration.Use<TestStep>()
    );
```

<br />

## Set the pipeline target

Target should be set for every pipeline or exception will be thrown during building of the pipeline. 

The target is the last step executed by the pipeline. 

<br/>

### `UseTarget`

`UseTarget` is used to set the target of the pipeline. 

<br/>

Using simple target from delegate:

```cs
var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>();

// pipeline configuration (steps)

pipelineBuilder.UseTarget(async (param, cancellationToken) => await Task.FromResult(new TestResult()));
```

<br/>

Or some service method can be set as pipeline target:

```cs
var testService = new TestService();

var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>();

// pipeline configuration (steps)

pipelineBuilder.UseTarget(testService.Execute);
```

Example of `TestService` implementation:

```cs
public class TestService
{
    public async Task<TestResult> Execute(TestArg param, CancellationToken cancellationToken) => await Task.FromResult(new TestResult());
}
```

<br />

Note: `UseTarget` can be called multiple times and the next call will rewrite the target which has been set before.

<br/>

## Building the pipeline

After configuration is done using `PipelineBuilder` to use the pipeline it should be built.

<br/>

### `Build`

`Build` method builds the pipeline using configured `PipelineBuilder`.

<br/>

Building the pipeline:

```cs
var serviceProvider = new ServiceCollection()
    .AddPipelines()
    .AddTransient<TestStep>()
    .BuildServiceProvider();


var pipelineBuilder = new PipelineBuilder<TestArg, CancellationToken, Task<TestResult>>(serviceProvider);

pipelineBuilder
    .Use<TestStep>()
    .UseIf
    (
        (param, cancellationToken) => param.Property > 0,
        branchBuilder => branchBuilder.Use
        (
            next => async (param, cancellationToken) =>
            {
                Console.WriteLine(param.Property);

                var nextResult = await next.Invoke(param, cancellationToken);

                nextResult.Property *= 10;

                return nextResult;
            }
        )
    )
    .UseTarget
    (
        async (param, cancellationToken) => await Task.FromResult
        (
            new TestResult()
            {
                Property = param.Property * param.Property
            }
        )
    );

var pipeline = pipelineBuilder.Build();

var result = await pipeline.Invoke(new TestArg() { Property = 10 }, CancellationToken.None);
```

<br />

The target of the pipeline can be set using `Build` method (the existing target of the pipeline will be rewritten):

```cs
var pipeline = pipelineBuilder.Build((param, cancellationToken) => Task.FromResult(new TestResult()));
```