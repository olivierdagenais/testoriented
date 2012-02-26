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
    // act
    var actual = AdminMenuItemController.InnerFormatResultMessage(null, 42);

    // assert
    Assert.AreEqual("Items was successfully created with Id = 42", actual);
}

        [Test]
        public void InnerFormatResultMessage_Updated()
        {
            // act
            var actual = AdminMenuItemController.InnerFormatResultMessage(42, 42);

            // assert
            Assert.AreEqual("Items was successfully updated", actual);
        }
    }
}
