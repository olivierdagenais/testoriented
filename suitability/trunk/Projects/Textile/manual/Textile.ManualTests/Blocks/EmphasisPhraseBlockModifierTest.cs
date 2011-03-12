using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="EmphasisPhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class EmphasisPhraseBlockModifierTest
    {
        [Test]
        public void InnerModifyLine ()
        {
            var actual = EmphasisPhraseBlockModifier.InnerModifyLine ("I _believe_ every word.");
            Assert.AreEqual ("I <em>believe</em> every word.", actual);
        }
    }
}
