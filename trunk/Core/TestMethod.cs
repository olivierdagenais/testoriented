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
        /// <summary>
        /// Initializes a new instance of the <see cref="TestMethod"/> parameterized template with the specified
        /// <paramref name="methodToTest"/> and <paramref name="classUnderTest"/>.
        /// </summary>
        /// 
        /// <param name="methodToTest">
        /// The <see cref="ParametrizedNode"/> representing the method or constructor for which tests are to be written.
        /// </param>
        /// 
        /// <param name="classUnderTest">
        /// The <see cref="TypeDeclaration"/> that contains <paramref name="methodToTest"/>.
        /// </param>
        public TestMethod (ParametrizedNode methodToTest, TypeDeclaration classUnderTest) 
            : base (methodToTest, classUnderTest)
        {
        }
    }
}
