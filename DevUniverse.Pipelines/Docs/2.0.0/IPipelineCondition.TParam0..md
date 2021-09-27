#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Conditions](Pipelines.md#DevUniverse.Pipelines.Core.Conditions 'DevUniverse.Pipelines.Core.Conditions')
## IPipelineCondition&lt;TParam0&gt; Interface
The pipeline condition with 1 parameter.  
```csharp
public interface IPipelineCondition<in TParam0> :
DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0..TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  

Implements [IPipelineConditionBasic](IPipelineConditionBasic.md 'DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0..Invoke(TParam0)'></a>
## IPipelineCondition&lt;TParam0&gt;.Invoke(TParam0) Method
Executes the logic of the condition.  
```csharp
bool Invoke(TParam0 param0);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0..Invoke(TParam0).param0'></a>
`param0` [TParam0](IPipelineCondition.TParam0..md#DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.TParam0..TParam0 'DevUniverse.Pipelines.Core.Conditions.IPipelineCondition&lt;TParam0&gt;.TParam0')  
The 1st parameter.
  
#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the condition is met, otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').
  
