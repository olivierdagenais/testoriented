using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using NUnit.Framework;
using SoftwareNinjas.Core;

namespace PivotStack.ManualTests
{
    [TestFixture]
    public class BitmapSourceExtensionsTest
    {
        [Test]
        public void ConvertToGdiPlusBitmap()
        {
            // arrange
            BitmapSource bitmapSource;
            const string fileName = "1200x1500.png";
            using (var inputStream = AssemblyExtensions.OpenScopedResourceStream<BitmapSourceExtensionsTest> (fileName))
            {
                var decoder = new PngBitmapDecoder (
                    inputStream,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default
                    );
                bitmapSource = decoder.Frames[0];
            }

            // act
            var bitmap = bitmapSource.ConvertToGdiPlusBitmap ();

            // assert
            using (var actualStream = new MemoryStream())
            {
                bitmap.Save (actualStream, ImageFormat.Png);
                ProgramTest.AssertStreamsAreEqual<BitmapSourceExtensionsTest> (fileName, actualStream);
            }
        }
    }
}
