:: And language build script for Windows

@echo off
call "%ProgramFiles(x86)%\Microsoft Visual Studio 14.0\VC\vcvarsall.bat" x86
echo Starting Build
echo.
MSBuild %~dp0Source\And.sln
echo Build completed.
pause >nul