﻿using System;

using ICSharpCode.NRefactory.Ast;

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
		/// Tests the <see cref="Parent.UnitTest.GenerateTestMethod(MethodDeclaration, TypeDeclaration)"/> method with
		/// a typical use-case.
		/// </summary>
		[Test]
		public void GenerateTestMethod_Typical()
		{
			#region public class Unformatter
			var classUnderTest = new TypeDeclaration(Modifiers.Public | Modifiers.Static, null){ Name = "Unformatter" };

			#region public static string[] Unformat ( this string format, string formatted )
			var methodToTest = new MethodDeclaration {Name = "Unformat", IsExtensionMethod = true};

			#region returns string[]
			var retval = new TypeReference("string", new[]{0});
			methodToTest.TypeReference = retval;
			#endregion

			#region string format
			var formatParam = new ParameterDeclarationExpression(new TypeReference("string", true), "format");
			methodToTest.Parameters.Add(formatParam);
			#endregion

			#region string formatted
			var formattedParam = new ParameterDeclarationExpression(new TypeReference("string", true), "formatted");
			methodToTest.Parameters.Add(formattedParam);
			#endregion

			#endregion

			classUnderTest.Children.Add(methodToTest);
			#endregion
			const string expected = @"
/// <summary>
/// Tests the <c>Unformat</c> method with
/// TODO: write about scenario
/// </summary>
[Test]
public void Unformat_TODO ( ) {
	Assert.Fail ( ""TODO: initialize variable(s) and expected value"" );
	string format = ""TODO"";
	string formatted = ""TODO"";
	string[] actual = Unformatter.Unformat ( format, formatted );
	string[] expected = default(string[]);
	Assert.AreEqual ( expected, actual );
}
";
			Assert.AreEqual(expected, Parent.UnitTest.GenerateTestMethod(methodToTest, classUnderTest));
		}

		/// <summary>
		/// Tests the <c>Unformat</c> method with
		/// TODO: write about scenario
		/// </summary>
		[Test]
		public void Unformat_TODO() {
			//Assert.Fail ( "TODO: initialize variable(s) and expected value" );
			string format = "TODO";
			string formatted = "TODO";
			string[] actual = Unformatter.Unformat ( format, formatted );
			string[] expected = default(string[]);
			Assert.AreEqual ( expected, actual );
		}
	}

	/// <summary>
	/// A class to support the generated test used to verify unit test generation.
	/// </summary>
	public static class Unformatter
	{
		/// <summary>
		/// The reverse of <see cref="string.Format(string,object[])"/>.
		/// </summary>
		/// 
		/// <param name="format">
		/// A string containing placeholders for values.
		/// </param>
		/// 
		/// <param name="formatted">
		/// A string with said placeholders replaced by values.
		/// </param>
		/// 
		/// <returns>
		/// The string representations of the values for each of the placeholders.
		/// </returns>
		/// 
		/// <remarks>
		/// This method is not actually implemented; it only serves to support the <see cref="UnitTest.Unformat_TODO"/>
		/// unit test.
		/// </remarks>
		public static string[] Unformat(this string format, string formatted)
		{
			return default(string[]);
		}
	}
}
