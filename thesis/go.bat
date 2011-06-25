@echo off
start "Close PDF" "E:\Programs\Utils\PDFV_Portable\PDFXCView.exe" /close "%~dp0bin\thesis.pdf"
call build.bat
if errorlevel 1 exit 1
start "Open PDF" "E:\Programs\Utils\PDFV_Portable\PDFXCView.exe" "%~dp0bin\thesis.pdf"