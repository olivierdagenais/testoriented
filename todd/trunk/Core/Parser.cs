using System;
using System.Collections.Generic;
using System.IO;

using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// Provides a façade to the NRefactory <see cref="IParser"/> implementations.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parses the provided <paramref name="code"/> as C# source code of a compilation unit and attaches code
        /// documentation comments to all appropriate <see cref="INode"/> instances.
        /// </summary>
        /// 
        /// <param name="code">
        /// Source code for a compilation unit.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="CompilationUnit"/> containing representing the provided <paramref name="code"/>.
        /// </returns>
        public static CompilationUnit ParseCompilationUnit(string code)
        {
            return ExtractAndAttachComments(Parse(code, DoParseCompilationUnit));
        }

        internal static void DoParseCompilationUnit(IParser parser)
        {
            parser.Parse();
        }

        /// <summary>
        /// Parses the provided <paramref name="code"/> as C# source code of type members and attaches code
        /// documentation comments to all appropriate <see cref="INode"/> instances.
        /// </summary>
        /// 
        /// <param name="code">
        /// Source code representing method declarations.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="CompilationUnit"/> containing an anonymous type to which the parsed members are attached.
        /// </returns>
        public static CompilationUnit ParseTypeMembers (string code)
        {
            return ExtractAndAttachComments(Parse (code, DoParseTypeMembers));
        }

        internal static CompilationUnit ExtractAndAttachComments(Pair<CompilationUnit, IList<ISpecial>> pair)
        {
            var cu = pair.First;
            cu.AttachDocumentationComments(pair.Second);
            return cu;
        }

        internal static void DoParseTypeMembers (IParser parser)
        {
            var members = parser.ParseTypeMembers ();
            // the parser will not add any of the members to the CompilationUnit,
            // but it will add all members to an anonymous TypeDeclaration, so we add _it_ to the CompilationUnit
            if (members.Count > 0)
            {
                parser.CompilationUnit.Children.Add (members[0].Parent);
            }
        }

        internal static Pair<CompilationUnit, IList<ISpecial>> Parse(string code, Action<IParser> parsingType)
        {
            using (TextReader tr = new StringReader (code))
            {
                return Parse (tr, parsingType);
            }
        }

        internal static Pair<CompilationUnit, IList<ISpecial>> Parse(TextReader tr, Action<IParser> parsingType)
        {
            using (IParser parser = ParserFactory.CreateParser (SupportedLanguage.CSharp, tr))
            {
                parsingType (parser);
                if (parser.Errors.Count > 0)
                {
                    throw new ArgumentException (parser.Errors.ErrorOutput);
                }
                var cu = parser.CompilationUnit;
                var specials = parser.Lexer.SpecialTracker.CurrentSpecials;
                return new Pair<CompilationUnit, IList<ISpecial>>(cu, specials);
            }
        }
    }
}
