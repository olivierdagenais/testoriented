using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.Ast;

namespace SoftwareNinjas.TestOriented.Core
{
    /// <summary>
    /// Provides extension methods to facilitate manipulation of NRefactory Abstract Syntax Tree types.
    /// </summary>
    public static class RefactoryExtensions
    {
        /// <summary>
        /// Adds a <see cref="ParameterDeclarationExpression"/> to <see cref="ParametrizedNode.Parameters"/> in a single
        /// call, for convenience.
        /// </summary>
        /// 
        /// <param name="node">
        /// The method or constructor to add the parameter to.
        /// </param>
        /// 
        /// <param name="parameterType">
        /// The <see cref="TypeReference"/> of the parameter to add.
        /// </param>
        /// 
        /// <param name="parameterName">
        /// The name of the parameter to add.
        /// </param>
        /// 
        /// <returns>
        /// The <see cref="ParameterDeclarationExpression"/> instance that was created and added to
        /// <paramref name="node"/>.
        /// </returns>
        public static ParameterDeclarationExpression AddParameter(this ParametrizedNode node,
            TypeReference parameterType, string parameterName)
        {
            var parameter = new ParameterDeclarationExpression(parameterType, parameterName);
            node.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Provides a mechanism for making a <see cref="ConstructorDeclaration"/> a bit more like a
        /// <see cref="MethodDeclaration"/> by simulating a common method for the result of their respective executions.
        /// </summary>
        /// 
        /// <param name="node">
        /// A constructor or method.
        /// </param>
        /// 
        /// <param name="parentType">
        /// The <see cref="TypeDeclaration"/> in which the constructor or method denoted by <paramref name="node"/> can
        /// be found.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="TypeReference"/>, if one could be determined; otherwise <see cref="NullTypeReference"/>.
        /// </returns>
        public static TypeReference GetTypeReference(this ParametrizedNode node, TypeDeclaration parentType)
        {
            var result = TypeReference.Null;
            var method = node as MethodDeclaration;
            if(method != null)
            {
                result = method.TypeReference;
            }
            else
            {
                var constructor = node as ConstructorDeclaration;
                if (constructor != null)
                {
                    result = new TypeReference(parentType.Name);
                }
            }
            return result;
        }
    }
}
