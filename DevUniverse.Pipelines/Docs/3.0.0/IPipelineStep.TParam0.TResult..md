#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Steps](Pipelines.md#DevUniverse.Pipelines.Core.Steps 'DevUniverse.Pipelines.Core.Steps')
## IPipelineStep&lt;TParam0,TResult&gt; Interface
The pipeline step with 1 parameter which returns the result.  
```csharp
public interface IPipelineStep<TParam0,TResult> :
DevUniverse.Pipelines.Core.Steps.Shared.IPipelineStepBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..TParam0'></a>
`TParam0`  
The type of the parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..TResult'></a>
`TResult`  
The type of the result.
  

Implements [IPipelineStepBasic](IPipelineStepBasic.md 'DevUniverse.Pipelines.Core.Steps.Shared.IPipelineStepBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..Invoke(TParam0.System.Func.TParam0.TResult.)'></a>
## IPipelineStep&lt;TParam0,TResult&gt;.Invoke(TParam0, Func&lt;TParam0,TResult&gt;) Method
Executes the logic of the step.  
```csharp
TResult Invoke(TParam0 param0, System.Func<TParam0,TResult> next);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..Invoke(TParam0.System.Func.TParam0.TResult.).param0'></a>
`param0` [TParam0](IPipelineStep.TParam0.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..TParam0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TResult&gt;.TParam0')  
The parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..Invoke(TParam0.System.Func.TParam0.TResult.).next'></a>
`next` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TParam0](IPipelineStep.TParam0.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..TParam0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TResult&gt;.TParam0')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TResult](IPipelineStep.TParam0.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The next step in the pipeline which can be executed after this one.
  
#### Returns
[TResult](IPipelineStep.TParam0.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TResult&gt;.TResult')  
The result of the step execution.
  
