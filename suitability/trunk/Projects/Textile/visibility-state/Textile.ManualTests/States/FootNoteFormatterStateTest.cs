using System;
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
        public void Enter()
        {
            // arrange
            var output = new StringBuilderTextileFormatter();
            output.Begin();
            var fnfs = new FootNoteFormatterState(new TextileFormatter(output));
            fnfs.m_noteID = 1;
            fnfs.m_alignNfo = String.Empty;
            fnfs.m_attNfo = "{color:red}";

            // act
            fnfs.Enter();

            // assert
            Assert.AreEqual("<p id=\"fn1\" style=\"color:red;\"><sup>1</sup> ", output.GetFormattedText());
        }

        [Test]
        public void OnContextAcquired()
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
            // do nothing, since Consume() already caused OnContextAcquired() to be called

            // assert
            Assert.AreEqual(1, fnfs.m_noteID);
        }
    }
}
