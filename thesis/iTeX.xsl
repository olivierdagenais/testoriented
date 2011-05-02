<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

    <xsl:output method="text" encoding="UTF-8" />

    <xsl:template match="/iTeX">
        <xsl:apply-templates select="preamble" />

        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'begin'" />
            <xsl:with-param name="param" select="'document'" />
        </xsl:call-template>
        <xsl:apply-templates select="document"/>
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'end'" />
            <xsl:with-param name="param" select="'document'" />
        </xsl:call-template>
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

    <xsl:template match="comment()">
        <xsl:variable name="escapedCrLf">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="." />
                <xsl:with-param name="charsIn" select="'&#xD;&#xA;'" />
                <xsl:with-param name="charsOut" select="'&#xD;&#xA;% '" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedLf">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedCrLf" />
                <xsl:with-param name="charsIn" select="'&#xA;'" />
                <xsl:with-param name="charsOut" select="'&#xA;% '" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedCr">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$escapedLf" />
                <xsl:with-param name="charsIn" select="'&#xD;'" />
                <xsl:with-param name="charsOut" select="'&#xD;% '" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:text>%</xsl:text><xsl:value-of select="$escapedCr" /><xsl:text>
</xsl:text>
    </xsl:template>
    
    <xsl:template match="raw">
        <xsl:if test="@begin">
            <xsl:call-template name="command">
                <xsl:with-param name="name" select="'begin'" />
                <xsl:with-param name="param" select="@begin" />
            </xsl:call-template>
        </xsl:if>
        <xsl:text></xsl:text>
        <xsl:value-of select="." />
        <xsl:text></xsl:text>
        <xsl:if test="@begin">
            <xsl:call-template name="command">
                <xsl:with-param name="name" select="'end'" />
                <xsl:with-param name="param" select="@begin" />
            </xsl:call-template>
        </xsl:if>
    </xsl:template>

    <xsl:template match="command">
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="@name" />
            <xsl:with-param name="opt" select="@opt" />
            <xsl:with-param name="param" select="@param" />
        </xsl:call-template>
        <xsl:apply-templates select="* | node() | comment()" />
    </xsl:template>
    
    <xsl:template match="block">
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'begin'" />
            <xsl:with-param name="opt" select="@opt" />
            <xsl:with-param name="param" select="@param" />
        </xsl:call-template>
        <xsl:apply-templates select="* | node() | comment()" />
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'end'" />
            <xsl:with-param name="opt" select="@opt" />
            <xsl:with-param name="param" select="@param" />
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="br">
        <xsl:text>\\</xsl:text>
    </xsl:template>

    <xsl:template match="e">
        <xsl:text>$</xsl:text><xsl:value-of select="." /><xsl:text>$</xsl:text>
    </xsl:template>

    <!-- Pass-through (identity transform) template -->
    <xsl:template match="* | @*">
        <xsl:apply-templates select="* | @* | node() | comment()" />
    </xsl:template>

    <xsl:template name="command">
        <xsl:param name="name" />
        <xsl:param name="opt" />
        <xsl:param name="param" />
        <xsl:text>\</xsl:text><xsl:value-of select="$name" />
        <xsl:if test="$opt">
            <xsl:text>[</xsl:text><xsl:value-of select="$opt" /><xsl:text>]</xsl:text>
        </xsl:if>
        <xsl:if test="$param">
            <xsl:text>{</xsl:text><xsl:value-of select="$param" /><xsl:text>}</xsl:text>
        </xsl:if>
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