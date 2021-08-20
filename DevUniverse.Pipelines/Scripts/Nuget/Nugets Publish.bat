set packageVersion=%~1
set apiKey=%~2

call "Nuget Publish Pipelines.Core.bat" %packageVersion% %apiKey%
call "Nuget Publish Pipelines.bat" %packageVersion% %apiKey%


