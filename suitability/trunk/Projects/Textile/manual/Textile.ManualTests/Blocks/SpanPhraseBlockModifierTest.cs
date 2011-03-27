using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="SpanPhraseBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class SpanPhraseBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var actual = SpanPhraseBlockModifier.InnerModifyLine ("I'm %{color:red}unaware% of most soft drinks.");
            Assert.AreEqual ("I'm <span style=\"color:red;\">unaware</span> of most soft drinks.", actual);
        }
    }
}
