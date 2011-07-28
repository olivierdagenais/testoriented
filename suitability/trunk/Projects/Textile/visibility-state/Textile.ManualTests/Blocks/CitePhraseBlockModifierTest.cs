using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="CitePhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class CitePhraseBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var cpbm = new CitePhraseBlockModifier ();
            var actual = cpbm.ModifyLine ("??Cat's Cradle?? by Vonnegut");
            Assert.AreEqual ("<cite>Cat's Cradle</cite> by Vonnegut", actual);
        }
    }
}
