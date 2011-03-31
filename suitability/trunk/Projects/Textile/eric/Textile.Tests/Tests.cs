using NUnit.Framework;

namespace Textile.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Hyperlink()
        {
            const string input = @"""get lost"":http://google.ca";
            const string expected = "<p><a href=\"http://google.ca\">get lost</a></p>\r\n";
            var actual = TextileFormatter.FormatString(input);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BulletList()
        {
            const string input = @"* me
* is
* the
* hello";
            const string expected = @"<ul>
<li>me</li>
<li>is</li>
<li>the</li>
<li>hello</li>
</ul>
";
            var actual = TextileFormatter.FormatString(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
