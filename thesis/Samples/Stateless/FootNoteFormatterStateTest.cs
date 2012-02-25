using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;

[TestFixture]
public class FootNoteFormatterStateTest
{
[Test]
public void FormatFootNote()
{
  // act
  var actual =
    FootNoteFormatterState.FormatFootNote
      (1, " style=\"color:red\"");

  // assert
  Assert.AreEqual(
    "<p id=\"fn1\" style=\"color:red\"><sup>1</sup> ",
    actual);
}

[Test]
public void ParseFootNoteId()
{
  // arrange
  var input = "fn1{color:red}. This is the footnote";

  // act
  var actual = FootNoteFormatterState.ParseFootNoteId(input);

  // assert
  Assert.AreEqual(1, actual);
}

[Test]
public void EnterAndOnContextAcquired()
{
  // arrange
  var output = new StringWriter();
  var fnfs = new FootNoteFormatterState(output);
  var expression = @"^\s*(?<tag>fn[0-9]+)"
    + @"(?:\{(?<atts>[^}]+)\})?"
    + @"\.(?:\s+)?(?<content>.*)$";
  var input = "fn1{color:red}. This is the footnote";
  Match m = Regex.Match(input, expression);

  // act
  // Consume() causes OnContextAcquired()
  // and Enter() to be called
  fnfs.Consume(m);

  // assert
  Assert.AreEqual(
    "<p id=\"fn1\" style=\"color:red\"><sup>1</sup> ",
    output.ToString());
}
}
