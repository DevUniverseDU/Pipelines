#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Steps](Pipelines.md#DevUniverse.Pipelines.Core.Steps 'DevUniverse.Pipelines.Core.Steps')
## IPipelineStep&lt;TResult&gt; Interface
The pipeline step without input parameters which returns the result.  
```csharp
public interface IPipelineStep<TResult> :
DevUniverse.Pipelines.Core.Steps.IPipelineStep
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..TResult'></a>
`TResult`  
The type of the result.
  

Implements [IPipelineStep](IPipelineStep.md 'DevUniverse.Pipelines.Core.Steps.IPipelineStep')  
### Methods
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..Invoke(System.Func.TResult.)'></a>
## IPipelineStep&lt;TResult&gt;.Invoke(Func&lt;TResult&gt;) Method
Executes the logic of the step.  
```csharp
TResult Invoke(System.Func<TResult> next);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..Invoke(System.Func.TResult.).next'></a>
`next` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineStep.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The next step in the pipeline which can be executed after this one.
  
#### Returns
[TResult](IPipelineStep.TResult..md#DevUniverse.Pipelines.Core.Steps.IPipelineStep.TResult..TResult 'DevUniverse.Pipelines.Core.Steps.IPipelineStep&lt;TResult&gt;.TResult')  
The result of the step execution.
  
