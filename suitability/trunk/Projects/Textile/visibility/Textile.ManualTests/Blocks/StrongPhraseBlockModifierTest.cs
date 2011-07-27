using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="StrongPhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class StrongPhraseBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var spbm = new StrongPhraseBlockModifier ();
            var actual = spbm.ModifyLine ("And then? She *fell*!");
            Assert.AreEqual ("And then? She <strong>fell</strong>!", actual);
        }
    }
}