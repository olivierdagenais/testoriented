using Textile.States;
using NUnit.Framework;

namespace Textile.ManualTests.States
{
    /// <summary>
    /// A class to test <see cref="FootNoteFormatterState"/>.
    /// </summary>
    [TestFixture]
    public class FootNoteFormatterStateTest
    {
        [Test]
        public void FormatFootNote()
        {
            // act
            var actual = FootNoteFormatterState.FormatFootNote(1, " style=\"color:red;\"");

            // assert
            Assert.AreEqual("<p id=\"fn1\" style=\"color:red;\"><sup>1</sup> ", actual);
        }
    }
}
