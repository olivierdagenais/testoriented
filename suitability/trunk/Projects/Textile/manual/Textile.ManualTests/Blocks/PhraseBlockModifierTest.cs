using System;
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
        [Test]
        public void PhraseModifierFormat ()
        {
            var actual = PhraseBlockModifier.PhraseModifierFormat ("/WARNING/", "/", "marquee");
            Assert.AreEqual ("<marquee>WARNING</marquee>", actual);
        }

        [Test]
        public void PhraseModifierFormat_ContentLooksLikeCss ()
        {
            var actual = PhraseBlockModifier.PhraseModifierFormat ("*(blah)*", "\\*", "strong");
            Assert.AreEqual ("<strong>(blah)</strong>", actual);
        }

        [Test]
        public void PhraseModifierFormat_WithCss ()
        {
            var actual = PhraseBlockModifier.PhraseModifierFormat ("/(annoying)WARNING/", "/", "marquee");
            Assert.AreEqual ("<marquee class=\"annoying\">WARNING</marquee>", actual);
        }

        [Test]
        public void PhraseModifierFormat_WithCitation ()
        {
            var actual = PhraseBlockModifier.PhraseModifierFormat ("/:cited WARNING/", "/", "marquee");
            Assert.AreEqual ("<marquee cite=\"cited\"> WARNING</marquee>", actual);
        }

        [Test]
        public void BuildTagElementString ()
        {
            var actual = PhraseBlockModifier.BuildTagElementString
                ("WARNING", "(annoying)", String.Empty, "marquee", String.Empty, String.Empty);
            Assert.AreEqual ("<marquee class=\"annoying\">WARNING</marquee>", actual);
        }
    }
}
