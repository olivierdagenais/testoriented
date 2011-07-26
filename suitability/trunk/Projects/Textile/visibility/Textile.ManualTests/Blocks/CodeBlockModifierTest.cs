using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="CodeBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class CodeBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var cbm = new CodeBlockModifier();
            var actual = cbm.ModifyLine ("(@|ruby|r.to_html@)");
            Assert.AreEqual (@"(<code language=""ruby"">r.to_html</code>)", actual);
        }

        [Test]
        public void Conclude ()
        {
            const string input = @"<code language=""ruby"">return &#39;3 < 5&#39;</code>";
            var cbm = new CodeBlockModifier ();
            var actual = cbm.Conclude (input);
            Assert.AreEqual (@"<code language=""ruby"">return '3 < 5'</code>", actual);
        }
    }
}
