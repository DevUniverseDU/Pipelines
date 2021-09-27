@echo off

set framework=%~1

if "%framework%" == "" (
    set framework="net6.0"
)

set framework=%framework:"=%

dotnet test "..\..\DevUniverse.Pipelines.sln" --framework %framework% --configuration Release --logger "trx;LogFileName=TestResults.trx" --logger "html;LogFileName=TestResults.html" --logger "console;verbosity=normal" --results-directory "..\..\Local\Tests\%framework%" --verbosity minimal "/p:CollectCoverage=true" "/p:CoverletOutput=\"..\..\..\Local\Tests\%framework%\TestCoverageResults/\"" "/p:MergeWith=\"..\..\..\Local\Tests\%framework%\TestCoverageResults\coverlet.json\"" "/p:CoverletOutputFormat=\"json,cobertura\""

reportgenerator -reports:"..\..\Local\Tests\%framework%\TestCoverageResults\*.xml" -targetdir:"..\..\Local\Tests\%framework%\TestCoverageResults\Reports" -reporttypes:"Html"
