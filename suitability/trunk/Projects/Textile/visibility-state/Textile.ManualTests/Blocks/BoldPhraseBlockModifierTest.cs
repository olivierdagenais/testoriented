using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="BoldPhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class BoldPhraseBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var bpbm = new BoldPhraseBlockModifier ();
            var actual = bpbm.ModifyLine ("I **really** __know__");
            Assert.AreEqual ("I <b>really</b> __know__", actual);
        }
    }
}
