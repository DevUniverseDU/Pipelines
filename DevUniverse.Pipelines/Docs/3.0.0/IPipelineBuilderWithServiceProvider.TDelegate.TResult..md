#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Builders.Shared](Pipelines.md#DevUniverse.Pipelines.Core.Builders.Shared 'DevUniverse.Pipelines.Core.Builders.Shared')
## IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt; Interface
The pipeline builder with service provider.  
```csharp
public interface IPipelineBuilderWithServiceProvider<TDelegate,out TResult> :
DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore<TDelegate, TResult>,
DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderBasic
    where TDelegate : System.Delegate
    where TResult : DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider<TDelegate, TResult>
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider.TDelegate.TResult..TDelegate'></a>
`TDelegate`  
The delegate type.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider.TDelegate.TResult..TResult'></a>
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
&#8627; [IPipelineBuilderConditionDelegateWithServiceProvider&lt;TDelegate,TPredicate,TResult&gt;](IPipelineBuilderConditionDelegateWithServiceProvider.TDelegate.TPredicate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegateWithServiceProvider&lt;TDelegate,TPredicate,TResult&gt;')  
&#8627; [IPipelineBuilder&lt;TDelegate,TPipelineStep,TPredicate,TPipelineCondition,TResult&gt;](IPipelineBuilder.TDelegate.TPipelineStep.TPredicate.TPipelineCondition.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilder&lt;TDelegate,TPipelineStep,TPredicate,TPipelineCondition,TResult&gt;')  
&#8627; [IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;](IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;')  

Implements [DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;')[TDelegate](IPipelineBuilderWithServiceProvider.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt;.TDelegate')[,](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;')[TResult](IPipelineBuilderWithServiceProvider.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider.TDelegate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt;.TResult')[&gt;](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;'), [IPipelineBuilderBasic](IPipelineBuilderBasic.md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderBasic')  
### Properties
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider.TDelegate.TResult..ServiceProvider'></a>
## IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt;.ServiceProvider Property
The service provider.  
```csharp
System.IServiceProvider? ServiceProvider { get; }
```
#### Property Value
[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')
  
