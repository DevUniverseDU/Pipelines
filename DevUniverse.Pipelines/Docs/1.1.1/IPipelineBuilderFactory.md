#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.BuilderFactories](Pipelines.md#DevUniverse.Pipelines.Core.BuilderFactories 'DevUniverse.Pipelines.Core.BuilderFactories')
## IPipelineBuilderFactory Interface
The pipeline builder factory.  
Creates the new instances of the pipeline builders.  
```csharp
public interface IPipelineBuilderFactory
```
### Methods
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create(System.Type.object..)'></a>
## IPipelineBuilderFactory.Create(Type, object[]) Method
Creates the pipeline builder of the specified type using the type variable.  
```csharp
object Create(System.Type type, params object[] constructorArgs);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create(System.Type.object..).type'></a>
`type` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')  
The type of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create(System.Type.object..).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')  
The new instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create(System.Type.System.Collections.Generic.IEnumerable.object.)'></a>
## IPipelineBuilderFactory.Create(Type, IEnumerable&lt;object&gt;) Method
Creates the pipeline builder of the specified type using the type variable.  
```csharp
object Create(System.Type type, System.Collections.Generic.IEnumerable<object> constructorArgs=null);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create(System.Type.System.Collections.Generic.IEnumerable.object.).type'></a>
`type` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')  
The type of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create(System.Type.System.Collections.Generic.IEnumerable.object.).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')  
The new instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..)'></a>
## IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(object[]) Method
Creates the pipeline builder with 4 input parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<T0,T1,T2,T3,TResult> Create<T0,T1,T2,T3,TResult>(params object[] constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).T0'></a>
`T0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).T1'></a>
`T1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).T2'></a>
`T2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).T3'></a>
`T3`  
The type of the 4th parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[T0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).T0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(object[]).T0')[,](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[T1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).T1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(object[]).T1')[,](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[T2](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).T2 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(object[]).T2')[,](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[T3](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).T3 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(object[]).T3')[,](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(object..).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(object[]).TResult')[&gt;](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.)'></a>
## IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(IEnumerable&lt;object&gt;) Method
Creates the pipeline builder with 4 input parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<T0,T1,T2,T3,TResult> Create<T0,T1,T2,T3,TResult>(System.Collections.Generic.IEnumerable<object> constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).T0'></a>
`T0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).T1'></a>
`T1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).T2'></a>
`T2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).T3'></a>
`T3`  
The type of the 4th parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[T0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).T0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T0')[,](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[T1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).T1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T1')[,](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[T2](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).T2 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T2')[,](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[T3](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).T3 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T3')[,](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.T3.TResult.(System.Collections.Generic.IEnumerable.object.).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,T3,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).TResult')[&gt;](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;](IPipelineBuilder.T0.T1.T2.T3.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,T3,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..)'></a>
## IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(object[]) Method
Creates the pipeline builder with 3 input parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<T0,T1,T2,TResult> Create<T0,T1,T2,TResult>(params object[] constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).T0'></a>
`T0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).T1'></a>
`T1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).T2'></a>
`T2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')[T0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).T0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(object[]).T0')[,](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')[T1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).T1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(object[]).T1')[,](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')[T2](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).T2 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(object[]).T2')[,](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(object..).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(object[]).TResult')[&gt;](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;T0,T1,T2,TResult&gt;](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.)'></a>
## IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(IEnumerable&lt;object&gt;) Method
Creates the pipeline builder with 3 input parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<T0,T1,T2,TResult> Create<T0,T1,T2,TResult>(System.Collections.Generic.IEnumerable<object> constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).T0'></a>
`T0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).T1'></a>
`T1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).T2'></a>
`T2`  
The type of the 3rd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')[T0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).T0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T0')[,](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')[T1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).T1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T1')[,](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')[T2](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).T2 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T2')[,](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.T2.TResult.(System.Collections.Generic.IEnumerable.object.).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,T2,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).TResult')[&gt;](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;T0,T1,T2,TResult&gt;](IPipelineBuilder.T0.T1.T2.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,T2,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(object..)'></a>
## IPipelineBuilderFactory.Create&lt;T0,T1,TResult&gt;(object[]) Method
Creates the pipeline builder with 2 input parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<T0,T1,TResult> Create<T0,T1,TResult>(params object[] constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(object..).T0'></a>
`T0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(object..).T1'></a>
`T1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(object..).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(object..).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;')[T0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(object..).T0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,TResult&gt;(object[]).T0')[,](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;')[T1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(object..).T1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,TResult&gt;(object[]).T1')[,](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(object..).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,TResult&gt;(object[]).TResult')[&gt;](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;T0,T1,TResult&gt;](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(System.Collections.Generic.IEnumerable.object.)'></a>
## IPipelineBuilderFactory.Create&lt;T0,T1,TResult&gt;(IEnumerable&lt;object&gt;) Method
Creates the pipeline builder with 2 input parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<T0,T1,TResult> Create<T0,T1,TResult>(System.Collections.Generic.IEnumerable<object> constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(System.Collections.Generic.IEnumerable.object.).T0'></a>
`T0`  
The type of the 1st parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(System.Collections.Generic.IEnumerable.object.).T1'></a>
`T1`  
The type of the 2nd parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(System.Collections.Generic.IEnumerable.object.).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(System.Collections.Generic.IEnumerable.object.).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;')[T0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(System.Collections.Generic.IEnumerable.object.).T0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T0')[,](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;')[T1](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(System.Collections.Generic.IEnumerable.object.).T1 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T1')[,](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.T1.TResult.(System.Collections.Generic.IEnumerable.object.).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,T1,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).TResult')[&gt;](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;T0,T1,TResult&gt;](IPipelineBuilder.T0.T1.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,T1,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(object..)'></a>
## IPipelineBuilderFactory.Create&lt;T0,TResult&gt;(object[]) Method
Creates the pipeline builder with 1 input parameter which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<T0,TResult> Create<T0,TResult>(params object[] constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(object..).T0'></a>
`T0`  
The type of the parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(object..).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(object..).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.T0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,TResult&gt;')[T0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(object..).T0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,TResult&gt;(object[]).T0')[,](IPipelineBuilder.T0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(object..).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,TResult&gt;(object[]).TResult')[&gt;](IPipelineBuilder.T0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;T0,TResult&gt;](IPipelineBuilder.T0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(System.Collections.Generic.IEnumerable.object.)'></a>
## IPipelineBuilderFactory.Create&lt;T0,TResult&gt;(IEnumerable&lt;object&gt;) Method
Creates the pipeline builder with 1 input parameter which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<T0,TResult> Create<T0,TResult>(System.Collections.Generic.IEnumerable<object> constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(System.Collections.Generic.IEnumerable.object.).T0'></a>
`T0`  
The type of the parameter.
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(System.Collections.Generic.IEnumerable.object.).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(System.Collections.Generic.IEnumerable.object.).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.T0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,TResult&gt;')[T0](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(System.Collections.Generic.IEnumerable.object.).T0 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).T0')[,](IPipelineBuilder.T0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.T0.TResult.(System.Collections.Generic.IEnumerable.object.).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;T0,TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).TResult')[&gt;](IPipelineBuilder.T0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;T0,TResult&gt;](IPipelineBuilder.T0.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;T0,TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(object..)'></a>
## IPipelineBuilderFactory.Create&lt;TResult&gt;(object[]) Method
Creates the pipeline builder without input parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Create<TResult>(params object[] constructorArgs);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(object..).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(object..).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(object..).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TResult&gt;(object[]).TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TResult&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;').
  
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(System.Collections.Generic.IEnumerable.object.)'></a>
## IPipelineBuilderFactory.Create&lt;TResult&gt;(IEnumerable&lt;object&gt;) Method
Creates the pipeline builder without input parameters which returns the result.  
```csharp
DevUniverse.Pipelines.Core.Builders.IPipelineBuilder<TResult> Create<TResult>(System.Collections.Generic.IEnumerable<object> constructorArgs=null);
```
#### Type parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(System.Collections.Generic.IEnumerable.object.).TResult'></a>
`TResult`  
The return type of the pipeline builder.
  
#### Parameters
<a name='DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(System.Collections.Generic.IEnumerable.object.).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')[TResult](IPipelineBuilderFactory.md#DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create.TResult.(System.Collections.Generic.IEnumerable.object.).TResult 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory.Create&lt;TResult&gt;(System.Collections.Generic.IEnumerable&lt;object&gt;).TResult')[&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;')  
The new instance of the [IPipelineBuilder&lt;TResult&gt;](IPipelineBuilder.TResult..md 'DevUniverse.Pipelines.Core.Builders.IPipelineBuilder&lt;TResult&gt;').
  
