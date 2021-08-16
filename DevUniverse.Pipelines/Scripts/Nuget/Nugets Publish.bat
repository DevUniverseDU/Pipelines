set apiKey=%~1

call "Nuget Publish Pipelines.Core.bat" %apiKey%
call "Nuget Publish Pipelines.bat" %apiKey%


