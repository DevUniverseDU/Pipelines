#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface](Pipelines.md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface')
## IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt; Interface
The pipeline builder with the possibility to add the pipeline step conditionally.  
```csharp
public interface IPipelineBuilderConditionInterfaceWithServiceProvider<TDelegate,in TCondition,TResult> :
DevUniverse.Pipelines.Core.Shared.Builders.IPipelineBuilderCore<TDelegate, TResult>,
DevUniverse.Pipelines.Core.Shared.Builders.IPipelineBuilderBasic
    where TDelegate : System.Delegate
    where TCondition : DevUniverse.Pipelines.Core.Shared.Conditions.IPipelineConditionBasic
    where TResult : DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider<TDelegate, TCondition, TResult>
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TDelegate'></a>
`TDelegate`  
The delegate type.
  
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TCondition'></a>
`TCondition`  
The condition type.
  
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TResult'></a>
`TResult`  
The result pipeline builder type.
  

Derived  
&#8627; [IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')  
&#8627; [IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')  
&#8627; [IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')  
&#8627; [IPipelineBuilder&lt;TParam0,TResult&gt;](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;')  
&#8627; [IPipelineBuilder&lt;TResult&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
&#8627; [IPipelineBuilderAsync&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;](IPipelineBuilderAsync.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilderAsync&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')  
&#8627; [IPipelineBuilderAsync&lt;TParam0,TParam1,TParam2,TResult&gt;](IPipelineBuilderAsync.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilderAsync&lt;TParam0,TParam1,TParam2,TResult&gt;')  
&#8627; [IPipelineBuilderAsync&lt;TParam0,TParam1,TResult&gt;](IPipelineBuilderAsync.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilderAsync&lt;TParam0,TParam1,TResult&gt;')  
&#8627; [IPipelineBuilderAsync&lt;TParam0,TResult&gt;](IPipelineBuilderAsync.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilderAsync&lt;TParam0,TResult&gt;')  
&#8627; [IPipelineBuilderAsync&lt;TResult&gt;](IPipelineBuilderAsync.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilderAsync&lt;TResult&gt;')  
&#8627; [IPipelineBuilderFull&lt;TDelegate,TPipelineStep,TPredicate,TPipelineCondition,TResult&gt;](IPipelineBuilderFull.TDelegate.TPipelineStep.TPredicate.TPipelineCondition.TResult..md 'DevUniverse.Pipelines.Core.Shared.Builders.IPipelineBuilderFull&lt;TDelegate,TPipelineStep,TPredicate,TPipelineCondition,TResult&gt;')  

Implements [DevUniverse.Pipelines.Core.Shared.Builders.IPipelineBuilderCore&lt;](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Shared.Builders.IPipelineBuilderCore&lt;TDelegate,TResult&gt;')[TDelegate](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TDelegate 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.TDelegate')[,](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Shared.Builders.IPipelineBuilderCore&lt;TDelegate,TResult&gt;')[TResult](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.TResult')[&gt;](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Shared.Builders.IPipelineBuilderCore&lt;TDelegate,TResult&gt;'), [IPipelineBuilderBasic](IPipelineBuilderBasic.md 'DevUniverse.Pipelines.Core.Shared.Builders.IPipelineBuilderBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseBranchIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..)'></a>
## IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.UseBranchIf&lt;TCustomCondition&gt;(Func&lt;IServiceProvider,TCustomCondition&gt;?, Action&lt;TResult&gt;, Func&lt;IServiceProvider,TResult&gt;?) Method
Adds the pipeline component to the pipeline.  
If the condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.  
If the condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
```csharp
TResult UseBranchIf<TCustomCondition>(System.Func<System.IServiceProvider,TCustomCondition>? conditionFactory, System.Action<TResult> branchBuilderConfiguration, System.Func<System.IServiceProvider,TResult>? branchBuilderFactory=null)
    where TCustomCondition : TCondition;
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseBranchIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).TCustomCondition'></a>
`TCustomCondition`  
The condition type.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseBranchIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).conditionFactory'></a>
`conditionFactory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TCustomCondition](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseBranchIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).TCustomCondition 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.UseBranchIf&lt;TCustomCondition&gt;(System.Func&lt;System.IServiceProvider,TCustomCondition&gt;?, System.Action&lt;TResult&gt;, System.Func&lt;System.IServiceProvider,TResult&gt;?).TCustomCondition')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The condition factory which provides the condition instance. Set to null if standard logic should be used to get condition instance.
  
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseBranchIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).branchBuilderConfiguration'></a>
`branchBuilderConfiguration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[TResult](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseBranchIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).branchBuilderFactory'></a>
`branchBuilderFactory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TResult](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The factory which provides the branch builder instance.
  
#### Returns
[TResult](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.TResult')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..)'></a>
## IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.UseIf&lt;TCustomCondition&gt;(Func&lt;IServiceProvider,TCustomCondition&gt;?, Action&lt;TResult&gt;, Func&lt;IServiceProvider,TResult&gt;?) Method
Adds the pipeline component to the pipeline.  
If the condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.  
If the condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
```csharp
TResult UseIf<TCustomCondition>(System.Func<System.IServiceProvider,TCustomCondition>? conditionFactory, System.Action<TResult> branchBuilderConfiguration, System.Func<System.IServiceProvider,TResult>? branchBuilderFactory=null)
    where TCustomCondition : TCondition;
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).TCustomCondition'></a>
`TCustomCondition`  
The condition type.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).conditionFactory'></a>
`conditionFactory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TCustomCondition](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).TCustomCondition 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.UseIf&lt;TCustomCondition&gt;(System.Func&lt;System.IServiceProvider,TCustomCondition&gt;?, System.Action&lt;TResult&gt;, System.Func&lt;System.IServiceProvider,TResult&gt;?).TCustomCondition')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The condition factory which provides the condition instance. Set to null if standard logic should be used to get condition instance.
  
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).branchBuilderConfiguration'></a>
`branchBuilderConfiguration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[TResult](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..UseIf.TCustomCondition.(System.Func.System.IServiceProvider.TCustomCondition...System.Action.TResult..System.Func.System.IServiceProvider.TResult..).branchBuilderFactory'></a>
`branchBuilderFactory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TResult](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The factory which provides the branch builder instance.
  
#### Returns
[TResult](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md#DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..TResult 'DevUniverse.Pipelines.Core.Shared.Builders.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;.TResult')  
The current instance of the pipeline builder.
  
