using KeePassLib.Cryptography;
using KeePassLib.Security;
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
            // arrange
            var psOutBuffer = new ProtectedString();
            var pwProfile = new PwProfile();
            pwProfile.Pattern = "g{5}";
            var pbKey = new byte[] { 0x00 };
            var crsRandomSource = new CryptoRandomStream(CrsAlgorithm.Salsa20, pbKey);
            var error = PatternBasedGenerator.Generate(psOutBuffer, pwProfile, crsRandomSource);

            // act
            // nothing to do as ExpandPattern() would have been called by calling Generate()

            // assert
            Assert.AreEqual(PwgError.Success, error);
            var actual = psOutBuffer.ReadString();
            Assert.AreEqual("ggggg", actual);
        }
    }
}
