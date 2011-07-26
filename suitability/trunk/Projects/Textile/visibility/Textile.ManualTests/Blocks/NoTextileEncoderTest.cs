using NUnit.Framework;
using Textile.Blocks;

namespace Textile.ManualTests.Blocks
{
    /// <summary>
    /// A class to test <see cref="NoTextileEncoder"/>.
    /// </summary>
    [TestFixture]
    public class NoTextileEncoderTest
    {
        [Test]
        public void EncodeNoTextileZones ()
        {
            var actual = NoTextileEncoder.EncodeNoTextileZones ("'before' =='(during)'== 'after'", "==", "==");
            Assert.AreEqual ("'before' &#39;&#40;during&#41;&#39; 'after'", actual);
        }

        [Test]
        public void EncodeNoTextileZones_Exception ()
        {
            var actual = NoTextileEncoder.EncodeNoTextileZones
                ("'before' =='(during)'== 'after'", "==", "==", new[] { "'" });
            Assert.AreEqual ("'before' '&#40;during&#41;' 'after'", actual);
        }

        [Test]
        public void DecodeNoTextileZones ()
        {
            var actual = NoTextileEncoder.DecodeNoTextileZones
                ("'before' ==&#39;&#40;during&#41;&#39;== 'after'", "==", "==");
            Assert.AreEqual ("'before' '(during)' 'after'", actual);
        }

        [Test]
        public void DecodeNoTextileZones_Exception ()
        {
            var actual = NoTextileEncoder.DecodeNoTextileZones
                ("'before' ==&#39;&#40;during&#41;&#39;== 'after'", "==", "==", new[] { "'" });
            Assert.AreEqual ("'before' &#39;(during)&#39; 'after'", actual);
        }
    }
}
