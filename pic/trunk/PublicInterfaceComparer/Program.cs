using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.PublicInterfaceComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var baselineFile = new FileInfo(args[0]);
            var challengerFile = new FileInfo(args[1]);
            var reportFile = new FileInfo(args[2]);

            using (var report = new StreamWriter(reportFile.FullName, false))
            {
                Compare(baselineFile, challengerFile, report);
            }
        }

        internal static void Compare(FileInfo baselineFile, FileInfo challengerFile, TextWriter report)
        {
            var baselinePublicMembers = LoadPublicMembers(baselineFile);
            var challengerPublicMembers = LoadPublicMembers(challengerFile);
            foreach (var difference in Difference(baselinePublicMembers, challengerPublicMembers))
            {
                report.WriteLine(difference);
            }
        }

        internal static void WriteToFile(IList<string> lines, string fileName)
        {
            using (var sw = new StreamWriter(fileName, false))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }

        internal static IList<string> LoadPublicMembers(FileInfo assemblyPath)
        {
            var childDomain = AppDomain.CreateDomain(assemblyPath.FullName);
            try
            {
                var pisType = typeof (PublicInterfaceScanner);
                Debug.Assert(pisType.FullName != null);
                var scanner = (PublicInterfaceScanner) childDomain.CreateInstanceFrom(
                    pisType.Assembly.Location,
                    pisType.FullName,
                    false,
                    BindingFlags.ExactBinding,
                    null,
                    new object[] {assemblyPath},
                    null,
                    null,
                    null
                ).Unwrap();
                var result = scanner.LoadPublicMembers();
                return result;
            }
            finally
            {
                AppDomain.Unload(childDomain);
            }
        }

        public static IEnumerable<string> Difference(IEnumerable<string> left, IEnumerable<string> right)
        {
            var le = left.GetEnumerator();
            var re = right.GetEnumerator();
            bool moveLeft = true, moveRight = true;
            while (true)
            {
                if (moveLeft && moveRight)
                {
                    if (!le.MoveNext())
                    {
                        break;
                    }
                    if (!re.MoveNext())
                    {
                        yield return le.Current;
                        break;
                    }
                }
                else if (moveLeft)
                {
                    if (!le.MoveNext())
                    {
                        yield return re.Current;
                        break;
                    }
                }
                else if (moveRight)
                {
                    if (!re.MoveNext())
                    {
                        yield return le.Current;
                        break;
                    }
                }
                var compare = String.Compare(le.Current, re.Current, StringComparison.InvariantCulture);
                if (compare < 0)
                {
                    yield return le.Current;
                    moveLeft = true;
                    moveRight = false;
                }
                else if (compare == 0)
                {
                    moveLeft = true;
                    moveRight = true;
                }
                else
                {
                    yield return re.Current;
                    moveLeft = false;
                    moveRight = true;
                }
            }

            // drain whatever is left
            while(le.MoveNext())
            {
                yield return le.Current;
            }
            while (re.MoveNext())
            {
                yield return re.Current;
            }
        }
    }
}
