@echo off

set framework=%~1

if "%framework%" == "" (
    set framework="net6.0"
)

set framework=%framework:"=%

defaultdocumentation --AssemblyFilePath "..\..\Sources\Core\DevUniverse.Pipelines.Core\bin\Release\%framework%\DevUniverse.Pipelines.Core.dll" ^
                     --AssemblyPageName Pipelines ^
                     --GeneratedPages Types ^
                     --InvalidCharReplacement . ^
                     --FileNameMode Name ^
                     --OutputDirectoryPath "..\..\Local\Docs\%framework%"
