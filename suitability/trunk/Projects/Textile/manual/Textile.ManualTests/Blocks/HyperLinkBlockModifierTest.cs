using System.Text.RegularExpressions;
using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="HyperLinkBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class HyperLinkBlockModifierTest
    {
        [Test]
        public void InnerModifyLine_Simple ()
        {
            var actual = HyperLinkBlockModifier.InnerModifyLine ("I searched \"Google\":http://google.com.");
            Assert.AreEqual ("I searched <a href=\"http://google.com\">Google</a>.", actual);
        }

        private static string AssertComplexParsing(Match m)
        {
            Assert.AreEqual ("(", m.Groups["pre"].Value);
            Assert.AreEqual ("(hyperlink)", m.Groups["atts"].Value);
            Assert.AreEqual ("link to a website", m.Groups["title"].Value);
            Assert.AreEqual ("click here", m.Groups["text"].Value);
            Assert.AreEqual ("http://example.com", m.Groups["url"].Value);
            Assert.AreEqual ("/", m.Groups["slash"].Value);
            Assert.AreEqual (")", m.Groups["post"].Value);
            return null;
        }

        [Test]
        public void InnerModifyLine_ParseComplex ()
        {
            HyperLinkBlockModifier.InnerModifyLine (
                "The website (\"(hyperlink)click here(link to a website)\":http://example.com/) explains everything.",
                AssertComplexParsing);
        }

        [Test]
        public void InnerModifyLine_Complex ()
        {
            var actual = HyperLinkBlockModifier.InnerModifyLine (
                "The website (\"(hyperlink)click here(link to a website)\":http://example.com/) explains everything.");
            Assert.AreEqual (
                "The website (<a href=\"http://example.com/\" class=\"hyperlink\" title=\"link to a website\">click here</a>) explains everything.",
                actual);
        }

        [Test]
        public void BuildHyperlinkElementString ()
        {
            var actual = HyperLinkBlockModifier.BuildHyperlinkElementString
                ("(", "(hyperlink)", "link to a website", "click here", "http://example.com", "/", ")");
            Assert.AreEqual (
                "(<a href=\"http://example.com/\" class=\"hyperlink\" title=\"link to a website\">click here</a>)",
                actual);
        }
    }
}
