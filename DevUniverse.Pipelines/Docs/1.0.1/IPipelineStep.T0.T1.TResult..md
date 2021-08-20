#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Steps](Pipelines.md#DevUniverse.Pipelines.Core.Steps 'DevUniverse.Pipelines.Core.Steps')
## IPipelineStep&lt;T0,T1,TResult&gt; Interface
The pipeline step with 2 input parameters which returns the result.  
```csharp
public interface IPipelineStep<T0,T1,TResult> :
DevUniverse.Pipelines.Core.Steps.IPipelineStep
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..T0'></a>
`T0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..T1'></a>
`T1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..TResult'></a>
`TResult`  
The type of the result.
  

Implements [IPipelineStep](IPipelineStep.md 'DevUniverse.Pipelines.Core.Steps.IPipelineStep')  
### Methods
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..Invoke(T0.T1.System.Func.T0.T1.TResult.)'></a>
## IPipelineStep&lt;T0,T1,TResult&gt;.Invoke(T0, T1, Func&lt;T0,T1,TResult&gt;) Method
Executes the logic of the step.  
```csharp
TResult Invoke(T0 param0, T1 param1, System.Func<T0,T1,TResult> next);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..Invoke(T0.T1.System.Func.T0.T1.TResult.).param0'></a>
`param0` [T0](IPipelineStep.T0.T1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..T0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,T1,TResult&gt;.T0')  
The 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..Invoke(T0.T1.System.Func.T0.T1.TResult.).param1'></a>
`param1` [T1](IPipelineStep.T0.T1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..T1 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,T1,TResult&gt;.T1')  
The 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..Invoke(T0.T1.System.Func.T0.T1.TResult.).next'></a>
`next` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3 'System.Func`3')[T0](IPipelineStep.T0.T1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..T0 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,T1,TResult&gt;.T0')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3 'System.Func`3')[T1](IPipelineStep.T0.T1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..T1 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,T1,TResult&gt;.T1')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3 'System.Func`3')[TResult](IPipelineStep.T0.T1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,T1,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-3 'System.Func`3')  
The next step in the pipeline which can be executed after this one.
  
#### Returns
[TResult](IPipelineStep.T0.T1.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.T0.T1.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;T0,T1,TResult&gt;.TResult')  
The result of the step execution.
  
