using System;
using System.Collections.Generic;
using System.Text;

namespace Textile.Blocks
{
    public class BoldPhraseBlockModifier : PhraseBlockModifier
    {
        public override string ModifyLine(string line)
        {
            return InnerModifyLine (line);
        }

        internal static string InnerModifyLine (string line)
        {
            return InternalPhraseModifierFormat(line, @"\*\*", "b");
        }
    }
}
