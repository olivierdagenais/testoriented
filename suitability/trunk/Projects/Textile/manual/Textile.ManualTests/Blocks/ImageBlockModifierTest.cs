using System.Text.RegularExpressions;
using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="ImageBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class ImageBlockModifierTest
    {
        [Test]
        public void ModifyLine_Simple ()
        {
            var actual = ImageBlockModifier.InnerModifyLine ("!http://redcloth.org/hobix.com/textile/sample.jpg!");
            Assert.AreEqual ("<img src=\"http://redcloth.org/hobix.com/textile/sample.jpg\" alt=\"\" />", actual);
        }

        private static string AssertComplexParsing (Match m)
        {
            Assert.AreEqual (">", m.Groups["algn"].Value);
            Assert.AreEqual ("(image)", m.Groups["atts"].Value);
            Assert.AreEqual ("openwindow1.gif", m.Groups["url"].Value);
            Assert.AreEqual ("Bunny.", m.Groups["title"].Value);
            Assert.AreEqual ("http://hobix.com/", m.Groups["href"].Value);
            return null;
        }

        [Test]
        public void ModifyLine_ParseComplex ()
        {
            ImageBlockModifier.InnerModifyLine
                ("!>(image)openwindow1.gif(Bunny.)!:http://hobix.com/", AssertComplexParsing);
        }

        [Test]
        public void BuildImageElementString_Complex ()
        {
            var actual = ImageBlockModifier.BuildImageElementString
                ("(image)", ">", "Bunny.", "openwindow1.gif", "http://hobix.com/");
            Assert.AreEqual (
                "<a href=\"http://hobix.com/\">"
                + "<img src=\"openwindow1.gif\" class=\"image\" align=\"right\" title=\"Bunny.\" alt=\"Bunny.\" />"
                + "</a>",
                actual);
        }

        [Test, Ignore ("Exposes a bug in the handling of href")]
        public void ModifyLine_Bug ()
        {
            var actual = ImageBlockModifier.InnerModifyLine ("|!>(image)openwindow1.gif(Bunny.)!:http://hobix.com/|");
            Assert.AreEqual (
                "|<a href=\"http://hobix.com/\">"
                + "<img src=\"openwindow1.gif\" class=\"image\" align=\"right\" title=\"Bunny.\" alt=\"Bunny.\" />"
                + "</a>|",
                actual);
        }
    }
}
