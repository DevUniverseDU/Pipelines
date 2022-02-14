#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Builders.Shared](Pipelines.md#DevUniverse.Pipelines.Core.Builders.Shared 'DevUniverse.Pipelines.Core.Builders.Shared')
## IPipelineBuilderCore&lt;TDelegate,TResult&gt; Interface
The core pipeline builder.  
```csharp
public interface IPipelineBuilderCore<TDelegate,out TResult> :
DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderBasic
    where TDelegate : System.Delegate
    where TResult : DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore<TDelegate, TResult>
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate'></a>
`TDelegate`  
The delegate type.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TResult'></a>
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
&#8627; [IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;](IPipelineBuilderConditionDelegate.TDelegate.TPredicate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegate&lt;TDelegate,TPredicate,TResult&gt;')  
&#8627; [IPipelineBuilderConditionDelegateWithServiceProvider&lt;TDelegate,TPredicate,TResult&gt;](IPipelineBuilderConditionDelegateWithServiceProvider.TDelegate.TPredicate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate.IPipelineBuilderConditionDelegateWithServiceProvider&lt;TDelegate,TPredicate,TResult&gt;')  
&#8627; [IPipelineBuilderConditionInterface&lt;TDelegate,TCondition,TResult&gt;](IPipelineBuilderConditionInterface.TDelegate.TCondition.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionInterface.IPipelineBuilderConditionInterface&lt;TDelegate,TCondition,TResult&gt;')  
&#8627; [IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;](IPipelineBuilderConditionInterfaceWithServiceProvider.TDelegate.TCondition.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionInterface.IPipelineBuilderConditionInterfaceWithServiceProvider&lt;TDelegate,TCondition,TResult&gt;')  
&#8627; [IPipelineBuilder&lt;TDelegate,TPipelineStep,TPredicate,TPipelineCondition,TResult&gt;](IPipelineBuilder.TDelegate.TPipelineStep.TPredicate.TPipelineCondition.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilder&lt;TDelegate,TPipelineStep,TPredicate,TPipelineCondition,TResult&gt;')  
&#8627; [IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt;](IPipelineBuilderWithServiceProvider.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt;')  
&#8627; [IPipelineBuilderStepInterface&lt;TDelegate,TPipelineStep,TResult&gt;](IPipelineBuilderStepInterface.TDelegate.TPipelineStep.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterface&lt;TDelegate,TPipelineStep,TResult&gt;')  
&#8627; [IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;](IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;')  

Implements [IPipelineBuilderBasic](IPipelineBuilderBasic.md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderBasic')  
### Properties
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..Components'></a>
## IPipelineBuilderCore&lt;TDelegate,TResult&gt;.Components Property
The pipeline builder component.  
```csharp
System.Collections.Generic.IReadOnlyCollection<System.Func<TDelegate,TDelegate>> Components { get; }
```
#### Property Value
[System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TDelegate](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TDelegate')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TDelegate](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TDelegate')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..Target'></a>
## IPipelineBuilderCore&lt;TDelegate,TResult&gt;.Target Property
The target (terminating step) of the pipeline.  
```csharp
TDelegate? Target { get; }
```
#### Property Value
[TDelegate](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TDelegate')
  
### Methods
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..Build(TDelegate.)'></a>
## IPipelineBuilderCore&lt;TDelegate,TResult&gt;.Build(TDelegate?) Method
Builds the pipeline.  
```csharp
TDelegate Build(TDelegate? target=null);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..Build(TDelegate.).target'></a>
`target` [TDelegate](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TDelegate')  
The target of the pipeline.
  
#### Returns
[TDelegate](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TDelegate')  
The pipeline delegate which is the start of the pipeline.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..Copy()'></a>
## IPipelineBuilderCore&lt;TDelegate,TResult&gt;.Copy() Method
Creates the new instance of the pipeline builder with same configuration (components/steps and target) as the current instance.  
```csharp
TResult Copy();
```
#### Returns
[TResult](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TResult')  
The new instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..Use(System.Func.TDelegate.TDelegate.)'></a>
## IPipelineBuilderCore&lt;TDelegate,TResult&gt;.Use(Func&lt;TDelegate,TDelegate&gt;) Method
Adds the component.  
```csharp
TResult Use(System.Func<TDelegate,TDelegate> component);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..Use(System.Func.TDelegate.TDelegate.).component'></a>
`component` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TDelegate](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TDelegate')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TDelegate](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TDelegate')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The component.
  
#### Returns
[TResult](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TResult')  
The current instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..UseTarget(TDelegate)'></a>
## IPipelineBuilderCore&lt;TDelegate,TResult&gt;.UseTarget(TDelegate) Method
Sets the pipeline target.  
The target is the last (terminating) step of the pipeline.  
```csharp
TResult UseTarget(TDelegate target);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..UseTarget(TDelegate).target'></a>
`target` [TDelegate](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TDelegate')  
The target.
  
#### Returns
[TResult](IPipelineBuilderCore.TDelegate.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore.TDelegate.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;.TResult')  
The current instance of the pipeline builder.
  
