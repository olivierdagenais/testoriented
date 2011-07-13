using NUnit.Framework;

[TestFixture]
public class DateFormatterTest
{
  [Test]
  public void FormatIso8601Utc()
  {
    var actual = DateFormatter.
      FormatIso8601Utc(2011, 07, 13, 14, 24, 38);
    Assert.AreEqual("2011-07-13T14:24:38Z", actual);
  }
}
