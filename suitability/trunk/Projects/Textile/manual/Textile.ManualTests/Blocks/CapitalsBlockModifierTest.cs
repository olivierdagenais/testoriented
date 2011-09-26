using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="CapitalsBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class CapitalsBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var actual = CapitalsBlockModifier.InnerModifyLine ("CSS");
            Assert.AreEqual (@"<span class=""caps"">CSS</span>", actual);
        }
    }
}
