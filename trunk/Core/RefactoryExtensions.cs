using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
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
            return (string) node.UserData;
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

        internal static IList<ISpecial> Collapse (this IEnumerable<ISpecial> specials)
        {
            var result = new List<ISpecial> ();
            var sb = new StringBuilder ();
            int lastCommentStartLine;
            int nextContiguousLine;
            var comments = specials.Filter (IsDocumentationComment);
            var e = comments.GetEnumerator();
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
                        CreateAndAddComment (result, sb, lastCommentStartLine, comment.EndPosition.Line);
                        sb.Length = 0;
                        lastCommentStartLine = comment.StartPosition.Line;
                    }
                    sb.AppendLine();
                    sb.Append (comment.CommentText);
                    nextContiguousLine = comment.EndPosition.Line;
                }
                CreateAndAddComment (result, sb, lastCommentStartLine, nextContiguousLine - 1);
            }

            return result;
        }

        private static void CreateAndAddComment(IList<ISpecial> dest, StringBuilder sb, int startLine, int endLine)
        {
            var startLocation = new Location (0, startLine);
            var endLocation = new Location (0, endLine);
            var comment = new Comment (CommentType.Documentation, sb.ToString(), true, startLocation, endLocation);
            dest.Add (comment);
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

    }
}
