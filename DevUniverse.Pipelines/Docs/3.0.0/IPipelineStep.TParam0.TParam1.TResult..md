#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Steps](Pipelines.md#DevUniverse.Pipelines.Core.Steps 'DevUniverse.Pipelines.Core.Steps')
## IPipelineStep&lt;TParam0,TParam1,TResult&gt; Interface
The pipeline step with 2 parameters which returns the result.  
```csharp
public interface IPipelineStep<TParam0,TParam1,TResult> :
DevUniverse.Pipelines.Core.Steps.Shared.IPipelineStepBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TResult'></a>
`TResult`  
The type of the result.
  

Implements [IPipelineStepBasic](IPipelineStepBasic.md 'DevUniverse.Pipelines.Core.Steps.Shared.IPipelineStepBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..Invoke(TParam0.TParam1.System.Func.TParam0.TParam1.TResult.)'></a>
## IPipelineStep&lt;TParam0,TParam1,TResult&gt;.Invoke(TParam0, TParam1, Func&lt;TParam0,TParam1,TResult&gt;) Method
Executes the logic of the step.  
```csharp
TResult Invoke(TParam0 param0, TParam1 param1, System.Func<TParam0,TParam1,TResult> next);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..Invoke(TParam0.TParam1.System.Func.TParam0.TParam1.TResult.).param0'></a>
`param0` [TParam0](IPipelineStep.TParam0.TParam1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TParam0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TResult&gt;.TParam0')  
The 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..Invoke(TParam0.TParam1.System.Func.TParam0.TParam1.TResult.).param1'></a>
`param1` [TParam1](IPipelineStep.TParam0.TParam1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TParam1 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TResult&gt;.TParam1')  
The 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..Invoke(TParam0.TParam1.System.Func.TParam0.TParam1.TResult.).next'></a>
`next` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3 'System.Func`3')[TParam0](IPipelineStep.TParam0.TParam1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TParam0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TResult&gt;.TParam0')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3 'System.Func`3')[TParam1](IPipelineStep.TParam0.TParam1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TParam1 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TResult&gt;.TParam1')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3 'System.Func`3')[TResult](IPipelineStep.TParam0.TParam1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3 'System.Func`3')  
The next step in the pipeline which can be executed after this one.
  
#### Returns
[TResult](IPipelineStep.TParam0.TParam1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TResult&gt;.TResult')  
The result of the step execution.
  
