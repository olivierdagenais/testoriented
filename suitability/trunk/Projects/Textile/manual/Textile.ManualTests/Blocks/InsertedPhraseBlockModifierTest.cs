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
            var actual = InsertedPhraseBlockModifier.InnerModifyLine ("You are a +pleasant+ child.");
            Assert.AreEqual ("You are a <ins>pleasant</ins> child.", actual);
        }
    }
}
