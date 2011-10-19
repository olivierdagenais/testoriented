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

namespace Textile.Blocks
{
    public class ImageBlockModifier : BlockModifier
    {
        public override string ModifyLine(string line)
        {
            return InnerModifyLine (line);
        }

        internal static string InnerModifyLine (string line)
        {
            return InnerModifyLine (line, ImageFormatMatchEvaluator);
        }

        internal static string InnerModifyLine (string line, MatchEvaluator evaluator)
        {
            line = Regex.Replace(line,
                                    @"\!" +                   // opening !
                                    @"(?<algn>\<|\=|\>)?" +   // optional alignment atts
                                    Globals.BlockModifiersPattern + // optional style, public class atts
                                    @"(?:\. )?" +             // optional dot-space
                                    @"(?<url>[^\s(!]+)" +     // presume this is the src
                                    @"\s?" +                  // optional space
                                    @"(?:\((?<title>([^\)]+))\))?" +// optional title
                                    @"\!" +                   // closing
                                    @"(?::(?<href>(\S+)))?" +     // optional href
                                    @"(?=\s|\.|,|;|\)|\||$)",               // lookahead: space or simple punctuation or end of string
                                evaluator
                                );
            return line;
        }

        static string ImageFormatMatchEvaluator(Match m)
        {
            return BuildImageElementString (
                m.Groups["atts"].Value,
                m.Groups["algn"].Value,
                m.Groups["title"].Value,
                m.Groups["url"].Value,
                m.Groups["href"].Value);
        }

        internal static string BuildImageElementString 
            (string attsValue, string algn, string title, string url, string href)
        {
            string atts = BlockAttributesParser.ParseBlockAttributes(attsValue, "");
            if (algn.Length > 0)
                atts += " align=\"" + Globals.ImageAlign[algn] + "\"";
            if (title.Length > 0)
            {
                atts += " title=\"" + title + "\"";
                atts += " alt=\"" + title + "\"";
            }
            else
            {
                atts += " alt=\"\"";
            }
            // Get Image Size?

            string res = "<img src=\"" + url + "\"" + atts + " />";

            if (href.Length > 0)
            {
                string end = string.Empty;
                Match endMatch = Regex.Match(href, @"(.*)(?<end>\.|,|;|\))$");
                if (endMatch.Success && !string.IsNullOrEmpty(endMatch.Groups["end"].Value))
                {
                    href = href.Substring(0, href.Length - 1);
                    end = endMatch.Groups["end"].Value;
                }
                res = "<a href=\"" + Globals.EncodeHTMLLink(href) + "\">" + res + "</a>" + end;
            }

            return res;
        }
    }
}
