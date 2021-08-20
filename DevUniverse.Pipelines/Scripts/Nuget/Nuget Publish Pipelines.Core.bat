set packageVersion=%~1
set apiKey=%~2

dotnet nuget push "..\..\Local\Nuget\DevUniverse.Pipelines.Core.%packageVersion=%.nupkg" --api-key %apiKey% --source https://api.nuget.org/v3/index.json