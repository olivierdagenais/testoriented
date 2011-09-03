using System;
using System.Text;

namespace StringExtensions
{
    public class StringExtensions
    {
        public static string Capitalize(string value)
        {
            var sb = new StringBuilder();
            bool word = false;  //  are  we  writing  a  word
            foreach (var c in value)
            {
                if (char.IsLetter(c))
                {
                    if (!word)
                    {
                        sb.Append(char.ToUpper(c));
                        word = true;
                    }
                    else
                        sb.Append(c);
                }
                else
                {
                    if (char.IsPunctuation(c))
                        sb.Append('_');
                    word = false;
                }
            }
            return sb.ToString();
        }
    }
}
