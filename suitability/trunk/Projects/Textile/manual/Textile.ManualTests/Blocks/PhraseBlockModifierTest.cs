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
            var actual = PhraseBlockModifier.InternalPhraseModifierFormat ("/WARNING/", "/", "marquee");
            Assert.AreEqual ("<marquee>WARNING</marquee>", actual);
        }

        [Test]
        public void PhraseModifierFormat_ContentLooksLikeCss ()
        {
            var actual = PhraseBlockModifier.InternalPhraseModifierFormat ("*(blah)*", "\\*", "strong");
            Assert.AreEqual ("<strong>(blah)</strong>", actual);
        }

        [Test]
        public void PhraseModifierFormat_WithCss ()
        {
            var actual = PhraseBlockModifier.InternalPhraseModifierFormat ("/(annoying)WARNING/", "/", "marquee");
            Assert.AreEqual ("<marquee class=\"annoying\">WARNING</marquee>", actual);
        }

        [Test]
        public void PhraseModifierFormat_WithCitation ()
        {
            var actual = PhraseBlockModifier.InternalPhraseModifierFormat ("/:cited WARNING/", "/", "marquee");
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
