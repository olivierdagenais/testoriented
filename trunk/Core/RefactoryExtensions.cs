using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.PrettyPrinter;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// Provides extension methods to facilitate manipulation of NRefactory Abstract Syntax Tree types.
    /// </summary>
    public static class RefactoryExtensions
    {
        /// <summary>
        /// Adds a <see cref="ParameterDeclarationExpression"/> to <see cref="ParametrizedNode.Parameters"/> in a single
        /// call, for convenience.
        /// </summary>
        /// 
        /// <param name="node">
        /// The method or constructor to add the parameter to.
        /// </param>
        /// 
        /// <param name="parameterType">
        /// The <see cref="TypeReference"/> of the parameter to add.
        /// </param>
        /// 
        /// <param name="parameterName">
        /// The name of the parameter to add.
        /// </param>
        /// 
        /// <returns>
        /// The <see cref="ParameterDeclarationExpression"/> instance that was created and added to
        /// <paramref name="node"/>.
        /// </returns>
        public static ParameterDeclarationExpression AddParameter(this ParametrizedNode node,
            TypeReference parameterType, string parameterName)
        {
            var parameter = new ParameterDeclarationExpression(parameterType, parameterName);
            node.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Provides a mechanism for making a <see cref="ConstructorDeclaration"/> a bit more like a
        /// <see cref="MethodDeclaration"/> by simulating a common method for the result of their respective executions.
        /// </summary>
        /// 
        /// <param name="node">
        /// A constructor or method.
        /// </param>
        /// 
        /// <param name="parentType">
        /// The <see cref="TypeDeclaration"/> in which the constructor or method denoted by <paramref name="node"/> can
        /// be found.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="TypeReference"/>, if one could be determined; otherwise <see cref="NullTypeReference"/>.
        /// </returns>
        public static TypeReference GetTypeReference(this ParametrizedNode node, TypeDeclaration parentType)
        {
            var result = TypeReference.Null;
            var method = node as MethodDeclaration;
            if(method != null)
            {
                result = method.TypeReference;
            }
            else
            {
                var constructor = node as ConstructorDeclaration;
                if (constructor != null)
                {
                    result = new TypeReference(parentType.Name);
                }
            }
            return result;
        }

        /// <summary>
        /// Produces a simplified <see cref="String"/> representation of the <paramref name="typeReference"/>, similar
        /// to the output of <see cref="TypeReference.ToString()"/> but using C# primitive type names where possible.
        /// </summary>
        /// 
        /// <param name="typeReference">
        /// The <see cref="TypeReference"/> to convert into a <see cref="String"/>.
        /// </param>
        /// 
        /// <returns>
        /// The result of converting <see cref="TypeReference.Type"/> to primitive type names, combined with any such
        /// conversion of nested generic type names, pointer specifiers and array rank specifiers.
        /// </returns>
        /// 
        /// <seealso cref="TypeReference.ToString()"/>
        public static string ToSimpleString(this TypeReference typeReference)
        {
            var typeName = typeReference.Type;
            if (TypeReference.PrimitiveTypesCSharpReverse.ContainsKey(typeName))
            {
                typeName = TypeReference.PrimitiveTypesCSharpReverse[typeName];
            }

            var sb = new StringBuilder(typeName);
            var genericTypes = typeReference.GenericTypes;
            if (genericTypes != null && genericTypes.Count > 0)
            {
                sb.Append('<');
                for (int i = 0; i < genericTypes.Count; i++)
                {
                    if (i > 0)
                        sb.Append(',');
                    sb.Append(genericTypes[i].ToSimpleString());
                }
                sb.Append('>');
            }
            if (typeReference.PointerNestingLevel > 0)
            {
                sb.Append('*', typeReference.PointerNestingLevel);
            }
            if (typeReference.IsArrayType)
            {
                foreach (int rank in typeReference.RankSpecifier)
                {
                    sb.Append('[');
                    if (rank < 0)
                        sb.Append('`', -rank);
                    else
                        sb.Append(',', rank);
                    sb.Append(']');
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Retrieves the code documentation comments associated with this <see cref="INode"/>.
        /// </summary>
        /// 
        /// <param name="node">
        /// The <see cref="INode"/> which may contain code documentation comments.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="String"/> representing the code documentation comment XML, without a comment prefix.
        /// </returns>
        public static string GetDocumentation(this INode node)
        {
            return node.UserData as string;
        }

        /// <summary>
        /// Sets the code documentation comments associated with this <see cref="INode"/>.
        /// </summary>
        /// 
        /// <param name="node">
        /// The <see cref="INode"/> to associate code documentation comments to.
        /// </param>
        /// 
        /// <param name="special">
        /// An <see cref="ISpecial"/> implementation which is likely an instance of <see cref="Comment"/> from which
        /// the value of <see cref="Comment.CommentText"/> will be extracted.
        /// </param>
        public static void SetDocumentation (this INode node, ISpecial special)
        {
            if (special is Comment)
            {
                var comment = (Comment)special;
                SetDocumentation (node, comment.CommentText);
            }
        }

        /// <summary>
        /// Sets the code documentation comments associated with this <see cref="INode"/>.
        /// </summary>
        /// 
        /// <param name="node">
        /// The <see cref="INode"/> to associate code documentation comments to.
        /// </param>
        /// 
        /// <param name="documentation">
        /// A <see cref="String"/> representing the code documentation comment XML to associate.
        /// </param>
        public static void SetDocumentation (this INode node, string documentation)
        {
            node.UserData = documentation;
        }

        internal static IEnumerable<ISpecial> Collapse (this IEnumerable<ISpecial> specials)
        {
            var sb = new StringBuilder ();
            int lastCommentStartLine;
            int nextContiguousLine;
            var comments = specials.Filter (IsDocumentationComment);
            var e = comments.GetEnumerator();
            Comment aggregatedComment;
            if (e.MoveNext())
            {
                Comment comment = (Comment) e.Current;
                sb.Append (comment.CommentText);
                lastCommentStartLine = comment.StartPosition.Line;
                nextContiguousLine = comment.EndPosition.Line;
                while (e.MoveNext())
                {
                    comment = (Comment) e.Current;
                    if(comment.StartPosition.Line != nextContiguousLine)
                    {
                        aggregatedComment = CreateComment (sb, lastCommentStartLine, nextContiguousLine);
                        yield return aggregatedComment;
                        sb.Length = 0;
                        lastCommentStartLine = comment.StartPosition.Line;
                    }
                    else
                    {
                        sb.AppendLine();
                    }
                    sb.Append (comment.CommentText);
                    nextContiguousLine = comment.EndPosition.Line;
                }
                aggregatedComment = CreateComment (sb, lastCommentStartLine, nextContiguousLine);
                yield return aggregatedComment;
            }
        }

        private static Comment CreateComment (StringBuilder sb, int startLine, int endLine)
        {
            var startLocation = new Location (0, startLine);
            var endLocation = new Location (0, endLine);
            var comment = new Comment (CommentType.Documentation, sb.ToString(), true, startLocation, endLocation);
            return comment;
        }

        internal static bool IsDocumentationComment (ISpecial special)
        {
            if (special is Comment)
            {
                var comment = (Comment) special;
                return comment.CommentType == CommentType.Documentation;
            }
            return false;
        }

        /// <summary>
        /// Scans the <paramref name="specials"/> and attaches those that it recognizes to their associated
        /// <see cref="INode"/> instance in the tree under <paramref name="root"/>.
        /// </summary>
        /// 
        /// <param name="root">
        /// The starting point for the search to attach collapsed <see cref="ISpecial"/> instances to.
        /// </param>
        /// 
        /// <param name="specials">
        /// A sequence of <see cref="ISpecial"/> instances that were originally created by parsing the source code that
        /// yielded the provided <paramref name="root"/>.
        /// </param>
        public static void AttachDocumentationComments (this INode root, IEnumerable<ISpecial> specials)
        {
            var collapsed = specials.Collapse ();
            foreach (var special in collapsed)
            {
                var nextLine = special.EndPosition.Line;
                var preOrdered = root.PreOrder (n => n.Children);
                var filtered = preOrdered.Filter (n => DetermineEarliestLine(n) == nextLine);
                var node = filtered.FirstOrDefault();
                if (node != null)
                {
                    node.SetDocumentation (special);
                }
            }
        }

        internal static int DetermineEarliestLine (INode node)
        {
            int earliestLine = node.StartLocation.Line;
            foreach(var child in node.FindRelatedNodes())
            {
                earliestLine = Math.Min (earliestLine, child.StartLocation.Line);
            }
            return earliestLine;
        }

        internal static IEnumerable<INode> FindRelatedNodes(this INode node)
        {
            if(node is AttributedNode)
            {
                var attributedNode = (AttributedNode) node;
                foreach (var attribute in attributedNode.Attributes)
                {
                    yield return attribute;
                }
            }
        }

        // TODO: We should add support for test class source code coming from a TextReader/IEnumerable<string>
        // TODO: The test method generation should be aware of the destination type so that it can know how to correctly
        // reference the class under test (i.e. prefix with Parent or include a namespace reference)
        /// <summary>
        /// Generates a test for the provided <paramref name="methodToTest"/> and inserts it near the end of the
        /// <paramref name="testClassSourceCode"/>.
        /// </summary>
        /// 
        /// <param name="testClassSourceCode">
        /// The source code to the compilation unit containing the class for testing the class under test.
        /// </param>
        /// 
        /// <param name="methodToTest">
        /// A method of the class under test.
        /// </param>
        /// 
        /// <returns>
        /// The source code to the compilation unit with the new test method stub added to it.
        /// </returns>
        public static string InsertTestFor(this string testClassSourceCode, ParametrizedNode methodToTest)
        {
            string sourceCodeToInsert = methodToTest.GenerateTest();
            var cu = Parser.ParseCompilationUnit(testClassSourceCode);
            int lineNumber = cu.DetermineMethodInsertionLine();

            string result = testClassSourceCode.InsertLines(sourceCodeToInsert, lineNumber);
            return result;
        }

        internal static int DetermineMethodInsertionLine(this CompilationUnit cu)
        {
            var lineNumber = cu.EndLocation.Line;
            var nodes = ( (INode) cu ).PreOrder(n => n.Children);
            NamespaceDeclaration @namespace = null;
            TypeDeclaration type = null;
            foreach (var node in nodes)
            {
                if (node is NamespaceDeclaration)
                {
                    @namespace = (NamespaceDeclaration) node;
                }
                else if (node is TypeDeclaration)
                {
                    type = (TypeDeclaration) node;
                }
            }

            if (type != null)
            {
                lineNumber = type.EndLocation.Line;
            }
            else if (@namespace != null)
            {
                lineNumber = @namespace.EndLocation.Line;
            }
            return lineNumber;
        }

        /// <summary>
        /// Generates a unit test method that exercises the provided <paramref name="methodToTest"/>.
        /// </summary>
        /// 
        /// <param name="methodToTest">
        /// The <see cref="ParametrizedNode"/> representing the method or constructor for which a test is to be written.
        /// </param>
        /// 
        /// <returns>
        /// The string representation of the source code of the generated method to test
        /// <paramref name="methodToTest"/>.
        /// </returns>
        public static string GenerateTest(this ParametrizedNode methodToTest)
        {
            return TestMethod.Generate(methodToTest);
        }

        /// <summary>
        /// Performs the reverse operation of parsing by serializing the specified <paramref name="compilationUnit"/>
        /// to source code.
        /// </summary>
        /// 
        /// <param name="compilationUnit">
        /// A <see cref="CompilationUnit"/> which is to be turned into source code.
        /// </param>
        /// 
        /// <returns>
        /// A string representing the source code that would compile to the provided <paramref name="compilationUnit"/>.
        /// </returns>
        public static string GenerateSourceCode(this CompilationUnit compilationUnit)
        {
            var outputVisitor = new CSharpOutputVisitor
            {
                Options =
                {
                    IndentationChar = ' ',
                    TabSize = 4,
                    IndentSize = 4,
                }
            };
            using (CodeCommentDecorator.Install(outputVisitor))
            {
                outputVisitor.VisitCompilationUnit(compilationUnit, null);
            }
            return outputVisitor.Text;
        }

        /// <summary>
        /// Retrieves a sequence of all <see cref="TypeDeclaration"/> instances associated with the provided
        /// <paramref name="compilationUnit"/>.
        /// </summary>
        /// 
        /// <param name="compilationUnit">
        /// The <see cref="CompilationUnit"/> in which to search for <see cref="TypeDeclaration"/> instances.
        /// </param>
        /// 
        /// <returns>
        /// A sequence of all the type declarations found in the provided <paramref name="compilationUnit"/>.
        /// </returns>
        public static IEnumerable<TypeDeclaration> GetTypeDeclarations(this CompilationUnit compilationUnit)
        {
            var nodes = ( (INode) compilationUnit ).PreOrder(n => n.Children);
            var typeNodes = nodes.Filter(c => c is TypeDeclaration);
            var result = typeNodes.Map(n => (TypeDeclaration) n);
            return result;
        }

        /// <summary>
        /// Recursively traverses the parents of the provided <paramref name="node"/> until the root
        /// <see cref="CompilationUnit"/> is found and then returns it.
        /// </summary>
        /// 
        /// <param name="node">
        /// The starting point of the search, the <see cref="INode"/> instance for which the root
        /// <see cref="CompilationUnit"/> is to be found.
        /// </param>
        /// 
        /// <returns>
        /// The <see cref="CompilationUnit"/> which contains the provided <paramref name="node"/>.
        /// </returns>
        public static CompilationUnit GetCompilationUnit(this INode node)
        {
            var parent = node.Parent;
            if (parent is CompilationUnit)
            {
                return (CompilationUnit) parent;
            }
            return parent.GetCompilationUnit();
        }
    }
}
