using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Textile.States;
using System.Linq;

namespace Textile
{
    public class StateFactory
    {
        private TextileFormatter formatter;
        private readonly Dictionary<Regex,Func<TextileFormatter,FormatterState>> stateRegex = new Dictionary<Regex, Func<TextileFormatter,FormatterState>>();

        public StateFactory(TextileFormatter formatter)
        {
            this.formatter = formatter;

            Register(f => new HeaderFormatterState(f),
                SimpleBlockFormatterState.PatternBegin + @"h[0-9]+" + SimpleBlockFormatterState.PatternEnd);

            Register(f => new BlockQuoteFormatterState(f),
                SimpleBlockFormatterState.PatternBegin + @"bq" + SimpleBlockFormatterState.PatternEnd);

            Register(f => new ParagraphFormatterState(f),
                SimpleBlockFormatterState.PatternBegin + @"p" + SimpleBlockFormatterState.PatternEnd);

            Register(f => new FootNoteFormatterState(f),
                SimpleBlockFormatterState.PatternBegin + @"fn[0-9]+" + SimpleBlockFormatterState.PatternEnd);

            Register(f => new OrderedListFormatterState(f),
                ListFormatterState.PatternBegin + @"#+" + ListFormatterState.PatternEnd);

            Register(f => new UnorderedListFormatterState(f),
                ListFormatterState.PatternBegin + @"\*+" + ListFormatterState.PatternEnd);

            Register(f => new TableFormatterState(f),
                @"^\s*(?<tag>table)" +
                   Globals.SpanPattern +
                   Globals.AlignPattern +
                   Globals.BlockModifiersPattern +
                   @"\.\s*$");

            Register(f => new TableRowFormatterState(f),
                @"^\s*(" + Globals.AlignPattern + Globals.BlockModifiersPattern + @"\.\s?)?" 
                    + @"\|(?<content>.*)\|\s*$");

            Register(f => new CodeFormatterState(f),
                @"^\s*<code" + Globals.HtmlAttributesPattern + ">");

            Register(f => new PreCodeFormatterState(f),
                SimpleBlockFormatterState.PatternBegin + @"bc" + SimpleBlockFormatterState.PatternEnd);

            Register(f => new PreFormatterState(f),
                @"^\s*<pre" + Globals.HtmlAttributesPattern + ">");

            Register(f => new NoTextileFormatterState(f),
                @"^\s*((?<tag><notextile>)\s*$|(?<tag>(notextile.))\s*)");

            //Register(f => new PassthroughFormatterState(f),
            //    @"^\s*<(h[0-9]|p|pre|blockquote)" + Globals.HtmlAttributesPattern + ">");
        }

        void Register(Func<TextileFormatter, FormatterState> constructor, string pattern)
        {
            var r = new Regex(pattern, RegexOptions.Compiled);
            stateRegex.Add(r, constructor);
        }

        // TODO: Do without "out" parameters
        internal FormatterState Find(string input, out Match match)
        {
            foreach(var regex in stateRegex.Keys)
            {
                match = regex.Match(input);
                if (match.Success)
                {
                    var result = stateRegex[regex](formatter);
                    return result;
                }
            }

            match = null;
            return null;
        }
    }
}
