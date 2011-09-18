using System;
using System.IO;
using NUnit.Framework;
using SoftwareNinjas.Core.Test;

namespace SoftwareNinjas.PublicInterfaceComparer.Test
{
    /// <summary>
    /// A class to test <see cref="Program"/>.
    /// </summary>
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        public void LoadPublicMembers_RemoteAppDomain()
        {
            var basePath = Environment.CurrentDirectory;
            var baseFullPath = Path.Combine(basePath, "Textile-base.dll");
            var assemblyPath = new FileInfo(baseFullPath);
            var actual = Program.LoadPublicMembers(assemblyPath);
            Assert.AreEqual(653, actual.Count);
        }

        [Test]
        public void Difference_Empty()
        {
            var actual = Program.Difference(new string[] {}, new string[] {});
            EnumerableExtensions.EnumerateSame(new string[] {}, actual);
        }
    }
}