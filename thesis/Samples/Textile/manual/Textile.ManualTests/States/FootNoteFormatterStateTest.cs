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
/// <summary>
/// Tests the stateless part of the <see cref="FootNoteFormatterState.Enter()"/> method.
/// </summary>
[Test]
public void FormatFootNote()
{
    // act
    var actual = FootNoteFormatterState.FormatFootNote(1, " style=\"color:red;\"");

    // assert
    Assert.AreEqual("<p id=\"fn1\" style=\"color:red;\"><sup>1</sup> ", actual);
}

/// <summary>
/// Tests the stateless part of the <see cref="FootNoteFormatterState.OnContextAcquired()"/> method.
/// </summary>
[Test]
public void ParseFootNoteId()
{
    // act
    var actual = FootNoteFormatterState.ParseFootNoteId("fn42");

    // assert
    Assert.AreEqual(42, actual);
}
    }
}
