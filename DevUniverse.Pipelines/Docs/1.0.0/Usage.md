<!-- omit in toc -->
# Usage examples

<!-- omit in toc -->
## Table of contents

- [Create pipeline builders](#create-pipeline-builders)
  - [`Constructor`](#constructor)
  - [`Create`](#create)
- [Add steps](#add-steps)
  - [`Use` component](#use-component)
  - [`Use` handler](#use-handler)
  - [`Use` interface implementation](#use-interface-implementation)
- [Add steps and execute conditionally](#add-steps-and-execute-conditionally)
  - [`UseIf`](#useif)
  - [`UseBranchIf`](#usebranchif)
  - [Custom factories](#custom-factories)
- [Set the pipeline target](#set-the-pipeline-target)
  - [`UseTarget`](#usetarget)
- [Building the pipeline](#building-the-pipeline)
  - [`Build`](#build)

<br/>

## Create pipeline builders

### `Constructor`

Any pipeline builder can be created using the constructor:

```cs
var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();
```

It is possible to pass `IServiceProvider` instance as a constructor argument:

```cs
var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider);
```

<br/>

### `Create`

`IPipelineBuilderFactory` can be used to create instances of pipeline builders:

```cs
var pipelineBuilderFactory = new PipelineBuilderFactory();

// using generic version of `Create` method
var pipelineBuilder = pipelineBuilderFactory.Create<SomeInput, CancellationToken, Task<SomeResult>>();

// using non-generic version of the `Create` method with type variable:
var anotherPipelineBuilder = pipelineBuilderFactory.Create(typeof(PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>));
```

The generic versions of `Create` methods returns the `interface` while the non-generic version returns the actual implementation (`class`).

<br/>

`Create` methods accept `constructorArg` which are used to create pipeline builder.

It is possible to create pipeline builders with `IServiceProvider` by passing the instance to `Create` methods:

```cs
var pipelineBuilderFactory = new PipelineBuilderFactory();

// using generic version of `Create` method
var pipelineBuilder = pipelineBuilderFactory.Create<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider);

// using non-generic version of the `Create` method with type variable:
var anotherPipelineBuilder = pipelineBuilderFactory.Create(typeof(PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>), serviceProvider);
 ```

<br/>

## Add steps

Every pipeline executes steps in the order these steps were added to the pipeline.

<br/>

### `Use` component

A component is the delegate which accepts one parameter (which is the next step of the pipeline) and returns the delegate containing the actual step logic.

`Use` method can be used to add pipeline step using the component:

```cs
var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();

pipelineBuilder.Use
(
    next => async (input, cancellationToken) =>
    {
        // do some stuff

        var nextStepResult = await next.Invoke(input, cancellationToken);

        // do some stuff

        return nextStepResult;
    }
);
```

<br/>

### `Use` handler

A handler can be used to add simple pipeline step. The code is simpler in comparison with using component (internally this method uses `Use` passing the component).

Under the hood this a component is created from this handler.

`Use` method can be used to add pipeline step using handler:

```cs
var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();

pipelineBuilder.Use
(
    async (input, cancellationToken, next) =>
    {
        // do some stuff

        var nextStepResult = await next.Invoke(input, cancellationToken);

        // do some stuff

        return nextStepResult;
    }
);
```

<br/>

### `Use` interface implementation

When pipeline step requires some external dependencies the new type implementing `IPipelineStep` interfaces should be created (internally this method uses `Use` passing the component, which is created from the interface method):


```cs
public class SomeStep : IPipelineStep<SomeInput, CancellationToken, Task<SomeResult>>
{
    // use constructor to inject needed dependencies

    public async Task<SomeResult> Invoke(SomeInput input, CancellationToken cancellationToken, Func<SomeInput, CancellationToken, Task<SomeResult>> next)
    {
        // do some stuff

        var nextStepResult = await next.Invoke(input, cancellationToken);

        // do some stuff

        return nextStepResult;
    }
}
```

<br/>

There are several `Use` method overloads to add pipeline step using interface implementation:

`Use` method with generic type parameter can be used if step instance can be provided by `IServiceProvider`:

```cs
var serviceCollection = new ServiceCollection()
    .AddTransient<SomeStep>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider);

pipelineBuilder.Use<SomeStep>();
```

<br/>

`Use` method with custom factory parameter. The factory provides instances of the pipeline step:

```cs
var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();

pipelineBuilder.Use(() => new SomeStep());
```

<br/>

`Use` method with custom factory accepting `IServiceProvider`. The factory provides instances of the pipeline step:

```cs
var serviceCollection = new ServiceCollection()
    .AddTransient<SomeStep>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider);

pipelineBuilder.Use(sp => sp.GetRequiredService<SomeStep>());
```

<br/>


Any step can skip calling the next step (i.e. some condition is not met). In that case next pipeline steps won't be executed:

```cs
var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();

pipelineBuilder.Use
(
    async (input, cancellationToken, next) =>
    {
        if (input.SomeProperty > 0)
        {
            return await next.Invoke(input, cancellationToken);
        }

        Console.WriteLine("Condition is not met. Return default result");

        return new SomeResult();
    }
);
```

<br/>

## Add steps and execute conditionally

It is possible to add steps which are executed only when some condition is met.

<br/>

### `UseIf`

`UseIf` is used to create new branches of the pipeline with conditional steps and execute these branch steps only when condition is met. The branch is rejoined to main pipeline.

If condition is met the branch steps are executed and then next steps of the main pipeline is executed.  

In case the condition is NOT met these branch steps are skipped and next step in the main pipeline is executed. 

<br/>

Branch pipeline can be configured using the configuration delegate.

<br />

Example:

```cs
var serviceCollection = new ServiceCollection()
    .AddSingleton<IPipelineBuilderFactory, PipelineBuilderFactory>()
    .AddTransient<SomeStep>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider);

pipelineBuilder
    .UseIf
    (
        (input, _) => input.SomeProperty > 10,
        branchBuilderConfiguration =>
            branchBuilderConfiguration
                .Use<SomeStep>()
                .Use
                (
                    async (input, cancellationToken, next) =>
                    {
                        // do some stuff

                        var nextStepResult = await next.Invoke(input, cancellationToken);

                        // do some stuff

                        return nextStepResult;
                    }
                )
    );
```

<br/>

### `UseBranchIf`

`UseBranchIf` is used to create new branches of the pipeline with conditional steps and execute these branch steps only when condition is met. The branch is NOT rejoined to main pipeline. Every branch should have own `Target` set;

If condition is met the branch steps are executed. This branch is not rejoined to the main pipeline, so steps in main pipeline won't be executed. 

In case the condition is not met the step is skipped and next step in pipeline is executed. The `ServiceProvider` and `IPipelineBuilderFactory` are used to get instance of branch pipeline builder.

<br/>

Branch pipeline can be configured using the configuration delegate.

The `Target` should be set for branch pipeline.

<br />

Example:

```cs
var serviceCollection = new ServiceCollection()
    .AddSingleton<IPipelineBuilderFactory, PipelineBuilderFactory>()
    .AddTransient<SomeStep>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider);

pipelineBuilder
    .UseBranchIf
    (
        (input, _) => input.SomeProperty > 10,
        branchBuilderConfiguration =>
            branchBuilderConfiguration
                .Use<SomeStep>()
                .Use
                (
                    async (input, cancellationToken, next) =>
                    {
                        // do some stuff

                        var nextStepResult = await next.Invoke(input, cancellationToken);

                        // do some stuff

                        return nextStepResult;
                    }
                )
                .UseTarget(async (input, cancellationToken) => await Task.FromResult(new SomeResult())) // target
    );
```

<br/>

### Custom factories

It is possible to pass custom factory which provides the branch pipeline builder instance.

<br/>

Using factory without `ServiceProvider`:

```cs
var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();

pipelineBuilder
    .UseIf
    (
        (input, _) => input.SomeProperty > 10,
        builder =>
            builder.Use
            (
                async (input, cancellationToken, next) =>
                {
                    // do some stuff

                    var nextStepResult = await next.Invoke(input, cancellationToken);

                    // do some stuff

                    return nextStepResult;
                }
            ),
        () => new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>()
    );
```

<br/>

Using factory with `ServiceProvider` (custom logic can be used to create branch builder):

```cs
var serviceProvider = new ServiceCollection()
    .AddTransient<IPipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>, PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>>()
    .BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider);

pipelineBuilder
    .UseBranchIf
    (
        (input, _) => input.SomeProperty > 10,
        builder =>
            builder.Use
            (
                async (input, cancellationToken, next) =>
                {
                    // do some stuff

                    var nextStepResult = await next.Invoke(input, cancellationToken);

                    // do some stuff

                    return nextStepResult;
                }
            ).UseTarget((input, ct) => Task.FromResult(new SomeResult())),
        sp => sp.GetRequiredService<IPipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>>()
    );
```

<br/>

Using factory without `ServiceProvider` - if interface is used to add pipeline step the `ServiceProvider` should be set for branch pipeline builder. Factory is used only for providing instances of branch pipeline builders:

```cs
var serviceCollection = new ServiceCollection()
    .AddTransient<SomeStep>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();

pipelineBuilder
    .UseIf
    (
        (input, _) => input.SomeProperty > 10,
        branchBuilderConfiguration =>
            branchBuilderConfiguration.Use<SomeStep>(), // service provider is needed to get step instance
        () => new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider) // service provider should be passed to the branch pipeline builder
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

Using simple target:

```cs
var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();

// pipeline configuration (steps)

pipelineBuilder.UseTarget(async (input, cancellationToken) => await Task.FromResult(new SomeResult()));
```

<br/>

Or some service method can be set as pipeline target:

```cs
var someService = new SomeService();

var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>();

// pipeline configuration (steps)

pipelineBuilder.UseTarget(someService.Execute);
```

Example of `SomeService` implementation:

```cs
public class SomeService
{
    public async Task<SomeResult> Execute(SomeInput input, CancellationToken cancellationToken) => await Task.FromResult(new SomeResult());
}
```

<br/>

## Building the pipeline

After configuration is done using `PipelineBuilder` to use the pipeline it should be built.

<br/>

### `Build`

`Build` method builds the pipeline using configured `PipelineBuilder`.

<br/>

Building the pipeline:

```cs
var serviceCollection = new ServiceCollection()
    .AddSingleton<IPipelineBuilderFactory, PipelineBuilderFactory>()
    .AddTransient<SomeStep>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var pipelineBuilder = new PipelineBuilder<SomeInput, CancellationToken, Task<SomeResult>>(serviceProvider);

pipelineBuilder
    .Use<SomeStep>()
    .UseIf
    (
        (input, _) => input.SomeProperty > 0,
        async (input, cancellationToken, next) =>
        {
            System.Console.WriteLine(input.SomeProperty);

            var nextResult = await next.Invoke(input, cancellationToken);

            nextResult.SomeProperty *= 10;

            return nextResult;
        }
    )
    .UseTarget
    (
        async (input, cancellationToken) => await Task.FromResult
        (
            new SomeResult()
            {
                SomeProperty = input.SomeProperty * input.SomeProperty
            }
        )
    );

var pipeline = pipelineBuilder.Build();

var result = await pipeline.Invoke(new SomeInput() { SomeProperty = 10 }, CancellationToken.None);
```