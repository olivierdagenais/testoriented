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
using System.Text;
using System.Text.RegularExpressions;
#endregion

class Globals
{
  public const string PunctuationPattern =
    @"[\!""#\$%&'()\*\+,\-\./:;<=>\?@\[\\\]\^_`{}~]";
}

public class BlockModifier
{
  protected BlockModifier() { }
  public virtual string ModifyLine(string line)
  { return line; }
  public virtual string Conclude(string line)
  { return line; }
}

public class CodeBlockModifier : BlockModifier
{
  internal const string Pattern =
      @"(?<before>^|([\s\([{]))" + // before
       "@" +
      @"(\|(?<lang>\w+)\|)?" +    // lang
       "(?<code>[^@]+)" +        // code
       "@" +
      @"(?<after>$|([\]}])|(?=" +
        Globals.PunctuationPattern +
        @"{1,2}|\s|$))";  // after

  public override string ModifyLine(string line)
  {
    // Replace "@...@" zones with "<code>" tags.
    line = Regex.Replace(line,
      Pattern,
      CodeFormatMatchEvaluator);
    return line;
  }

  static public string CodeFormatMatchEvaluator
    (Match m)
  {
    return BuildCodeElementString(
      m.Groups["before"].Value,
      m.Groups["lang"].Value,
      m.Groups["code"].Value,
      m.Groups["after"].Value
    );
  }

  internal static string BuildCodeElementString
    (string before, string lang,
    string code, string after)
  {
    string res = before + "<code";
    if (lang.Length > 0)
      res += " language=\"" + lang + "\"";
    res += ">" + code + "</code>" + after;
    return res;
  }
}
