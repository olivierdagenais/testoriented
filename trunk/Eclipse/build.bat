@echo off
if "%JAVA_HOME%"=="" goto error
setlocal
set ANT_HOME=%~dp0\..\Tools\ant
set PATH=%PATH%;%ANT_HOME%\bin
if "%1"=="release" goto release
call ant -Dconfiguration=debug %*
goto fin

:release
call ant -Dconfiguration=release %2 %3 %4 %5 %6 %7 %8 %9
goto fin

:error
echo.
echo ERROR:  The environment variable JAVA_HOME is not set!  It must point to JDK 1.6
echo.

:fin
endlocal
