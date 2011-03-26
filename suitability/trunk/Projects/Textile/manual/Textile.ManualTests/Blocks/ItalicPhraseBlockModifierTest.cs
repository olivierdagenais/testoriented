using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="ItalicPhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class ItalicPhraseBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var actual = ItalicPhraseBlockModifier.InnerModifyLine ("I __know__.");
            Assert.AreEqual ("I <i>know</i>.", actual);
        }
    }
}
