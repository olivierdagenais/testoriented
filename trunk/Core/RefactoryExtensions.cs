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
    }
}
