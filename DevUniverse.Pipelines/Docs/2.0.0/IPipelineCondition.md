#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Conditions](Pipelines.md#DevUniverse.Pipelines.Core.Conditions 'DevUniverse.Pipelines.Core.Conditions')
## IPipelineCondition Interface
The pipeline condition with no parameters.  
```csharp
public interface IPipelineCondition :
DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic
```

Implements [IPipelineConditionBasic](IPipelineConditionBasic.md 'DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineCondition.Invoke()'></a>
## IPipelineCondition.Invoke() Method
Executes the logic of the condition.  
```csharp
bool Invoke();
```
#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the condition is met, otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').
  
