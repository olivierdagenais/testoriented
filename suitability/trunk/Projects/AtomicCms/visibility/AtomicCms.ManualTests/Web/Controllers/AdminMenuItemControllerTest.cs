using AtomicCms.Core.DomainObjectsImp;
using NUnit.Framework;
using AtomicCms.Web.Controllers;

namespace AtomicCms.ManualTests.Web.Controllers
{
    /// <summary>
    /// A class to test <see cref="AdminMenuItemController"/>.
    /// </summary>
    [TestFixture]
    public class AdminMenuItemControllerTest
    {
        [Test]
        public void FormatResultMessage_Created()
        {
            // arrange
            var iut = new AdminMenuItemController(null);
            var item = new MenuItem
            {
                Id = 42,
            };

            // act
            iut.FormatResultMessage(item, null);

            // assert
            var actual = iut.TempData["SaveResult"];
            Assert.AreEqual("Items was successfully created with Id = 42", actual);
        }

        [Test]
        public void FormatResultMessage_Updated()
        {
            // arrange
            var iut = new AdminMenuItemController(null);
            var item = new MenuItem
            {
                Id = 42,
            };

            // act
            iut.FormatResultMessage(item, 42);

            // assert
            var actual = iut.TempData["SaveResult"];
            Assert.AreEqual("Items was successfully updated", actual);
        }
    }
}
