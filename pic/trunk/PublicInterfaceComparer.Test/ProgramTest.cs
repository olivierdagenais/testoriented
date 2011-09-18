using System;
using System.Collections.Generic;
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
        private static readonly IEnumerable<string> EmptyStringSequence = new string[] {};

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
            EnumerableExtensions.EnumerateSame(EmptyStringSequence, actual);
        }

        [Test]
        public void Difference_Identical()
        {
            IEnumerable<string> one = new[] {"one"};
            var actual = Program.Difference(one, one);
            EnumerableExtensions.EnumerateSame(EmptyStringSequence, actual);
        }

        [Test]
        public void Difference_OneTooManyAtTheEndOfLeft()
        {
            var actual = Program.Difference(new[] {"a", "b"}, new[] {"a"});
            EnumerableExtensions.EnumerateSame(new[] {"b"}, actual);
        }

        [Test]
        public void Difference_OneTooManyAtTheEndOfRight()
        {
            var actual = Program.Difference(new[] {"a"}, new[] {"a", "b"});
            EnumerableExtensions.EnumerateSame(new[] {"b"}, actual);
        }

        [Test]
        public void Difference_OneTooManyAtTheBeginningOfLeft()
        {
            var actual = Program.Difference(new[] {"a", "b"}, new[] {"b"});
            EnumerableExtensions.EnumerateSame(new[] {"a"}, actual);
        }

        [Test]
        public void Difference_OneTooManyAtTheBeginningOfRight()
        {
            var actual = Program.Difference(new[] {"b"}, new[] {"a", "b"});
            EnumerableExtensions.EnumerateSame(new[] {"a"}, actual);
        }
    }
}
