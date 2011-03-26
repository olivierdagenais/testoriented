using System;
using System.Collections.Generic;
using System.Text;

namespace Textile.Blocks
{
    public class ItalicPhraseBlockModifier : PhraseBlockModifier
    {
        public override string ModifyLine(string line)
        {
            return InnerModifyLine (line);
        }

        internal static string InnerModifyLine (string line)
        {
            return PhraseModifierFormat(line, @"__", "i");
        }
    }
}
