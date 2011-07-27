using System;
using System.Collections.Generic;
using System.Text;

namespace Textile.Blocks
{
    public class SubScriptPhraseBlockModifier : PhraseBlockModifier
    {
        public override string ModifyLine(string line)
        {
            return InternalPhraseModifierFormat(line, @"~", "sub");
        }
    }
}
