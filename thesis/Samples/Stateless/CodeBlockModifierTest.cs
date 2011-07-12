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
  public void ParseZone()
  {
    const string input =
      "Call the [@|ruby|r_tohtml();@] method";
    var actual =
      CodeBlockModifier.CodeBlockRegex.Match(input);
    Assert.AreEqual("[",
      actual.Groups["before"].Value);
    Assert.AreEqual("ruby",
      actual.Groups["lang"].Value);
    Assert.AreEqual("r_tohtml();",
      actual.Groups["code"].Value);
    Assert.AreEqual("]",
      actual.Groups["after"].Value);
  }

  [Test]
  public void BuildCodeElementString()
  {
    var actual =
      CodeBlockModifier.BuildCodeElementString(
        "Call the ",
        "ruby",
        "r_tohtml();",
        " method"
      );
    const string expected =
      "Call the <code language=\"ruby\">r_tohtml();"
      + "</code> method";
    Assert.AreEqual(expected, actual);
  }
}
