using System;
using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="BlockAttributesParser"/>.
    /// </summary>
    [TestFixture]
    public class BlockAttributesParserTest
    {

        [Test]
        public void ParseBlockAttributes_TableData ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes ("\\4/5^", "td");
            Assert.AreEqual (@" style=""vertical-align:top;"" colspan=""4"" rowspan=""5""", actual);
        }

        [Test]
        public void ParseBlockAttributes_CustomStyles ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes ("{border:1px solid black}");
            Assert.AreEqual (@" style=""border:1px solid black;""", actual);
        }

        [Test]
        public void ParseBlockAttributes_Language ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes ("[fr]");
            Assert.AreEqual (@" lang=""fr""", actual);
        }

        [Test]
        public void ParseBlockAttributes_ClassAndID ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes ("(class#id)");
            Assert.AreEqual (@" class=""class"" id=""id""", actual);
        }

        [Test]
        public void ParseBlockAttributes_Class ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes ("(class)");
            Assert.AreEqual (@" class=""class""", actual);
        }

        [Test]
        public void ParseBlockAttributes_ID ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes ("(#id)");
            Assert.AreEqual (@" id=""id""", actual);
        }

        [Test]
        public void ParseBlockAttributes_PaddingLeft ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes ("(");
            Assert.AreEqual (@" style=""padding-left:1em;""", actual);
        }

        [Test]
        public void ParseBlockAttributes_PaddingRight ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes (")))");
            Assert.AreEqual (@" style=""padding-right:3em;""", actual);
        }

        [Test]
        public void ParseBlockAttributes_HorizontalAlignment ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes ("<");
            Assert.AreEqual (@" style=""text-align:left;""", actual);
        }

        [Test]
        public void ParseBlockAttributes_EverythingPossible ()
        {
            var actual =
                BlockAttributesParser.ParseBlockAttributes ("\\4/5^{border:1px solid black}[fr](class#id)()<>", "td");
            Assert.AreEqual (
                  @" style=""vertical-align:top;border:1px solid black;padding-left:1em;padding-right:1em;text-align:justify;"""
                + @" class=""class"" lang=""fr"" id=""id"" colspan=""4"" rowspan=""5""", actual);
        }

        [Test]
        public void ParseBlockAttributes_EmptyInput ()
        {
            var actual = BlockAttributesParser.ParseBlockAttributes (String.Empty);
            Assert.AreEqual (String.Empty, actual);
        }
    }
}
