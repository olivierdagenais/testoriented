using System;
using System.Collections.Generic;
using System.Text;

namespace Textile.Blocks
{
    public class SuperScriptPhraseBlockModifier : PhraseBlockModifier
    {
        public override string ModifyLine(string line)
        {
            return InternalPhraseModifierFormat(line, @"\^", "sup");
        }
    }
}
