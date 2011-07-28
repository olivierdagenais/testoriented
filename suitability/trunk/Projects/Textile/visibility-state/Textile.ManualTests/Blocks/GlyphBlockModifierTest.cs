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
            var gbm = new GlyphBlockModifier();
            var actual = gbm.ModifyLine("{stuff'}");
            Assert.AreEqual ("{stuff&#8217;}", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingWithPossessive ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("{Oli's stuff}");
            Assert.AreEqual ("{Oli&#8217;s stuff}", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingWithContraction ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("{Don't do it}");
            Assert.AreEqual ("{Don&#8217;t do it}", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingAroundParenthesis ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("'(quoted)'");
            Assert.AreEqual ("&#8217;(quoted)&#8217;", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingAroundSquareBrackets ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("'[quoted]'");
            Assert.AreEqual ("&#8217;[quoted]&#8217;", actual);
        }

        [Test]
        public void ModifyLine_SingleClosingAroundReversedAngleBrackets ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("'>quoted<'");
            Assert.AreEqual ("&#8217;>quoted<&#8217;", actual);
        }

        [Test]
        public void ModifyLine_SingleOpeningAroundAngleBrackets ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("'<quoted>'");
            Assert.AreEqual ("&#8216;<quoted>&#8216;", actual);
        }

        [Test]
        public void ModifyLine_SingleOpeningAndClosing ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("'quoted'");
            Assert.AreEqual ("&#8216;quoted&#8217;", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingBeforePunctuation ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("{stuff\"}");
            Assert.AreEqual ("{stuff&#8221;}", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingWithPossessive ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("{Oli\"s stuff}");
            Assert.AreEqual ("{Oli&#8221;s stuff}", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingWithContraction ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("{Don\"t do it}");
            Assert.AreEqual ("{Don&#8221;t do it}", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingAroundParenthesis ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("\"(quoted)\"");
            Assert.AreEqual ("&#8221;(quoted)&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingAroundSquareBrackets ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("\"[quoted]\"");
            Assert.AreEqual ("&#8221;[quoted]&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_DoubleClosingAroundReversedAngleBrackets ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("\">quoted<\"");
            Assert.AreEqual ("&#8221;>quoted<&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_DoubleOpeningAndClosingAroundAngleBrackets ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("\"<quoted>\"");
            Assert.AreEqual ("&#8220;<quoted>&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_DoubleOpeningAndClosing ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("\"quoted\"");
            Assert.AreEqual ("&#8220;quoted&#8221; ", actual);
        }

        [Test]
        public void ModifyLine_Ellipsis ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("Observe...");
            Assert.AreEqual ("Observe&#8230;", actual);
        }

        [Test]
        public void ModifyLine_UppercaseAcronym ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("We use CSS(Cascading Style Sheets).");
            Assert.AreEqual (@"We use <acronym title=""Cascading Style Sheets"">CSS</acronym>.", actual);
        }

        [Test]
        public void ModifyLine_EmDash ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("Observe -- very nice!");
            Assert.AreEqual (@"Observe &#8212; very nice!", actual);
        }

        [Test]
        public void ModifyLine_EnDash ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("Observe - tiny and brief.");
            Assert.AreEqual (@"Observe &#8211; tiny and brief.", actual);
        }

        [Test]
        public void ModifyLine_DimensionSign ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("Observe: 2 x 2.");
            Assert.AreEqual (@"Observe: 2 &#215; 2.", actual);
        }

        [Test]
        public void ModifyLine_TrademarkRegisteredCopyright ()
        {
            var gbm = new GlyphBlockModifier ();
            var actual = gbm.ModifyLine ("one(TM), two(R), three(C).");
            Assert.AreEqual (@"one&#8482;, two&#174;, three&#169;.", actual);
        }
    }
}
