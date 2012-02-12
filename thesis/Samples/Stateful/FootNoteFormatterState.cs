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
  private string m_atts = null;
  protected SimpleBlockFormatterState(TextWriter w)
  {
    Writer = w;
  }

  public abstract void Enter();
  public abstract void Exit();
  public abstract void FormatLine(string input);
  public string Consume(Match m)
  {
    m_tag = m.Groups["tag"].Value;
    m_atts = m.Groups["atts"].Value;
    var input = m.Groups["content"].Value;
    OnContextAcquired(); Enter();
    return input;
  }

  protected abstract void OnContextAcquired();
  protected string FormattedStylesAndAlignment()
  {
    return String.IsNullOrEmpty(m_atts)
      ? "" : " style=\"" + m_atts + "\"";
  }

  public string Tag
  {
    get { return m_tag; }
  }
}

public class FootNoteFormatterState
  : SimpleBlockFormatterState
{
  private int m_noteID = 0;

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

public class IntermediateFootNoteFormatterState
  : SimpleBlockFormatterState
{
  private int m_noteID = 0;

  public IntermediateFootNoteFormatterState(TextWriter w)
    : base(w)
  {
  }

  public override void Enter()
  {
    int noteId = m_noteID;
    string atts = FormattedStylesAndAlignment();
    string result = string.Format(
      "<p id=\"fn{0}\"{1}><sup>{2}</sup> ",
      noteId,
      atts,
      noteId);
    Writer.Write(result);
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
    string input = Tag;
    Match m = Regex.Match(input, @"^fn(?<id>[0-9]+)");
    int result = Int32.Parse(m.Groups["id"].Value);
    m_noteID = result;
  }
}