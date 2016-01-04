// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeExpr.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser
{
    public class NodeExpr : AstNode
    {
        public NodeExpr(int position, AstNode node) 
            : base(position)
        {
            Children.Add(node);
        }

        public override void Visit(IAstVisitor visitor) 
            => visitor.Visit(this);
    }
}