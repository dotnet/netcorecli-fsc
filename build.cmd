@echo off

powershell -NoProfile -NoLogo -Command "%~dp0scripts\build.ps1 %*; exit $LastExitCode;"
if %errorlevel% neq 0 exit /b %errorlevel%
 
