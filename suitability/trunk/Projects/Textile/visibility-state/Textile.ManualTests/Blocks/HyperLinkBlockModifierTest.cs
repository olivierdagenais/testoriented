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
        public void ModifyLine_Simple ()
        {
            var hlbm = new HyperLinkBlockModifier ();
            var actual = hlbm.ModifyLine ("I searched \"Google\":http://google.com.");
            Assert.AreEqual ("I searched <a href=\"http://google.com\">Google</a>.", actual);
        }

        [Test]
        public void ModifyLine_Complex ()
        {
            var hlbm = new HyperLinkBlockModifier ();
            var actual = hlbm.ModifyLine
                ("The website (\"(hyperlink)click here(link to a website)\":http://example.com/) explains everything.");
            Assert.AreEqual (
                "The website (<a href=\"http://example.com/\" class=\"hyperlink\" title=\"link to a website\">click here</a>) explains everything.",
                actual);
        }
    }
}
