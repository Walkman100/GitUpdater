%ProgramFiles%\MSBuild\12.0\bin\msbuild.exe /property:OutDir=bin\Release\ GitUpdater.sln

%ProgramFiles%\NSIS\makensis.exe "NSIS Installer for GitUpdater.nsi"

explorer.exe "%~dp0bin\Release"