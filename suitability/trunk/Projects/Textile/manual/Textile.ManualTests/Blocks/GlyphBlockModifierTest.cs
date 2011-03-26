using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="GlyphBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class GlyphBlockModifierTest
    {
        [Test]
        public void ModifyLine_SingleClosingBeforePunctuation ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("{stuff'}");
            Assert.AreEqual ("{stuff&#8217;}", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingWithPossessive ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("{Oli's stuff}");
            Assert.AreEqual ("{Oli&#8217;s stuff}", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingWithContraction ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("{Don't do it}");
            Assert.AreEqual ("{Don&#8217;t do it}", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingAroundParenthesis ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("'(quoted)'");
            Assert.AreEqual ("&#8217;(quoted)&#8217;", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingAroundSquareBrackets ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("'[quoted]'");
            Assert.AreEqual ("&#8217;[quoted]&#8217;", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingAroundReversedAngleBrackets ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("'>quoted<'");
            Assert.AreEqual ("&#8217;>quoted<&#8217;", actual);
        }

        [Test]
        public void ModifyLine_SingleOpeningAroundAngleBrackets ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("'<quoted>'");
            Assert.AreEqual ("&#8216;<quoted>&#8216;", actual);
        }

        [Test]
        public void ModifyLine_SingleOpeningAndClosing ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("'quoted'");
            Assert.AreEqual ("&#8216;quoted&#8217;", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingBeforePunctuation ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("{stuff\"}");
            Assert.AreEqual ("{stuff&#8221;}", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingWithPossessive ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("{Oli\"s stuff}");
            Assert.AreEqual ("{Oli&#8221;s stuff}", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingWithContraction ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("{Don\"t do it}");
            Assert.AreEqual ("{Don&#8221;t do it}", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingAroundParenthesis ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("\"(quoted)\"");
            Assert.AreEqual ("&#8221;(quoted)&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingAroundSquareBrackets ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("\"[quoted]\"");
            Assert.AreEqual ("&#8221;[quoted]&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingAroundReversedAngleBrackets ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("\">quoted<\"");
            Assert.AreEqual ("&#8221;>quoted<&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_DoubleOpeningAndClosingAroundAngleBrackets ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("\"<quoted>\"");
            Assert.AreEqual ("&#8220;<quoted>&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_DoubleOpeningAndClosing ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("\"quoted\"");
            Assert.AreEqual ("&#8220;quoted&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_Ellipsis ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("Observe...");
            Assert.AreEqual ("Observe&#8230;", actual);
        }

        [Test]
        public void ModifyLine_UppercaseAcronym ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("We use CSS(Cascading Style Sheets).");
            Assert.AreEqual (@"We use <acronym title=""Cascading Style Sheets"">CSS</acronym>.", actual);
        }

        [Test]
        public void ModifyLine_EmDash ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("Observe -- very nice!");
            Assert.AreEqual (@"Observe &#8212; very nice!", actual);
        }

        [Test]
        public void ModifyLine_EnDash ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("Observe - tiny and brief.");
            Assert.AreEqual (@"Observe &#8211; tiny and brief.", actual);
        }

        [Test]
        public void ModifyLine_DimensionSign ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("Observe: 2 x 2.");
            Assert.AreEqual (@"Observe: 2 &#215; 2.", actual);
        }

        [Test]
        public void ModifyLine_TrademarkRegisteredCopyright ()
        {
            var actual = GlyphBlockModifier.InnerModifyLine ("one(TM), two(R), three(C).");
            Assert.AreEqual (@"one&#8482;, two&#174;, three&#169;.", actual);
        }
    }
}
