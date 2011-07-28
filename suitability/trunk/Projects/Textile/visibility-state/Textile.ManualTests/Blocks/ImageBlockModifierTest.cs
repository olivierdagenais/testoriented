using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="ImageBlockModifier"/>.
    /// </summary>
    [TestFixture]
    public class ImageBlockModifierTest
    {
        [Test]
        public void ModifyLine_Simple ()
        {
            var ibm = new ImageBlockModifier ();
            var actual = ibm.ModifyLine ("!http://redcloth.org/hobix.com/textile/sample.jpg!");
            Assert.AreEqual ("<img src=\"http://redcloth.org/hobix.com/textile/sample.jpg\" alt=\"\" />", actual);
        }

        [Test]
        public void ModifyLine_Complex ()
        {
            var ibm = new ImageBlockModifier ();
            var actual = ibm.ModifyLine ("!>(image)openwindow1.gif(Bunny.)!:http://hobix.com/");
            Assert.AreEqual (
                "<a href=\"http://hobix.com/\">"
                + "<img src=\"openwindow1.gif\" class=\"image\" align=\"right\" title=\"Bunny.\" alt=\"Bunny.\" />" 
                + "</a>",
                actual);
        }

        [Test, Ignore ("Exposes a bug in the handling of href")]
        public void ModifyLine_Bug ()
        {
            var ibm = new ImageBlockModifier ();
            var actual = ibm.ModifyLine ("|!>(image)openwindow1.gif(Bunny.)!:http://hobix.com/|");
            Assert.AreEqual (
                "|<a href=\"http://hobix.com/\">"
                + "<img src=\"openwindow1.gif\" class=\"image\" align=\"right\" title=\"Bunny.\" alt=\"Bunny.\" />"
                + "</a>|",
                actual);
        }
    }
}
