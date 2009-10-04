using System;

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
		/// Tests the <see cref="Parent.UnitTest.GenerateTestMethod(MethodDeclaration)"/>
		/// with a typical use-case.
		/// </summary>
		[Test]
		public void GenerateTestMethod_Typical()
		{
			var t = new MethodDeclaration {Name = "Unformat"};
			const string expected = @"
/// <summary>
/// Tests the <c>Unformat</c> method with
/// TODO: write about scenario
/// </summary>
[Test]
public void Unformat_TODO ( ) {
	// TODO: invoke Unformat and assert properties of its effects/output
	Assert.Fail ( ""Test not yet written"" );
}
";
			Assert.AreEqual(expected, Parent.UnitTest.GenerateTestMethod(t));
		}
    }
}
