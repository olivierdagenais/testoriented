using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SoftwareNinjas.Core;
using EnumerableExtensions = SoftwareNinjas.Core.Test.EnumerableExtensions;

namespace SoftwareNinjas.PublicInterfaceComparer.Test
{
    /// <summary>
    /// A class to test <see cref="Program"/>.
    /// </summary>
    [TestFixture]
    public class ProgramTest
    {
        private static readonly IEnumerable<string> EmptyStringSequence = new string[] {};

        private readonly FileInfo _baseline, _manual, _visibility;

        public ProgramTest()
        {
            var basePath = Environment.CurrentDirectory;
            var baselineFullPath = Path.Combine(basePath, "Textile-base.dll");
            _baseline = new FileInfo(baselineFullPath);
            var manualFullPath = Path.Combine(basePath, "Textile-manual.dll");
            _manual = new FileInfo(manualFullPath);
            var visibilityFullPath = Path.Combine(basePath, "Textile-visibility.dll");
            _visibility = new FileInfo(visibilityFullPath);

        }

        private static TextReader Compare(FileInfo left, FileInfo right)
        {
            // arrange
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            // act
            Program.Compare(left, right, sw);
            // assert
            var sr = new StringReader(sb.ToString());
            return sr;
        }

        [Test]
        public void Compare_SameAssembly()
        {
            var actual = Compare(_baseline, _baseline);
            EnumerableExtensions.EnumerateSame(EmptyStringSequence, actual.Lines());
        }

        [Test]
        public void Compare_DifferentButIdenticalAssembly()
        {
            var actual = Compare(_baseline, _manual);
            EnumerableExtensions.EnumerateSame(EmptyStringSequence, actual.Lines());
        }

        [Test]
        public void Compare_DifferentAssemblies()
        {
            var actual = Compare(_baseline, _visibility);
            var listed = actual.Lines().ToList();
            var count = listed.Count;
            Assert.AreEqual(103, count);
        }

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

        [Test]
        public void Difference_AlternatingWithLastOneOnLeft()
        {
            var actual = Program.Difference(new[] {"b", "d"}, new[] {"c"});
            EnumerableExtensions.EnumerateSame(new[] {"b", "c", "d"}, actual);
        }

        [Test]
        public void Difference_AlternatingWithLastTwoOnLeft()
        {
            var actual = Program.Difference(new[] { "b", "d", "e" }, new[] { "c" });
            EnumerableExtensions.EnumerateSame(new[] { "b", "c", "d", "e" }, actual);
        }

        [Test]
        public void Difference_AlternatingWithLastOneOnRight()
        {
            var actual = Program.Difference(new[] {"c"}, new[] {"b", "d"});
            EnumerableExtensions.EnumerateSame(new[] {"b", "c", "d"}, actual);
        }

        [Test]
        public void Difference_AlternatingWithLastTwoOnRight()
        {
            var actual = Program.Difference(new[] {"c"}, new[] {"b", "d", "e"});
            EnumerableExtensions.EnumerateSame(new[] {"b", "c", "d", "e"}, actual);
        }

        [Test]
        public void Difference_OnlyOneItemOnTheLeft()
        {
            var actual = Program.Difference(new[] {"a"}, EmptyStringSequence);
            EnumerableExtensions.EnumerateSame(new[] {"a"}, actual);
        }

        [Test]
        public void Difference_OnlyItemsOnTheLeft()
        {
            var actual = Program.Difference(new[] {"a", "b", "c"}, EmptyStringSequence);
            EnumerableExtensions.EnumerateSame(new[] {"a", "b", "c"}, actual);
        }

        [Test]
        public void Difference_OnlyOneItemOnTheRight()
        {
            var actual = Program.Difference(EmptyStringSequence, new[] {"a"});
            EnumerableExtensions.EnumerateSame(new[] {"a"}, actual);
        }

        [Test]
        public void Difference_OnlyItemsOnTheRight()
        {
            var actual = Program.Difference(EmptyStringSequence, new[] {"a", "b", "c"});
            EnumerableExtensions.EnumerateSame(new[] {"a", "b", "c"}, actual);
        }

        [Test]
        public void Difference_LastRightEqual()
        {
            var actual = Program.Difference(new[] {"a", "b", "c"}, new[] {"c"});
            EnumerableExtensions.EnumerateSame(new[] {"a", "b"}, actual);
        }

        [Test]
        public void Difference_LastLeftEqual()
        {
            var actual = Program.Difference(new[] {"c"}, new[] {"a", "b", "c"});
            EnumerableExtensions.EnumerateSame(new[] {"a", "b"}, actual);
        }
    }
}
