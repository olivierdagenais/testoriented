using ICSharpCode.NRefactory.Ast;
using NUnit.Framework;
using SoftwareNinjas.Core;

using Parent = SoftwareNinjas.TestOriented.Core;

namespace SoftwareNinjas.TestOriented.Core.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.Parser"/>.
    /// </summary>
    [TestFixture]
    public class Parser
    {

        /// <summary>
        /// Tests the <see cref="Parent.Parser.ParseTypeMembers(string)"/> method with
        /// the source code of a single test method, with code documentation comments.
        /// </summary>
        [Test]
        public void ParseTypeMembers_MethodWithDocumentation ()
        {
            var compilationUnit = Parent.Parser.ParseTypeMembers(RefactoryExtensions.DocumentedMethod);
            var typeDeclaration = compilationUnit.Children.FirstOrDefault ();
            var method = (ParametrizedNode) typeDeclaration.Children.FirstOrDefault ();
            Assert.AreEqual ("Unformat_TODO", method.Name);
            Assert.AreEqual(RefactoryExtensions.SimpleCodeDocumentationComment, method.GetDocumentation());
        }
    }
}
