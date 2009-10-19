﻿using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.Ast;
using SoftwareNinjas.Core;
using Parent = SoftwareNinjas.TestOriented.Core;
using NUnit.Framework;

namespace SoftwareNinjas.TestOriented.Core.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.TestMethod"/>.
    /// </summary>
    [TestFixture]
    public class TestMethod
    {
        private static readonly TypeReference StringTypeReference = new TypeReference("string", true);

        /// <summary>
        /// Tests <see cref="Parent.TestMethod.HasReturnValue"/> with
        /// the case where there was a return value.
        /// </summary>
        [Test]
        public void HasReturnValue_HasOne()
        {
            var methodToTest = new MethodDeclaration();
            var retval = new TypeReference("string", new[] { 0 });
            methodToTest.TypeReference = retval;
            var victim = new Parent.TestMethod (methodToTest, null);

            Assert.IsTrue(victim.HasReturnValue);
        }

        /// <summary>
        /// Tests <see cref="Parent.TestMethod.HasReturnValue"/> with
        /// the case where there was no return value.
        /// </summary>
        [Test]
        public void HasReturnValue_HasNone()
        {
            var methodToTest = new MethodDeclaration();
            var victim = new Parent.TestMethod (methodToTest, null);

            Assert.IsFalse(victim.HasReturnValue);
        }

        /// <summary>
        /// Tests <see cref="Parent.TestMethod.HasReturnValue"/> with
        /// the case where there was no return value.
        /// </summary>
        [Test]
        public void HasReturnValue_HasVoid()
        {
            var methodToTest = new MethodDeclaration();
            var retval = new TypeReference("void");
            methodToTest.TypeReference = retval;
            var victim = new Parent.TestMethod (methodToTest, null);

            Assert.IsFalse(victim.HasReturnValue);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineDeclarationForType(TypeReference)" /> method with
        /// an array of arrays of explicit 32-bit integers.
        /// </summary>
        [Test]
        public void DetermineDeclarationForType_ArrayOfArrayOfInt32()
        {
            var intArrayArray = new TypeReference("System.Int32", new[] { 0, 0 }) { IsKeyword = true };
            var actual = Parent.TestMethod.DetermineDeclarationForType(intArrayArray);
            Assert.AreEqual("System.Int32[][]", actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineDeclarationForType(TypeReference)" /> method with
        /// an array of arrays of implicit 32-bit integers.
        /// </summary>
        [Test]
        public void DetermineDeclarationForType_ArrayOfArrayOfInt()
        {
            var intArrayArray = new TypeReference("int", new[] { 0, 0 }) { IsKeyword = true };
            var actual = Parent.TestMethod.DetermineDeclarationForType(intArrayArray);
            Assert.AreEqual("int[][]", actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineDeclarationForType(TypeReference)" /> method with
        /// a <see cref="string"/> defined as the simple type.
        /// </summary>
        [Test]
        public void DetermineDeclarationForType_SimpleString()
        {
            var actual = Parent.TestMethod.DetermineDeclarationForType(StringTypeReference);
            Assert.AreEqual("string", actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineDeclarationForType(TypeReference)" /> method with
        /// a <see cref="String"/> defined as the official type.
        /// </summary>
        [Test]
        public void DetermineDeclarationForType_ExplicitString()
        {
            var systemString = new TypeReference("String", false);
            var actual = Parent.TestMethod.DetermineDeclarationForType(systemString);
            Assert.AreEqual("String", actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineDeclarationForType(TypeReference)" /> method with
        /// a <see cref="String"/> defined as the official scoped type.
        /// </summary>
        [Test]
        public void DetermineDeclarationForType_ExplicitAndScopedString()
        {
            var systemString = new TypeReference("System.String", false);
            var actual = Parent.TestMethod.DetermineDeclarationForType(systemString);
            Assert.AreEqual("System.String", actual);
        }        

        private static TypeDeclaration CreateClassUnderTest()
        {
            var cut = new TypeDeclaration(Modifiers.Public, Parent.TestMethod.EmptyAttributeList)
            {
                Name = "MainClass"
            };
            return cut;
        }

        private static ConstructorDeclaration CreateDefaultConstructor(TypeDeclaration cut)
        {
            var result = new ConstructorDeclaration(cut.Name, Modifiers.Public,
                Parent.TestMethod.EmptyParameterList, Parent.TestMethod.EmptyAttributeList);
            cut.AddChild(result);
            return result;
        }

        private static ConstructorDeclaration CreateParameterizedConstructor(TypeDeclaration cut)
        {
            var result = new ConstructorDeclaration(cut.Name, Modifiers.Public,
                null, Parent.TestMethod.EmptyAttributeList);
            result.AddParameter(StringTypeReference, "name");
            cut.AddChild(result);
            return result;
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineConstructor(TypeDeclaration)" /> method with
        /// a typical scenario of a default constructor and a parameterized constructor.
        /// </summary>
        [Test]
        public void DetermineConstructor_Typical()
        {
            var cut = CreateClassUnderTest();
            var defaultConstructor = CreateDefaultConstructor(cut);

            CreateParameterizedConstructor(cut);

            var actual = Core.TestMethod.DetermineConstructor(cut);
            Assert.AreEqual(defaultConstructor, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineConstructor(TypeDeclaration)" /> method with
        /// the non-default constructor listed first.
        /// </summary>
        [Test]
        public void DetermineConstructor_LongerFirst()
        {
            var cut = CreateClassUnderTest();
            var defaultConstructor = CreateDefaultConstructor(cut);

            CreateParameterizedConstructor(cut);

            var actual = Core.TestMethod.DetermineConstructor(cut);
            Assert.AreEqual(defaultConstructor, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineConstructor(TypeDeclaration)" /> method with
        /// the parameterless constructor hidden.
        /// </summary>
        [Test]
        public void DetermineConstructor_InaccessibleDefaultConstructor()
        {
            var cut = CreateClassUnderTest();
            var defaultConstructor = CreateDefaultConstructor(cut);
            defaultConstructor.Modifier = Modifiers.Private;

            var parameterizedConstructor = CreateParameterizedConstructor(cut);

            var actual = Core.TestMethod.DetermineConstructor(cut);
            Assert.AreEqual(parameterizedConstructor, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineConstructor(TypeDeclaration)" /> method with
        /// the default constructor implemented by the author.
        /// </summary>
        [Test]
        public void DetermineConstructor_HasExplicitDefaultConstructor()
        {
            var cut = CreateClassUnderTest();
            var defaultConstructor = CreateDefaultConstructor(cut);

            var actual = Core.TestMethod.DetermineConstructor(cut);
            Assert.AreEqual(defaultConstructor, actual);
        }

        /// <summary>
        /// Tests the <see cref="Parent.TestMethod.DetermineConstructor(TypeDeclaration)" /> method with
        /// the default constructor provided by the compiler.
        /// </summary>
        [Test]
        public void DetermineConstructor_ImplicitDefaultConstructor()
        {
            var cut = CreateClassUnderTest();

            var actual = Core.TestMethod.DetermineConstructor(cut);
            Assert.AreEqual(typeof(ConstructorDeclaration), actual.GetType());
            Assert.IsTrue(actual.Modifier.HasFlag(Modifiers.Public));
            Assert.AreEqual(0, actual.Parameters.Count);
        }
    }
}
