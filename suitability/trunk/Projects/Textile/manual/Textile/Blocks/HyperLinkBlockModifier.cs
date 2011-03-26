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
    public class HyperLinkBlockModifier : BlockModifier
    {
        public override string ModifyLine(string line)
        {
            return InnerModifyLine (line);
        }

        internal static string InnerModifyLine (string line)
        {
            return InnerModifyLine (line, HyperLinksFormatMatchEvaluator);
        }

        internal static string InnerModifyLine (string line, MatchEvaluator evaluator)
        {
            line = Regex.Replace(line,
                                    @"(?<pre>[\s[{(]|" + Globals.PunctuationPattern + @")?" +       // $pre
                                    "\"" +									// start
                                    Globals.BlockModifiersPattern +			// attributes
                                    "(?<text>[^\"(]+)" +					// text
                                    @"\s?" +
									@"(?:\((?<title>[^)]+)\)(?=""))?" +		// title
                                    "\":" +
                                    @"(?<url>\S+\b)" +						// url
                                    @"(?<slash>\/)?" +						// slash
                                    @"(?<post>[^\w\/;]*)" +					// post
                                    @"(?=\s|$)",
                                   evaluator);
            return line;
        }

        internal static string HyperLinksFormatMatchEvaluator(Match m)
        {
            return BuildHyperlinkElementString (
                m.Groups["pre"].Value,
                m.Groups["atts"].Value,
                m.Groups["title"].Value,
                m.Groups["text"].Value,
                m.Groups["url"].Value,
                m.Groups["slash"].Value,
                m.Groups["post"].Value);
        }

        internal static string BuildHyperlinkElementString
            (string pre, string attsValue, string title, string text, string url, string slash, string post)
        {
            //TODO: check the URL
            string atts = BlockAttributesParser.ParseBlockAttributes (attsValue, "");
            if (title.Length > 0)
                atts += " title=\"" + title + "\"";
            string linkText = text.Trim(' ');

            string str = pre + "<a ";
            str += "href=\"" +
                   Globals.EncodeHTMLLink(url) + slash + "\"" +
                   atts +
                   ">" + linkText + "</a>" + post;
            return str;
        }
    }
}
