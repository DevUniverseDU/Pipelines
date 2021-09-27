#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Conditions](Pipelines.md#DevUniverse.Pipelines.Core.Conditions 'DevUniverse.Pipelines.Core.Conditions')
## IPipelineConditionAsync Interface
The async pipeline condition with no parameters.  
```csharp
public interface IPipelineConditionAsync :
DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsyncBasic,
DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic
```

Implements [IPipelineConditionAsyncBasic](IPipelineConditionAsyncBasic.md 'DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsyncBasic'), [IPipelineConditionBasic](IPipelineConditionBasic.md 'DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Conditions.IPipelineConditionAsync.InvokeAsync()'></a>
## IPipelineConditionAsync.InvokeAsync() Method
Executes the logic of the condition.  
```csharp
System.Threading.Tasks.Task<bool> InvokeAsync();
```
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the condition is met, otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').
  
