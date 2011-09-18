using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SoftwareNinjas.PublicInterfaceComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var baselineFile = new FileInfo(args[0]);
            var challengerFile = new FileInfo(args[1]);
            var reportFile = new FileInfo(args[2]);
            // TODO: scan both files, then produce a report based on their differences
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
                    moveLeft = false;
                    moveRight = false;
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
                    moveLeft = false;
                    if (!le.MoveNext())
                    {
                        yield return re.Current;
                        break;
                    }
                }
                else if (moveRight)
                {
                    moveRight = false;
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
                }
                else if (compare == 0)
                {
                    moveLeft = true;
                    moveRight = true;
                }
                else
                {
                    yield return re.Current;
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
