// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeFuncDecl.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser
{
    using System.Collections.Generic;

    public class NodeFuncDecl : AstNode
    {
        public string FunctionName { get; set; }
        public List<string> Parameters { get; set; }
        public AstNode Body => Children[0];

        protected NodeFuncDecl(int position, string functionName, List<string> parameterList, AstNode body)
            : base(position)
        {
            Parameters = parameterList;
            FunctionName = functionName;
            Children.Add(body);
        }

        public override void Visit(IAstVisitor visitor)
            => visitor.Visit(this);
    }
}