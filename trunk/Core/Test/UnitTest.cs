using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Parent = SoftwareNinjas.TestOriented.Core;

namespace SoftwareNinjas.TestOriented.Core.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.UnitTest"/>.
    /// </summary>
    [TestFixture]
    public class UnitTest
    {
        /// <summary>
        /// Tests the <see cref="Parent.UnitTest.GenerateJavaMethod(String, String)"/> method with a typical use-case.
        /// </summary>
        [Test]
        public void GenerateJavaMethod_Typical()
        {
            var expectedMethod = @"
/**
 * Tests the <i>unformat</i> method with
 * TODO: write about scenario
 */
@Test public void unformat_TODO ( ) {
    Unformatter unformatter = new Unformatter (  );
	fail ( ""TODO: initialize variable(s) and expected value"" );
	String format = ""TODO"";
	String formatted = ""TODO"";
	String[] actual = unformatter.unformat ( format, formatted );
	String[] expected = new String[] { ""TODO"" };
	assertEquals ( expected, actual );
}
";
            var body = @"    Unformatter unformatter = new Unformatter (  );
	fail ( ""TODO: initialize variable(s) and expected value"" );
	String format = ""TODO"";
	String formatted = ""TODO"";
	String[] actual = unformatter.unformat ( format, formatted );
	String[] expected = new String[] { ""TODO"" };
	assertEquals ( expected, actual );";
            Assert.AreEqual(expectedMethod, Parent.UnitTest.GenerateJavaMethod("unformat", body));
        }
    }
}
