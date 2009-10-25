using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TextTemplating;
using SoftwareNinjas.Core;
using ICSharpCode.NRefactory.Ast;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// A base class for method-related templates.
    /// </summary>
    public abstract class AbstractMethodTemplate : TextTransformation
    {
        static readonly IDictionary<string, string> SimpleTypeDefaultValues = new Dictionary<string, string>
        {
            { "byte",	"0" },
            { "char",	"'x'" },
            { "double",	"0.0" },
            { "float",	"0.0f" },
            { "int",	"0" },
            { "long",	"0L" },
            { "short",	"0" },
            { "void",	"null" },
            { "bool",	"false" },
            { "string",	"\"TODO\"" },
        };

        private readonly ParametrizedNode _method;
        private readonly TypeDeclaration _parentType;
        /// <summary>
        /// Identifies if the <see cref="Method"/> that will be tested needs an instance of <see cref="ParentType"/>
        /// to be invoked.
        /// </summary>
        ///
        /// <value>
        /// <see langword="true"/> if <see cref="Method"/> is an instance method; <see langword="false"/> if it can be
        /// called statically.
        /// </value>
        public readonly bool NeedsInstance;

        internal static readonly List<ParameterDeclarationExpression> EmptyParameterList
            = new List<ParameterDeclarationExpression>(0);
        internal static readonly List<AttributeSection> EmptyAttributeList 
            = new List<AttributeSection>(0);

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMethodTemplate"/> parameterized template with the
        /// specified <paramref name="node"/> and <paramref name="parentType"/>.
        /// </summary>
        /// 
        /// <param name="node">
        /// The <see cref="ParametrizedNode"/> representing the node for which templates are to be written.
        /// </param>
        /// 
        /// <param name="parentType">
        /// The <see cref="TypeDeclaration"/> that contains <paramref name="node"/>.
        /// </param>
        protected AbstractMethodTemplate(ParametrizedNode node, TypeDeclaration parentType)
        {
            _method = node;
            _parentType = parentType;
            NeedsInstance = !_method.Modifier.HasFlag(Modifiers.Static);

            ReturnValue = node.GetTypeReference(parentType);
            HasReturnValue =
            !(
                ReturnValue.IsNull
                || "System.Void" == ReturnValue.Type
                || "void" == ReturnValue.Type
            );
        }

        /// <summary>
        /// The value created or returned by the constructor or method call, if appropriate.
        /// </summary>
        public readonly TypeReference ReturnValue;

        /// <summary>
        /// Determines if the <see cref="Method"/> is expected to return a value.
        /// </summary>
        public readonly bool HasReturnValue;

        /// <summary>
        /// The method or constructor that is central to the template.
        /// </summary>
        public ParametrizedNode Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// The class in which <see cref="Method"/> can be found.
        /// </summary>
        public TypeDeclaration ParentType
        {
            get
            {
                return _parentType;
            }
        }

        // TODO: the results of this method could be cached
        // TODO: visibility should probably be abstracted out, since it could depend on actual set-up or an option
        internal static ParametrizedNode DetermineConstructor(TypeDeclaration source)
        {
            // TODO: TypeDeclaration could have the Static modifier, at which point there are no constructors possible!

            // TODO: The TypeDeclaration could be _partial_, at which point the search would be inconclusive!
            ParametrizedNode simplestConstructor = null;
            source.Children.Filter(node => node is ConstructorDeclaration).ForElse(
            node =>
            {
                var constructor = (ConstructorDeclaration) node;
                if(constructor.Modifier.HasFlag(Modifiers.Public) || constructor.Modifier.HasFlag(Modifiers.Internal))
                {
                    if (null == simplestConstructor)
                    {
                        simplestConstructor = constructor;
                    }
                    else
                    {
                        // TODO: Improve the meaning of "better", because in this case, there is "another kind of
                        // better". For example, maybe we should avoid recursive constructors, deprecated ones, etc.
                        // TODO: consider the types of the parameters, as well
                        if (constructor.Parameters.Count < simplestConstructor.Parameters.Count)
                        {
                            simplestConstructor = constructor;
                        }
                    }
                }
            },
            () =>
            {
                // there were no constructor declarations; default constructor should be available
                simplestConstructor = new ConstructorDeclaration(source.Name, Modifiers.Public, 
                    EmptyParameterList, EmptyAttributeList);
            } );

            if (null == simplestConstructor)
            {
                // there was at least one constructor declaration, but none were visible
                // TODO: scan for factory methods:
                // visible static methods with a return type equal to TypeDeclaration
            }

            return simplestConstructor;
        }

        // TODO: Will need a method for constructing a call to a ParametrizedNode that returns a TypeDeclaration
        // TODO: The call would only need to be recursive if the parameters require a non-null instance, something
        // that could be determined through static analysis (i.e. null check that throws ArgumentNullException,
        // use of instance without checking for null or maybe annotation)
        // TODO: Will need a method to suggest local variable names for parameters, although that is state-sensitive
        // (i.e. if there are two instances of Point required, we'll need x1 and x2 or maybe x and x1)

        /// <summary>
        /// Attempts to determine a reasonable default value for the type represented by <paramref name="source"/>.
        /// </summary>
        /// 
        /// <param name="source">
        /// The <see cref="TypeReference"/> for which to generate a default value.
        /// </param>
        /// 
        /// <returns>
        /// A string representing the best default value to generate, if one was found; the type's default otherwise.
        /// </returns>
        public static string DefaultValue(TypeReference source)
        {
            var typeName = source.ToString();
            if(SimpleTypeDefaultValues.ContainsKey(typeName))
            {
                return SimpleTypeDefaultValues[typeName];
            }
            // TODO: if source represents a class with at least one constructor, set-up a call to the simplest one
            return "default({0})".FormatInvariant(typeName);
        }

        internal static string DetermineDeclarationForType(TypeReference type)
        {
            // TODO: It might be worth improving this to use TypeReference.PrimitiveTypesCSharpReverse
            // so the code looks more "natural"
            var result = type.ToString();
            return result;
        }

        internal static string GenerateConstructorCall(TypeDeclaration parentType)
        {
            var instanceVariableName = DetermineInstanceVariableName(parentType);
            var constructor = DetermineConstructor(parentType);
            var result = MethodCallStub.Generate(constructor, parentType, instanceVariableName);
            return result;
        }

        internal static string DetermineInstanceVariableName(TypeDeclaration type)
        {
            // TODO: Look for an existing (and better!) variable name suggestion method

            // TODO: Handle nested classes (such as Map<K,V>.Entry<K,V>) and
            // qualified names (such as java.lang.String)
            var result = "instance";
            var typeName = type.Name;
            if (Char.IsUpper(typeName[0]))
            {
                result = Char.ToLower(typeName[0]) + typeName.Substring(1);
                // TODO: if that name is already taken and there's another
                // uppercase letter in the typeName, try to form a variable name
                // using such successive "words" until all remaining splits have
                // been exhausted.
            }
            return result;
        }
    }
}
