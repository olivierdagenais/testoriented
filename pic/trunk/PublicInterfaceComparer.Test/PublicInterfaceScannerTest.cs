using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SoftwareNinjas.Core;
using EnumerableExtensions = SoftwareNinjas.Core.Test.EnumerableExtensions;

namespace SoftwareNinjas.PublicInterfaceComparer.Test
{
    /// <summary>
    /// A class to test <see cref="PublicInterfaceScanner"/>.
    /// </summary>
    [TestFixture]
    public class PublicInterfaceScannerTest
    {
        private const BindingFlags DefaultBindingFlags = 
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
        private const BindingFlags NonPublicInstance =
            BindingFlags.NonPublic | BindingFlags.Instance;
        private const string TextileBlockModifier = "Textile.BlockModifier";
        private const string TextileFormatterStateAttribute = "Textile.FormatterStateAttribute";
        private const string TextileBlocksCodeBlockModifier = "Textile.Blocks.CodeBlockModifier";
        private const string TextileBlocksPhraseBlockModifier = "Textile.Blocks.PhraseBlockModifier";
        private const string TextileBlocksBoldPhraseBlockModifier = "Textile.Blocks.BoldPhraseBlockModifier";
        private const string TextileBlocksCapitalsBlockModifier = "Textile.Blocks.CapitalsBlockModifier";
        private const string TextileBlocksHyperLinkBlockModifier = "Textile.Blocks.HyperLinkBlockModifier";
        private const string TextileBlocksNoTextileBlockModifier = "Textile.Blocks.NoTextileBlockModifier";
        private const string TextileFormatterState = "Textile.FormatterState";

        private static readonly MemberInfo[] EmptyMemberInfoSequence = new MemberInfo[] { };

        private readonly Assembly _baseline, _manual, _visibility;

        /// <summary>
        /// Initializes the fields used for testing.
        /// </summary>
        public PublicInterfaceScannerTest()
        {
            var basePath = Environment.CurrentDirectory;
            var baselineFullPath = Path.Combine(basePath, "Textile-base.dll");
            _baseline = Assembly.LoadFile(baselineFullPath);
            var manualFullPath = Path.Combine(basePath, "Textile-manual.dll");
            _manual = Assembly.LoadFile(manualFullPath);
            var visibilityFullPath = Path.Combine(basePath, "Textile-visibility.dll");
            _visibility = Assembly.LoadFile(visibilityFullPath);
        }

        /// <summary>
        /// Tests the <see cref="PublicInterfaceScanner.Describe(MemberInfo)"/> method with
        /// a method.
        /// </summary>
        [Test]
        public void Describe_MethodInfo()
        {
            // arrange
            var methodInfo = GetConvertToStringOverload(typeof(Int32));
            // act
            var actual = PublicInterfaceScanner.Describe((MemberInfo) methodInfo);
            // assert
            Assert.AreEqual("System.Convert System.String ToString(Int32)", actual);
        }

        /// <summary>
        /// Tests the <see cref="PublicInterfaceScanner.Describe(MemberInfo)"/> method with
        /// a type.
        /// </summary>
        [Test]
        public void Describe_Type()
        {
            // arrange
            var type = typeof (Convert);
            // act
            var actual = PublicInterfaceScanner.Describe((MemberInfo) type);
            // assert
            Assert.AreEqual("System.Convert", actual);
        }

        /// <summary>
        /// Tests the <see cref="PublicInterfaceScanner.Describe(MemberInfo)"/> method with
        /// a constructor.
        /// </summary>
        [Test]
        public void Describe_Constructor()
        {
            // arrange
            var stringType = typeof (String);
            var constructorCharCount = stringType.GetConstructor(new[] {typeof (Char), typeof(Int32)});
            // act
            var actual = PublicInterfaceScanner.Describe((MemberInfo) constructorCharCount);
            // assert
            Assert.AreEqual("System.String Void .ctor(Char, Int32)", actual);
        }

        /// <summary>
        /// Tests the <see cref="PublicInterfaceScanner.Describe(MemberInfo)"/> method with
        /// a field.
        /// </summary>
        [Test]
        public void Describe_Field()
        {
            // arrange
            var stringType = typeof(String);
            var field = stringType.GetField("Empty");
            // act
            var actual = PublicInterfaceScanner.Describe((MemberInfo) field);
            // assert
            Assert.AreEqual("System.String System.String Empty", actual);
        }

        /// <summary>
        /// Tests the <see cref="PublicInterfaceScanner.Describe(MemberInfo)"/> method with
        /// an event.
        /// </summary>
        [Test]
        public void Describe_Event()
        {
            // arrange
            var processType = typeof(Process);
            var disposedEvent = processType.GetEvent("Disposed");
            // act
            var actual = PublicInterfaceScanner.Describe((MemberInfo) disposedEvent);
            // assert
            Assert.AreEqual("System.Diagnostics.Process System.EventHandler Disposed", actual);
        }

