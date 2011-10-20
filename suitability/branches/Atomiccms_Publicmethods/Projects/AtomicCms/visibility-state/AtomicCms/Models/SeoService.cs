namespace AtomicCms.Core.Models
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using Common.Abstract.Models;
    using Common.Extensions;

    public class SeoService : ISeoService
    {
        public string CreateAlias(string source)
        {
            source = Sanitarize(source.NullSafe().ToLower());
            char[] separator = new[] { ';', ',', ' ', '.', ':' };
            string[] words = source.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string s in words)
            {
                if (s.Length <= 2)
                {
                    continue;
                }

                if (sb.Length == 0)
                {
                    sb.Append(s.Trim());
                }
                else
                {
                    sb.Append("-" + s.Trim());
                }
            }

            return sb.ToString();
        }

        private string Sanitarize(string source)
        {
            source = source.NullSafe();
            string pattern = @"['""{}.;,:]";
            Regex regex = new Regex(pattern);
            return regex.Replace(source,
                          " ");
        }
    }
}