
%.html: html.xsl %.itex 
	xsltproc -o $@ $^

%.epub: epub.zip %.html %.css
	cp $< $@
	zip -9u $@ $(filter-out $<,$^)

all: thesis.epub

