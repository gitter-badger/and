// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeFuncCall.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser
{
    public class NodeFuncCall : AstNode
    {
        public AstNode Target => Children[0];
        public AstNode Arguments => Children[1];

        public NodeFuncCall(AstNode target, AstNode arguments, int position)
            : base(position)
        {
            Children.Add(target);
            Children.Add(arguments);
        }

        public override void Visit(IAstVisitor visitor)
            => visitor.Visit(this);
    }
}