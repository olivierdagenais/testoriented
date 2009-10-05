using System;

using ICSharpCode.NRefactory.Ast;

namespace SoftwareNinjas.TestOriented.Core
{
	/// <summary>
	/// A template for C# unit test method skeletons.
	/// </summary>
	public partial class TestMethod
	{
		/// <summary>
		/// The method being tested.
		/// </summary>
		public MethodDeclaration Method { get; set; }

		/// <summary>
		/// The class in which <see cref="Method"/> can be found.
		/// </summary>
		public TypeDeclaration ClassUnderTest { get; set; }
	}
}
