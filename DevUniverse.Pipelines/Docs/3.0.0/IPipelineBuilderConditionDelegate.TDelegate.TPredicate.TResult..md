#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate](Pipelines.md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate')
## IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt; Interface
The pipeline builder with the possibility to add the pipeline step conditionally.  
```csharp
public interface IPipelineBuilderConditionDelegate<TDelegate,in TPredicate,TResult> :
DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore<TDelegate, TResult>,
DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderBasic
    where TDelegate : System.Delegate
    where TPredicate : System.Delegate
    where TResult : DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate<TDelegate, TPredicate, TResult>
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TDelegate'></a>
`TDelegate`  
The delegate type.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TPredicate'></a>
`TPredicate`  
The predicate type.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TResult'></a>
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
&#8627; [IPipelineBuilder&lt;TDelegate,TPipelineStep,TPredicate,TPipelineCondition,TResult&gt;](IPipelineBuilder.TDelegate.TPipelineStep.TPredicate.TPipelineCondition.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilder&lt;TDelegate,TPipelineStep,TPredicate,TPipelineCondition,TResult&gt;')  

Implements [DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;')[TDelegate](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TDelegate')[,](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;')[TResult](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TResult')[&gt;](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;'), [IPipelineBuilderBasic](IPipelineBuilderBasic.md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..UseBranchIf(TPredicate.System.Action.TResult..System.Func.TResult.)'></a>
## IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.UseBranchIf(TPredicate, Action&lt;TResult&gt;, Func&lt;TResult&gt;) Method
Adds the pipeline component to the pipeline.  
If the condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.  
If the condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
```csharp
TResult UseBranchIf(TPredicate predicate, System.Action<TResult> branchBuilderConfiguration, System.Func<TResult> branchBuilderFactory);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..UseBranchIf(TPredicate.System.Action.TResult..System.Func.TResult.).predicate'></a>
`predicate` [TPredicate](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TPredicate 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TPredicate')  
The predicate which determines if the added pipeline component should be executed.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..UseBranchIf(TPredicate.System.Action.TResult..System.Func.TResult.).branchBuilderConfiguration'></a>
`branchBuilderConfiguration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[TResult](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..UseBranchIf(TPredicate.System.Action.TResult..System.Func.TResult.).branchBuilderFactory'></a>
`branchBuilderFactory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The factory which provides the branch builder instance.
  
#### Returns
[TResult](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TResult')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..UseIf(TPredicate.System.Action.TResult..System.Func.TResult.)'></a>
## IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.UseIf(TPredicate, Action&lt;TResult&gt;, Func&lt;TResult&gt;) Method
Adds the pipeline component to the pipeline.  
If the condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.  
If the condition is NOT met the configuration is just skipped and next step of the main branch is executed.  
```csharp
TResult UseIf(TPredicate predicate, System.Action<TResult> branchBuilderConfiguration, System.Func<TResult> branchBuilderFactory);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..UseIf(TPredicate.System.Action.TResult..System.Func.TResult.).predicate'></a>
`predicate` [TPredicate](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TPredicate 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TPredicate')  
The predicate which determines if the added pipeline component should be executed.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..UseIf(TPredicate.System.Action.TResult..System.Func.TResult.).branchBuilderConfiguration'></a>
`branchBuilderConfiguration` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[TResult](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
The configuration of the branch pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..UseIf(TPredicate.System.Action.TResult..System.Func.TResult.).branchBuilderFactory'></a>
`branchBuilderFactory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[TResult](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')  
The factory which provides the branch builder instance.
  
#### Returns
[TResult](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;.TResult')  
The current instance of the pipeline builder.
  
