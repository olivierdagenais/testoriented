using System;
using System.Runtime.CompilerServices;
using ICSharpCode.NRefactory.Ast;

using NUnit.Framework;
using SoftwareNinjas.Core;
using Parent = SoftwareNinjas.TestOriented.Core;
using System.IO;
using System.Diagnostics;

namespace SoftwareNinjas.TestOriented.Core.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.UnitTest"/>.
    /// </summary>
    [TestFixture]
    public class UnitTest
    {
        internal const string UnformatterSource = @"using System;
namespace SoftwareNinjas.TestOriented.Core.Test.InsertTestFor_ExistingFixture
{
    /// <summary>
    /// A class to support the generated test used to verify unit test generation.
    /// </summary>
    public static class Unformatter
    {
        /// <summary>
        /// The reverse of <see cref=""string.Format(string,object[])""/>.
        /// </summary>
        /// 
        /// <param name=""format"">
        /// A string containing placeholders for values.
        /// </param>
        /// 
        /// <param name=""formatted"">
        /// A string with said placeholders replaced by values.
        /// </param>
        /// 
        /// <returns>
        /// The string representations of the values for each of the placeholders.
        /// </returns>
        /// 
        /// <remarks>
        /// This method is not actually implemented; it only serves to support the <see cref=""UnitTest.Unformat_TODO""/>
        /// unit test.
        /// </remarks>
        public static string[] Unformat(this string format, string formatted)
        {
            return default(string[]);
        }
    }
}";

        // TODO: Although this works, it's a deformation of the original as it is missing blank lines and comments
        // TODO: The call to the method under test should be Parent.Unformatter.Unformat
        internal const string UnformatterTestSource = @"using System;
using NUnit.Framework;
using Parent = SoftwareNinjas.TestOriented.Core.Test.InsertTestFor_ExistingFixture;
namespace SoftwareNinjas.TestOriented.Core.Test.InsertTestFor_ExistingFixture.Test
{
    /// <summary>
    /// A class to test <see cref=""Parent.Unformatter""/>.
    /// </summary>
    [TestFixture()]
    public class Unformatter
    {
        /// <summary>
        /// Tests the <c>Unformat</c> method with
        /// TODO: write about scenario
        /// </summary>
        [Test()]
        public void Unformat_TODO()
        {
            Assert.Fail(""TODO: initialize variable(s) and expected value"");
            string format = ""TODO"";
            string formatted = ""TODO"";
            string[] actual = Unformatter.Unformat(format, formatted);
            string[] expected = default(string[]);
            Assert.AreEqual(expected, actual);
        }
    }
}";

        internal const string UnformatterTestSourceBlank = @"using System;
using NUnit.Framework;
using Parent = SoftwareNinjas.TestOriented.Core.Test.InsertTestFor_ExistingFixture;
namespace SoftwareNinjas.TestOriented.Core.Test.InsertTestFor_ExistingFixture.Test
{
    /// <summary>
    /// A class to test <see cref=""Parent.Unformatter""/>.
    /// </summary>
    [TestFixture()]
    public class Unformatter
    {
    }
}
";

        internal const string ClassCodeDocumentationComment = @" <summary>
 A class to test <see cref=""Parent.Unformatter""/>.
 </summary>";

        /// <summary>
        /// Tests the <see cref="Parent.UnitTest.GenerateTestMethod(ParametrizedNode, TypeDeclaration)"/> method with
        /// a typical use-case.
        /// </summary>
        [Test]
        public void GenerateTestMethod_Typical()
        {
            #region public static class Unformatter
            var classUnderTest = new TypeDeclaration(Modifiers.Public | Modifiers.Static, null){ Name = "Unformatter" };

            #region public static string[] Unformat ( this string format, string formatted )
            var methodToTest = new MethodDeclaration
            {
                Name = "Unformat", IsExtensionMethod = true, Modifier = Modifiers.Static
            };

            #region returns string[]
            var retval = new TypeReference("string", new[]{0});
            methodToTest.TypeReference = retval;
            #endregion

            #region string format
            var formatParam = new ParameterDeclarationExpression(new TypeReference("string", true), "format");
            methodToTest.Parameters.Add(formatParam);
            #endregion

            #region string formatted
            var formattedParam = new ParameterDeclarationExpression(new TypeReference("string", true), "formatted");
            methodToTest.Parameters.Add(formattedParam);
            #endregion

            #endregion

            classUnderTest.Children.Add(methodToTest);
            #endregion
            const string expected = @"        /// <summary>
        /// Tests the <c>Unformat</c> method with
        /// TODO: write about scenario
        /// </summary>
        [Test()]
        public void Unformat_TODO()
        {
            Assert.Fail(""TODO: initialize variable(s) and expected value"");
            string format = ""TODO"";
            string formatted = ""TODO"";
            string[] actual = Unformatter.Unformat(format, formatted);
            string[] expected = default(string[]);
            Assert.AreEqual(expected, actual);
        }
";
            Assert.AreEqual(expected, Parent.UnitTest.GenerateTestMethod(methodToTest, classUnderTest));
        }

        /// <summary>
        /// Tests the <c>Unformat</c> method with
        /// TODO: write about scenario
        /// </summary>
        [Test]
        public void Unformat_TODO() {
            //Assert.Fail ( "TODO: initialize variable(s) and expected value" );
            string format = "TODO";
            string formatted = "TODO";
            string[] actual = Unformatter.Unformat ( format, formatted );
            string[] expected = default(string[]);
            Assert.AreEqual ( expected, actual );
        }

        /// <summary>
        /// A class to support generated tests.
        /// </summary>
        public class Cruncher
        {
            private static int _z;

            /// <summary>
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public static void CrunchNumbers(int x, int y)
            {
                _z += x * y;
            }

            internal int SafeCrunch(int x, int y)
            {
                return x * y;
            }
        }

        /// <summary>
        /// Tests the <see cref="Parent.UnitTest.GenerateTestMethod(ParametrizedNode,TypeDeclaration)" /> method with
        /// an instance method.
        /// </summary>
        [Test]
        public void GenerateTestMethod_InstanceMethod()
        {
            #region public class Cruncher
            var classUnderTest = new TypeDeclaration(Modifiers.Public, null) { Name = "Cruncher" };

            #region internal int SafeCrunch ( int x, int y )
            var methodToTest = new MethodDeclaration
            {
                Name = "SafeCrunch",
                Modifier = Modifiers.Internal
            };

            #region returns int
            var retval = new TypeReference("int", true);
            methodToTest.TypeReference = retval;
            #endregion

            #region int x
            var xParam = new ParameterDeclarationExpression(new TypeReference("int", true), "x");
            methodToTest.Parameters.Add(xParam);
            #endregion

            #region int y
            var yParam = new ParameterDeclarationExpression(new TypeReference("int", true), "y");
            methodToTest.Parameters.Add(yParam);
            #endregion

            #endregion

            classUnderTest.Children.Add(methodToTest);
            #endregion

            const string expected = @"        /// <summary>
        /// Tests the <c>SafeCrunch</c> method with
        /// TODO: write about scenario
        /// </summary>
        [Test()]
        public void SafeCrunch_TODO()
        {
            Cruncher cruncher = new Cruncher();
            Assert.Fail(""TODO: initialize variable(s) and expected value"");
            int x = 0;
            int y = 0;
            int actual = cruncher.SafeCrunch(x, y);
            int expected = 0;
            Assert.AreEqual(expected, actual);
        }
";
            Assert.AreEqual(expected, Parent.UnitTest.GenerateTestMethod(methodToTest, classUnderTest));
        }


        /// <summary>
        /// Tests the <c>SafeCrunch</c> method with
        /// TODO: write about scenario
        /// </summary>
        [Test]
        public void SafeCrunch_TODO ( ) {
            Cruncher cruncher = new Cruncher (  );
            // Assert.Fail ( "TODO: initialize variable(s) and expected value" );
            int x = 0;
            int y = 0;
            int actual = cruncher.SafeCrunch ( x, y );
            int expected = 0;
            Assert.AreEqual ( expected, actual );
        }

        /// <summary>
        /// Tests the <see cref="Parent.RefactoryExtensions.InsertTestFor(String,ParametrizedNode)"/> method when
        /// inserting a test into an existing (but empty) fixture.
        /// </summary>
        public void InsertTestFor_ExistingFixture()
        {
            TestTheGenerationOfTests(n => true);
        }

        private const string Test = "Test";
        private const string ClassUnderTest = "ClassUnderTest.cs";
        private const string InputTestClass = "InputTestClass.cs";
        private const string ExpectedTestClass = "ExpectedTestClass.cs";
        internal void TestTheGenerationOfTests(Func<ParametrizedNode, bool> whichMethodsToWriteTestsFor)
        {
            string methodName = GetCallingMethodName();
            var pathToTestFiles = Environment.CurrentDirectory.CombinePath("..", "..", "Test", methodName);
            CompilationUnit classUnderTestCompilationUnit;
            using (var tr = new StreamReader(pathToTestFiles.CombinePath(ClassUnderTest)))
            {
                classUnderTestCompilationUnit = Parent.Parser.Parse(tr, Parent.Parser.DoParseCompilationUnit).First;
            }
            string inputTestClass;
            using (var tr = new StreamReader(pathToTestFiles.CombinePath(Test, InputTestClass)))
            {
                inputTestClass = tr.ReadToEnd();
            }
            var traversal = ((INode) classUnderTestCompilationUnit).PreOrder(n => n.Children);
            var methodsToTest = traversal.Filter(n => n is ParametrizedNode &&
                                                whichMethodsToWriteTestsFor((ParametrizedNode) n));
            foreach (var methodToTest in methodsToTest)
            {
                inputTestClass = inputTestClass.InsertTestFor((ParametrizedNode)methodToTest);
            }
            string expectedTestClass;
            using (var tr = new StreamReader(pathToTestFiles.CombinePath(Test, ExpectedTestClass)))
            {
                expectedTestClass = tr.ReadToEnd();
            }

            RefactoryExtensions.AssertAreEqualNormalized(expectedTestClass, inputTestClass);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static string GetCallingMethodName()
        {
            var st = new StackTrace(false);
            // 0 is this very method, 1 is who called us and therefore 2 is who called our caller
            var callingFrame = st.GetFrame(2);
            return callingFrame.GetMethod().Name;
        }

        /// <summary>
        /// Tests the <see cref="Parent.UnitTest.GenerateTestMethod(ParametrizedNode, TypeDeclaration)"/> method with
        /// a static method that returns no value.
        /// </summary>
        [Test]
        public void GenerateTestMethod_MethodWithNotReturnValue()
        {
            #region public class Cruncher
            var classUnderTest = new TypeDeclaration(Modifiers.Public, null) { Name = "Cruncher" };

            #region public static void CrunchNumbers ( int x, int y )
            var methodToTest = new MethodDeclaration
            {
                Name = "CrunchNumbers", Modifier = Modifiers.Public | Modifiers.Static
            };

            #region returns void
            var retval = new TypeReference("void", true);
            methodToTest.TypeReference = retval;
            #endregion

            #region int x
            var xParam = new ParameterDeclarationExpression(new TypeReference("int", true), "x");
            methodToTest.Parameters.Add(xParam);
            #endregion

            #region int y
            var yParam = new ParameterDeclarationExpression(new TypeReference("int", true), "y");
            methodToTest.Parameters.Add(yParam);
            #endregion

            #endregion

            classUnderTest.Children.Add(methodToTest);
            #endregion

            const string expected = @"        /// <summary>
        /// Tests the <c>CrunchNumbers</c> method with
        /// TODO: write about scenario
        /// </summary>
        [Test()]
        public void CrunchNumbers_TODO()
        {
            Assert.Fail(""TODO: initialize variable(s)"");
            int x = 0;
            int y = 0;
            Cruncher.CrunchNumbers(x, y);
        }
";
            Assert.AreEqual(expected, Parent.UnitTest.GenerateTestMethod(methodToTest, classUnderTest));
        }

        /// <summary>
        /// Tests the <c>CrunchNumbers</c> method with
        /// TODO: write about scenario
        /// </summary>
        [Test]
        public void CrunchNumbers_TODO() {
            // Assert.Fail ( "TODO: initialize variable(s)" );
            int x = 0;
            int y = 0;
            Cruncher.CrunchNumbers ( x, y );
        }
    }

    /// <summary>
    /// A class to support the generated test used to verify unit test generation.
    /// </summary>
    public static class Unformatter
    {
        /// <summary>
        /// The reverse of <see cref="string.Format(string,object[])"/>.
        /// </summary>
        /// 
        /// <param name="format">
        /// A string containing placeholders for values.
        /// </param>
        /// 
        /// <param name="formatted">
        /// A string with said placeholders replaced by values.
        /// </param>
        /// 
        /// <returns>
        /// The string representations of the values for each of the placeholders.
        /// </returns>
        /// 
        /// <remarks>
        /// This method is not actually implemented; it only serves to support the <see cref="UnitTest.Unformat_TODO"/>
        /// unit test.
        /// </remarks>
        public static string[] Unformat(this string format, string formatted)
        {
            return default(string[]);
        }
    }
}
