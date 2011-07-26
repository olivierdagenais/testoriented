using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Textile.States
{
    public abstract class SimpleBlockFormatterState : FormatterState
    {
        internal const string PatternBegin = @"^\s*(?<tag>";
        internal const string PatternEnd = @")" + Globals.AlignPattern + Globals.BlockModifiersPattern + @"\.(?:\s+)?(?<content>.*)$";

        internal string m_tag = null;
        public string Tag
        {
            get { return m_tag; }
        }

        internal string m_alignNfo = null;
        public string AlignInfo
        {
            get { return m_alignNfo; }
        }

        internal string m_attNfo = null;
        public string AttInfo
        {
            get { return m_attNfo; }
        }

        internal SimpleBlockFormatterState(TextileFormatter formatter)
            : base(formatter)
        {
        }

        public override string Consume(string input, Match m)
        {
            m_tag = m.Groups["tag"].Value;
            m_alignNfo = m.Groups["align"].Value;
            m_attNfo = m.Groups["atts"].Value;
            input = m.Groups["content"].Value;

            OnContextAcquired();

            this.Formatter.ChangeState(this);

            return input;
        }

        public override bool ShouldNestState(FormatterState other)
        {
            SimpleBlockFormatterState blockFormatterState = (SimpleBlockFormatterState)other;
            return (blockFormatterState.m_tag != m_tag ||
                    blockFormatterState.m_alignNfo != m_alignNfo ||
                    blockFormatterState.m_attNfo != m_attNfo);
        }

        internal virtual void OnContextAcquired()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal string FormattedAlignment()
        {
            return Blocks.BlockAttributesParser.ParseBlockAttributes(m_alignNfo);
        }

        internal string FormattedStyles()
        {
            return Blocks.BlockAttributesParser.ParseBlockAttributes(m_attNfo);
        }

        internal string FormattedStylesAndAlignment()
        {
            return Blocks.BlockAttributesParser.ParseBlockAttributes(m_alignNfo + m_attNfo);
        }
    }
}
