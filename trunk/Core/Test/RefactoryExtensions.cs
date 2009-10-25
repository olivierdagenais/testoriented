using System;
using ICSharpCode.NRefactory.Ast;
using NUnit.Framework;

using Parent = SoftwareNinjas.TestOriented.Core;

namespace SoftwareNinjas.TestOriented.Core.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.RefactoryExtensions"/>.
    /// </summary>
    [TestFixture]
    public class RefactoryExtensions
    {

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.GetTypeReference(ParametrizedNode,TypeDeclaration)" />
        /// method with a method.
        /// </summary>
        [Test]
        public void GetTypeReference_Method()
        {
            var cut = AbstractMethodTemplate.CreateClassUnderTest();
            var method = new MethodDeclaration
                { Name = "DoStuff", TypeReference = AbstractMethodTemplate.StringTypeReference };
            var actual = Parent.RefactoryExtensions.GetTypeReference(method, cut);
            Assert.AreEqual(AbstractMethodTemplate.StringTypeReference, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.GetTypeReference(ParametrizedNode,TypeDeclaration)" />
        /// method with a constructor.
        /// </summary>
        [Test]
        public void GetTypeReference_Constructor()
        {
            var cut = AbstractMethodTemplate.CreateClassUnderTest();
            var defaultConstructor = AbstractMethodTemplate.CreateDefaultConstructor(cut);
            var actual = Parent.RefactoryExtensions.GetTypeReference(defaultConstructor, cut);
            Assert.AreEqual(cut.Name, actual.Type);
        }

    }
}
