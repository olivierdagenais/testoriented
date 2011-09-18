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
    }
}
