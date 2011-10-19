using System.Text.RegularExpressions;
using NUnit.Framework;
using Textile.States;

namespace Textile.ManualTests.States
{
    /// <summary>
    /// A class to test <see cref="FootNoteFormatterState"/>.
    /// </summary>
    [TestFixture]
    public class FootNoteFormatterStateTest
    {
        [Test]
        public void EnterAndOnContextAcquired()
        {
            // arrange
            var output = new StringBuilderTextileFormatter ();
            output.Begin();
            var fnfs = new FootNoteFormatterState(new TextileFormatter(output));
            var expression = SimpleBlockFormatterState.PatternBegin + @"fn[0-9]+" + SimpleBlockFormatterState.PatternEnd;
            var input = "fn1{color:red}. This is the footnote";
            Match m = Regex.Match(input, expression);
            fnfs.Consume (input, m);

            // act
            // do nothing, since Consume() already caused OnContextAcquired() and Enter() to be called

            // assert
            Assert.AreEqual("<p id=\"fn1\" style=\"color:red;\"><sup>1</sup> ", output.GetFormattedText());
        }

    }
}
