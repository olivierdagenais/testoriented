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

        /// <summary>
        /// Convenience method to create an instance of <see cref="TestMethod"/> initialized with the provided
        /// <paramref name="methodToTest"/> and then execute the template to produce the string representation of the
        /// unit test method.
        /// </summary>
        /// 
        /// <param name="methodToTest">
        /// The <see cref="ParametrizedNode"/> representing the method or constructor for which tests are to be written.
        /// </param>
        /// 
        /// <returns>
        /// The string representation of the source code of the generated method to test
        /// <paramref name="methodToTest"/>.
        /// </returns>
        public static string Generate (ParametrizedNode methodToTest)
        {
            var testMethodGenerator = new TestMethod(methodToTest, (TypeDeclaration)methodToTest.Parent);
            var testMethodCode = testMethodGenerator.TransformText();
            return testMethodCode;
        }
    }
}
