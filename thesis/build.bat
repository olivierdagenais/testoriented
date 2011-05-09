@echo off
setlocal
call "%VS100COMNTOOLS%\vsvars32.bat"
msbuild.exe thesis.msbuild /nologo /verbosity:minimal %*
endlocal