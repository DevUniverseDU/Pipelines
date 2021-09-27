#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Conditions](Pipelines.md#DevUniverse.Pipelines.Core.Conditions 'DevUniverse.Pipelines.Core.Conditions')
## IPipelineConditionAsync&lt;TParam0,TParam1,TParam2,TParam3&gt; Interface
The async pipeline condition with 4 parameters.  
```csharp
public interface IPipelineConditionAsync<in TParam0,in TParam1,in TParam2,in TParam3> :
DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsyncBasic,
DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..TParam2'></a>
`TParam2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..TParam3'></a>
`TParam3`  
The type of the 4th parameter.
  

Implements [IPipelineConditionAsyncBasic](IPipelineConditionAsyncBasic.md 'DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsyncBasic'), [IPipelineConditionBasic](IPipelineConditionBasic.md 'DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..InvokeAsync(TParam0.TParam1.TParam2.TParam3)'></a>
## IPipelineConditionAsync&lt;TParam0,TParam1,TParam2,TParam3&gt;.InvokeAsync(TParam0, TParam1, TParam2, TParam3) Method
Executes the logic of the condition.  
```csharp
System.Threading.Tasks.Task<bool> InvokeAsync(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..InvokeAsync(TParam0.TParam1.TParam2.TParam3).param0'></a>
`param0` [TParam0](IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..md#DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..TParam0 'DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync&lt;TParam0,TParam1,TParam2,TParam3&gt;.TParam0')  
The 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..InvokeAsync(TParam0.TParam1.TParam2.TParam3).param1'></a>
`param1` [TParam1](IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..md#DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..TParam1 'DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync&lt;TParam0,TParam1,TParam2,TParam3&gt;.TParam1')  
The 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..InvokeAsync(TParam0.TParam1.TParam2.TParam3).param2'></a>
`param2` [TParam2](IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..md#DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..TParam2 'DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync&lt;TParam0,TParam1,TParam2,TParam3&gt;.TParam2')  
The 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..InvokeAsync(TParam0.TParam1.TParam2.TParam3).param3'></a>
`param3` [TParam3](IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..md#DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.TParam0.TParam1.TParam2.TParam3..TParam3 'DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync&lt;TParam0,TParam1,TParam2,TParam3&gt;.TParam3')  
The 4th parameter.
  
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the condition is met, otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').
  
