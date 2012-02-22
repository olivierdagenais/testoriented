using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Textile.Blocks
{
    public class CapitalsBlockModifier : BlockModifier
    {
        public override string ModifyLine(string line)
        {
            return InnerModifyLine (line);
        }

        internal static string InnerModifyLine (string line)
        {
            MatchEvaluator me = new MatchEvaluator(CapitalsFormatMatchEvaluator);
            line = Regex.Replace(line, @"(?<=^|\s|" + Globals.PunctuationPattern + @")(?<caps>[A-Z][A-Z0-9]+)(?=$|\s|" + Globals.PunctuationPattern + @")", me);
            return line;
        }

        static internal string CapitalsFormatMatchEvaluator(Match m)
        {
            return @"<span class=""caps"">" + m.Groups["caps"].Value + @"</span>";
        }
    }
}
