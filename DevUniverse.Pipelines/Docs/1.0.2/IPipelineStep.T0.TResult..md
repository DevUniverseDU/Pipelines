#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Steps](Pipelines.md#DevUniverse.Pipelines.Core.Steps 'DevUniverse.Pipelines.Core.Steps')
## IPipelineStep&lt;T0,TResult&gt; Interface
The pipeline step with 1 input parameter which returns the result.  
```csharp
public interface IPipelineStep<T0,TResult> :
DevUniverse.Pipelines.Core.Steps.IPipelineStep
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..T0'></a>
`T0`  
The type of the parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..TResult'></a>
`TResult`  
The type of the result.
  

Implements [IPipelineStep](IPipelineStep.md 'DevUniverse.Pipelines.Core.Steps.IPipelineStep')  
### Methods
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..Invoke(T0.System.Func.T0.TResult.)'></a>
## IPipelineStep&lt;T0,TResult&gt;.Invoke(T0, Func&lt;T0,TResult&gt;) Method
Executes the logic of the step.  
```csharp
TResult Invoke(T0 param0, System.Func<T0,TResult> next);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..Invoke(T0.System.Func.T0.TResult.).param0'></a>
`param0` [T0](IPipelineStep.T0.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..T0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,TResult&gt;.T0')  
The parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..Invoke(T0.System.Func.T0.TResult.).next'></a>
`next` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[T0](IPipelineStep.T0.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..T0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,TResult&gt;.T0')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TResult](IPipelineStep.T0.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The next step in the pipeline which can be executed after this one.
  
#### Returns
[TResult](IPipelineStep.T0.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,TResult&gt;.TResult')  
The result of the step execution.
  
