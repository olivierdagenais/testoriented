@echo off
"E:\Programs\Utils\PDFV_Portable\PDFXCView.exe" /close "%~dp0bin\thesis.pdf"
call build.bat
if errorlevel 1 exit 1
"E:\Programs\Utils\PDFV_Portable\PDFXCView.exe" "%~dp0bin\thesis.pdf"