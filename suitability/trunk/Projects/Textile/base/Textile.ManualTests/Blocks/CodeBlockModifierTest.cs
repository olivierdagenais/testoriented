using System.Text.RegularExpressions;
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

        [Test]
        public void CodeFormatMatchEvaluator ()
        {
            var m = Regex.Match (
                  "Call the ruby r_tohtml(); method",
                  @"(?<before>Call\sthe\s)" +
                  @"(?<lang>ruby)\s" +
                  @"(?<code>r_tohtml\(\);)" +
                  @"(?<after>\smethod)"
            );
            var actual = CodeBlockModifier.CodeFormatMatchEvaluator(m);
            Assert.AreEqual ("Call the <code language=\"ruby\">r_tohtml();</code> method", actual);
        }
    }
}
