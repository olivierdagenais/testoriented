using System;
using System.Collections.Generic;

using SoftwareNinjas.Core;
using ICSharpCode.NRefactory.Ast;

namespace SoftwareNinjas.TestOriented.Core
{
	/// <summary>
	/// A template for C# unit test method skeletons.
	/// </summary>
	public partial class TestMethod
	{
		static readonly IDictionary<string, string> SimpleTypeDefaultValues = new Dictionary<string, string>
		{
			{ "byte",	"0" },
			{ "char",	"'x'" },
			{ "double",	"0.0" },
			{ "float",	"0.0f" },
			{ "int",	"0" },
			{ "long",	"0L" },
			{ "short",	"0" },
			{ "void",	"null" },
			{ "bool",	"false" },
			{ "string",	"\"TODO\"" },
		};

		/// <summary>
		/// The method being tested.
		/// </summary>
		public MethodDeclaration Method { get; set; }

		/// <summary>
		/// The class in which <see cref="Method"/> can be found.
		/// </summary>
		public TypeDeclaration ClassUnderTest { get; set; }

		/// <summary>
		/// Attempts to determine a reasonable default value for the type represented by <paramref name="source"/>.
		/// </summary>
		/// 
		/// <param name="source">
		/// The <see cref="TypeReference"/> for which to generate a default value.
		/// </param>
		/// 
		/// <returns>
		/// A string representing the best default value to generate, if one was found; the type's default otherwise.
		/// </returns>
		public static string DefaultValue(TypeReference source)
		{
			var typeName = source.ToString();
			if(SimpleTypeDefaultValues.ContainsKey(typeName))
			{
				return SimpleTypeDefaultValues[typeName];
			}
			// TODO: if source represents a class with at least one constructor, set-up a call to the simplest one
			return "default({0})".FormatInvariant(typeName);
		}
	}
}
