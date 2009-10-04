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
		/// Generates a string representation of a C# unit test method.
		/// </summary>
		/// 
		/// <param name="method">
		/// A <see cref="MethodDeclaration"/> representing the method for which a test is to be written.
		/// </param>
		/// 
		/// <returns>
		/// A C# NUnit test method stub.
		/// </returns>
		public static string GenerateTestMethod(MethodDeclaration method)
		{
			var template = new TestMethod { Method = method };
			return template.TransformText();
		}
    }
}
