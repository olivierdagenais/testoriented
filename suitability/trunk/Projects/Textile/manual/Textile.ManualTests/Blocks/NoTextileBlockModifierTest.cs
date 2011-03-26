using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="NoTextileBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class NoTextileBlockModifierTest
    {
        [Test]
        public void ModifyLine_Element ()
        {
            var actual = NoTextileBlockModifier.InnerModifyLine ("<notextile>\"%*+-<=>?^_~@'|!().x</notextile>");
            Assert.AreEqual (
                "&#34;&#37;&#42;&#43;&#45;&lt;&#61;&gt;&#63;&#94;&#95;&#126;&#64;&#39;&#124;&#33;&#40;&#41;&#46;&#120;",
                actual);
        }

        [Test]
        public void Conclude ()
        {
            var actual = NoTextileBlockModifier.InnerConclude ("&#120;&#40;&#41;&#46;");
            Assert.AreEqual ("x().", actual);
        }
    }
}
