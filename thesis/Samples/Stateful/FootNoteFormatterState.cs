#region License Statement
// Copyright (c) L.A.B.Soft.  All rights reserved.
//
// The use and distribution terms for this software
// are covered by the Common Public License 1.0
// (http://opensource.org/licenses/cpl.php) which can
// be found in the file CPL.TXT at the root of this
// distribution.
// By using this software in any fashion, you are
// agreeing to be bound by the terms of this license.
//
// You must not remove this notice, or any other,
// from this software.
#endregion

#region Using Statements
using System;
using System.IO;
using System.Text.RegularExpressions;
#endregion

/// <summary>
/// Stand-in for the real implementation
/// </summary>
public abstract class SimpleBlockFormatterState
{
  protected readonly TextWriter Writer;
  private string m_tag = null;
  protected SimpleBlockFormatterState
    (TextWriter writer)
  { Writer = writer; }
  public abstract void Enter();
  public abstract void Exit();
  public abstract void FormatLine(string input);
  protected abstract void OnContextAcquired();
  protected string FormattedStylesAndAlignment()
  { throw new NotImplementedException(); }
  public string Tag
  { get { return m_tag; } }
}

public class FootNoteFormatterState
  : SimpleBlockFormatterState
{
  int m_noteID = 0;

  public FootNoteFormatterState(TextWriter w)
    : base(w)
  {
  }

  public override void Enter()
  {
    Writer.Write(
      string.Format(
        "<p id=\"fn{0}\"{1}><sup>{2}</sup> ",
        m_noteID,
        FormattedStylesAndAlignment(),
        m_noteID));
  }

  public override void Exit()
  {
      Writer.WriteLine("</p>");
  }

  public override void FormatLine(string input)
  {
      Writer.Write(input);
  }

  protected override void OnContextAcquired()
  {
    Match m = Regex.Match(Tag, @"^fn(?<id>[0-9]+)");
    m_noteID = Int32.Parse(m.Groups["id"].Value);
  }
}
