using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="PreBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class PreBlockModifierTest
    {
        [Test]
        public void ModifyLine ()
        {
            var actual = PreBlockModifier.InnerModifyLine ("<pre><code language=\"C#\">[Test] public void ModifyLine() { }</code></pre>");
            Assert.AreEqual (
                "<pre>"
                + "&lt;code language&#61;&#34;C#&#34;&gt;[Test] public void ModifyLine&#40;&#41; { }&lt;/code&gt;"
                + "</pre>", actual);
        }

        [Test, Ignore ("Exposes a bug in HtmlAttributesPattern")]
        public void ModifyLine_EmptyDoubleQuotedAttribute ()
        {
            var actual = PreBlockModifier.InnerModifyLine ("<pre empty=\"\">[Test] public void ModifyLine() { }</pre>");
            Assert.AreEqual (
                "<pre empty=\"\">"
                + "[Test] public void ModifyLine&#40;&#41; { }"
                + "</pre>", actual);
        }

        [Test, Ignore ("Exposes a bug in HtmlAttributesPattern")]
        public void ModifyLine_EmptySingleQuotedAttribute ()
        {
            var actual = PreBlockModifier.InnerModifyLine ("<pre empty=''>[Test] public void ModifyLine() { }</pre>");
            Assert.AreEqual (
                "<pre empty=''>"
                + "[Test] public void ModifyLine&#40;&#41; { }"
                + "</pre>", actual);
        }

        [Test]
        public void Conclude ()
        {
            var actual = PreBlockModifier.InnerConclude (
                "<pre>"
                + "&lt;code language&#61;&#34;C#&#34;&gt;[Test] public void ModifyLine&#40;&#41; { }&lt;/code&gt;"
                + "</pre>");
            Assert.AreEqual (
                "<pre>"
                + "&lt;code language=\"C#\"&gt;[Test] public void ModifyLine() { }&lt;/code&gt;"
                + "</pre>", actual);
        }
    }
}
