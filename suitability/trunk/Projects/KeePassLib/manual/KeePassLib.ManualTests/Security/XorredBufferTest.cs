using NUnit.Framework;
using KeePassLib.Security;

namespace KeePassLib.ManualTests.Security
{
    /// <summary>
    /// A class to test <see cref="XorredBuffer"/>.
    /// </summary>
    [TestFixture]
    public class XorredBufferTest
    {
        [Test]
        public void InternalChangeKey()
        {
            // arrange
            var data = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };
            var firstPad = new byte[] { 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17 };
            var protectedData = new byte[data.Length];
            for (var i = 0; i < data.Length; i++)
            {
                protectedData[i] = (byte) (data[i] ^ firstPad[i]);
            }
            var secondPad = new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27 };

            // act
            var actual = XorredBuffer.InternalChangeKey(protectedData, firstPad, secondPad);

            // assert
            Assert.AreEqual(data.Length, actual.Length);
            for (var i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(0x20, actual[i]);
            }
        }
    }
}
