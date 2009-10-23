using System;
using System.Collections.Generic;

using SoftwareNinjas.Core;
using ICSharpCode.NRefactory.Ast;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// A template for C# method call stubs.
    /// </summary>
    public partial class MethodCallStub
    {

        /// <summary>
        /// The name of the variable that will be assigned the result of the method call, if appropriate.
        /// </summary>
        public readonly string ReturnVariableName;

        /// <summary>
        /// The origin of the call to <see cref="MethodDeclaration"/>.
        /// </summary>
        public readonly string InstanceOrClass;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCallStub"/> parameterized template with the specified
        /// <paramref name="methodToCall"/> and <paramref name="parentType"/>.
        /// </summary>
        /// 
        /// <param name="methodToCall">
        /// The <see cref="MethodDeclaration"/> representing the method for a call stub is to be written.
        /// </param>
        /// 
        /// <param name="parentType">
        /// The <see cref="TypeDeclaration"/> that contains <paramref name="methodToCall"/>.
        /// </param>
        /// 
        /// <param name="returnVariableName">
        /// The name of the variable to which the result of the method will be assigned, if appropriate.
        /// </param>
        public MethodCallStub (MethodDeclaration methodToCall, TypeDeclaration parentType, string returnVariableName) 
            : base (methodToCall, parentType)
        {
            ReturnVariableName = returnVariableName ?? "result";
            InstanceOrClass = NeedsInstance ? DetermineInstanceVariableName(parentType) : parentType.Name;
        }

        internal static string Generate(MethodDeclaration methodToCall, TypeDeclaration parentType, 
            string returnVariableName)
        {
            var template = new MethodCallStub(methodToCall, parentType, returnVariableName);
            var text = template.TransformText();
            return text;
        }
    }
}
