﻿using System;
using System.Collections.Generic;

using SoftwareNinjas.Core;
using ICSharpCode.NRefactory.Ast;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// A template for C# unit test method skeletons.
    /// </summary>
    public partial class TestMethod
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

        private readonly MethodDeclaration _method;
        private readonly TypeDeclaration _type;
        /// <summary>
        /// Identifies if the <see cref="Method"/> that will be tested needs an instance of <see cref="ClassUnderTest"/>
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
        /// Initializes a new instance of the <see cref="TestMethod"/> parameterized template with the specified
        /// <paramref name="methodToTest"/> and <paramref name="classUnderTest"/>.
        /// </summary>
        /// 
        /// <param name="methodToTest">
        /// The <see cref="MethodDeclaration"/> representing the method for which tests are to be written.
        /// </param>
        /// 
        /// <param name="classUnderTest">
        /// The <see cref="TypeDeclaration"/> that contains <paramref name="methodToTest"/>.
        /// </param>
        public TestMethod (MethodDeclaration methodToTest, TypeDeclaration classUnderTest)
        {
            _method = methodToTest;
            _type = classUnderTest;
            NeedsInstance = _method.Modifier.HasFlag(Modifiers.Static);
        }

        /// <summary>
        /// Determines if the <see cref="Method"/> that will be tested is expected to return a value.
        /// </summary>
        /// 
        /// <returns>
        /// <see langword="true"/> if the <see cref="Method"/> returns a value; <see langword="false"/> otherwise.
        /// </returns>
        public bool HasReturnValue
        {
            get
            {
                var returnType = _method.TypeReference;
                return !(
                    returnType.IsNull
                    || "System.Void" == returnType.Type
                    || "void" == returnType.Type
                );
            }
        }

        /// <summary>
        /// The method being tested.
        /// </summary>
        public MethodDeclaration Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// The class in which <see cref="Method"/> can be found.
        /// </summary>
        public TypeDeclaration ClassUnderTest
        {
            get
            {
                return _type;
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
    }
}
