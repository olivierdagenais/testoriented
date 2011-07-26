using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="DeletedPhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class DeletedPhraseBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var dpbm = new DeletedPhraseBlockModifier ();
            var actual = dpbm.ModifyLine ("I'm -sure- not sure.");
            Assert.AreEqual ("I'm <del>sure</del> not sure.", actual);
        }
    }
}
