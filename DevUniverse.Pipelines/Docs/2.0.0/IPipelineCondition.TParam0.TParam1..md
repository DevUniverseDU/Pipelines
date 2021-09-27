#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Conditions](Pipelines.md#DevUniverse.Pipelines.Core.Conditions 'DevUniverse.Pipelines.Core.Conditions')
## IPipelineCondition&lt;TParam0,TParam1&gt; Interface
The pipeline condition with 2 parameters.  
```csharp
public interface IPipelineCondition<in TParam0,in TParam1> :
DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1..TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1..TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  

Implements [IPipelineConditionBasic](IPipelineConditionBasic.md 'DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1..Invoke(TParam0.TParam1)'></a>
## IPipelineCondition&lt;TParam0,TParam1&gt;.Invoke(TParam0, TParam1) Method
Executes the logic of the condition.  
```csharp
bool Invoke(TParam0 param0, TParam1 param1);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1..Invoke(TParam0.TParam1).param0'></a>
`param0` [TParam0](IPipelineCondition.TParam0.TParam1..md#DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1..TParam0 'DevUniverse.Pipelines.Core.Conditions.IPipelineCondition&lt;TParam0,TParam1&gt;.TParam0')  
The 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1..Invoke(TParam0.TParam1).param1'></a>
`param1` [TParam1](IPipelineCondition.TParam0.TParam1..md#DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0.TParam1..TParam1 'DevUniverse.Pipelines.Core.Conditions.IPipelineCondition&lt;TParam0,TParam1&gt;.TParam1')  
The 2nd parameter.
  
#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the condition is met, otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').
  
