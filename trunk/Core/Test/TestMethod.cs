using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.Ast;
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
    }
}
