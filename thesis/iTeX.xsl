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
            Also converts:
            ' "' => ' ``' (opening quotes)
            space dash space => dash dash (mid-sentence interruption)
        -->
        <xsl:variable name="openingQuotes">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="." />
                <xsl:with-param name="charsIn" select="' &quot;'" />
                <xsl:with-param name="charsOut" select="' ``'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="midSentenceInterruption">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$openingQuotes" />
                <xsl:with-param name="charsIn" select="' - '" />
                <xsl:with-param name="charsOut" select="'--'" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:variable name="escapedBackslash">
            <xsl:call-template name="replaceCharsInString">
                <xsl:with-param name="stringIn" select="$midSentenceInterruption" />
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
        <xsl:text>\begin{</xsl:text><xsl:value-of select="@param" /><xsl:text>}</xsl:text>
        <xsl:if test="@opt">
            <xsl:text>[</xsl:text><xsl:value-of select="@opt" /><xsl:text>]</xsl:text>
        </xsl:if>
        <xsl:apply-templates select="* | node() | comment()" />
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'end'" />
            <xsl:with-param name="param" select="@param" />
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="br">
        <xsl:text>\\</xsl:text>
    </xsl:template>

    <xsl:template match="m">
        <xsl:text>$</xsl:text><xsl:value-of select="." /><xsl:text>$</xsl:text>
    </xsl:template>
    <xsl:template match="tt">
        <xsl:text>\texttt{</xsl:text><xsl:value-of select="." /><xsl:text>}</xsl:text>
    </xsl:template>
    <xsl:template match="em">
        <xsl:text>\emph{</xsl:text><xsl:value-of select="." /><xsl:text>}</xsl:text>
    </xsl:template>
    <xsl:template match="u">
        <xsl:text>\underline{</xsl:text><xsl:value-of select="." /><xsl:text>}</xsl:text>
    </xsl:template>

    <xsl:template match="url">
        <xsl:text>\url{</xsl:text><xsl:value-of select="." /><xsl:text>}</xsl:text>
    </xsl:template>

    <xsl:template match="footnote">
        <xsl:text>\unskip</xsl:text>
        <xsl:text>\footnote{</xsl:text>
        <xsl:apply-templates select="* | node() | comment()" />
        <xsl:text>}</xsl:text>
    </xsl:template>

    <xsl:template match="table">
        <xsl:text>\begin{table}</xsl:text>
        <xsl:if test="@placement">
            <xsl:text>[</xsl:text><xsl:value-of select="@placement" /><xsl:text>]</xsl:text>
        </xsl:if>
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'begin'" />
            <xsl:with-param name="param" select="'minipage}{\textwidth'" />
        </xsl:call-template>
        <xsl:apply-templates select="* | node() | comment()" />
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'end'" />
            <xsl:with-param name="param" select="'minipage'" />
        </xsl:call-template>
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'end'" />
            <xsl:with-param name="param" select="'table'" />
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="tbody | tabular">
        <xsl:text>\begin{tabular}</xsl:text>
        <xsl:if test="@pos">
            <xsl:text>[</xsl:text><xsl:value-of select="@pos" /><xsl:text>]</xsl:text>
        </xsl:if>
        <xsl:text>{</xsl:text><xsl:value-of select="@spec" /><xsl:text>}
</xsl:text>
        <xsl:apply-templates select="* | node() | comment()" />
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'end'" />
            <xsl:with-param name="param" select="'tabular'" />
        </xsl:call-template>
    </xsl:template>
    
    <xsl:template match="tr">
        <xsl:apply-templates select="* | node() | comment()" />
        <xsl:text>\tabularnewline
</xsl:text>
    </xsl:template>
    
    <xsl:template match="td[@multicolumn]">
        <xsl:if test="count(preceding-sibling::td) > 0">
            <xsl:text> &amp; </xsl:text>
        </xsl:if>
        <xsl:text>\multicolumn</xsl:text>
        <xsl:value-of select="@multicolumn" />
        <xsl:text>{</xsl:text>
        <xsl:apply-templates select="* | node() | comment()" />
        <xsl:text>}</xsl:text>
    </xsl:template>

    <xsl:template match="td[@multirow]">
        <xsl:if test="count(preceding-sibling::td) > 0">
            <xsl:text> &amp; </xsl:text>
        </xsl:if>
        <xsl:text>\multirow</xsl:text>
        <xsl:value-of select="@multirow" />
        <xsl:text>{</xsl:text>
        <xsl:apply-templates select="* | node() | comment()" />
        <xsl:text>}</xsl:text>
    </xsl:template>

    <xsl:template match="td">
        <xsl:if test="count(preceding-sibling::td) > 0">
            <xsl:text> &amp; </xsl:text>
        </xsl:if>
        <xsl:apply-templates select="* | node() | comment()" />
    </xsl:template>
    
    <xsl:template match="hline">
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'hline'" />
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="see[@ref]">
        <!-- <command name="ref" param="tbl:SpreadsheetColumns" /> -->
        <xsl:text>\unskip~</xsl:text>
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'ref'" />
            <xsl:with-param name="param" select="@ref" />
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="see[@nameref]">
        <!-- <command name="nameref" param="tbl:SpreadsheetColumns" /> -->
        <xsl:text>\unskip~</xsl:text>
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'nameref'" />
            <xsl:with-param name="param" select="@nameref" />
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="see[@cite]">
        <!-- <command name="cite" param="shaw06nant" /> -->
        <xsl:text>\unskip~</xsl:text>
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'cite'" />
            <xsl:with-param name="param" select="@cite" />
        </xsl:call-template>
    </xsl:template>

    <xsl:template match="listing">
      <!--
      <listing label="lst:StatefulSmokeDetector" style="realCode" file="Stateful/SmokeDetector.cs">
          SmokeDetector class with hidden mutable state
      </listing>
      
      \lstinputlisting[label=lst:StatefulSmokeDetector,caption=SmokeDetector class with hidden mutable state,style=realCode]{Stateful/SmokeDetector.cs}
      -->
        <xsl:variable name="opt">
          <xsl:text>label=</xsl:text>
          <xsl:value-of select="@label" />
          <xsl:if test="@linerange">
            <xsl:text>,linerange={</xsl:text>
            <xsl:value-of select="@linerange" />
            <xsl:text>}</xsl:text>
          </xsl:if>
          <xsl:text>,caption={</xsl:text>
          <xsl:apply-templates select="* | node() | comment()" />
          <xsl:text>},style=</xsl:text>
          <xsl:value-of select="@style" />
        </xsl:variable>
        <xsl:call-template name="command">
            <xsl:with-param name="name" select="'lstinputlisting'" />
            <xsl:with-param name="opt" select="$opt" />
            <xsl:with-param name="param" select="@file" />
        </xsl:call-template>
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