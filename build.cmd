@echo off

powershell -NoProfile -NoLogo -Command "%~dp0\run-build.ps1 %*; exit $LastExitCode;"
if %errorlevel% neq 0 exit /b %errorlevel%
