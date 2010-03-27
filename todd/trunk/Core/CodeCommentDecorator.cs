using System;
using System.Diagnostics;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;

using SoftwareNinjas.Core;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// Augments an <see cref="IOutputAstVisitor"/> instance to allow an abstract syntax tree to be converted back to
    /// source code with code documentation comments.
    /// </summary>
    public class CodeCommentDecorator : IDisposable
    {
        private readonly IOutputFormatter _formatter;

        /// <summary>
        /// Initializes a new instance of <see cref="CodeCommentDecorator"/> with the provided
        /// <paramref name="outputFormatter"/>.
        /// </summary>
        /// 
        /// <param name="outputFormatter">
        /// An <see cref="IOutputFormatter"/> implementation to which code documentation comments will be written.
        /// </param>
        public CodeCommentDecorator(IOutputFormatter outputFormatter)
        {
            if (outputFormatter == null)
            {
                Debug.Assert(false, "outputFormatter is null");
                throw new ArgumentNullException("outputFormatter");
            }
            _formatter = outputFormatter;
        }

        internal void AcceptNodeStart(INode node)
        {
            var doc = node.GetDocumentation();
            if (doc == null)
            {
                return;
            }

            foreach (var line in doc.Lines())
            {
                var comment = new Comment(CommentType.Documentation, line, true, Location.Empty, Location.Empty);
                _formatter.PrintComment(comment, false);
            }
        }

        internal void AcceptNodeEnd(INode node)
        {
            // nothing to do right now
        }

        void IDisposable.Dispose()
        {
            // nothing to do right now
        }

        /// <summary>
        /// Registers a new <see cref="CodeCommentDecorator"/> with the specified <paramref name="outputVisitor"/>.
        /// </summary>
        public static CodeCommentDecorator Install(IOutputAstVisitor outputVisitor)
        {
            var ccd = new CodeCommentDecorator(outputVisitor.OutputFormatter);
            outputVisitor.BeforeNodeVisit += ccd.AcceptNodeStart;
            outputVisitor.AfterNodeVisit += ccd.AcceptNodeEnd;
            return ccd;
        }
    }
}
