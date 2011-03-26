using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="PhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class PhraseBlockModifierTest
    {
        private class PhraseBlockModifierTester : PhraseBlockModifier
        {
            public new string PhraseModifierFormat (string input, string modifier, string tag)
            {
                return base.PhraseModifierFormat (input, modifier, tag);
            }
        }

        [Test]
        public void PhraseModifierFormat ()
        {
            var pbmt = new PhraseBlockModifierTester ();
            var actual = pbmt.PhraseModifierFormat ("/WARNING/", "/", "marquee");
            Assert.AreEqual ("<marquee>WARNING</marquee>", actual);
        }

        [Test]
        public void PhraseModifierFormat_ContentLooksLikeCss ()
        {
            var pbmt = new PhraseBlockModifierTester ();
            var actual = pbmt.PhraseModifierFormat ("*(blah)*", "\\*", "strong");
            Assert.AreEqual ("<strong>(blah)</strong>", actual);
        }

        [Test]
        public void PhraseModifierFormat_WithCss ()
        {
            var pbmt = new PhraseBlockModifierTester ();
            var actual = pbmt.PhraseModifierFormat ("/(annoying)WARNING/", "/", "marquee");
            Assert.AreEqual ("<marquee class=\"annoying\">WARNING</marquee>", actual);
        }

        [Test]
        public void PhraseModifierFormat_WithCitation ()
        {
            var pbmt = new PhraseBlockModifierTester ();
            var actual = pbmt.PhraseModifierFormat ("/:cited WARNING/", "/", "marquee");
            Assert.AreEqual ("<marquee cite=\"cited\"> WARNING</marquee>", actual);
        }
    }
}
