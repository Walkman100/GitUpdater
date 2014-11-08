%ProgramFiles%\MSBuild\12.0\bin\msbuild.exe GitUpdater.sln

%ProgramFiles%\NSIS\makensisw.exe "NSIS Installer for GitUpdater.nsi"

explorer.exe "%~dp0bin\Release"

pause