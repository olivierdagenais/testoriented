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
        public void InnerModifyLine ()
        {
            var actual = CodeBlockModifier.InnerModifyLine ("(@|ruby|r.to_html@)");
            Assert.AreEqual (@"(<code language=""ruby"">r.to_html</code>)", actual);
        }

        [Test]
        public void InnerConclude ()
        {
            const string input = @"<code language=""ruby"">return &#39;3 < 5&#39;</code>";
            var actual = CodeBlockModifier.InnerConclude (input);
            Assert.AreEqual (@"<code language=""ruby"">return '3 < 5'</code>", actual);
        }

        [Test]
        public void BuildCodeElementString ()
        {
            var actual = CodeBlockModifier.BuildCodeElementString ("(", "ruby", "r.to_html", ")");
            Assert.AreEqual (@"(<code language=""ruby"">r.to_html</code>)", actual);
        }
    }
}
