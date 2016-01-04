// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeNumber.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser
{
    public class NodeNumber : AstNode
    {
        public int Value { get; set; }

        public NodeNumber(int position, int value)
            : base(position)
        {
            Value = value;
        }

        public override void Visit(IAstVisitor visitor)
            => visitor.Visit(this);
    }
}