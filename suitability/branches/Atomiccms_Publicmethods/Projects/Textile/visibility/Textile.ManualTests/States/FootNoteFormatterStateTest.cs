using System;
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
            var output = new StringBuilderTextileFormatter ();
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
            var output = new StringBuilderTextileFormatter();
            output.Begin();
            var fnfs = new FootNoteFormatterState(new TextileFormatter(output));
            fnfs.m_tag = "fn42";

            // act
            fnfs.OnContextAcquired();

            // assert
            Assert.AreEqual(42, fnfs.m_noteID);
        }
    }
}
