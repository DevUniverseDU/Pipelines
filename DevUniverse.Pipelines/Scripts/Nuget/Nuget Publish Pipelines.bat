set apiKey=%~1

dotnet nuget push "..\..\Local\Nuget\DevUniverse.Pipelines.1.0.0.nupkg" --api-key %apiKey% --source https://api.nuget.org/v3/index.json