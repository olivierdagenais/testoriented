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
			var jmb = new JavaMethod() { MethodName = methodName, Body = body };
            return jmb.TransformText();
        }
    }
}
