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
        public readonly string VariableName;

        /// <summary>
        /// The origin of the call to <see cref="MethodDeclaration"/>, when initialized against a method.
        /// </summary>
        public readonly string InstanceOrClass;

        /// <summary>
        /// How the method or constructor is ultimately called.
        /// </summary>
        public readonly string Invocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCallStub"/> parameterized template with the specified
        /// <paramref name="methodToCall"/> and <paramref name="parentType"/>.
        /// </summary>
        /// 
        /// <param name="methodToCall">
        /// The <see cref="ParametrizedNode"/> representing the method or constructor for which a call stub is to be
        /// written.
        /// </param>
        /// 
        /// <param name="parentType">
        /// The <see cref="TypeDeclaration"/> that contains <paramref name="methodToCall"/>.
        /// </param>
        /// 
        /// <param name="variableName">
        /// The name of the variable to which the result of the method will be assigned, if appropriate.
        /// </param>
        public MethodCallStub (ParametrizedNode methodToCall, TypeDeclaration parentType, string variableName) 
            : base (methodToCall, parentType)
        {
            VariableName = variableName ?? "result";
            InstanceOrClass = NeedsInstance ? DetermineInstanceVariableName(parentType) : parentType.Name;
            Invocation = methodToCall is MethodDeclaration 
                ? InstanceOrClass + "." + methodToCall.Name
                : "new " + parentType.Name;
        }

        internal static string Generate(ParametrizedNode methodToCall, TypeDeclaration parentType, 
            string returnVariableName)
        {
            var template = new MethodCallStub(methodToCall, parentType, returnVariableName);
            var text = template.TransformText();
            return text;
        }
    }
}
