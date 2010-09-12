using System;
using NUnit.Framework;
using Parent = SoftwareNinjas.TestOriented.Core.Test.InsertTestFor_ExistingFixture;
namespace SoftwareNinjas.TestOriented.Core.Test.InsertTestFor_ExistingFixture.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.Unformatter"/>.
    /// </summary>
    [TestFixture]
    public class Unformatter
    {
        /// <summary>
        /// Tests the <c>Unformat</c> method with
        /// TODO: write about scenario
        /// </summary>
        [Test()]
        public void Unformat_TODO()
        {
            Assert.Fail("TODO: initialize variable(s) and expected value");
            string format = "TODO";
            string formatted = "TODO";
            string[] actual = Unformatter.Unformat(format, formatted);
            string[] expected = default(string[]);
            Assert.AreEqual(expected, actual);
        }
    }
}
