#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Builders](Pipelines.md#DevUniverse.Pipelines.Core.Builders 'DevUniverse.Pipelines.Core.Builders')
## IPipelineBuilder&lt;TResult&gt; Interface
The pipeline builder without input parameters which returns the result.  
```csharp
public interface IPipelineBuilder<TResult> :
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult'></a>
`TResult`  
The type of the result.
  

Implements [IPipelineBuilder](IPipelineBuilder.md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder')  
### Properties
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Target'></a>
## IPipelineBuilder&lt;TResult&gt;.Target Property
The target (terminating step) of the pipeline.  
```csharp
System.Func<TResult> Target { get; }
```
#### Property Value
[System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')
  
### Methods
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Build()'></a>
## IPipelineBuilder&lt;TResult&gt;.Build() Method
Builds the pipeline.  
```csharp
System.Func<TResult> Build();
```
#### Returns
[System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The pipeline delegate which is the start of the pipeline.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Copy()'></a>
## IPipelineBuilder&lt;TResult&gt;.Copy() Method
Creates the new instance of the pipeline builder with same configuration (components/steps and target) as the current instance.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Copy();
```
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The new instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use(System.Func.DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.Use(Func&lt;IPipelineStep&lt;TResult&gt;&gt;) Method
Add the component from the pipeline step interface implementation.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Use(System.Func<DevUniverse.Pipelines.Core.Steps.IPipelineStep<TResult>> factory);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use(System.Func.DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..).factory'></a>
`factory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;](IPipelineStep.TResult..md 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineStep.TResult..md 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The factory which provides the pipeline step instance.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use(System.Func.System.Func.TResult..System.Func.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.Use(Func&lt;Func&lt;TResult&gt;,Func&lt;TResult&gt;&gt;) Method
Adds the component.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Use(System.Func<System.Func<TResult>,System.Func<TResult>> component);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use(System.Func.System.Func.TResult..System.Func.TResult..).component'></a>
`component` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The component.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use(System.Func.System.Func.TResult..TResult.)'></a>
## IPipelineBuilder&lt;TResult&gt;.Use(Func&lt;Func&lt;TResult&gt;,TResult&gt;) Method
Adds the component created from the handler.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Use(System.Func<System.Func<TResult>,TResult> handler);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use(System.Func.System.Func.TResult..TResult.).handler'></a>
`handler` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The handler.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use(System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.Use(Func&lt;IServiceProvider,IPipelineStep&lt;TResult&gt;&gt;) Method
Add the component from the pipeline step interface implementation.  
Requires the service provider to be set.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Use(System.Func<System.IServiceProvider,DevUniverse.Pipelines.Core.Steps.IPipelineStep<TResult>> factory);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use(System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..).factory'></a>
`factory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;](IPipelineStep.TResult..md 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineStep.TResult..md 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The factory which provides the pipeline step instance.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use.TPipelineStep.()'></a>
## IPipelineBuilder&lt;TResult&gt;.Use&lt;TPipelineStep&gt;() Method
Add the component from the pipeline step interface implementation.  
Requires the service provider to be set.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Use<TPipelineStep>()
    where TPipelineStep : DevUniverse.Pipelines.Core.Steps.IPipelineStep<TResult>;
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..Use.TPipelineStep.().TPipelineStep'></a>
`TPipelineStep`  
The type of the pipeline step.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.UseBranchIf(Func&lt;bool&gt;, Action&lt;IPipelineBuilder&lt;TResult&gt;&gt;, Func&lt;IPipelineBuilder&lt;TResult&gt;&gt;) Method
Adds the pipeline component to the pipeline.  
If condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.  
If condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> UseBranchIf(System.Func<bool> predicate, System.Action<DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> configuration, System.Func<DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> factory);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).predicate'></a>
`predicate` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The predicate which determines if the added pipeline component should be executed.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).configuration'></a>
`configuration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).factory'></a>
`factory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The factory which provides the branch builder instance.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.UseBranchIf(Func&lt;bool&gt;, Action&lt;IPipelineBuilder&lt;TResult&gt;&gt;, Func&lt;IServiceProvider,IPipelineBuilder&lt;TResult&gt;&gt;) Method
Adds the pipeline component to the pipeline.  
If condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.  
If condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
Requires the service provider to be set.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> UseBranchIf(System.Func<bool> predicate, System.Action<DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> configuration, System.Func<System.IServiceProvider,DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> factory);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).predicate'></a>
`predicate` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The predicate which determines if the added pipeline component should be executed.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).configuration'></a>
`configuration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).factory'></a>
`factory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The factory which provides the branch builder instance.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.UseBranchIf(Func&lt;bool&gt;, Action&lt;IPipelineBuilder&lt;TResult&gt;&gt;) Method
Adds the pipeline component to the pipeline.  
If condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.  
If condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
Requires the service provider to be set.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> UseBranchIf(System.Func<bool> predicate, System.Action<DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> configuration);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).predicate'></a>
`predicate` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The predicate which determines if the added pipeline component should be executed.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseBranchIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).configuration'></a>
`configuration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.UseIf(Func&lt;bool&gt;, Action&lt;IPipelineBuilder&lt;TResult&gt;&gt;, Func&lt;IPipelineBuilder&lt;TResult&gt;&gt;) Method
Adds the pipeline component to the pipeline.  
If condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.  
If condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> UseIf(System.Func<bool> predicate, System.Action<DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> configuration, System.Func<DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> factory);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).predicate'></a>
`predicate` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The predicate which determines if the added pipeline component should be executed.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).configuration'></a>
`configuration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).factory'></a>
`factory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The factory which provides the branch builder instance.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.UseIf(Func&lt;bool&gt;, Action&lt;IPipelineBuilder&lt;TResult&gt;&gt;, Func&lt;IServiceProvider,IPipelineBuilder&lt;TResult&gt;&gt;) Method
Adds the pipeline component to the pipeline.  
If condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.  
If condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
Requires the service provider to be set.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> UseIf(System.Func<bool> predicate, System.Action<DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> configuration, System.Func<System.IServiceProvider,DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> factory);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).predicate'></a>
`predicate` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The predicate which determines if the added pipeline component should be executed.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).configuration'></a>
`configuration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult...System.Func.System.IServiceProvider.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).factory'></a>
`factory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The factory which provides the branch builder instance.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..)'></a>
## IPipelineBuilder&lt;TResult&gt;.UseIf(Func&lt;bool&gt;, Action&lt;IPipelineBuilder&lt;TResult&gt;&gt;) Method
Adds the pipeline component to the pipeline.  
If condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.  
If condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
Requires the service provider to be set.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> UseIf(System.Func<bool> predicate, System.Action<DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult>> configuration);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).predicate'></a>
`predicate` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The predicate which determines if the added pipeline component should be executed.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseIf(System.Func.bool..System.Action.DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..).configuration'></a>
`configuration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseTarget(System.Func.TResult.)'></a>
## IPipelineBuilder&lt;TResult&gt;.UseTarget(Func&lt;TResult&gt;) Method
Sets the pipeline target.  
The target is the last (terminating) step of the pipeline.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> UseTarget(System.Func<TResult> target);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..UseTarget(System.Func.TResult.).target'></a>
`target` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The target.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilder.TResult..md#DevUniverse.Pipelines.Core.Builders.IPipelineBuilder.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;.TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The current instance of the pipeline builder.
  
