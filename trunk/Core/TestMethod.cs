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
		/// The name of the method being tested.
		/// </summary>
		public MethodDeclaration Method { get; set; }
	}
}
