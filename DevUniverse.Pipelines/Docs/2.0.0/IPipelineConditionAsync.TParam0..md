#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Conditions](Pipelines.md#DevUniverse.Pipelines.Core.Conditions 'DevUniverse.Pipelines.Core.Conditions')
## IPipelineConditionAsync&lt;TParam0&gt; Interface
The async pipeline condition with 1 parameter.  
```csharp
public interface IPipelineConditionAsync<in TParam0> :
DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsyncBasic,
DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0..TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  

Implements [IPipelineConditionAsyncBasic](IPipelineConditionAsyncBasic.md 'DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsyncBasic'), [IPipelineConditionBasic](IPipelineConditionBasic.md 'DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0..InvokeAsync(TParam0)'></a>
## IPipelineConditionAsync&lt;TParam0&gt;.InvokeAsync(TParam0) Method
Executes the logic of the condition.  
```csharp
System.Threading.Tasks.Task<bool> InvokeAsync(TParam0 param0);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0..InvokeAsync(TParam0).param0'></a>
`param0` [TParam0](IPipelineConditionAsync.TParam0..md#DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0..TParam0 'DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync&lt;TParam0&gt;.TParam0')  
The 1st parameter.
  
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the condition is met, otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').
  