        private static MethodInfo GetPhraseModifierFormatMethod(Type type)
        {
            return type.GetMethod("PhraseModifierFormat", 
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }

        private static MethodInfo GetCodeFormatMatchEvaluatorMethod(Type type)
        {
            return type.GetMethod("CodeFormatMatchEvaluator",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        }

        private static MethodInfo GetConvertToStringOverload(Type type)
        {
            var convertType = typeof (Convert);
            var result = convertType.GetMethod("ToString", DefaultBindingFlags | BindingFlags.ExactBinding,
                null, new[] { type }, null);
            return result;
        }

        private static MethodInfo GetModifyLineMethod(Assembly assembly, string className)
        {
            var blockModifierType = assembly.GetType(className);
            var formalParameterTypes = new[] {typeof (String)};
            var result = blockModifierType.GetMethod("ModifyLine", 
                DefaultBindingFlags | BindingFlags.ExactBinding, null, formalParameterTypes, null);
            return result;
        }

        private static MethodInfo GetHyperLinkBlockModifierInnerModifyLineMethod
            (Assembly assembly, Type[] formalParameterTypes)
        {
            var blockModifierType = assembly.GetType(TextileBlocksHyperLinkBlockModifier);
            var result = blockModifierType.GetMethod("InnerModifyLine",
                DefaultBindingFlags | BindingFlags.NonPublic | BindingFlags.ExactBinding, 
                null, formalParameterTypes, null);
            return result;
        }

        /// <summary>
        /// Test the <see cref="PublicInterfaceScanner.IsVisible(MethodBase)"/> method
        /// with a method marked public.
        /// </summary>
        [Test]
        public void IsVisibleMethodBase_Public()
        {
            var baseline = _baseline.GetType(TextileBlocksBoldPhraseBlockModifier);
            var baselineMethod = baseline.GetMethod("ModifyLine");
            var actual = PublicInterfaceScanner.IsVisible(baselineMethod);
            Assert.AreEqual(true, actual);
        }

        /// <summary>
        /// Test the <see cref="PublicInterfaceScanner.IsVisible(MethodBase)"/> method
        /// with a method marked protected.
        /// </summary>
        [Test]
        public void IsVisibleMethodBase_Protected()
        {
            var baseline = _baseline.GetType(TextileBlocksPhraseBlockModifier);
            var baselineConstructor = baseline.GetConstructor
                (NonPublicInstance, null, Type.EmptyTypes, null);
            var actual = PublicInterfaceScanner.IsVisible(baselineConstructor);
            Assert.AreEqual(true, actual);
        }

        /// <summary>
        /// Test the <see cref="PublicInterfaceScanner.IsVisible(MethodBase)"/> method
        /// with a method marked protected internal.
        /// </summary>
        [Test]
        public void IsVisibleMethodBase_ProtectedInternal()
        {
            var visibility = _visibility.GetType("Textile.States.CodeFormatterState");
            var visibilityFixEntities = visibility.GetMethod
                ("FixEntities", NonPublicInstance, null, new[] {typeof (String)}, null);
            var actual = PublicInterfaceScanner.IsVisible(visibilityFixEntities);
            Assert.AreEqual(true, actual);
        }

        /// <summary>
        /// Test the <see cref="PublicInterfaceScanner.IsVisible(MethodBase)"/> method
        /// with a method marked internal.
        /// </summary>
        [Test]
        public void IsVisibleMethodBase_Internal()
        {
            var formatterStateType = _baseline.GetType("Textile.FormatterState");
            var baseline = _baseline.GetType("Textile.TextileFormatter");
            var oneFormatterState = new[] { formatterStateType };
            var baselineMethod = baseline.GetMethod
                ("ChangeState", NonPublicInstance, null, oneFormatterState, null);
            var actual = PublicInterfaceScanner.IsVisible(baselineMethod);
            Assert.AreEqual(false, actual);
        }

        /// <summary>
        /// Test the <see cref="PublicInterfaceScanner.IsVisible(MethodBase)"/> method
        /// with a method marked private.
        /// </summary>
        [Test]
        public void IsVisibleMethodBase_Private()
        {
            var baseline = _baseline.GetType("Textile.TextileFormatter");
            var oneString = new[] {typeof (string)};
            var baselineMethod = baseline.GetMethod
                ("CleanWhiteSpace", NonPublicInstance, null, oneString, null);
            var actual = PublicInterfaceScanner.IsVisible(baselineMethod);
            Assert.AreEqual(false, actual);
        }
    }
}
