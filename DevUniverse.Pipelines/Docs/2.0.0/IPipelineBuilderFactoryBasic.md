#### [DevUniverse.Pipelines.Core](Pipelines.md 'Pipelines')
### [DevUniverse.Pipelines.Core.Shared.BuilderFactories](Pipelines.md#DevUniverse.Pipelines.Core.Shared.BuilderFactories 'DevUniverse.Pipelines.Core.Shared.BuilderFactories')
## IPipelineBuilderFactoryBasic Interface
The basic pipeline builder factory.  
```csharp
public interface IPipelineBuilderFactoryBasic
```

Derived  
&#8627; [IPipelineBuilderAsyncFactory](IPipelineBuilderAsyncFactory.md 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderAsyncFactory')  
&#8627; [IPipelineBuilderFactory](IPipelineBuilderFactory.md 'DevUniverse.Pipelines.Core.BuilderFactories.IPipelineBuilderFactory')  
### Methods
<a name='DevUniverse.Pipelines.Core.Shared.BuilderFactories.IPipelineBuilderFactoryBasic.Create(System.Type.object....)'></a>
## IPipelineBuilderFactoryBasic.Create(Type, object?[]?) Method
Creates the pipeline builder of the specified type using the type variable.  
```csharp
object Create(System.Type type, params object?[]? constructorArgs);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Shared.BuilderFactories.IPipelineBuilderFactoryBasic.Create(System.Type.object....).type'></a>
`type` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')  
The type of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Shared.BuilderFactories.IPipelineBuilderFactoryBasic.Create(System.Type.object....).constructorArgs'></a>
`constructorArgs` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
The constructor arguments.
  
#### Returns
[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')  
The new instance of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Shared.BuilderFactories.IPipelineBuilderFactoryBasic.Create(System.Type.System.Collections.Generic.IEnumerable.object...)'></a>
## IPipelineBuilderFactoryBasic.Create(Type, IEnumerable&lt;object?&gt;?) Method
Creates the pipeline builder of the specified type using the type variable.  
```csharp
object Create(System.Type type, System.Collections.Generic.IEnumerable<object?>? constructorArgs=null);
```
#### Parameters
<a name='DevUniverse.Pipelines.Core.Shared.BuilderFactories.IPipelineBuilderFactoryBasic.Create(System.Type.System.Collections.Generic.IEnumerable.object...).type'></a>
`type` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')  
The type of the pipeline builder.
  
<a name='DevUniverse.Pipelines.Core.Shared.BuilderFactories.IPipelineBuilderFactoryBasic.Create(System.Type.System.Collections.Generic.IEnumerable.object...).constructorArgs'></a>
`constructorArgs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
The constructor arguments.
  
#### Returns
[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')  
The new instance of the pipeline builder.
  
