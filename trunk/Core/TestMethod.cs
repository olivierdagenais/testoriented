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

        private readonly MethodDeclaration _method;
        private readonly TypeDeclaration _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestMethod"/> parameterized template with the specified
        /// <paramref name="methodToTest"/> and <paramref name="classUnderTest"/>.
        /// </summary>
        /// 
        /// <param name="methodToTest">
        /// The <see cref="MethodDeclaration"/> representing the method for which tests are to be written.
        /// </param>
        /// 
        /// <param name="classUnderTest">
        /// The <see cref="TypeDeclaration"/> that contains <paramref name="methodToTest"/>.
        /// </param>
        public TestMethod (MethodDeclaration methodToTest, TypeDeclaration classUnderTest)
        {
            _method = methodToTest;
            _type = classUnderTest;
        }

        /// <summary>
        /// Determines if the <see cref="Method"/> that will be tested is expected to return a value.
        /// </summary>
        /// 
        /// <returns>
        /// <see langword="true"/> if the <see cref="Method"/> returns a value; <see langword="false"/> otherwise.
        /// </returns>
        public bool HasReturnValue
        {
            get
            {
                var returnType = _method.TypeReference;
                return !(
                    returnType.IsNull
                    || "System.Void" == returnType.Type
                    || "void" == returnType.Type
                );
            }
        }

        /// <summary>
        /// The method being tested.
        /// </summary>
        public MethodDeclaration Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// The class in which <see cref="Method"/> can be found.
        /// </summary>
        public TypeDeclaration ClassUnderTest
        {
            get
            {
                return _type;
            }
        }

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
