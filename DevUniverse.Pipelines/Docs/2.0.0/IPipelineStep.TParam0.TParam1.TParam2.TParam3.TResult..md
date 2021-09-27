#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Shared.Steps](Pipelines.md#DevUniverse.Pipelines.Core.Shared.Steps 'DevUniverse.Pipelines.Core.Shared.Steps')
## IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt; Interface
The pipeline step with 4 parameters which returns the result.  
```csharp
public interface IPipelineStep<TParam0,TParam1,TParam2,TParam3,TResult> :
DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStepBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam2'></a>
`TParam2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam3'></a>
`TParam3`  
The type of the 4th parameter.
  
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TResult'></a>
`TResult`  
The type of the result.
  

Implements [IPipelineStepBasic](IPipelineStepBasic.md 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStepBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..Invoke(TParam0.TParam1.TParam2.TParam3.System.Func.TParam0.TParam1.TParam2.TParam3.TResult.)'></a>
## IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.Invoke(TParam0, TParam1, TParam2, TParam3, Func&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;) Method
Executes the logic of the step.  
```csharp
TResult Invoke(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3, System.Func<TParam0,TParam1,TParam2,TParam3,TResult> next);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..Invoke(TParam0.TParam1.TParam2.TParam3.System.Func.TParam0.TParam1.TParam2.TParam3.TResult.).param0'></a>
`param0` [TParam0](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam0 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TParam0')  
The 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..Invoke(TParam0.TParam1.TParam2.TParam3.System.Func.TParam0.TParam1.TParam2.TParam3.TResult.).param1'></a>
`param1` [TParam1](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam1 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TParam1')  
The 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..Invoke(TParam0.TParam1.TParam2.TParam3.System.Func.TParam0.TParam1.TParam2.TParam3.TResult.).param2'></a>
`param2` [TParam2](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam2 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TParam2')  
The 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..Invoke(TParam0.TParam1.TParam2.TParam3.System.Func.TParam0.TParam1.TParam2.TParam3.TResult.).param3'></a>
`param3` [TParam3](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam3 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TParam3')  
The 4th parameter.
  
<a name='DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..Invoke(TParam0.TParam1.TParam2.TParam3.System.Func.TParam0.TParam1.TParam2.TParam3.TResult.).next'></a>
`next` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-5 'System.Func`5')[TParam0](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam0 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TParam0')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-5 'System.Func`5')[TParam1](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam1 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TParam1')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-5 'System.Func`5')[TParam2](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam2 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TParam2')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-5 'System.Func`5')[TParam3](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TParam3 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TParam3')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-5 'System.Func`5')[TResult](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-5 'System.Func`5')  
The next step in the pipeline which can be executed after this one.
  
#### Returns
[TResult](IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..md#DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep.TParam0.TParam1.TParam2.TParam3.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Steps.IPipelineStep&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;.TResult')  
The result of the step execution.
  
