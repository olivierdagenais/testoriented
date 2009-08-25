using System;
using System.Collections.Generic;
using System.Text;

using SoftwareNinjas.Core;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// Methods related to unit tests.
    /// </summary>
    public class UnitTest
    {
        internal const string JavaMethodBody = @"
/**
 * Tests the <i>{0}</i> method with
 * TODO: write about scenario
 */
@Test public void {0}_TODO ( ) {{
{1}
}}
";
        /// <summary>
        /// Generates a string representation of a Java unit test method.
        /// </summary>
        /// 
        /// <param name="methodName">
        /// The name of the method.
        /// </param>
        /// 
        /// <param name="body">
        /// The body of the method.
        /// </param>
        /// 
        /// <returns>
        /// A JUnit test method stub.
        /// </returns>
        public static string GenerateJavaMethod(string methodName, string body)
        {
            // TODO: the previous version handled a custom newLine
            return JavaMethodBody.FormatInvariant(methodName, body);
        }
    }
}
