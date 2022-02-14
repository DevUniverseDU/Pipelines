#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Conditions](Pipelines.md#DevUniverse.Pipelines.Core.Conditions 'DevUniverse.Pipelines.Core.Conditions')
## IPipelineCondition&lt;TParam0,TParam1,TParam2,TParam3&gt; Interface
The pipeline condition with 4 parameters.  
```csharp
public interface IPipelineCondition<in TParam0,in TParam1,in TParam2,in TParam3> :
DevUniverse.Pipelines.Core.Conditions.Shared.IPipelineConditionBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..TParam2'></a>
`TParam2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..TParam3'></a>
`TParam3`  
The type of the 4th parameter.
  

Implements [IPipelineConditionBasic](IPipelineConditionBasic.md 'DevUniverse.Pipelines.Core.Conditions.Shared.IPipelineConditionBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..Invoke(TParam0.TParam1.TParam2.TParam3)'></a>
## IPipelineCondition&lt;TParam0,TParam1,TParam2,TParam3&gt;.Invoke(TParam0, TParam1, TParam2, TParam3) Method
Executes the logic of the condition.  
```csharp
bool Invoke(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..Invoke(TParam0.TParam1.TParam2.TParam3).param0'></a>
`param0` [TParam0](IPipelineCondition.TParam0.TParam1.TParam2.TParam3..md#DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..TParam0 'DevUniverse.Pipelines.Core.Conditions.IPipelineCondition&lt;TParam0,TParam1,TParam2,TParam3&gt;.TParam0')  
The 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..Invoke(TParam0.TParam1.TParam2.TParam3).param1'></a>
`param1` [TParam1](IPipelineCondition.TParam0.TParam1.TParam2.TParam3..md#DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..TParam1 'DevUniverse.Pipelines.Core.Conditions.IPipelineCondition&lt;TParam0,TParam1,TParam2,TParam3&gt;.TParam1')  
The 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..Invoke(TParam0.TParam1.TParam2.TParam3).param2'></a>
`param2` [TParam2](IPipelineCondition.TParam0.TParam1.TParam2.TParam3..md#DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..TParam2 'DevUniverse.Pipelines.Core.Conditions.IPipelineCondition&lt;TParam0,TParam1,TParam2,TParam3&gt;.TParam2')  
The 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..Invoke(TParam0.TParam1.TParam2.TParam3).param3'></a>
`param3` [TParam3](IPipelineCondition.TParam0.TParam1.TParam2.TParam3..md#DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1.TParam2.TParam3..TParam3 'DevUniverse.Pipelines.Core.Conditions.IPipelineCondition&lt;TParam0,TParam1,TParam2,TParam3&gt;.TParam3')  
The 4th parameter.
  
#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the condition is met, otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').
  
