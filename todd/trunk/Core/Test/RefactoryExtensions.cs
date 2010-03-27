using System;
using System.Collections.Generic;

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
        internal const string SimpleCodeDocumentationComment = @" <summary>
 Tests the <c>Unformat</c> method with
 TODO: write about scenario
 </summary>";

        internal const string DocumentedMethod = @"
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
            var pair = Parent.Parser.Parse(DocumentedMethod, Parent.Parser.DoParseTypeMembers);
            var specials = pair.Second.Collapse();
            var member = pair.First.Children[0].Children[0];
            Parent.RefactoryExtensions.SetDocumentation (member, specials.FirstOrDefault());
            var actual = Parent.RefactoryExtensions.GetDocumentation (member);
            Assert.AreEqual (SimpleCodeDocumentationComment, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.Collapse(IEnumerable{ISpecial})"/> method
        /// with a list of <see cref="ISpecial"/> instances created by parsing source code.
        /// </summary>
        [Test]
        public void Collapse_MethodParsedFromSourceCode ()
        {
            var pair = Parent.Parser.Parse(DocumentedMethod, Parent.Parser.DoParseTypeMembers);
            var specials = pair.Second;
            var collapsedSpecials = Parent.RefactoryExtensions.Collapse ( specials );
            var comment = (Comment)collapsedSpecials.FirstOrDefault();
            Assert.AreEqual (2, comment.StartPosition.Line);
            Assert.AreEqual (6, comment.EndPosition.Line, "The last of the specials also ended at line 6");
            Assert.AreEqual (SimpleCodeDocumentationComment, comment.CommentText);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.Collapse(IEnumerable{ISpecial})"/> method
        /// with a list of <see cref="ISpecial"/> instances created by parsing source code of a compilation unit.
        /// </summary>
        [Test]
        public void Collapse_CompilationUnitParsedFromSourceCode()
        {
            var pair = Parent.Parser.Parse(UnitTest.UnformatterTestSource, Parent.Parser.DoParseCompilationUnit);
            var specials = pair.Second;
            var collapsedSpecials = Parent.RefactoryExtensions.Collapse(specials);
            var e = collapsedSpecials.GetEnumerator();

            Assert.IsTrue(e.MoveNext());
            var comment = (Comment) e.Current;
            Assert.AreEqual(6, comment.StartPosition.Line);
            Assert.AreEqual(9, comment.EndPosition.Line);
            Assert.AreEqual(UnitTest.ClassCodeDocumentationComment, comment.CommentText);

            Assert.IsTrue(e.MoveNext());
            comment = (Comment) e.Current;
            Assert.AreEqual(12, comment.StartPosition.Line);
            Assert.AreEqual(16, comment.EndPosition.Line);
            Assert.AreEqual(SimpleCodeDocumentationComment, comment.CommentText);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.InsertTestFor(String, ParametrizedNode)" /> method with
        /// the sample Unformat() method.
        /// </summary>
        [Test]
        public void InsertTestFor_UnformatMethod()
        {
            var cutCu = Parent.Parser.ParseCompilationUnit(UnitTest.UnformatterSource);
            var cutType = cutCu.GetTypeDeclarations().FirstOrDefault();
            var methodToTest = (ParametrizedNode) cutType.Children[0];

            var actualTestSourceCode = 
                Parent.RefactoryExtensions.InsertTestFor(UnitTest.UnformatterTestSourceBlank, methodToTest);

            Assert.AreEqual(UnitTest.UnformatterTestSource, actualTestSourceCode);
        }


        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.GenerateSourceCode(CompilationUnit)"/> method with
        /// the typical case.
        /// </summary>
        [Test]
        public void GenerateSourceCode_Typical()
        {
            var cu = Parent.Parser.ParseCompilationUnit(UnitTest.UnformatterTestSource);
            var actualSource = Parent.RefactoryExtensions.GenerateSourceCode(cu);
            AssertAreEqualNormalized(UnitTest.UnformatterTestSource, actualSource);
        }

        internal static void AssertAreEqualNormalized(string expected, string actual)
        {
            var trimAndReplace = new Func<string, string>( s => s.TrimEnd().Replace("\r", "") );
            var normalizedExpected = trimAndReplace(expected);
            var normalizedActual = trimAndReplace(actual);
            Assert.AreEqual(normalizedExpected, normalizedActual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.AttachDocumentationComments(INode,IEnumerable{ISpecial})"/>
        /// method with a list of <see cref="ISpecial"/> instances created by parsing the source code of a method.
        /// </summary>
        [Test]
        public void AttachDocumentationComments_ParsedFromSourceCode ()
        {
            var pair = Parent.Parser.Parse (DocumentedMethod, Parent.Parser.DoParseTypeMembers);
            var node = pair.First.Children[0].Children[0];
            Parent.RefactoryExtensions.AttachDocumentationComments (node, pair.Second);
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
            var pair = Parent.Parser.Parse(DocumentedMethod, Parent.Parser.DoParseTypeMembers);
            var node = pair.First.Children[0].Children[0];
            var actual = Parent.RefactoryExtensions.DetermineEarliestLine(node);
            Assert.AreEqual(6, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.GetTypeDeclarations" /> method with
        /// the typical case.
        /// </summary>
        [Test]
        public void GetTypeDeclarations_Typical()
        {
            var cu = Parent.Parser.ParseCompilationUnit(UnitTest.UnformatterSource);

            var actualTypeDeclarations = Core.RefactoryExtensions.GetTypeDeclarations(cu);

            var e = actualTypeDeclarations.GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("Unformatter", e.Current.Name);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.GetCompilationUnit(INode)" /> method with
        /// the typical case.
        /// </summary>
        [Test]
        public void GetCompilationUnit_Typical()
        {
            var cu = Parent.Parser.ParseCompilationUnit(UnitTest.UnformatterSource);
            var typeDeclarations = Core.RefactoryExtensions.GetTypeDeclarations(cu);
            var type = typeDeclarations.FirstOrDefault();
            var method = (MethodDeclaration) type.Children[0];

            Assert.AreEqual(cu, Parent.RefactoryExtensions.GetCompilationUnit(type));
            Assert.AreEqual(cu, Parent.RefactoryExtensions.GetCompilationUnit(method));
        }

    }
}
