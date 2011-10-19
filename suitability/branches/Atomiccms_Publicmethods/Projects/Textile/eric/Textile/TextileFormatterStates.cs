#region License Statement
// Copyright (c) L.A.B.Soft.  All rights reserved.
//
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Textile.States;

#endregion


namespace Textile
{
    public partial class TextileFormatter
    {
        #region State Management

        private List<Type> m_disabledFormatterStates = new List<Type>();
        private Stack<FormatterState> m_stackOfStates = new Stack<FormatterState>();

        private bool IsFormatterStateEnabled(Type type)
        {
            return !m_disabledFormatterStates.Contains(type);
        }

        private void SwitchFormatterState(Type type, bool onOff)
        {
            if (onOff)
                m_disabledFormatterStates.Remove(type);
            else if (!m_disabledFormatterStates.Contains(type))
                m_disabledFormatterStates.Add(type);
        }

        /// <summary>
        /// Pushes a new state on the stack.
        /// </summary>
        /// <param name="s">The state to push.</param>
        /// The state will be entered automatically.
        private void PushState(FormatterState s)
        {
            m_stackOfStates.Push(s);
            s.Enter();
        }

        /// <summary>
        /// Removes the last state from the stack.
        /// </summary>
        /// The state will be exited automatically.
        private void PopState()
        {
            m_stackOfStates.Peek().Exit();
            m_stackOfStates.Pop();
        }

        /// <summary>
        /// The current state, if any.
        /// </summary>
        internal FormatterState CurrentState
        {
            get
            {
                if (m_stackOfStates.Count > 0)
                    return m_stackOfStates.Peek();
                else
                    return null;
            }
        }

        internal void ChangeState(FormatterState formatterState)
        {
            if (CurrentState != null && CurrentState.GetType() == formatterState.GetType())
            {
                if (!CurrentState.ShouldNestState(formatterState))
                    return;
            }
            PushState(formatterState);
        }

        #endregion

        #region State Handling

        /// <summary>
        /// Parses the string and updates the state accordingly.
        /// </summary>
        /// <param name="input">The text to process.</param>
        /// <returns>The text, ready for formatting.</returns>
        /// This method modifies the text because it removes some
        /// syntax stuff. Maybe the states themselves should handle
        /// their own syntax and remove it?
        private string HandleFormattingState(string input)
        {
            {
                Match match;
                var formatterState = FindState(input, out match);
                if (formatterState != null)
                {
                    var result = formatterState.Consume(input, match);
                    return result;
                }
            }

            // Default, when no block is specified, we ask the current state, or
            // use the paragraph state.
            if (CurrentState != null)
            {
                if (CurrentState.FallbackFormattingState != null)
                {
                    FormatterState formatterState = (FormatterState)Activator.CreateInstance(CurrentState.FallbackFormattingState, this);
                    ChangeState(formatterState);
                }
                // else, the current state doesn't want to be superceded by
                // a new one. We'll leave him be.
            }
            else
            {
                ChangeState(new States.ParagraphFormatterState(this));
            }
            return input;
        }

        #endregion

        #region State Registration

        private static readonly Dictionary<Regex,Func<TextileFormatter,FormatterState>> stateRegex = new Dictionary<Regex, Func<TextileFormatter,FormatterState>>();

        private static void RegisterAllStates()
        {
            const string simpleBlockPatternBegin = @"^\s*(?<tag>";
            const string simpleBlockPatternEnd = @")" + Globals.AlignPattern + Globals.BlockModifiersPattern + @"\.(?:\s+)?(?<content>.*)$";

            const string listPatternBegin = @"^\s*(?<tag>";
            const string listPatternEnd = @")" + Globals.BlockModifiersPattern + @"(?:\s+)? (?<content>.*)$";

            Register(f => new HeaderFormatterState(f),
                simpleBlockPatternBegin + @"h[0-9]+" + simpleBlockPatternEnd);

            Register(f => new BlockQuoteFormatterState(f),
                simpleBlockPatternBegin + @"bq" + simpleBlockPatternEnd);

            Register(f => new ParagraphFormatterState(f),
                simpleBlockPatternBegin + @"p" + simpleBlockPatternEnd);

            Register(f => new FootNoteFormatterState(f),
                simpleBlockPatternBegin + @"fn[0-9]+" + simpleBlockPatternEnd);

            Register(f => new OrderedListFormatterState(f),
                listPatternBegin + @"#+" + listPatternEnd);

            Register(f => new UnorderedListFormatterState(f),
                listPatternBegin + @"\*+" + listPatternEnd);

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
                simpleBlockPatternBegin + @"bc" + simpleBlockPatternEnd);

            Register(f => new PreFormatterState(f),
                @"^\s*<pre" + Globals.HtmlAttributesPattern + ">");

            Register(f => new NoTextileFormatterState(f),
                @"^\s*((?<tag><notextile>)\s*$|(?<tag>(notextile.))\s*)");

            //Register(f => new PassthroughFormatterState(f),
            //    @"^\s*<(h[0-9]|p|pre|blockquote)" + Globals.HtmlAttributesPattern + ">");
        }

        private static void Register(Func<TextileFormatter, FormatterState> constructor, string pattern)
        {
            var r = new Regex(pattern, RegexOptions.Compiled);
            stateRegex.Add(r, constructor);
        }

        // TODO: Do without "out" parameters
        internal FormatterState FindState(string input, out Match match)
        {
            foreach(var regex in stateRegex.Keys)
            {
                match = regex.Match(input);
                if (match.Success)
                {
                    var result = stateRegex[regex](this);
                    return result;
                }
            }

            match = null;
            return null;
        }

        #endregion
    }
}
