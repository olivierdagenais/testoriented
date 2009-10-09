using System;
using System.Collections.Generic;
using System.Text;

using SoftwareNinjas.Core;
using ICSharpCode.NRefactory.Ast;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// Methods related to unit tests.
    /// </summary>
    public class UnitTest
    {
        /// <summary>
        /// Generates a string representation of a C# unit test method that tests <paramref name="methodToTest"/>.
        /// </summary>
        /// 
        /// <param name="methodToTest">
        /// A <see cref="MethodDeclaration"/> representing the method for which a test is to be written.
        /// </param>
        /// 
        /// <param name="classUnderTest">
        /// The class in which the method to test is located.
        /// </param>
        /// 
        /// <returns>
        /// A C# NUnit test method stub.
        /// </returns>
        public static string GenerateTestMethod(MethodDeclaration methodToTest, TypeDeclaration classUnderTest)
        {
            var template = new TestMethod (methodToTest, classUnderTest);
            return template.TransformText();
        }
    }
}
