using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="InsertedPhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class InsertedPhraseBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var ipbm = new InsertedPhraseBlockModifier ();
            var actual = ipbm.ModifyLine ("You are a +pleasant+ child.");
            Assert.AreEqual ("You are a <ins>pleasant</ins> child.", actual);
        }
    }
}
