using NUnit.Framework;
using KeePassLib.Cryptography.PasswordGenerator;

namespace KeePassLib.ManualTests.Cryptography.PasswordGenerator
{
    /// <summary>
    /// A class to test <see cref="PatternBasedGenerator"/>.
    /// </summary>
    [TestFixture]
    public class PatternBasedGeneratorTest
    {
[Test]
public void ExpandPattern()
{
    var actual = PatternBasedGenerator.ExpandPattern("g{5}");
    Assert.AreEqual("ggggg", actual);
}
    }
}
