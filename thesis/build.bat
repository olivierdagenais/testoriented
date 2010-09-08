@echo off
rmdir /s /q bin
mkdir bin
copy dalthesis.cls bin\
copy thesis.bib bin\
xsl thesis.itex iTeX.xsl bin\thesis.tex
cd bin

rem http://www.latex-community.org/forum/viewtopic.php?f=5&t=2011&start=0
rem 1. Run LaTeX to generate the AUX file. It adds a line every time it finds a cite.
latex thesis.tex
rem 2. Then run BibTeX (executable: bibtex) to generate the BBL (from the AUX lines).
bibtex thesis.aux
rem 3. Then run LaTeX again, which will include the BBL.
latex thesis.tex
rem 4. Then run LaTeX again to make any updates corresponding to including the possibly massive BBL
rem (e.g., updating your final page count on each of your pages, updating your ToC, etc.).
latex thesis.tex
rem Generating DVI then converting to PDF yields smaller files than the one-step pdflatex
dvipdfm thesis.dvi
cd ..