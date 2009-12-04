using System;
using System.Collections.Generic;
using System.IO;

using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using NUnit.Framework;
using SoftwareNinjas.Core;

using Parent = SoftwareNinjas.TestOriented.Core;

namespace SoftwareNinjas.TestOriented.Core.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.RefactoryExtensions"/>.
    /// </summary>
    [TestFixture]
    public class RefactoryExtensions
    {
        private const string SimpleCodeDocumentationComment = @" <summary>
 Tests the <c>Unformat</c> method with
 TODO: write about scenario
 </summary>";

        private const string DocumentedMethod = @"
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

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.GetDocumentation(INode)" /> method
        /// and the <see cref="Core.RefactoryExtensions.SetDocumentation(INode, string)"/> with
        /// strings.
        /// </summary>
        [Test]
        public void NodeDocumentation_String()
        {
            INode node = AbstractMethodTemplate.CreateClassUnderTest();
            Parent.RefactoryExtensions.SetDocumentation(node, SimpleCodeDocumentationComment);
            var actual = Parent.RefactoryExtensions.GetDocumentation(node);
            Assert.AreEqual(SimpleCodeDocumentationComment, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.GetDocumentation(INode)" /> method
        /// and the <see cref="Core.RefactoryExtensions.SetDocumentation(INode, ISpecial)"/> with
        /// <see cref="ISpecial"/>.
        /// </summary>
        [Test]
        public void NodeDocumentation_ISpecial_Artificial()
        {
            INode node = AbstractMethodTemplate.CreateClassUnderTest();
            var comment = new Comment(CommentType.Documentation, 
                SimpleCodeDocumentationComment, true, Location.Empty, Location.Empty);
            Parent.RefactoryExtensions.SetDocumentation(node, comment);
            var actual = Parent.RefactoryExtensions.GetDocumentation(node);
            Assert.AreEqual(SimpleCodeDocumentationComment, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.GetDocumentation(INode)" /> method
        /// and the <see cref="Core.RefactoryExtensions.SetDocumentation(INode, ISpecial)"/> with
        /// an <see cref="ISpecial"/> instance obtained by parsing source code.
        /// </summary>
        [Test]
        public void NodeDocumentation_ISpecial_ParsedFromSourceCode ()
        {
            var pair = ParseTypeMembers (DocumentedMethod);
            var specials = pair.First.Collapse();
            var member = pair.Second[0];
            Parent.RefactoryExtensions.SetDocumentation (member, specials[0]);
            var actual = Parent.RefactoryExtensions.GetDocumentation (member);
            Assert.AreEqual (SimpleCodeDocumentationComment, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.Collapse(IEnumerable{ISpecial})"/> method
        /// with a list of <see cref="ISpecial"/> instances created by parsing source code.
        /// </summary>
        [Test]
        public void Collapse_ParsedFromSourceCode ()
        {
            var pair = ParseTypeMembers (DocumentedMethod);
            var specials = pair.First;
            var collapsedSpecials = Parent.RefactoryExtensions.Collapse ( specials );
            Assert.AreEqual (1, collapsedSpecials.Count);
            Comment comment = (Comment)collapsedSpecials[0];
            Assert.AreEqual (2, comment.StartPosition.Line);
            Assert.AreEqual (6, comment.EndPosition.Line, "The last of the specials also ended at line 6");
            Assert.AreEqual (SimpleCodeDocumentationComment, comment.CommentText);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.AttachDocumentationComments(INode,IEnumerable{ISpecial})"/>
        /// method with a list of <see cref="ISpecial"/> instances created by parsing the source code of a method.
        /// </summary>
        [Test]
        public void AttachDocumentationComments_ParsedFromSourceCode ()
        {
            var pair = ParseTypeMembers (DocumentedMethod);
            var node = pair.Second[0];
            Parent.RefactoryExtensions.AttachDocumentationComments (node, pair.First);
            var actual = node.GetDocumentation ();
            Assert.AreEqual (SimpleCodeDocumentationComment, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.DetermineEarliestLine(INode)" /> method with
        /// a typical scenario of a unit test method decorated with an attribute.
        /// </summary>
        [Test]
        public void DetermineEarliestLine_Typical()
        {
            var pair = ParseTypeMembers(DocumentedMethod);
            var node = pair.Second[0];
            var actual = Parent.RefactoryExtensions.DetermineEarliestLine(node);
            Assert.AreEqual(6, actual);
        }


        private static Pair<IList<ISpecial>, IList<INode>> ParseTypeMembers (string code)
        {
            using (TextReader tr = new StringReader (code))
            {
                using (IParser parser = ParserFactory.CreateParser (SupportedLanguage.CSharp, tr))
                {
                    List<INode> members = parser.ParseTypeMembers ();
                    Assert.AreEqual ("", parser.Errors.ErrorOutput);
                    IList<ISpecial> specials = parser.Lexer.SpecialTracker.CurrentSpecials;
                    return new Pair<IList<ISpecial>, IList<INode>> (specials, members);
                }
            }
        }
    }
}
