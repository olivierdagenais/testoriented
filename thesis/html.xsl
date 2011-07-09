<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

    <xsl:output method="xml" encoding="UTF-8" indent="yes" />

    <xsl:template match="/iTeX">
		<xsl:element name="html">
			<xsl:attribute name="xmlns">
				<xsl:text>http://www.w3.org/1999/xhtml</xsl:text>
			</xsl:attribute>
			<xsl:element name="head">
				<xsl:element name="title">
					<xsl:value-of select="//command[@name='title']/@param"/>
				</xsl:element>
				<!--
				<xsl:element name="meta">
					<xsl:attribute name="content">
						<xsl:text>http://www.w3.org/1999/xhtml; charset=utf-8</xsl:text>
					</xsl:attribute>
					<xsl:attribute name="http-equiv">
						<xsl:text>Content-Type</xsl:text>
					</xsl:attribute>
				</xsl:element>
				<xsl:element name="link">
					<xsl:attribute name="href">
						<xsl:text>stylesheet.css</xsl:text>
					</xsl:attribute>
					<xsl:attribute name="type">
						<xsl:text>text/css</xsl:text>
					</xsl:attribute>
					<xsl:attribute name="rel">
						<xsl:text>stylesheet</xsl:text>
					</xsl:attribute>
				</xsl:element>
				<xsl:element name="style">
					<xsl:attribute name="type">
						<xsl:text>@page { margin-bottom: 5.000000pt; margin-top: 5.000000pt; }</xsl:text>
					</xsl:attribute>
				</xsl:element>
				-->
			</xsl:element>
			<xsl:element name="body">
        		<xsl:apply-templates select="document"/>
			</xsl:element>
		</xsl:element>
    </xsl:template>

    <xsl:template match="text()">
        <!-- escapes/converts the following characters and only emits the result if it isn't just spaces:
            \ => \textbackslash
            # => \#
            $ => \$
            % => \%
            & => \&
            _ => \_
            { => \{
            } => \}
            ^ => \^{}
            ~ => \~{}
        -->
        <xsl:variable name="escapedBackslash">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="." />
                <xsl:with-param name="charsIn" select="'\'" />
                <xsl:with-param name="charsOut" select="'\textbackslash'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedHash">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedBackslash" />
                <xsl:with-param name="charsIn" select="'#'" />
                <xsl:with-param name="charsOut" select="'\#'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedDollar">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedHash" />
                <xsl:with-param name="charsIn" select="'$'" />
                <xsl:with-param name="charsOut" select="'\$'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedPercent">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedDollar" />
                <xsl:with-param name="charsIn" select="'%'" />
                <xsl:with-param name="charsOut" select="'\%'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedAmpersand">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedPercent" />
                <xsl:with-param name="charsIn" select="'&amp;'" />
                <xsl:with-param name="charsOut" select="'\&amp;'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedUnderscore">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedAmpersand" />
                <xsl:with-param name="charsIn" select="'_'" />
                <xsl:with-param name="charsOut" select="'\_'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedOpenCurly">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedUnderscore" />
                <xsl:with-param name="charsIn" select="'{'" />
                <xsl:with-param name="charsOut" select="'\{'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedCloseCurly">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedOpenCurly" />
                <xsl:with-param name="charsIn" select="'}'" />
                <xsl:with-param name="charsOut" select="'\}'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedCaret">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedCloseCurly" />
                <xsl:with-param name="charsIn" select="'^'" />
                <xsl:with-param name="charsOut" select="'\^{}'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedTilde">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedCaret" />
                <xsl:with-param name="charsIn" select="'~'" />
                <xsl:with-param name="charsOut" select="'\~{}'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="trimmed">
            <xsl:call-template name="trim">
                <xsl:with-param name="s" select="$escapedTilde" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:if test="string-length($trimmed) > 0">
                <xsl:value-of select="$escapedTilde" />
        </xsl:if>
    </xsl:template>

    <xsl:template match="comment()"/>
    <xsl:template match="raw"/>

    <xsl:template match="command">
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="@name" />
            <xsl:with-param name="opt" select="@opt" />
            <xsl:with-param name="param" select="@param" />
        </xsl:call-template>
        <xsl:apply-templates select="* | node() | comment()" />
    </xsl:template>
    
    <xsl:template match="block">
		<xsl:choose>
			<xsl:when test="@param='itemize'">
				<xsl:element name="ul">
					<xsl:for-each select="command">
						<xsl:element name="li">
							<xsl:value-of select="."/>
						</xsl:element>
					</xsl:for-each>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@param='quote'">
				<xsl:element name="blockquote">
					<xsl:value-of select="."/>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@param='enumerate'">
				<xsl:element name="ol">
					<xsl:for-each select="command">
						<xsl:element name="li">
							<xsl:value-of select="."/>
						</xsl:element>
					</xsl:for-each>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@param='description'">
				<xsl:element name="dl">
					<xsl:for-each select="command">
						<xsl:element name="dt">
							<xsl:value-of select="@opt"/>
						</xsl:element>
						<xsl:element name="dd">
							<xsl:value-of select="."/>
						</xsl:element>
					</xsl:for-each>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@param='abstract'"/>
			<xsl:when test="@param='acknowledgements'"/>
			<xsl:when test="@param='table'"/>
			<xsl:otherwise>
				<xsl:message terminate="yes">
					Unknown block with @param=<xsl:value-of select="@param"/>
				</xsl:message>
			</xsl:otherwise>
		</xsl:choose>
    </xsl:template>

    <xsl:template match="br">
        <xsl:text>\\</xsl:text>
    </xsl:template>

    <xsl:template match="m">
        <xsl:text>$</xsl:text><xsl:value-of select="." /><xsl:text>$</xsl:text>
    </xsl:template>
    <xsl:template match="tt">
		<xsl:element name="tt">
			<xsl:value-of select="."/>
		</xsl:element>
    </xsl:template>
    <xsl:template match="em">
		<xsl:element name="em">
			<xsl:value-of select="."/>
		</xsl:element>
    </xsl:template>

    <!-- Pass-through (identity transform) template -->
    <xsl:template match="* | @*">
        <xsl:apply-templates select="* | @* | node() | comment()" />
    </xsl:template>

    <xsl:template name="command">
        <xsl:param name="name" />
        <xsl:param name="opt" />
        <xsl:param name="param" />

		<xsl:choose>
			<xsl:when test="@name='chapter'">
				<xsl:element name="h1">
					<xsl:value-of select="@param"/>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@name='section'">
				<xsl:element name="h2">
					<xsl:value-of select="@param"/>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@name='subsection'">
				<xsl:element name="h3">
					<xsl:value-of select="@param"/>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@name='cite'">
				<xsl:element name="cite">
					<xsl:value-of select="@param"/>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@name='lstinputlisting'">
				<xsl:variable name="label" select="substring-before(substring-after(@opt, 'label='),',')"/>
				<xsl:value-of select="$label"/>(<xsl:element name="a">
					<xsl:attribute name="name">
						<xsl:value-of select="$label"/>
					</xsl:attribute>
					<xsl:attribute name="href">
						<xsl:value-of select="@param"/>
					</xsl:attribute>
					<xsl:value-of select="@param"/>
				</xsl:element>)
			</xsl:when>
			<xsl:when test="@name='ref'">
				<xsl:element name="a">
					<!--
					Anchor links crash at least the Kobo Touch, might not be part of the epub specs
					<xsl:attribute name="href">#<xsl:value-of select="@param"/></xsl:attribute>
					-->
					<xsl:value-of select="@param"/>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@name='label'">
				<xsl:element name="a">
					<xsl:attribute name="name">
						<xsl:value-of select="@param"/>
					</xsl:attribute>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@name='underline'">
				<xsl:element name="u">
					<xsl:value-of select="@param"/>
				</xsl:element>
			</xsl:when>
			<xsl:when test="@name='appendix'"/>
			<xsl:when test="@name='bibliography'"/>
			<xsl:when test="@name='bibliographystyle'"/>
			<xsl:when test="@name='title'"/>
			<xsl:when test="@name='author'"/>
			<xsl:when test="@name='submitdate'"/>
			<xsl:when test="@name='copyrightyear'"/>
			<xsl:when test="@name='degree'"/>
			<xsl:when test="@name='frontmatter'"/>
			<xsl:when test="@name='addcontentsline'"/>
			<xsl:when test="@name='clearpage'"/>
			<xsl:when test="@name='mainmatter'"/>
			<xsl:when test="@name='lstlistoflistings'"/>
			<xsl:otherwise>
				<xsl:message terminate="yes">
					Unknown command name=<xsl:value-of select="@name"/>
				</xsl:message>
			</xsl:otherwise>
		</xsl:choose>
    </xsl:template>

    <!-- Stolen from http://www.dpawson.co.uk/xsl/sect2/replace.html -->
    <xsl:template name="replaceCharsInString">
        <xsl:param name="stringIn"/>
        <xsl:param name="charsIn"/>
        <xsl:param name="charsOut"/>
        <xsl:choose>
            <xsl:when test="contains($stringIn,$charsIn)">
                <xsl:value-of select="concat(substring-before($stringIn,$charsIn),$charsOut)"/>
                <xsl:call-template name="replaceCharsInString">
                    <xsl:with-param name="stringIn" select="substring-after($stringIn,$charsIn)"/>
                    <xsl:with-param name="charsIn" select="$charsIn"/>
                    <xsl:with-param name="charsOut" select="$charsOut"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$stringIn"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- The following three were adapted from http://snippets.dzone.com/posts/show/4032 -->
    <xsl:template name="left-trim">
      <xsl:param name="s" />
      <xsl:choose>
        <xsl:when test="substring($s, 1, 1) = ''">
          <xsl:value-of select="$s"/>
        </xsl:when>
        <xsl:when test="substring($s, 1, 1) = ' '">
          <xsl:call-template name="left-trim">
            <xsl:with-param name="s" select="substring($s, 2)" />
          </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$s" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:template>

    <xsl:template name="right-trim">
      <xsl:param name="s" />
      <xsl:choose>
        <xsl:when test="substring($s, 1, 1) = ''">
          <xsl:value-of select="$s"/>
        </xsl:when>
        <xsl:when test="substring($s, string-length($s)) = ' '">
          <xsl:call-template name="right-trim">
            <xsl:with-param name="s" select="substring($s, 1, string-length($s) - 1)" />
          </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$s" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:template>

    <xsl:template name="trim">
      <xsl:param name="s" />
      <xsl:call-template name="right-trim">
        <xsl:with-param name="s">
          <xsl:call-template name="left-trim">
            <xsl:with-param name="s" select="$s" />
          </xsl:call-template>
        </xsl:with-param>
      </xsl:call-template>
    </xsl:template>

</xsl:stylesheet>
