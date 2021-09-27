#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.BuilderFactories](Pipelines.md#DevUniverse.Pipelines.Core.BuilderFactories 'DevUniverse.Pipelines.Core.BuilderFactories')
## IPipelineBuilderFactory Interface
The pipeline builder factory.  
Creates the new instances of the pipeline builders.  
```csharp
public interface IPipelineBuilderFactory :
DevUniverse.Pipelines.Core.Shared.BuilderFactories.IPipelineBuilderFactoryBasic
```

Implements [IPipelineBuilderFactoryBasic](IPipelineBuilderFactoryBasic.md 'DevUniverse.Pipelines.Core.Shared.BuilderFactories.IPipelineBuilderFactoryBasic')  
### Methods
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....)'></a>
## IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(object?[]?) Method
Creates the pipeline builder with 4 parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TParam0,TParam1,TParam2,TParam3,TResult> Create<TParam0,TParam1,TParam2,TParam3,TResult>(params object?[]? constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TParam2'></a>
`TParam2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TParam3'></a>
`TParam3`  
The type of the 4th parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TParam0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TParam0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(object?[]?).TParam0')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TParam1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TParam1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(object?[]?).TParam1')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TParam2](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TParam2 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(object?[]?).TParam2')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TParam3](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TParam3 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(object?[]?).TParam3')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(object....).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(object?[]?).TResult')[&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...)'></a>
## IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(IEnumerable&lt;object?&gt;?) Method
Creates the pipeline builder with 4 parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TParam0,TParam1,TParam2,TParam3,TResult> Create<TParam0,TParam1,TParam2,TParam3,TResult>(System.Collections.Generic.IEnumerable<object?>? constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TParam2'></a>
`TParam2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TParam3'></a>
`TParam3`  
The type of the 4th parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TParam0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TParam0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam0')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TParam1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TParam1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam1')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TParam2](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TParam2 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam2')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TParam3](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TParam3 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam3')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TParam3.TResult.(System.Collections.Generic.IEnumerable.object...).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TResult')[&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TParam3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TParam3,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....)'></a>
## IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(object?[]?) Method
Creates the pipeline builder with 3 parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TParam0,TParam1,TParam2,TResult> Create<TParam0,TParam1,TParam2,TResult>(params object?[]? constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).TParam2'></a>
`TParam2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')[TParam0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).TParam0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(object?[]?).TParam0')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')[TParam1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).TParam1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(object?[]?).TParam1')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')[TParam2](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).TParam2 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(object?[]?).TParam2')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(object....).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(object?[]?).TResult')[&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...)'></a>
## IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(IEnumerable&lt;object?&gt;?) Method
Creates the pipeline builder with 3 parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TParam0,TParam1,TParam2,TResult> Create<TParam0,TParam1,TParam2,TResult>(System.Collections.Generic.IEnumerable<object?>? constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).TParam2'></a>
`TParam2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')[TParam0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).TParam0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam0')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')[TParam1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).TParam1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam1')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')[TParam2](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).TParam2 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam2')[,](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TParam2.TResult.(System.Collections.Generic.IEnumerable.object...).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TParam2,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TResult')[&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TParam2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TParam2,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(object....)'></a>
## IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TResult&gt;(object?[]?) Method
Creates the pipeline builder with 2 parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TParam0,TParam1,TResult> Create<TParam0,TParam1,TResult>(params object?[]? constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(object....).TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(object....).TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(object....).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(object....).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')[TParam0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(object....).TParam0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TResult&gt;(object?[]?).TParam0')[,](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')[TParam1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(object....).TParam1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TResult&gt;(object?[]?).TParam1')[,](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(object....).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TResult&gt;(object?[]?).TResult')[&gt;](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(System.Collections.Generic.IEnumerable.object...)'></a>
## IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TResult&gt;(IEnumerable&lt;object?&gt;?) Method
Creates the pipeline builder with 2 parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TParam0,TParam1,TResult> Create<TParam0,TParam1,TResult>(System.Collections.Generic.IEnumerable<object?>? constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(System.Collections.Generic.IEnumerable.object...).TParam0'></a>
`TParam0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(System.Collections.Generic.IEnumerable.object...).TParam1'></a>
`TParam1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(System.Collections.Generic.IEnumerable.object...).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(System.Collections.Generic.IEnumerable.object...).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')[TParam0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(System.Collections.Generic.IEnumerable.object...).TParam0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam0')[,](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')[TParam1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(System.Collections.Generic.IEnumerable.object...).TParam1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam1')[,](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TParam1.TResult.(System.Collections.Generic.IEnumerable.object...).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TParam1,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TResult')[&gt;](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;](IPipelineBuilder.TParam0.TParam1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TParam1,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(object....)'></a>
## IPipelineBuilderFactory.Create&lt;TParam0,TResult&gt;(object?[]?) Method
Creates the pipeline builder with 1 parameter which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TParam0,TResult> Create<TParam0,TResult>(params object?[]? constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(object....).TParam0'></a>
`TParam0`  
The type of the parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(object....).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(object....).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;')[TParam0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(object....).TParam0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TResult&gt;(object?[]?).TParam0')[,](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(object....).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TResult&gt;(object?[]?).TResult')[&gt;](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TParam0,TResult&gt;](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(System.Collections.Generic.IEnumerable.object...)'></a>
## IPipelineBuilderFactory.Create&lt;TParam0,TResult&gt;(IEnumerable&lt;object?&gt;?) Method
Creates the pipeline builder with 1 parameter which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TParam0,TResult> Create<TParam0,TResult>(System.Collections.Generic.IEnumerable<object?>? constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(System.Collections.Generic.IEnumerable.object...).TParam0'></a>
`TParam0`  
The type of the parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(System.Collections.Generic.IEnumerable.object...).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(System.Collections.Generic.IEnumerable.object...).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;')[TParam0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(System.Collections.Generic.IEnumerable.object...).TParam0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TParam0')[,](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TParam0.TResult.(System.Collections.Generic.IEnumerable.object...).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TParam0,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TResult')[&gt;](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TParam0,TResult&gt;](IPipelineBuilder.TParam0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TParam0,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(object....)'></a>
## IPipelineBuilderFactory.Create&lt;TResult&gt;(object?[]?) Method
Creates the pipeline builder without the parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Create<TResult>(params object?[]? constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(object....).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(object....).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(object....).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TResult&gt;(object?[]?).TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TResult&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(System.Collections.Generic.IEnumerable.object...)'></a>
## IPipelineBuilderFactory.Create&lt;TResult&gt;(IEnumerable&lt;object?&gt;?) Method
Creates the pipeline builder without the parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Create<TResult>(System.Collections.Generic.IEnumerable<object?>? constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(System.Collections.Generic.IEnumerable.object...).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(System.Collections.Generic.IEnumerable.object...).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(System.Collections.Generic.IEnumerable.object...).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TResult&gt;(System.Collections.Generic.IEnumerable&lt;object?&gt;?).TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TResult&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;').
  
