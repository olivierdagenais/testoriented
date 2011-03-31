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
                var factory = new StateFactory(this);
                Match match;
                var formatterState = factory.Find(input, out match);
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
    }
}
