using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="FootNoteReferenceBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class FootNoteReferenceBlockModifierTest
    {
        [Test]
        public void InnerModifyLine ()
        {
            var actual = FootNoteReferenceBlockModifier.InnerModifyLine ("This is covered elsewhere[1].");
            Assert.AreEqual (@"This is covered elsewhere<sup><a href=""#fn1"">1</a></sup>.", actual);
        }
    }
}
