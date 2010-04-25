<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msbuild="http://schemas.microsoft.com/developer/msbuild/2003"
    version="1.0"
    exclude-result-prefixes="msbuild">

    <xsl:output method="xml" encoding="UTF-8" indent="yes" omit-xml-declaration="yes" />
    <xsl:strip-space elements="*" />
    
    <xsl:param name="baseDir" />
    <xsl:param name="projectName" />

    <!-- Pass-through (identity transform) template -->
    <xsl:template match="* | @* | node()">
        <xsl:copy>
            <xsl:apply-templates select="* | @* | node()" />
        </xsl:copy>
    </xsl:template>

    <!-- Remove entire redundant block, because there's a project reference already doing this better:
    <Reference Include="StringExtensions">
      <HintPath>(...)\trunk\Projects\StringExtensions\testoriented\StringExtensions\bin\Debug\StringExtensions.dll</HintPath>
    </Reference>
    -->
    <xsl:template match="msbuild:Reference[msbuild:HintPath]">
        <xsl:choose>
            <xsl:when test="starts-with(@Include, $projectName) and starts-with(string(msbuild:HintPath), $baseDir)" />
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates select="* | @* | node()" />
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template match="msbuild:Reference[@Include='mscorlib']" />
    <xsl:template match="msbuild:Reference[@Include='nunit.framework, Version=2.4.8.0, Culture=neutral, PublicKeyToken=96d09a1eb7f44a77']" />

</xsl:stylesheet>