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
        public void InnerFormatResultMessage_Created()
        {
            // arrange
            var item = new MenuItem
            {
                Id = 42,
            };

            // act
            var actual = AdminMenuItemController.InnerFormatResultMessage(null, item);

            // assert
            Assert.AreEqual("Items was successfully created with Id = 42", actual);
        }

        [Test]
        public void InnerFormatResultMessage_Updated()
        {
            // arrange
            var item = new MenuItem
            {
                Id = 42,
            };

            // act
            var actual = AdminMenuItemController.InnerFormatResultMessage(42, item);

            // assert
            Assert.AreEqual("Items was successfully updated", actual);
        }
    }
}
