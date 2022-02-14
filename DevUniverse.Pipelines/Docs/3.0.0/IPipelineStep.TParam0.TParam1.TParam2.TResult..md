#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Steps](Pipelines.md#DevUniverse.Pipelines.Core.Steps 'DevUniverse.Pipelines.Core.Steps')
## IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt; Interface
The pipeline step with 3 parameters which returns the result.  
```csharp
public interface IPipelineStep<TParam0,TParam1,TParam2,TResult> :
DevUniverse.Pipelines.Core.Steps.Shared.IPipelineStepBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam2'></a>
`TParam2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TResult'></a>
`TResult`  
The type of the result.
  

Implements [IPipelineStepBasic](IPipelineStepBasic.md 'DevUniverse.Pipelines.Core.Steps.Shared.IPipelineStepBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..Invoke(TParam0.TParam1.TParam2.System.Func.TParam0.TParam1.TParam2.TResult.)'></a>
## IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.Invoke(TParam0, TParam1, TParam2, Func&lt;TParam0,TParam1,TParam2,TResult&gt;) Method
Executes the logic of the step.  
```csharp
TResult Invoke(TParam0 param0, TParam1 param1, TParam2 param2, System.Func<TParam0,TParam1,TParam2,TResult> next);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..Invoke(TParam0.TParam1.TParam2.System.Func.TParam0.TParam1.TParam2.TResult.).param0'></a>
`param0` [TParam0](IPipelineStep.TParam0.TParam1.TParam2.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.TParam0')  
The 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..Invoke(TParam0.TParam1.TParam2.System.Func.TParam0.TParam1.TParam2.TResult.).param1'></a>
`param1` [TParam1](IPipelineStep.TParam0.TParam1.TParam2.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam1 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.TParam1')  
The 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..Invoke(TParam0.TParam1.TParam2.System.Func.TParam0.TParam1.TParam2.TResult.).param2'></a>
`param2` [TParam2](IPipelineStep.TParam0.TParam1.TParam2.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam2 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.TParam2')  
The 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..Invoke(TParam0.TParam1.TParam2.System.Func.TParam0.TParam1.TParam2.TResult.).next'></a>
`next` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-4 'System.Func`4')[TParam0](IPipelineStep.TParam0.TParam1.TParam2.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.TParam0')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-4 'System.Func`4')[TParam1](IPipelineStep.TParam0.TParam1.TParam2.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam1 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.TParam1')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-4 'System.Func`4')[TParam2](IPipelineStep.TParam0.TParam1.TParam2.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TParam2 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.TParam2')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-4 'System.Func`4')[TResult](IPipelineStep.TParam0.TParam1.TParam2.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-4 'System.Func`4')  
The next step in the pipeline which can be executed after this one.
  
#### Returns
[TResult](IPipelineStep.TParam0.TParam1.TParam2.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TParam0.TParam1.TParam2.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TResult&gt;.TResult')  
The result of the step execution.
  
