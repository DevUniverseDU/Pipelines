#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Builders.Shared.StepInterface](Pipelines.md#DevUniverse.Pipelines.Core.Builders.Shared.StepInterface 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface')
## IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt; Interface
The pipeline builder with the possibility to add the pipeline step using the [IPipelineStepBasic](IPipelineStepBasic.md 'DevUniverse.Pipelines.Core.Steps.Shared.IPipelineStepBasic')> implementation.  
```csharp
public interface IPipelineBuilderStepInterfaceWithServiceProvider<TDelegate,in TPipelineStep,out TResult> :
DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider<TDelegate, TResult>,
DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore<TDelegate, TResult>,
DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderBasic
    where TDelegate : System.Delegate
    where TPipelineStep : DevUniverse.Pipelines.Core.Steps.Shared.IPipelineStepBasic
    where TResult : DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider<TDelegate, TPipelineStep, TResult>
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..TDelegate'></a>
`TDelegate`  
The delegate type.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..TPipelineStep'></a>
`TPipelineStep`  
The pipeline step type.
  
<a name='DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..TResult'></a>
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

Implements [DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider&lt;](IPipelineBuilderWithServiceProvider.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt;')[TDelegate](IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;.TDelegate')[,](IPipelineBuilderWithServiceProvider.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt;')[TResult](IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;.TResult')[&gt;](IPipelineBuilderWithServiceProvider.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderWithServiceProvider&lt;TDelegate,TResult&gt;'), [DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;')[TDelegate](IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..TDelegate 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;.TDelegate')[,](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;')[TResult](IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;.TResult')[&gt;](IPipelineBuilderCore.TDelegate.TResult..md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderCore&lt;TDelegate,TResult&gt;'), [IPipelineBuilderBasic](IPipelineBuilderBasic.md 'DevUniverse.Pipelines.Core.Builders.Shared.IPipelineBuilderBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..Use.TStep.(System.Func.System.IServiceProvider.TStep..)'></a>
## IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;.Use&lt;TStep&gt;(Func&lt;IServiceProvider,TStep&gt;?) Method
Add the component from the pipeline step interface implementation.  
Requires the service provider to be set.  
```csharp
TResult Use<TStep>(System.Func<System.IServiceProvider,TStep>? pipelineStepFactory=null)
    where TStep : TPipelineStep;
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..Use.TStep.(System.Func.System.IServiceProvider.TStep..).TStep'></a>
`TStep`  
The pipeline step type.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..Use.TStep.(System.Func.System.IServiceProvider.TStep..).pipelineStepFactory'></a>
`pipelineStepFactory` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[TStep](IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..Use.TStep.(System.Func.System.IServiceProvider.TStep..).TStep 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;.Use&lt;TStep&gt;(System.Func&lt;System.IServiceProvider,TStep&gt;?).TStep')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')  
The factory which provides the pipeline step instance.
  
#### Returns
[TResult](IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..md#DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider.TDelegate.TPipelineStep.TResult..TResult 'DevUniverse.Pipelines.Core.Builders.Shared.StepInterface.IPipelineBuilderStepInterfaceWithServiceProvider&lt;TDelegate,TPipelineStep,TResult&gt;.TResult')  
  
