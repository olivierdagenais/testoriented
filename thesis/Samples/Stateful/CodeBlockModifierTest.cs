using System.Text.RegularExpressions;
using NUnit.Framework;

[TestFixture]
public class CodeBlockModifierTest
{
  [Test]
  public void ModifyLine()
  {
    var cbm = new CodeBlockModifier();
    const string input =
      "Call the @|ruby|r_tohtml();@ method";
    var actual = cbm.ModifyLine(input);
    const string expected =
      "Call the <code language=\"ruby\">r_tohtml();"
      + "</code> method";
    Assert.AreEqual(expected, actual);
  }

  [Test]
  public void CodeFormatMatchEvaluator()
  {
    var m = Regex.Match(
      "Call the ruby r_tohtml(); method",
      @"(?<before>Call\sthe\s)" +
      @"(?<lang>ruby)\s" +
      @"(?<code>r_tohtml\(\);)" +
      @"(?<after>\smethod)"
    );
    var actual =
      CodeBlockModifier.CodeFormatMatchEvaluator(m);
    const string expected =
      "Call the <code language=\"ruby\">r_tohtml();"
      + "</code> method";
    Assert.AreEqual(expected, actual);
  }
}
