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

    <!-- Remove redundant content -->

    <!-- <Compile Include="Properties\Properties\AssemblyInfo.cs.cs" /> -->
    <xsl:template match="msbuild:Compile[@Include='Properties\Properties\AssemblyInfo.cs.cs']" />

    <!-- <Reference Include="mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" /> -->
    <xsl:template match="msbuild:Reference[@Include='mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089']" />

    <!-- <Reference Include="Microsoft.Pex.Framework, Version=0.22.50128.1, Culture=neutral, PublicKeyToken=76a274db078248c8" /> -->
    <xsl:template match="msbuild:Reference[@Include='Microsoft.Pex.Framework, Version=0.22.50128.1, Culture=neutral, PublicKeyToken=76a274db078248c8']" />

    <!-- Remove entire redundant block:
    <Reference Include="StringExtensions">
      <HintPath>(...)\trunk\Projects\StringExtensions\testoriented\StringExtensions\bin\Debug\StringExtensions.dll</HintPath>
    </Reference>
    -->
    <xsl:template match="msbuild:Reference[msbuild:HintPath]">
        <xsl:choose>
            <xsl:when test="@Include=$projectName" />
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates select="* | @* | node()" />
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- Rewrite absolute HintPath as relative -->
    <xsl:template match="msbuild:HintPath">
        <xsl:copy>
            <xsl:choose>
                <xsl:when test="starts-with(string(.), $baseDir)">
                    <xsl:text>..\..\..\..</xsl:text><xsl:value-of select="substring-after(string(.), $baseDir)" />
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="string(.)" />
                </xsl:otherwise>
            </xsl:choose>
        </xsl:copy>
    </xsl:template>

    <!-- Fix partial reference <Reference Include="Microsoft.Pex.Linq" /> -->
    <xsl:template match="msbuild:Reference[@Include='Microsoft.Pex.Linq']">
        <xsl:copy>
            <xsl:apply-templates select="@*" />
            <HintPath xmlns="http://schemas.microsoft.com/developer/msbuild/2003">..\..\..\..\Tools\Pex-0.22.50128.1\Microsoft.Pex.Linq.dll</HintPath>
        </xsl:copy>
    </xsl:template>

</xsl:stylesheet>