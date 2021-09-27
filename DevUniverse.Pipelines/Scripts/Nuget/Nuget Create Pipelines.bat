@echo off

dotnet pack "..\..\Sources\Infrastructure\DevUniverse.Pipelines.Infrastructure\DevUniverse.Pipelines.Infrastructure.csproj" --configuration Release --output "..\..\Local\Nuget"
